using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;

namespace MaddenEditor.Forms
{
    public partial class ProgressionForm : Form
    {
        EditorModel model;
        Dictionary<int, int> increaseYoungDistributions;
        Dictionary<int, int> decreaseYoungDistributions;
        Dictionary<int, int> increaseOldDistributions;
        Dictionary<int, int> decreaseOldDistributions;

        List<List<bool>> attributesByPosition;

        bool triggerChangedEvent = true;

        public ProgressionForm(EditorModel em)
        {
            model = em;
            InitializeStandardDistributions();
            InitializeComponent();
        }

        private void mainButton_Click(object sender, EventArgs e)
        {
            LocalMath math = new LocalMath(model.FileVersion);
            Random rand = new Random();
            DepthChartRepairer dcr = new DepthChartRepairer(model, null);

            // current players sorted by position
            Dictionary<int, double> playerMeans = new Dictionary<int, double>();
            List<List<PlayerRecord>> playersByPosition = new List<List<PlayerRecord>>();
            List<List<int>> targetsByPosition = new List<List<int>>();
            Dictionary<int, List<double>> playerTargets = new Dictionary<int, List<double>>();

            List<int> playersLeft = new List<int>();
            Dictionary<int, double> playedGamesFactor = new Dictionary<int, double>();
            Dictionary<int, double> startedGamesFactor = new Dictionary<int, double>();

            int currentSeason = model.FranchiseTime.Season;
            Dictionary<int, int> gamesPlayed = new Dictionary<int, int>();
            Dictionary<int, int> gamesPlayedLastYear = new Dictionary<int, int>();
            Dictionary<int, int> gamesStarted = new Dictionary<int, int>();
            Dictionary<int, int> gamesStartedLastYear = new Dictionary<int, int>();

            foreach (SeasonGamesPlayedRecord stat in model.TableModels[EditorModel.SEASON_GAMES_PLAYED_TABLE].GetRecords())
            {
                if (stat.Season == currentSeason - 1)
                {
                    gamesStartedLastYear[stat.PlayerId] = stat.GamesStarted;
                    gamesPlayedLastYear[stat.PlayerId] = stat.GamesPlayed;
                }
                else
                {
                    if (!gamesPlayed.ContainsKey(stat.PlayerId))
                    {
                        gamesPlayed[stat.PlayerId] = 0;
                        gamesStarted[stat.PlayerId] = 0;
                    }

                    gamesPlayed[stat.PlayerId] += stat.GamesPlayed;

                    if (stat.Season < 0)
                        gamesStarted[stat.PlayerId] += stat.GamesPlayed / 2;
                    else
                        gamesStarted[stat.PlayerId] += stat.GamesStarted;
                }
            }

            foreach (KeyValuePair<int, int> pair in gamesStartedLastYear)
            {
                if (!gamesStarted.ContainsKey(pair.Key) || gamesStarted[pair.Key] == 0)
                    startedGamesFactor[pair.Key] = 1;
                else
                    startedGamesFactor[pair.Key] = Math.Tanh(pair.Value / gamesStarted[pair.Key]);
            }

            foreach (KeyValuePair<int, int> pair in gamesPlayedLastYear)
            {
                if (!gamesPlayed.ContainsKey(pair.Key) || gamesPlayed[pair.Key] == 0)
                    playedGamesFactor[pair.Key] = 1;
                else
                    playedGamesFactor[pair.Key] = Math.Tanh(pair.Value / gamesPlayed[pair.Key]);
            }

            List<PlayerRecord> freeAgentsAndRookies = new List<PlayerRecord>();

            double youthCredit = ((double)youthSlider.Value) / 10.0;
            double ageDebit = ((double)ageSlider.Value) / 10.0;
            double playedCredit = ((double)playedSlider.Value) / 10.0;
            double startedCredit = ((double)startedSlider.Value) / 10.0;

            double randomness = ((double)randomSlider.Value) / 50.0;

            for (int i = 0; i < 21; i++)
            {
                playersByPosition.Add(new List<PlayerRecord>());
                targetsByPosition.Add(new List<int>());
                playersLeft.Add(0);

                for (int j = 0; j < 100; j++)
                    targetsByPosition[i].Add(0);
            }

            int numPlayers = 0;
            foreach (PlayerRecord player in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                playerTargets[player.PlayerId] = new List<double>();

                if (player.TeamId < 32)
                {
                    playersByPosition[player.PositionId].Add(player);
                    playersLeft[player.PositionId]++;

                    if (player.YearsPro == 0)
                        freeAgentsAndRookies.Add(player);

                    double delta = playedCredit * playedGamesFactor[player.PlayerId] +
                        startedCredit * startedGamesFactor[player.PlayerId] + 
                        youthCredit*math.theta(6 - player.YearsPro) * (double)(6 - player.YearsPro) / 6.0 -
                        ageDebit*math.theta(player.Age + 4.0 - dcr.positionData[player.PositionId].RetirementAge) * (player.Age + 4.0 - dcr.positionData[player.PositionId].RetirementAge) / 4.0;

                    delta = Math.Min(maxSlider.Value, Math.Max(-maxSlider.Value, delta));

                    double mu = player.Overall + delta;
                    playerMeans[player.PlayerId] = mu;

                    double sigma = 1.0 + randomness * delta;

                    double total = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        double thisOne = Math.Exp(-Math.Pow((double)i - mu, 2.0) / (2.0 * Math.Pow(sigma, 2.0)));
                        playerTargets[player.PlayerId].Add(thisOne);
                        total += thisOne;
                    }

                    // add anything above 100 to the 99 bin
                    double extra = 0;
                    for (int i = 100; i < 200; i++)
                        extra += Math.Exp(-Math.Pow((double)i - mu, 2.0) / (2.0 * Math.Pow(sigma, 2.0)));
                    playerTargets[player.PlayerId][99] += extra;
                    total += extra;

                    // normalize
                    for (int i = 0; i < 100; i++)
                        playerTargets[player.PlayerId][i] /= total;
                }
                else if (player.TeamId == 1009 || player.TeamId == 1015)
                {
                    playersLeft[player.PositionId]++;
                    freeAgentsAndRookies.Add(player);
                }
                else
                    continue;

                numPlayers++;
            }

