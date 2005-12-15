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
    public partial class TradeUpForm : Form
    {
        DraftModel dm;
        DraftForm df;

        List<int> myPicksList = new List<int>();
        List<int> theirPicksList = new List<int>();

        TradeOffer to;

        int HigherCON;
        int LowerCON;

        string ourteam;
        string theirteam;

        bool kill = false;

        public bool strikeAdded;

        public TradeUpForm(DraftModel draftModel, DraftForm draftForm, TradeOffer tradeOffer)
        {
            dm = draftModel;
            df = draftForm;

            dm.tradeUpForm = this;

            HigherCON = dm.model.TeamModel.GetTeamRecord(df.CurrentSelectingId).CON;
            LowerCON = dm.model.TeamModel.GetTeamRecord(dm.HumanTeamId).CON;

            InitializeComponent();

            ourteam = dm.model.TeamModel.GetTeamNameFromTeamId(dm.HumanTeamId);
            theirteam = dm.model.TeamModel.GetTeamNameFromTeamId(df.CurrentSelectingId);

            myLabel.Text = ourteam + "' Picks";
            CPUlabel.Text = theirteam + "' Picks";

                to = tradeOffer;

                foreach (int pick in to.lowerAvailable)
                {
                    myPicksList.Add(pick);
                }

                theirPicksList.Add(to.pickNumber);

                foreach (int pick in to.higherAvailable)
                {
                    theirPicksList.Add(pick);
                }

                for (int i = 1; i <= 7; i++)
                {
                    if (!dm.futureTradedPicks[to.LowerTeam].ContainsKey(i))
                    {
                        myPicksList.Add(1000 + i);
                    }
                }

                if (to.allowFuturePicksFromHigher)
                {
                    int startRound = to.pickNumber / 32 + 3;
                    for (int i = startRound; i <= 7; i++)
                    {
                        if (!dm.futureTradedPicks[to.HigherTeam].ContainsKey(i))
                        {
                            theirPicksList.Add(1000 + i);
                        }
                    }
                }

                refreshPickLists();

                offerButton.Enabled = false;
                resetButton.Enabled = false;

                refreshTotals();
        }

        private void selectPrevious()
        {
            CPUpicks.SelectedIndices.Clear();
            myPicks.SelectedIndices.Clear();

            foreach (int pick in to.PicksFromLower)
            {
                int index = myPicksList.IndexOf(pick);
                myPicks.SelectedIndices.Add(index);
            }

            CPUpicks.SelectedIndices.Add(0);

            foreach (int pick in to.PicksFromHigher)
            {
                int index = theirPicksList.IndexOf(pick);
                CPUpicks.SelectedIndices.Add(index);
            }

            resetButton.Enabled = false;
            offerButton.Enabled = false;

            if (to.PicksFromHigher.Count == 0 && to.PicksFromLower.Count == 0)
            {
                approveButton.Enabled = false;
            }
            else
            {
                approveButton.Enabled = true;
            }
        }

        private void refreshPickLists()
        {
            foreach (int pick in myPicksList)
            {
                myPicks.Items.Add(df.pickToString(pick, LowerCON));
            }

            foreach (int pick in theirPicksList)
            {
                CPUpicks.Items.Add(df.pickToString(pick, HigherCON));
            }

            selectPrevious();
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?  Once you reject, you can't negotiate on this pick anymore.", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                df.DisableTradeButton();
                to.status = (int)TradeOfferStatus.Rejected;

                dm.tradeUpForm = null;
                df.tradeUpForm = null;

                this.Close();
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            selectPrevious();
            refreshTotals();
        }

        private void refreshTotals()
        {
            double humanTotal = 0;
            double CPUtotal = 0;

            foreach (int index in myPicks.SelectedIndices)
            {
                humanTotal += df.pickvalue(myPicksList[index], LowerCON);
            }

            foreach (int index in CPUpicks.SelectedIndices)
            {
                CPUtotal += df.pickvalue(theirPicksList[index], HigherCON);
            }

            humanValue.Text = humanTotal.ToString();
            CPUvalue.Text = CPUtotal.ToString();
        }

        private void myPicks_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPUpicks.SelectedIndices.Add(0);

            approveButton.Enabled = false;
            offerButton.Enabled = true;
            resetButton.Enabled = true;

            refreshTotals();
        }

        private void approveButton_Click(object sender, EventArgs e)
        {
            to.offersFromLower.Add(to.offersFromHigher[to.offersFromHigher.Count - 1]);
            to.status = (int)TradeOfferStatus.HigherResponsePending;
            approveButton.Enabled = false;
        }

        public void HigherOffer(int response)
        {
            selectPrevious();
            refreshTotals();

            offerButton.Enabled = false;
            resetButton.Enabled = false;

            List<string> lines = new List<string>(conversation.Lines);

            switch (response)
            {       
                case (int)TradeResponse.Accept:
                    lines.Add(theirteam + ": Ok, we'll take this offer.");
                    break;
                case (int)TradeResponse.BiddingWar:
                    lines.Add(theirteam + ": We just received an acceptable offer from another team.  If you want to make this deal, we'll need something like this.");
                    break;
                case (int)TradeResponse.BiddingWarReject:
                    lines.Add(theirteam + ": We just received an acceptable offer from another team, and it doesn't look like you've got the picks to match it.  Maybe next time guys.");
                    break;
                case (int)TradeResponse.CounterOffer:
                    if (strikeAdded)
                    {
                        if (to.lowerStrikes == 1)
                        {
                            lines.Add(theirteam + ": Come on guys.  If you're not serious about negotiating, then don't waste our time.");
                        }
                        else
                        {
                            lines.Add(theirteam + ": You're really trying our patience.  We aren't going to go much lower than this.");
                        }
                        strikeAdded = false;
                        to.lastWasStrike = false;
                    }
                    else
                    {
                        lines.Add(theirteam + ": That's not quite good enough.  What do you think of this instead?");
                    }
                    break;
                case (int)TradeResponse.PendingAccept:
                    lines.Add(theirteam + ": This looks pretty good.  We might go with this, but hang on for a bit.");
                    break;
                case (int)TradeResponse.Reject:
                    if (to.lowerStrikes >= 3)
                    {
                        lines.Add(theirteam + ": Doesn't seem like we're going to make this work guys.  We need to look elsewhere.");
                    }
                    else
                    {
                        lines.Add(theirteam + ": Sorry, we're just not interested.");
                    }
                    break;
            }

            if (response > 1)
            {
                approveButton.Enabled = false;

                if (response > 2)
                {
                    rejectButton.Enabled = false;
                    df.DisableTradeButton();

                    kill = true;
                }
            }
            else
            {
                approveButton.Enabled = true;
            }

            conversation.Lines = lines.ToArray();

            conversation.ScrollToCaret();
        }

        private void offerButton_Click(object sender, EventArgs e)
        {
            to.PicksFromHigher = new List<int>();
            to.PicksFromLower = new List<int>();

            foreach (int i in myPicks.SelectedIndices)
            {
                to.PicksFromLower.Add(myPicksList[i]);
            }

            foreach (int i in CPUpicks.SelectedIndices)
            {
                to.PicksFromHigher.Add(theirPicksList[i]);
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
                    tempValue += dm.futureValues(pick - 1000, LowerCON);
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
                    tempValue -= dm.futureValues(pick - 1000, HigherCON);
                }
            }

            to.offersFromLower.Add(tempValue);
            to.SetMinTake();
            to.status = (int)TradeOfferStatus.HigherResponsePending;

            approveButton.Enabled = false;
            offerButton.Enabled = false;
            resetButton.Enabled = false;
        }

        private void TradeUpForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (kill)
            {
                dm.tradeUpForm = null;
                df.tradeUpForm = null;
                this.Close();
            }
        }

        private void tradeHepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpstring =
                "This dialog lets you negotiate a trade with the currently drafting CPU team,\n" +
                "while the CPU also negotiates with other teams in the background.\n\n" +
                "Listed in the boxes at the top are the picks that you and the CPU have\n" +
                "available to trade, followed by the value of that pick in parentheses, according\n" +
                "to the draft trade value charts used by real football teams.\n\n" +
                "If you like the offer the CPU has proposed, click \"Approve\".  They will then try to\n" +
                "drive up the bidding by informing other teams that you have accepted their offer.  If no\n" +
                "other team outbids you, the trade will go through.  If you get outbid, however, the CPU will\n" +
                "tell you that you need to up the ante to get their pick.\n\n" +
                "You can also counter-offer by changing the picks in the list, then clicking the \"Offer\" button.\n" +
                "If after changing some picks around, you want to revert to the CPU's last offer, then\n" +
                "click \"Reset\".  You can reject the current trade by clicking \"Reject\" at any time.";

            MessageBox.Show(helpstring, "Help");
        }

        private void TradeUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!kill)
            {
                MessageBox.Show("Trade talks can not be resumed for the rest of this pick.", "Trade Talks Ending");
            }

            to.status = (int)TradeOfferStatus.Rejected;
        }
    }

    public enum TradeResponse
    {
        CounterOffer = 0,
        BiddingWar,
        PendingAccept,
        BiddingWarReject,
        Reject,
        Accept
    }
}