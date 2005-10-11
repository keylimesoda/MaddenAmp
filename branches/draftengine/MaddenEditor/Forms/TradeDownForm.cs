/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class TradeDownForm : Form
    {
        int activeTeam = -1;
        int HumanTeam;
        bool kill = false;

        DraftModel dm;
        DraftForm df;

        List<int> higherPendingList = new List<int>();
        List<int> lowerPendingList = new List<int>();
        List<int> noOfferList = new List<int>();
        List<int> rejectedList = new List<int>();

        Dictionary<int, List<int>> picksList = new Dictionary<int, List<int>>();

        Dictionary<int, int> cons = new Dictionary<int, int>();
        Dictionary<int, int> nextpicks = new Dictionary<int,int>();
        Dictionary<int, string> teamnames = new Dictionary<int,string>();

        Dictionary<int, List<string>> conversations = new Dictionary<int, List<string>>();

        // Stuff gets refreshed so often that we have to be careful with implicit multi-threading
        bool allowTeamRefresh = true;


        public TradeDownForm(DraftModel draftModel, DraftForm draftForm, TradeOffer to)
        {
            activeTeam = to.LowerTeam;
            HumanTeam = to.HigherTeam;

            dm = draftModel;
            df = draftForm;

            dm.tradeDownForm = this;

            InitializeComponent();

            for (int i = 0; i < 32; i++)
            {
                nextpicks[i] = dm.GetNextPick(i, df.CurrentPick)+1;
                teamnames[i] = dm.model.TeamModel.GetTeamNameFromTeamId(i);
                cons[i] = dm.model.TeamModel.GetTeamRecord(i).CON;

                conversations[i] = new List<string>();
            }
            
            myLabel.Text = teamnames[HumanTeam] + "' Picks";
            CPUlabel.Text = teamnames[activeTeam] + "' Picks";

            picksList[to.HigherTeam] = new List<int>();
            picksList[to.HigherTeam].Add(to.pickNumber);

            foreach (int pick in to.higherAvailable)
            {
                picksList[to.HigherTeam].Add(pick);
            }

            for (int i = 1; i <= 7; i++)
            {
                if (!dm.futureTradedPicks[to.HigherTeam].ContainsKey(i))
                {
                    picksList[to.HigherTeam].Add(1000 + i);
                }
            }

            FillTeamBoxes();
            FillPickLists(activeTeam);
            RefreshPickBoxes();

            selectPrevious();
        }

        private void selectPrevious()
        {
            CPUpicks.SelectedIndices.Clear();
            myPicks.SelectedIndices.Clear();

            myPicks.SelectedIndices.Add(0);

            foreach (int pick in dm.tradeOffers[activeTeam].PicksFromHigher)
            {
                int index = picksList[dm.tradeOffers[activeTeam].HigherTeam].IndexOf(pick);
                myPicks.SelectedIndices.Add(index);
            }

            foreach (int pick in dm.tradeOffers[activeTeam].PicksFromLower)
            {
                int index = picksList[dm.tradeOffers[activeTeam].LowerTeam].IndexOf(pick);
                CPUpicks.SelectedIndices.Add(index);
            }

            resetButton.Enabled = false;
            offerButton.Enabled = false;

            if (dm.tradeOffers[activeTeam].PicksFromHigher.Count == 0 && dm.tradeOffers[activeTeam].PicksFromLower.Count == 0)
            {
                acceptButton.Enabled = false;
            }
            else
            {
                acceptButton.Enabled = true;
            }

            refreshTotals();
        }

        private void RefreshPickBoxes()
        {
            myPicks.Items.Clear();
            CPUpicks.Items.Clear();

            foreach (int pick in picksList[dm.tradeOffers[activeTeam].HigherTeam])
            {
                myPicks.Items.Add(df.pickToString(pick, cons[dm.tradeOffers[activeTeam].HigherTeam]));
            }

            foreach (int pick in picksList[activeTeam])
            {
                CPUpicks.Items.Add(df.pickToString(pick, cons[activeTeam]));
            }

            selectPrevious();
        }

        private void refreshTotals()
        {
            double humanTotal = 0;
            double CPUtotal = 0;

            foreach (int index in myPicks.SelectedIndices)
            {
                humanTotal += df.pickvalue(picksList[HumanTeam][index], cons[HumanTeam]);
            }

            foreach (int index in CPUpicks.SelectedIndices)
            {
                CPUtotal += df.pickvalue(picksList[dm.tradeOffers[activeTeam].LowerTeam][index], cons[dm.tradeOffers[activeTeam].LowerTeam]);
            }

            humanValue.Text = humanTotal.ToString();
            CPUvalue.Text = CPUtotal.ToString();
        }

        private void FillPickLists(int TeamId)
        {
            picksList[TeamId] = new List<int>();

            // If they click on a team in the NoOfferYet list, we'll need to create
            // a TradeOffer

            if (!dm.tradeOffers.ContainsKey(TeamId))
            {
                dm.tradeOffers.Add(TeamId, dm.setupTradeOffer(TeamId, df.CurrentPick));
            }

            foreach (int pick in dm.tradeOffers[TeamId].lowerAvailable)
            {
                picksList[TeamId].Add(pick);
            }

            if (dm.tradeOffers[TeamId].allowFuturePicksFromLower)
            {
                int startRound = dm.tradeOffers[TeamId].pickNumber / 32 + 1;
                for (int i = startRound; i <= 7; i++)
                {
                    if (!dm.futureTradedPicks[dm.tradeOffers[TeamId].LowerTeam].ContainsKey(i))
                    {
                        picksList[TeamId].Add(1000 + i);
                    }
                }
            }
        }

        public void FillTeamBoxes() 
        {
            if (!allowTeamRefresh) { return; }

            higherPendingList = new List<int>();
            lowerPendingList = new List<int>();
            noOfferList = new List<int>();
            rejectedList = new List<int>();

            Point higherScroll = HigherPendingBox.AutoScrollOffset;
            Point lowerScroll = LowerPendingBox.AutoScrollOffset;
            Point noofferScroll = NoOfferBox.AutoScrollOffset;
            Point rejectedScroll = RejectedBox.AutoScrollOffset;

            for (int i = 0; i < 32; i++)
            {
                if (dm.tradeOffers.ContainsKey(i))
                {
                    if (dm.tradeOffers[i].status == (int)TradeOfferStatus.HigherResponsePending)
                    {
                        higherPendingList.Add(i);
                    }
                    else if (dm.tradeOffers[i].status == (int)TradeOfferStatus.LowerResponsePending)
                    {
                        lowerPendingList.Add(i);
                    }
                    else if (dm.tradeOffers[i].status == (int)TradeOfferStatus.Rejected)
                    {
                        rejectedList.Add(i);
                    }
                }
                else if (i != dm.HumanTeamId)
                {
                    noOfferList.Add(i);
                }
            }

            HigherPendingBox.Items.Clear();
            foreach (int teamId in higherPendingList)
            {
                HigherPendingBox.Items.Add(teamnames[teamId] + " (" + nextpicks[teamId] + ")");
            }
            HigherPendingBox.AutoScrollOffset = higherScroll;

            LowerPendingBox.Items.Clear();
            foreach (int teamId in lowerPendingList)
            {
                LowerPendingBox.Items.Add(teamnames[teamId] + " (" + nextpicks[teamId] + ")");
            }
            LowerPendingBox.AutoScrollOffset = lowerScroll;

            NoOfferBox.Items.Clear();
            foreach (int teamId in noOfferList)
            {
                NoOfferBox.Items.Add(teamnames[teamId] + " (" + nextpicks[teamId] + ")");
            }
            NoOfferBox.AutoScrollOffset = noofferScroll;

            RejectedBox.Items.Clear();
            foreach (int teamId in rejectedList)
            {
                RejectedBox.Items.Add(teamnames[teamId] + " (" + nextpicks[teamId] + ")");
            }
            RejectedBox.AutoScrollOffset = rejectedScroll;
        }

        public void Message(TradeOffer to, int response)
        {
            string theirteam = teamnames[to.LowerTeam];
            switch (response)
            {
                case (int)TradeResponse.Accept:
                    conversations[to.LowerTeam].Add(theirteam + ": Ok, we could do this offer.");
                    break;
                case (int)TradeResponse.CounterOffer:
                    if (to.lastWasStrike)
                    {
                        if (to.higherStrikes == 1)
                        {
                            conversations[to.LowerTeam].Add(theirteam + ": Come on guys.  If you're not serious about negotiating, then don't waste our time.");
                        }
                        else
                        {
                            conversations[to.LowerTeam].Add(theirteam + ": You're really trying our patience.  We aren't going to go much higher than this.");
                        }
                        to.lastWasStrike = false;
                    }
                    else
                    {
                        conversations[to.LowerTeam].Add(theirteam + ": That's not quite good enough.  What do you think of this instead?");
                    }
                    break;
                case (int)TradeResponse.Reject:
                    if (to.higherStrikes >= 3)
                    {
                        conversations[to.LowerTeam].Add(theirteam + ": Doesn't seem like we're going to make this work guys.");
                    }
                    else
                    {
                        conversations[to.LowerTeam].Add(theirteam + ": Sorry, we're just not interested.");
                    }
                    break;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            selectPrevious();
        }

        private void Picks_SelectedIndexChanged(object sender, EventArgs e)
        {
            myPicks.SelectedIndices.Add(0);

            acceptButton.Enabled = false;
            offerButton.Enabled = true;
            resetButton.Enabled = true;

            refreshTotals();
        }

        private void offerButton_Click(object sender, EventArgs e)
        {
            TradeOffer to = dm.tradeOffers[activeTeam];

            to.PicksFromHigher = new List<int>();
            to.PicksFromLower = new List<int>();

            foreach (int i in myPicks.SelectedIndices)
            {
                to.PicksFromHigher.Add(picksList[to.HigherTeam][i]);
            }

            foreach (int i in CPUpicks.SelectedIndices)
            {
                to.PicksFromLower.Add(picksList[to.LowerTeam][i]);
            }

            to.PicksFromHigher.Remove(to.pickNumber);

            double tempValue = 0;

            foreach (int pick in to.PicksFromLower)
            {
                if (pick < 1000)
                {
                    tempValue += dm.pickValues[pick];
                }
                else
                {
                    tempValue += dm.futureValues(pick - 1000, cons[to.LowerTeam]);
                }
            }

            foreach (int pick in to.PicksFromHigher)
            {
                if (pick < 1000)
                {
                    tempValue -= dm.pickValues[pick];
                }
                else
                {
                    tempValue -= dm.futureValues(pick - 1000, cons[to.HigherTeam]);
                }
            }

            to.offersFromHigher.Add(tempValue);
            to.status = (int)TradeOfferStatus.LowerResponsePending;

            acceptButton.Enabled = false;
            offerButton.Enabled = false;
            resetButton.Enabled = false;

            FillTeamBoxes();
        }

        private void TeamSelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex < 0)
            {
                return;
            }

            allowTeamRefresh = false;

            if (sender == HigherPendingBox)
            {
                activeTeam = higherPendingList[HigherPendingBox.SelectedIndex];

                offerButton.Enabled = false;
                acceptButton.Enabled = true;
                resetButton.Enabled = false;
                rejectButton.Enabled = true;
            }
            else if (sender == LowerPendingBox)
            {
                activeTeam = lowerPendingList[LowerPendingBox.SelectedIndex];

                offerButton.Enabled = false;
                acceptButton.Enabled = false;
                resetButton.Enabled = false;
                rejectButton.Enabled = true;
            }
            else if (sender == NoOfferBox)
            {
                activeTeam = noOfferList[NoOfferBox.SelectedIndex];
                dm.setupTradeOffer(activeTeam, df.CurrentPick);

                offerButton.Enabled = false;
                acceptButton.Enabled = false;
                resetButton.Enabled = false;
                rejectButton.Enabled = true;
            }
            else if (sender == RejectedBox)
            {
                allowTeamRefresh = true;
                return;
            }

            conversation.Lines = conversations[activeTeam].ToArray();
            CPUlabel.Text = teamnames[activeTeam] + "' Picks";
            FillPickLists(activeTeam);
            RefreshPickBoxes();

            selectPrevious();
            allowTeamRefresh = true;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to accept this trade offer?", "Accept?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                kill = true;
                dm.AcceptTrade(dm.tradeOffers[activeTeam]);
                df.ProcessTrade(dm.tradeOffers[activeTeam]);
                this.Close();
            }
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to reject this trade offer?  This action can not be undone.", "Reject?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                dm.tradeOffers[activeTeam].status = (int)TradeOfferStatus.Rejected;

                rejectButton.Enabled = false;
                offerButton.Enabled = false;
                acceptButton.Enabled = false;
                resetButton.Enabled = false;

                FillTeamBoxes();
            }
        }

        private void tradeHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpstring =
                "This form lets you simultaneously negotiate a trade with any CPU team.\n\n" +
                "The boxes on the left contain each team's name, along with the current status of\n" +
                "your trade offer with them:  Waiting on their response, waiting on your response,\n" +
                "no offer yet, and rejected.  Click on a team in one of these boxes to start or\n" +
                "continue negotiating with them.  Occasionally, a CPU team will initiate trade talks\n" +
                "itself by making an offer -- their team will then move from the \"No Offer Yet\" box to\n" +
                "the \"Waiting on Your Response\" box.\n\n" +
            "Listed in the boxes on the right are the picks that you and the CPU have\n" +
            "available to trade, followed by the value of that pick in parentheses, according\n" +
            "to the draft trade value charts used by real football teams.\n\n" +
            "If you like the offer the CPU has proposed, click \"Accept\", and the trade will go through.\n\n" +
            "You can also counter-offer by changing the picks in the list, then clicking the \"Offer\" button.\n" +
            "If after changing some picks around, you want to revert to the CPU's last offer, then\nclick \"Reset\".  You can reject the current trade by clicking \"Reject\" at any time.";

            MessageBox.Show(helpstring, "Help");
        }

        private void TradeDownForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kill)
            {
                MessageBox.Show("Trade talks can not be resumed for the rest of this pick.", "Trade Talks Ending");
            }

            foreach (TradeOffer to in dm.tradeOffers.Values)
            {
                to.status = (int)TradeOfferStatus.Rejected;
            }
        }
    }
}