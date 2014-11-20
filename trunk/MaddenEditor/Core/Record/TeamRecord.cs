/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MaddenEditor.Core.Record
{
	public class TeamRecord : TableRecordModel
	{
		// TEAM

        public const string CUSTOM_ART_FILE = "CART";
        public const string CONFERENCE_ID = "CGID";
        public const string CTHT = "CTHT";
        public const string CITY_ID = "CYID";
        public const string DIVISION_ID = "DGID";
        public const string DIVISION_ORDER = "DISN";
        public const string FRANCHISE_ID = "FRID";
        public const string JJNM = "JJNM";
        public const string LEAGUE_ID = "LGID";
        public const string OAC1 = "OAC1";
        public const string OAC2 = "OAC2";
        public const string OAC3 = "OAC3";
        public const string OAC4 = "OAC4";
        public const string OAC5 = "OAC5";
        public const string OCBW = "OCBW";
        public const string OCPW = "OCPW";
        public const string OCRW = "OCRW";
        public const string OCTW = "OCTW";
        public const string ONFD = "ONFD";
        public const string OTPF = "OTPF";
        public const string OWFI = "OWFI";
        public const string OWFW = "OWFW";
        public const string STADIUM_ID = "SGID";
        public const string T2YC = "T2YC";                          // 2008?
        public const string TAss = "TAss";
        public const string TAth = "TAth";
        public const string TAUO = "TAUO";
        public const string SECONDARY_BLUE = "TB2B";
        public const string SECONDARY_GREEN = "TB2G";
        public const string SECONDARY_RED = "TB2R";
        public const string PRIMARY_BLUE = "TBCB";
        public const string PRIMARY_GREEN = "TBCG";
        public const string PRIMARY_RED = "TBCR";
        public const string TCDO = "TCDO";
        public const string TCHE = "TCHE";
        public const string SALARY_CAP_PENALTY_THIS_YEAR = "TCP0";
        public const string SALARY_CAP_PENALTY_NEXT_YEAR = "TCP1";
        public const string SEASON_CONF_POINTS_AGAINST = "TCPA";
        public const string SEASON_CONF_POINTS_SCORED = "TCPF";
        public const string TCRP = "TCRP";
        public const string TCTX = "TCTX";
        public const string CONF_WIN_PERC = "TCWP";
        public const string SEASON_DEFENSE_RANK = "TDER";
        public const string TEAM_NAME = "TDNA";
        public const string SEASON_DIV_POINTS_AGAINST = "TDPA";
        public const string DEFENSIVE_PLAYBOOK = "TDPB";
        public const string SEASON_DIV_POINTS_SCORED = "TDPF";
        public const string TEAM_ORDER = "TDRI";
        public const string DIV_WIN_PERC = "TDWP";
        public const string TEZ1 = "TEZ1";
        public const string TEZ2 = "TEZ2";
        public const string TFCA = "TFCA";
        public const string TFCR = "TFCR";
        public const string TFET = "TFET";
        public const string TFLO = "TFLO";
        public const string TFTL = "TFTL";
        public const string TEAM_ID = "TGID";
        public const string TGPT = "TGPT";
        public const string TGRP = "TGRP";
        public const string TEAM_LOGO = "TLGL";
        public const string TEAM_HELMET_LOGO = "TLGS";
        public const string TEAM_LONG_NAME = "TLNA";
        public const string TEAM_SHORT_NAME = "TLSA";
        public const string TMFL = "TMFL";
        public const string TEAM_NICK_NAME = "TMNC";
        public const string TMPS = "TMPS";
        public const string TMRE = "TMRE";
        public const string TEAM_SALARY = "TMSA";
        public const string NON_CONF_LOSSES = "TNCL";
        public const string NON_CONF_TIES = "TNCT";
        public const string NON_CONF_WINS = "TNCW";
        public const string SEASON_OFFENSE_RANK = "TOFR";
        public const string TEAM_ORIGINAL_ID = "TOID";
        public const string OFFENSIVE_PLAYBOOK = "TOPB";
        public const string TEAM_ROSTER_ORDER = "TORD";
        public const string PREVIOUS_CON_STANDING = "TPCS";
        public const string PREVIOUS_DIV_STANDING = "TPDS";
        public const string TPNS = "TPNS";
        public const string PREVIOUS_SEASON_LOSSES = "TPSL";
        public const string PREVIOUS_SEASON_TIES = "TPST";
        public const string PREVIOUS_SEASON_WINS = "TPSW";
        public const string DB_RATING = "TRDB";
        public const string DEFENSIVE_RATING = "TRDE";
        public const string DL_RATING = "TRDL";
        public const string REPUTATION = "TREP";
        public const string LB_RATING = "TRLB";
        public const string OFFENSIVE_RATING = "TROF";
        public const string OL_RATING = "TROL";
        public const string OVERALL_RATING = "TROV";
		public const string QB_RATING = "TRQB";
		public const string RB_RATING = "TRRB";
        public const string ST_RATING = "TRST";
        public const string TEAM_RIVAL_1 = "TRV1";
        public const string TEAM_RIVAL_2 = "TRV2";
        public const string TEAM_RIVAL_3 = "TRV3";
        public const string SEASON_AWAY_LOSSES = "tsal";
        public const string SEASON_AWAY_TIES = "tsat";
        public const string SEASON_AWAY_WINS = "tsaw";
        public const string SEASON_BIGGEST_LOSS = "tsbl";
        public const string SEASON_BIGGEST_WIN = "tsbw";
        public const string SEASON_CONF_LOSSES = "tscl";
        public const string SEASON_CURRENT_STREAK = "TSCS";
        public const string SEASON_CONF_STANDING = "tscs";
        public const string SEASON_CONF_TIES = "tsct";
        public const string SEASON_CONF_WINS = "tscw";
        public const string TSDI = "tsdi";
        public const string SEASON_DIV_LOSSES = "tsdl";
        public const string SEASON_DIV_STANDING = "TSDS";
        public const string SEASON_DIV_WINS = "TSDW";
        public const string SEASON_HOME_LOSSES = "TSHL";
        public const string TEAM_SHOE_COLOR = "TSHO";               // 2004-2006 only?
        public const string SEASON_HOME_TIES = "tsht";
        public const string SEASON_HOME_WINS = "tshw";
        public const string TSID = "TSID";
        public const string SEASON_LOSSES = "TSLO";
        public const string TSLW = "TSLW";
        public const string TSNA = "TSNA";
        public const string TSNS = "tsns";
        public const string TSOW = "TSOW";
        public const string SEASON_POINTS_ALLOWED = "TSPA";
        public const string SEASON_POINTS_SCORED = "TSPF";
        public const string SEASON_TIES = "TSTI";
        public const string SEASON_WINS = "TSWI";
        public const string TEAM_TYPE = "TTYP";
        public const string TUAW = "TUAW";
        public const string TUFT = "TUFT";
        public const string TUHO = "TUHO";
        public const string TVIS = "TVIS";
        public const string TVQS = "TVQS";
        public const string WINNING_PERCENTAGE = "TWPC";
		public const string WR_RATING = "TWRR";	
		
		
		



        // MADDEN DRAFT EDIT
        private int wins;
        private int playoffExit;
        private int strengthOfSchedule;
        private int effectiveSOS;
        private int con;
        // MADDEN DRAFT EDIT

		public TeamRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}
        // MADDEN DRAFT EDIT
        public enum Defense {
            Front43=0,
            Front34,
            Cover2
        }

        public int DefensiveSystem
        {
            get {
                if (DefensivePlaybook == 1) {
                    return (int)Defense.Front34;
                } else if (DefensivePlaybook == 3) {
                    return (int)Defense.Cover2;
                } else {
                    return (int)Defense.Front43;
                }
            }
        }
            
        public int CON
        {
            get
            {
                return con;
            }
            set
            {
                con = value;
            }
        }

        public int Wins
        {
            get
            {
                return wins;
            }
        }

        public int EffectiveSOS
        {
            get
            {
                return effectiveSOS;
            }
        }

        public int StrengthOfSchedule
        {
            get
            {
                return strengthOfSchedule;
            }
        }

        public int PlayoffExit
        {
            get
            {
                return playoffExit;
            }
            set
            {
                playoffExit = value;
            }
        }

        // In principle I'd prefer a better way of calculating OVR, but in the 
        // interest of saving time, we'll go with this for now.
        public int GetOverall()
        {
            return OverallRating;
        }

		public void SimulateMinicamp()
		{
			bool foundWR = false;
			bool foundDB = false;
			bool foundLB = false;
			bool foundDL = false;
			bool foundHB = false;

			List<int> skip = new List<int>();
			PlayerRecord player;
			Random rand = new Random();
			int toAdd;

			// Pocket Presence
			player = bestEORInRange((int)MaddenPositions.QB, (int)MaddenPositions.QB, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double AWRProb = 99 - player.Awareness;
				double THAProb = 99 - player.ThrowAccuracy;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < AWRProb / (AWRProb + THAProb))
					{
						player.Awareness = player.Awareness + 1;
					}
					else
					{
						player.ThrowAccuracy = player.ThrowAccuracy + 1;
					}
				}

                Trace.Write("Pocket Presence: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
			}

			// Precision Passing
			player = bestEORInRange((int)MaddenPositions.QB, (int)MaddenPositions.QB, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double THPProb = 99 - player.ThrowPower;
				double THAProb = 99 - player.ThrowAccuracy;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < THPProb / (THPProb + THAProb))
					{
						player.ThrowPower = player.ThrowPower + 1;
					}
					else
					{
						player.ThrowAccuracy = player.ThrowAccuracy + 1;
					}
				}
                
                Trace.Write("Precision Passing: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Field Goal
			player = bestEORInRange((int)MaddenPositions.K, (int)MaddenPositions.K, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double KACProb = 99 - player.KickAccuracy;
				double KPRProb = 99 - player.KickPower;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < KACProb / (KACProb + KPRProb))
					{
						player.KickAccuracy = player.KickAccuracy + 1;
					}
					else
					{
						player.KickPower = player.KickPower + 1;
					}
				}

                Trace.Write("Field Goal: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Coffin Corner
			player = bestEORInRange((int)MaddenPositions.P, (int)MaddenPositions.P, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double KACProb = 99 - player.KickAccuracy;
				double KPRProb = 99 - player.KickPower;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < KACProb / (KACProb + KPRProb))
					{
						player.KickAccuracy = player.KickAccuracy + 1;
					}
					else
					{
						player.KickPower = player.KickPower + 1;
					}
				}

                Trace.Write("Coffin Corner: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }



			// Chase and Tackle
			player = bestEORInRange((int)MaddenPositions.LOLB, (int)MaddenPositions.ROLB, skip);

			if (player != null)
			{
				foundLB = true;
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double AWRProb = 99 - player.Awareness;
				double TAKProb = 99 - player.Tackle;
				double AGIProb = 99 - player.Agility;

				double test = rand.NextDouble();

				for (int i = 0; i < toAdd; i++)
				{
					if (test < 4 * TAKProb / (4 * TAKProb + 3 * AWRProb + 2 * AGIProb))
					{
						player.Tackle = player.Tackle + 1;
					}
					else if (test < (3 * AWRProb + 4 * TAKProb) / (4 * TAKProb + 3 * AWRProb + 2 * AGIProb))
					{
						player.Awareness = player.Awareness + 1;
					}
					else
					{
						player.Agility = player.Agility + 1;
					}
				}

                Trace.Write("Chase and Tackle: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Swat Ball -- first check CB's, SS's are overinflated anyway.
			player = bestEORInRange((int)MaddenPositions.CB, (int)MaddenPositions.CB, skip);

			if (player != null)
			{
				foundDB = true;
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double CTHProb = 99 - player.Catching;
				double JMPProb = 99 - player.Jumping;
				double ACCProb = 99 - player.Acceleration;

				double test = rand.NextDouble();

				for (int i = 0; i < toAdd; i++)
				{
					if (test < 4 * ACCProb / (4 * ACCProb + 3 * JMPProb + 2 * CTHProb))
					{
						player.Acceleration = player.Acceleration + 1;
					}
                    else if (test < (3 * JMPProb + 4 * ACCProb) / (4 * ACCProb + 3 * JMPProb + 2 * CTHProb))
					{
						player.Jumping = player.Jumping + 1;
					}
					else
					{
						player.Catching = player.Catching + 1;
					}
				}

                Trace.Write("Swat Ball: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Ground Attack
			player = bestEORInRange((int)MaddenPositions.HB, (int)MaddenPositions.HB, skip);

			if (player != null)
			{
				foundHB = true;
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double CARProb = 99 - player.Carrying;
				double BTKProb = 99 - player.BreakTackle;
				double AGIProb = 99 - player.Agility;

				double test = rand.NextDouble();

				for (int i = 0; i < toAdd; i++)
				{
                    if (test < 4 * BTKProb / (4 * BTKProb + 3 * AGIProb + 2 * CARProb))
					{
						player.BreakTackle = player.BreakTackle + 1;
					}
                    else if (test < (3 * AGIProb + 4 * BTKProb) / (4 * BTKProb + 3 * AGIProb + 2 * CARProb))
					{
						player.Agility = player.Agility + 1;
					}
					else
					{
						player.Carrying = player.Carrying + 1;
					}
				}

                Trace.Write("Ground Attack: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Catch Ball
			player = bestEORInRange((int)MaddenPositions.WR, (int)MaddenPositions.WR, skip);

			if (player != null)
			{
				foundWR = true;
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double CTHProb = 99 - player.Catching;
				double JMPProb = 99 - player.Jumping;
				double ACCProb = 99 - player.Acceleration;

				double test = rand.NextDouble();

				for (int i = 0; i < toAdd; i++)
				{
					if (test < ACCProb / (ACCProb + JMPProb + CTHProb))
					{
						player.Acceleration = player.Acceleration + 1;
					}
					else if (test < (JMPProb + ACCProb) / (ACCProb + JMPProb + CTHProb))
					{
						player.Jumping = player.Jumping + 1;
					}
					else
					{
						player.Catching = player.Catching + 1;
					}
				}

                Trace.Write("WR Catch: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }

			// Trench Fight
			player = bestEORInRange((int)MaddenPositions.LE, (int)MaddenPositions.DT, skip);

			if (player != null)
			{
				foundDL = true;
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double STRProb = 99 - player.Strength;
				double ACCProb = 99 - player.Acceleration;

				double test = rand.NextDouble();

				for (int i = 0; i < toAdd; i++)
				{
					if (test < ACCProb / (ACCProb + 2 * STRProb))
					{
						player.Acceleration = player.Acceleration + 1;
					}
					else
					{
						player.Strength = player.Strength + 1;
					}
				}

                Trace.Write("Trench Fight: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                player.Overall = player.CalculateOverallRating(player.PositionId);
                Trace.WriteLine(", Updated OVR: " + player.Overall);
            }





			// Use a player at a different position if the primary position is unavailable.

			// Swat Ball
			if (foundDB == false)
			{
				player = bestEORInRange((int)MaddenPositions.LOLB, (int)MaddenPositions.SS, skip);

				if (player != null)
				{
					skip.Add(player.PlayerId);

					toAdd = pointsToAdd(false, rand);

					double CTHProb = 99 - player.Catching;
					double JMPProb = 99 - player.Jumping;
					double ACCProb = 99 - player.Acceleration;

					double test = rand.NextDouble();

					for (int i = 0; i < toAdd; i++)
					{
						if (test < 4 * ACCProb / (4 * ACCProb + 3 * JMPProb + 2 * CTHProb))
						{
							player.Acceleration = player.Acceleration + 1;
						}
                        else if (test < (3 * JMPProb + 4 * ACCProb) / (4 * ACCProb + 3 * JMPProb + 2 * CTHProb))
						{
							player.Jumping = player.Jumping + 1;
						}
						else
						{
							player.Catching = player.Catching + 1;
						}
					}

                    Trace.Write("Swat Ball: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                    player.Overall = player.CalculateOverallRating(player.PositionId);
                    Trace.WriteLine(", Updated OVR: " + player.Overall);
                }
            }

			// Chase and Tackle
			if (foundLB == false)
			{
				player = bestEORInRange((int)MaddenPositions.CB, (int)MaddenPositions.SS, skip);

				if (player != null)
				{
					foundLB = true;
					skip.Add(player.PlayerId);

					toAdd = pointsToAdd(false, rand);

					double AWRProb = 99 - player.Awareness;
					double TAKProb = 99 - player.Tackle;
					double AGIProb = 99 - player.Agility;

					double test = rand.NextDouble();

					for (int i = 0; i < toAdd; i++)
					{
						if (test < 4 * TAKProb / (4 * TAKProb + 3 * AWRProb + 2 * AGIProb))
						{
							player.Tackle = player.Tackle + 1;
						}
						else if (test < (3 * AWRProb + 4 * TAKProb) / (4 * TAKProb + 3 * AWRProb + 2 * AGIProb))
						{
							player.Awareness = player.Awareness + 1;
						}
						else
						{
							player.Agility = player.Agility + 1;
						}
					}

                    Trace.Write("Chase and Tackle: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                    player.Overall = player.CalculateOverallRating(player.PositionId);
                    Trace.WriteLine(", Updated OVR: " + player.Overall);
                }
            }

			// Ground Attack
			if (foundHB == false)
			{
				player = bestEORInRange((int)MaddenPositions.TE, (int)MaddenPositions.WR, skip);

				if (player != null)
				{
					foundHB = true;
					skip.Add(player.PlayerId);

					toAdd = pointsToAdd(false, rand);

					double CARProb = 99 - player.Carrying;
					double BTKProb = 99 - player.BreakTackle;
					double AGIProb = 99 - player.Agility;

					double test = rand.NextDouble();

					for (int i = 0; i < toAdd; i++)
					{
                        if (test < 4 * BTKProb / (4 * BTKProb + 3 * AGIProb + 2 * CARProb))
						{
							player.BreakTackle = player.BreakTackle + 1;
						}
                        else if (test < (3 * AGIProb + 4 * BTKProb) / (4 * BTKProb + 3 * AGIProb + 2 * CARProb))
						{
							player.Agility = player.Agility + 1;
						}
						else
						{
							player.Carrying = player.Carrying + 1;
						}
					}

                    Trace.Write("Ground Attack: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                    player.Overall = player.CalculateOverallRating(player.PositionId);
                    Trace.WriteLine(", Updated OVR: " + player.Overall);
                }
            }

			// Catch Ball
			if (foundWR == false)
			{
				player = bestEORInRange((int)MaddenPositions.HB, (int)MaddenPositions.TE, skip);

				if (player != null)
				{
					foundWR = true;
					skip.Add(player.PlayerId);

					toAdd = pointsToAdd(false, rand);

					double CTHProb = 99 - player.Catching;
					double JMPProb = 99 - player.Jumping;
					double ACCProb = 99 - player.Acceleration;

					double test = rand.NextDouble();

					for (int i = 0; i < toAdd; i++)
					{
						if (test < ACCProb / (ACCProb + JMPProb + CTHProb))
						{
							player.Acceleration = player.Acceleration + 1;
						}
						else if (test < (JMPProb + ACCProb) / (ACCProb + JMPProb + CTHProb))
						{
							player.Jumping = player.Jumping + 1;
						}
						else
						{
							player.Catching = player.Catching + 1;
						}
					}

                    Trace.Write("WR Catch: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                    player.Overall = player.CalculateOverallRating(player.PositionId);
                    Trace.WriteLine(", Updated OVR: " + player.Overall);
                }
            }

			// Trench Fight
			if (foundDL == false)
			{
				player = bestEORInRange((int)MaddenPositions.LT, (int)MaddenPositions.ROLB, skip);

				if (player != null)
				{
					foundDL = true;
					skip.Add(player.PlayerId);

					toAdd = pointsToAdd(false, rand);

					double STRProb = 99 - player.Strength;
					double ACCProb = 99 - player.Acceleration;

					double test = rand.NextDouble();

					for (int i = 0; i < toAdd; i++)
					{
						if (test < ACCProb / (ACCProb + 2 * STRProb))
						{
							player.Acceleration = player.Acceleration + 1;
						}
						else
						{
							player.Strength = player.Strength + 1;
						}
					}

                    Trace.Write("Trench Fight: " + player.FirstName + " " + player.LastName + ", Original OVR: " + player.Overall);
                    player.Overall = player.CalculateOverallRating(player.PositionId);
                    Trace.WriteLine(", Updated OVR: " + player.Overall);
                }
            }
		}

		private int pointsToAdd(bool native, Random rand) 
		{
			double test = rand.NextDouble();

			if (native)
			{
				if (test < 0.1)
				{
					return 2;
				}
				else if (test < 0.5)
				{
					return 4;
				}
				else if (test < 0.9)
				{
					return 5;
				}
				else
				{
					return 7;
				}
			}
			else
			{
				if (test < 0.4)
				{
					return 2;
				}
				else if (test < 0.8)
				{
					return 4;
				}
				else if (test < 0.95)
				{
					return 5;
				}
				else
				{
					return 7;
				}
			}
		}

		private PlayerRecord bestEORInRange(int StartPosition, int EndPosition, List<int> skip)
		{
			PlayerRecord bestPlayer = null;
			double bestEOR = 0;
			LocalMath lmath = new LocalMath(editorModel.FileVersion);

			foreach (TableRecordModel record in editorModel.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				PlayerRecord player = (PlayerRecord)record;

				if (player.TeamId == TeamId && player.PositionId >= StartPosition && player.PositionId <= EndPosition
					&& !skip.Contains(player.PlayerId) && player.YearsPro < 3 && (player.Overall + lmath.pointboost(player, CON, 35)) > bestEOR)
				{
					bestPlayer = player;
					bestEOR = player.Overall + lmath.pointboost(player, CON, 35);
				}
			}

			return bestPlayer;
		}

        public void ComputeWins(EditorModel model)
        {
            wins = 0;
            playoffExit = 0;

            foreach (TableRecordModel record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                ScheduleRecord rec = (ScheduleRecord)record;

                if (rec.HomeTeam.TeamId == TeamId || rec.AwayTeam.TeamId == TeamId)
                {
                    if (rec.WeekNumber < 17)
                    {
                        // If it's a regular season week, then add to their number of wins
                        if (rec.Winner() == TeamId)
                        {
                            wins++;
                        }
                    }
                    else if (rec.WeekNumber < 20 && rec.Loser() == TeamId)
                    {
                        // If it's a non-Superbowl playoff game, record the exit value
                        playoffExit = rec.WeekNumber - 16;
                    }
                    else if (rec.WeekNumber == 20)
                    {
                        if (rec.Loser() == TeamId)
                        {
                            playoffExit = 4;
                        }
                        else if (rec.Winner() == TeamId)
                        {
                            playoffExit = 5;
                        }
                    }
                }
            }
        }

        public void ComputeStrengthOfSchedule(EditorModel model)
        {
            strengthOfSchedule = 0;

            foreach (TableRecordModel record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                ScheduleRecord rec = (ScheduleRecord)record;

                if (rec.WeekNumber < 17)
                {

                    if (rec.HomeTeam.TeamId == TeamId)
                    {
                        strengthOfSchedule += rec.AwayTeam.Wins;
                    }
                    else if (rec.AwayTeam.TeamId == TeamId)
                    {
                        strengthOfSchedule += rec.HomeTeam.Wins;
                    }
                }
            }

            effectiveSOS = 1000 * playoffExit + strengthOfSchedule;
        }

        // MADDEN DRAFT EDIT

		public override string ToString()
		{
			return Name;
		}

		public string Name
		{
			get
			{
				return GetStringField(TEAM_NAME);
			}
			set
			{
				SetField(TEAM_NAME, value);
			}
		}

		public string LongName
		{
			get
			{
				return GetStringField(TEAM_LONG_NAME);
			}
			set
			{
				SetField(TEAM_LONG_NAME, value);
			}
		}

		public string ShortName
		{
			get
			{
				return GetStringField(TEAM_SHORT_NAME);
			}
			set
			{
				SetField(TEAM_SHORT_NAME, value);
			}
		}

		public string NickName
		{
			get
			{
				return GetStringField(TEAM_NICK_NAME);
			}
			set
			{
				SetField(TEAM_NICK_NAME, value);
			}
		}

		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}

		public int DivisionId
		{
			get
			{
				return GetIntField(DIVISION_ID);
			}
			set
			{
				SetField(DIVISION_ID, value);
			}
		}

		public int ConferenceId
		{
			get
			{
				return GetIntField(CONFERENCE_ID);
			}
			set
			{
				SetField(CONFERENCE_ID, value);
			}
		}

		public int LeagueId
		{
			get
			{
				return GetIntField(LEAGUE_ID);
			}
			set
			{
				SetField(LEAGUE_ID, value);
			}
		}

		public int CityId
		{
			get
			{
				return GetIntField(CITY_ID);
			}
			set
			{
				SetField(CITY_ID, value);
			}
		}

		public int Salary
		{
			get
			{
				return GetIntField(TEAM_SALARY);
			}
			set
			{
				SetField(TEAM_SALARY, value);
			}
		}

		public int Reputation
		{
			//Max size 1023
			get
			{
				return GetIntField(REPUTATION);
			}
			set
			{
				SetField(REPUTATION, value);
			}
		}

		public int QBRating
		{
			get
			{
				return GetIntField(QB_RATING);
			}
			set
			{
				SetField(QB_RATING, value);
			}
		}

		public int RBRating
		{
			get
			{
				return GetIntField(RB_RATING);
			}
			set
			{
				SetField(RB_RATING, value);
			}
		}

		public int OLRating
		{
			get
			{
				return GetIntField(OL_RATING);
			}
			set
			{
				SetField(OL_RATING, value);
			}
		}

		public int WRRating
		{
			get
			{
				return GetIntField(WR_RATING);
			}
			set
			{
				SetField(WR_RATING, value);
			}
		}

		public int DLRating
		{
			get
			{
				return GetIntField(DL_RATING);
			}
			set
			{
				SetField(DL_RATING, value);
			}
		}

		public int LBRating
		{
			get
			{
				return GetIntField(LB_RATING);
			}
			set
			{
				SetField(LB_RATING, value);
			}
		}

		public int DBRating
		{
			get
			{
				return GetIntField(DB_RATING);
			}
			set
			{
				SetField(DB_RATING, value);
			}
		}

		public int SpecialTeamsRating
		{
			get
			{
				return GetIntField(ST_RATING);
			}
			set
			{
				SetField(ST_RATING, value);
			}
		}

		public int OffensiveRating
		{
			get
			{
				return GetIntField(OFFENSIVE_RATING);
			}
			set
			{
				SetField(OFFENSIVE_RATING, value);
			}
		}

		public int DefensiveRating
		{
			get
			{
				return GetIntField(DEFENSIVE_RATING);
			}
			set
			{
				SetField(DEFENSIVE_RATING, value);
			}
		}

		public int OverallRating
		{
			get
			{
				return GetIntField(OVERALL_RATING);
			}
			set
			{
				SetField(OVERALL_RATING, value);
			}
		}

		public int OffensivePlaybook
		{
			get
			{
				return GetIntField(OFFENSIVE_PLAYBOOK);
			}
			set
			{
				SetField(OFFENSIVE_PLAYBOOK, value);
			}
		}

		public int DefensivePlaybook
		{
			get
			{
				return GetIntField(DEFENSIVE_PLAYBOOK);
			}
			set
			{
				SetField(DEFENSIVE_PLAYBOOK, value);
			}
		}

		public int ShoeColor
		{
			get
			{
				return GetIntField(TEAM_SHOE_COLOR);
			}
			set
			{
				SetField(TEAM_SHOE_COLOR, value);
			}
		}

		public System.Drawing.Color PrimaryColor
		{
			get
			{
				return System.Drawing.Color.FromArgb(GetIntField(PRIMARY_RED), GetIntField(PRIMARY_GREEN), GetIntField(PRIMARY_BLUE));
			}
			set
			{
				SetField(PRIMARY_RED, value.R);
				SetField(PRIMARY_GREEN, value.G);
				SetField(PRIMARY_BLUE, value.B);
			}
		}

		public System.Drawing.Color SecondaryColor
		{
			get
			{
				return System.Drawing.Color.FromArgb(GetIntField(SECONDARY_RED), GetIntField(SECONDARY_GREEN), GetIntField(SECONDARY_BLUE));
			}
			set
			{
				SetField(SECONDARY_RED, value.R);
				SetField(SECONDARY_GREEN, value.G);
				SetField(SECONDARY_BLUE, value.B);
			}
		}

		public int TeamRival1
		{
			get
			{
				return GetIntField(TEAM_RIVAL_1);
			}
			set
			{
				SetField(TEAM_RIVAL_1, value);
			}
		}

		public int TeamRival2
		{
			get
			{
				return GetIntField(TEAM_RIVAL_2);
			}
			set
			{
				SetField(TEAM_RIVAL_2, value);
			}
		}

		public int TeamRival3
		{
			get
			{
				return GetIntField(TEAM_RIVAL_3);
			}
			set
			{
				SetField(TEAM_RIVAL_3, value);
			}
		}

		public int TeamType
		{
			get
			{
				return GetIntField(TEAM_TYPE);
			}
			set
			{
				SetField(TEAM_TYPE, value);
			}
		}

		public int DivisionOrder
		{
			get
			{
				return GetIntField(DIVISION_ORDER);
			}
			set
			{
				SetField(DIVISION_ORDER, value);
			}
		}

		public int TeamOrder
		{
			get
			{
				return GetIntField(TEAM_ORDER);
			}
			set
			{
				SetField(TEAM_ORDER, value);
			}
		}

		public int TeamRosterOrder
		{
			get
			{
				return GetIntField(TEAM_ROSTER_ORDER);
			}
			set
			{
				SetField(TEAM_ROSTER_ORDER, value);
			}
		}
	}
}