            // distribute ratings amongst the bins
            for (int i = 0; i < numPlayers; i++)
            {
                int bin;

                // pick a bin to adjust
                while (true)
                {
                    bin = (int)Math.Round(math.bellcurve(77.83, 9.04));
                    if (bin < 100 && bin >= 0)
                        break;
                }

                int randPos = rand.Next(numPlayers - i);

                for (int j = 0; j < 21; j++)
                {
                    if (randPos < playersLeft[j])
                    {
                        randPos = j;
                        break;
                    }

                    randPos -= playersLeft[j];
                }

                targetsByPosition[randPos][bin]++;
                playersLeft[randPos]--;
            }

            /*            // targets for each OVR
                        List<int> targets = new List<int>();
                        int total = 0;
                        for (int x = 0; x < 100; x++)
                        {
                            double num = ((double)numPlayers / 22.4726) *
                                Math.Exp(-Math.Pow(x - 77.83433433433433, 2) / 163.4356002649296);

                            int target = (int)Math.Round(math.bellcurve(num, Math.Sqrt(num), rand));

                            targets.Add(target);
                            total += target;
                        }
            */
            // subtract FA's and rookies as players already allocated, since
            // we don't want to adjust these guys at all.
            foreach (PlayerRecord player in freeAgentsAndRookies)
            {
                // find the closest bin to adjust
                int binshift = 0;
                while (targetsByPosition[player.PositionId][player.Overall + binshift] <= 0)
                {
                    if (binshift >= 0)
                        binshift = -binshift - 1;
                    else
                        binshift = -binshift;
                }

                targetsByPosition[player.PositionId][player.Overall + binshift]--;
            }

            // now make adjustments
            for (int i = 0; i < 21; i++)
            {
                List<int> adjusted = new List<int>();
                for (int j = 99; j >= 0; j--)
                {
                    for (int k = 0; k < targetsByPosition[i][j]; k++)
                    {
                        Dictionary<int, double> relProbs = new Dictionary<int, double>();
                        double totalProbs = -100;

                        double exponent = -1;
                        double expMax = -1;
                        double expMin = 0;

                        while (Math.Abs((totalProbs - (double)targetsByPosition[i][j] + k) / totalProbs) > 0.01)
                        {
                            if (exponent == -1)
                                exponent = 1;
                            else if (expMax == -1)
                            {
                                if (totalProbs > (double)targetsByPosition[i][j] - k)
                                    exponent *= 2;
                                else
                                {
                                    expMax = exponent;
                                    exponent = (expMax + expMin) / 2.0;
                                }
                            }
                            else
                            {
                                if (totalProbs > (double)targetsByPosition[i][j] - k)
                                    expMin = exponent;
                                else
                                    expMax = exponent;

                                exponent = (expMax + expMin) / 2.0;
                            }

                            totalProbs = 0;
                            foreach (PlayerRecord player in playersByPosition[i])
                            {
                                if (adjusted.Contains(player.PlayerId))
                                    continue;

                                relProbs[player.PlayerId] = Math.Pow(playerTargets[player.PlayerId][j], exponent);
                                totalProbs += relProbs[player.PlayerId];
                            }
                        }

                        double tempProb = 0;
                        double testRand = totalProbs * rand.NextDouble();

                        foreach (PlayerRecord player in playersByPosition[i])
                        {
                            if (adjusted.Contains(player.PlayerId))
                                continue;


                            if (testRand < tempProb + relProbs[player.PlayerId])
                            {
                                adjusted.Add(player.PlayerId);
                                if (player.Overall == j)
                                    break;

                                Dictionary<int, int> baseDistributions;
                                Dictionary<int, int> attributeDistributions = new Dictionary<int,int>();

                                bool young = player.Age < (player.Age - player.YearsPro + dcr.positionData[player.PositionId].RetirementAge) / 2;

                                if (j > player.Overall && young)
                                    baseDistributions = increaseYoungDistributions;
                                else if (j < player.Overall && young)
                                    baseDistributions = decreaseYoungDistributions;
                                else if (j > player.Overall && !young)
                                    baseDistributions = increaseOldDistributions;
                                else
                                    baseDistributions = decreaseOldDistributions;

                                double total = 0;
                                for (int m = 0; m < attributesByPosition[player.PositionId].Count; m++)
                                {
                                    if (attributesByPosition[player.PositionId][m])
                                    {
                                        attributeDistributions[m] = baseDistributions[m] * (j > player.Overall ? 99 - player.GetAttribute(m) : player.GetAttribute(m));
                                        total += attributeDistributions[m];
                                    }
                                }

                                Console.WriteLine("Adjusting " + player.ToString() + " -- Current Overall: " + player.Overall + ", Target: " + j);
                                for (int m = 0; m < attributesByPosition[player.PositionId].Count; m++)
                                {
                                    if (attributesByPosition[player.PositionId][m])
                                        Console.WriteLine("\t" + Enum.GetName(typeof(MaddenAttribute), m) + ": " + player.GetAttribute(m));
                                }

                                while (player.Overall != j)
                                {
                                    testRand = total * rand.NextDouble();

                                    tempProb = 0;
                                    foreach (KeyValuePair<int, int> pair in attributeDistributions)
                                    {
                                        if (testRand < tempProb + pair.Value)
                                        {

                                            player.SetAttribute(pair.Key, player.GetAttribute(pair.Key) + Math.Sign(j - player.Overall));
                                            break;
                                        }

                                        tempProb += pair.Value;
                                    }

                                    player.Overall = player.CalculateOverallRating(player.PositionId, false);
                                }

                                Console.WriteLine("New attributes:");
                                for (int m = 0; m < attributesByPosition[player.PositionId].Count; m++)
                                {
                                    if (attributesByPosition[player.PositionId][m])
                                        Console.WriteLine("\t" + Enum.GetName(typeof(MaddenAttribute), m) + ": " + player.GetAttribute(m));
                                }

                                break;
                            }

                            tempProb += relProbs[player.PlayerId];
                        }
                    }

                    // add back on any excess from players who weren't picked
                    foreach (PlayerRecord player in playersByPosition[i])
                    {
                        if (adjusted.Contains(player.PlayerId))
                            continue;

                        double thisBin = playerTargets[player.PlayerId][j];

                        if (playerMeans[player.PlayerId] > 98)
                            playerTargets[player.PlayerId][j - 1] += thisBin;
                        else
                        {
                            for (int k = 0; k < 100; k++)
                                playerTargets[player.PlayerId][k] /= (1 - thisBin);
                        }
                    }
                }
            }

            /*
            Dictionary<int, int> bins = new Dictionary<int, int>();

            for (int i = 0; i < 100; i++)
                bins[i] = 0;

            foreach (PlayerRecord player in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                bins[player.Overall]++;
            }
             * */
        }

        private void InitializeStandardDistributions()
        {
            increaseYoungDistributions = new Dictionary<int, int>();
            increaseOldDistributions = new Dictionary<int, int>();
            decreaseYoungDistributions = new Dictionary<int, int>();
            decreaseOldDistributions = new Dictionary<int, int>();

            attributesByPosition = new List<List<bool>>();

            for (int i = 0; i < 21; i++)
            {
                attributesByPosition.Add(new List<bool>());

                for (int j = 0; j < Enum.GetValues(typeof(MaddenAttribute)).GetLength(0); j++)
                    attributesByPosition[i].Add(false);
            }

            attributesByPosition[(int)MaddenPositions.QB][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.QB][(int)MaddenAttribute.THA] = true;
            attributesByPosition[(int)MaddenPositions.QB][(int)MaddenAttribute.THP] = true;
            attributesByPosition[(int)MaddenPositions.QB][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.QB][(int)MaddenAttribute.SPD] = true;

            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.BTK] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.CAR] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.CTH] = true;
            attributesByPosition[(int)MaddenPositions.HB][(int)MaddenAttribute.AWR] = true;

            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.CAR] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.PBK] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.RBK] = true;
            attributesByPosition[(int)MaddenPositions.FB][(int)MaddenAttribute.AWR] = true;

            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.CTH] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.BTK] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.WR][(int)MaddenAttribute.JMP] = true;

            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.PBK] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.RBK] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.CTH] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.BTK] = true;
            attributesByPosition[(int)MaddenPositions.TE][(int)MaddenAttribute.STR] = true;

            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.PBK] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.RBK] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.LT][(int)MaddenAttribute.STR] = true;

            for (int i = (int)MaddenPositions.LG; i <= (int)MaddenPositions.RT; i++)
            {
                for (int j = 0; j < attributesByPosition[(int)MaddenPositions.LT].Count; j++)
                    attributesByPosition[i][j] = attributesByPosition[(int)MaddenPositions.LT][j];
            }

            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.TAK] = true;
            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.LE][(int)MaddenAttribute.STR] = true;

            for (int j = 0; j < attributesByPosition[(int)MaddenPositions.LE].Count; j++)
            {
                attributesByPosition[(int)MaddenPositions.RE][j] = attributesByPosition[(int)MaddenPositions.LE][j];
                attributesByPosition[(int)MaddenPositions.DT][j] = attributesByPosition[(int)MaddenPositions.LE][j];
            }

            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.SPD] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.TAK] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.CTH] = true;
            attributesByPosition[(int)MaddenPositions.LOLB][(int)MaddenAttribute.STR] = true;

            for (int j = 0; j < attributesByPosition[(int)MaddenPositions.LOLB].Count; j++)
            {
                attributesByPosition[(int)MaddenPositions.MLB][j] = attributesByPosition[(int)MaddenPositions.LOLB][j];
                attributesByPosition[(int)MaddenPositions.ROLB][j] = attributesByPosition[(int)MaddenPositions.LOLB][j];
            }

            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.STR] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.ACC] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.AGI] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.CTH] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.JMP] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.TAK] = true;
            attributesByPosition[(int)MaddenPositions.CB][(int)MaddenAttribute.SPD] = true;

            for (int j = 0; j < attributesByPosition[(int)MaddenPositions.CB].Count; j++)
            {
                attributesByPosition[(int)MaddenPositions.FS][j] = attributesByPosition[(int)MaddenPositions.CB][j];
                attributesByPosition[(int)MaddenPositions.SS][j] = attributesByPosition[(int)MaddenPositions.CB][j];
            }

            attributesByPosition[(int)MaddenPositions.K][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.K][(int)MaddenAttribute.KPR] = true;
            attributesByPosition[(int)MaddenPositions.K][(int)MaddenAttribute.KAC] = true;

            attributesByPosition[(int)MaddenPositions.P][(int)MaddenAttribute.AWR] = true;
            attributesByPosition[(int)MaddenPositions.P][(int)MaddenAttribute.KPR] = true;
            attributesByPosition[(int)MaddenPositions.P][(int)MaddenAttribute.KAC] = true;

            increaseYoungDistributions[(int)MaddenAttribute.AWR] = 100;
            increaseYoungDistributions[(int)MaddenAttribute.ACC] = 20;
            increaseYoungDistributions[(int)MaddenAttribute.AGI] = 24;
            increaseYoungDistributions[(int)MaddenAttribute.BTK] = 50;
            increaseYoungDistributions[(int)MaddenAttribute.CAR] = 14;
            increaseYoungDistributions[(int)MaddenAttribute.CTH] = 60;
            increaseYoungDistributions[(int)MaddenAttribute.JMP] = 16;
            increaseYoungDistributions[(int)MaddenAttribute.KAC] = 40;
            increaseYoungDistributions[(int)MaddenAttribute.KPR] = 50;
            increaseYoungDistributions[(int)MaddenAttribute.PBK] = 70;
            increaseYoungDistributions[(int)MaddenAttribute.RBK] = 70;
            increaseYoungDistributions[(int)MaddenAttribute.SPD] = 12;
            increaseYoungDistributions[(int)MaddenAttribute.STR] = 18;
            increaseYoungDistributions[(int)MaddenAttribute.TAK] = 75;
            increaseYoungDistributions[(int)MaddenAttribute.THA] = 40;
            increaseYoungDistributions[(int)MaddenAttribute.THP] = 25;

            increaseOldDistributions[(int)MaddenAttribute.AWR] = 50;
            increaseOldDistributions[(int)MaddenAttribute.ACC] = 8;
            increaseOldDistributions[(int)MaddenAttribute.AGI] = 12;
            increaseOldDistributions[(int)MaddenAttribute.BTK] = 40;
            increaseOldDistributions[(int)MaddenAttribute.CAR] = 14;
            increaseOldDistributions[(int)MaddenAttribute.CTH] = 60;
            increaseOldDistributions[(int)MaddenAttribute.JMP] = 4;
            increaseOldDistributions[(int)MaddenAttribute.KAC] = 40;
            increaseOldDistributions[(int)MaddenAttribute.KPR] = 50;
            increaseOldDistributions[(int)MaddenAttribute.PBK] = 70;
            increaseOldDistributions[(int)MaddenAttribute.RBK] = 70;
            increaseOldDistributions[(int)MaddenAttribute.SPD] = 2;
            increaseOldDistributions[(int)MaddenAttribute.STR] = 10;
            increaseOldDistributions[(int)MaddenAttribute.TAK] = 75;
            increaseOldDistributions[(int)MaddenAttribute.THA] = 40;
            increaseOldDistributions[(int)MaddenAttribute.THP] = 10;

            decreaseYoungDistributions[(int)MaddenAttribute.AWR] = 20;
            decreaseYoungDistributions[(int)MaddenAttribute.ACC] = 15;
            decreaseYoungDistributions[(int)MaddenAttribute.AGI] = 20;
            decreaseYoungDistributions[(int)MaddenAttribute.BTK] = 25;
            decreaseYoungDistributions[(int)MaddenAttribute.CAR] = 20;
            decreaseYoungDistributions[(int)MaddenAttribute.CTH] = 40;
            decreaseYoungDistributions[(int)MaddenAttribute.JMP] = 10;
            decreaseYoungDistributions[(int)MaddenAttribute.KAC] = 30;
            decreaseYoungDistributions[(int)MaddenAttribute.KPR] = 40;
            decreaseYoungDistributions[(int)MaddenAttribute.PBK] = 55;
            decreaseYoungDistributions[(int)MaddenAttribute.RBK] = 55;
            decreaseYoungDistributions[(int)MaddenAttribute.SPD] = 12;
            decreaseYoungDistributions[(int)MaddenAttribute.STR] = 14;
            decreaseYoungDistributions[(int)MaddenAttribute.TAK] = 60;
            decreaseYoungDistributions[(int)MaddenAttribute.THA] = 30;
            decreaseYoungDistributions[(int)MaddenAttribute.THP] = 15;

            decreaseOldDistributions[(int)MaddenAttribute.AWR] = 5;
            decreaseOldDistributions[(int)MaddenAttribute.ACC] = 40;
            decreaseOldDistributions[(int)MaddenAttribute.AGI] = 50;
            decreaseOldDistributions[(int)MaddenAttribute.BTK] = 30;
            decreaseOldDistributions[(int)MaddenAttribute.CAR] = 10;
            decreaseOldDistributions[(int)MaddenAttribute.CTH] = 20;
            decreaseOldDistributions[(int)MaddenAttribute.JMP] = 40;
            decreaseOldDistributions[(int)MaddenAttribute.KAC] = 10;
            decreaseOldDistributions[(int)MaddenAttribute.KPR] = 50;
            decreaseOldDistributions[(int)MaddenAttribute.PBK] = 10;
            decreaseOldDistributions[(int)MaddenAttribute.RBK] = 10;
            decreaseOldDistributions[(int)MaddenAttribute.SPD] = 50;
            decreaseOldDistributions[(int)MaddenAttribute.STR] = 30;
            decreaseOldDistributions[(int)MaddenAttribute.TAK] = 30;
            decreaseOldDistributions[(int)MaddenAttribute.THA] = 10;
            decreaseOldDistributions[(int)MaddenAttribute.THP] = 50;
        }

        private void youthSlider_ValueChanged(object s, EventArgs e)
        {
            if (!triggerChangedEvent)
            {
                return;
            }

            NumericUpDown UpDownToChange = null;
            TrackBar sender = (TrackBar)s;

            if (sender == youthSlider)
                UpDownToChange = youthUpDown;
            else if (sender == ageSlider)
                UpDownToChange = ageUpDown;
            else if (sender == playedSlider)
                UpDownToChange = playedUpDown;
            else if (sender == startedSlider)
                UpDownToChange = startedUpDown;
            else if (sender == maxSlider)
                UpDownToChange = maxUpDown;
            else if (sender == randomSlider)
                UpDownToChange = randomUpDown;

            triggerChangedEvent = false;
            UpDownToChange.Value = sender.Value;
            triggerChangedEvent = true;
        }

        private void youthUpDown_ValueChanged(object s, EventArgs e)
        {
            if (!triggerChangedEvent)
            {
                return;
            }

            NumericUpDown sender = (NumericUpDown)s;
            TrackBar sliderToChange = null;

            if (sender == youthUpDown)
                sliderToChange = youthSlider;
            else if (sender == ageUpDown)
                sliderToChange = ageSlider;
            else if (sender == playedUpDown)
                sliderToChange = playedSlider;
            else if (sender == startedUpDown)
                sliderToChange = startedSlider;
            else if (sender == maxUpDown)
                sliderToChange = maxSlider;
            else if (sender == randomUpDown)
                sliderToChange = randomSlider;

            triggerChangedEvent = false;
            sliderToChange.Value = (int)sender.Value;
            triggerChangedEvent = true;
        }
    }
}