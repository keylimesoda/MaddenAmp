/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Domain
{
	public class PlayerRecord : TableRecordModel
	{
		public const string FIRST_NAME = "PFNA";
		public const string LAST_NAME = "PLNA";
		public const string POSITION_ID = "PPOS";
		
		public const string TEAM_ID = "TGID";
		public const string PLAYER_ID = "PGID";
		public const string COLLEGE_ID = "PCOL";
		public const string AGE = "PAGE";
		public const string JERSEY_NUMBER = "PJEN";
		public const string YRS_PRO = "PYRP";
		public const string WEIGHT = "PWGT";
		public const string HEIGHT = "PHGT";
		public const string DOMINANT_HAND = "PHAN";

		public const string OVERALL = "POVR";
		public const string SPEED = "PSPD";
		public const string STRENGTH = "PSTR";
		public const string AWARENESS = "PAWR";
		public const string AGILITY = "PAGI";
		public const string ACCELERATION = "PACC";
		public const string CATCHING = "PCTH";
		public const string CARRYING = "PCAR";
		public const string JUMPING = "PJMP";
		public const string BREAK_TACKLE = "PBTK";
		public const string TACKLE = "PTAK";
		public const string THROW_POWER = "PTHP";
		public const string THROW_ACCURACY = "PTHA";
		public const string PASS_BLOCKING = "PPBK";
		public const string RUN_BLOCKING = "PRBK";
		public const string KICK_POWER = "PKPR";
		public const string KICK_ACCURACY = "PKAC";
		public const string KICK_RETURN = "PKRT";
		public const string STAMINA = "PSTA";
		public const string INJURY = "PINJ";
		public const string TOUGHNESS = "PTGH";
		public const string THROWING_STYLE = "PSTY";

		public const string MORALE = "PMOR";
		public const string IMPORTANCE = "PIMP";
		public const string XP_POINTS = "PSXP";
		public const string NFL_ICON = "PICN";
		public const string PRO_BOWL = "PFPB";
		public const string CONTRACT_LENGTH = "PCON";
		public const string CONTRACT_YRS_LEFT = "PCYL";
		public const string SIGNING_BONUS = "PSBO";
		public const string TOTAL_SALARY = "PTSA";

		public const string BODY_WEIGHT = "PMTS";
		public const string BODY_MUSCLE = "PUTS";
		public const string BODY_FAT = "PFTS";
		public const string EQP_SHOES = "PLSS";
		public const string EQP_PAD_HEIGHT = "PTSS";
		public const string EQP_PAD_WIDTH = "PWSS";
		public const string EQP_PAD_SHELF = "PCHS";
		public const string EQP_FLAK_JACKET = "PQTS";
		public const string ARMS_MUSCLE = "PMAS";
		public const string ARMS_FAT = "PFAS";
		public const string LEGS_THIGH_MUSCLE = "PMHS";
		public const string LEGS_THIGH_FAT = "PFHS";
		public const string LEGS_CALF_MUSCLE = "PMCS";
		public const string LEGS_CALF_FAT = "PFCS";
		public const string REAR_REAR_FAR = "PMGS";
		public const string REAR_SHAPE = "PQGS";
				
		public const string HAIR_COLOR = "PHCL";

		public const string HELMET_STYLE = "PHLM";
		public const string FACE_MASK = "PFMK";

		public PlayerRecord(int record, RosterModel rosterModel)
			: base(record, rosterModel)
		{

		}

		public string FirstName
		{
			get
			{
				return stringFields[FIRST_NAME];
			}
			set
			{
				SetFieldWithBackup(FIRST_NAME, value);
			}
		}

		public string LastName
		{
			get
			{
				return stringFields[LAST_NAME];
			}
			set
			{
				SetFieldWithBackup(LAST_NAME, value);
			}
		}

		public int PositionId
		{
			get
			{
				return intFields[POSITION_ID];
			}
			set
			{
				SetFieldWithBackup(POSITION_ID, value);
			}
		}

		public int TeamId
		{
			get
			{
				return intFields[TEAM_ID];
			}
			set
			{
				SetFieldWithBackup(TEAM_ID, value);
			}
		}

		public int PlayerId
		{
			get
			{
				return intFields[PLAYER_ID];
			}
			set
			{
				SetFieldWithBackup(PLAYER_ID, value);
			}
		}

		public int CollegeId
		{
			get
			{
				return intFields[COLLEGE_ID];
			}
			set
			{
				SetFieldWithBackup(COLLEGE_ID, value);
			}
		}

		public int Age
		{
			get
			{
				return intFields[AGE];
			}
			set
			{
				SetFieldWithBackup(AGE, value);
			}
		}

		public int YearsPro
		{
			get
			{
				return intFields[YRS_PRO];
			}
			set
			{
				SetFieldWithBackup(YRS_PRO, value);
			}
		}

		public int XPPoints
		{
			get
			{
				return intFields[XP_POINTS];
			}
			set
			{
				SetFieldWithBackup(XP_POINTS, value);
			}
		}

		public bool NFLIcon
		{
			get
			{
				return (intFields[NFL_ICON] == 1);
			}
			set
			{
				SetFieldWithBackup(NFL_ICON, Convert.ToInt32(value));
			}
		}

		public bool ProBowl
		{
			get
			{
				return (intFields[PRO_BOWL] == 1);
			}
			set
			{
				SetFieldWithBackup(PRO_BOWL, Convert.ToInt32(value));
			}
		}

		public bool DominantHand
		{
			get
			{
				return (intFields[DOMINANT_HAND] == 1);
			}
			set
			{
				SetFieldWithBackup(DOMINANT_HAND, Convert.ToInt32(value));
			}
		}

		public int JerseyNumber
		{
			get
			{
				return intFields[JERSEY_NUMBER];
			}
			set
			{
				SetFieldWithBackup(JERSEY_NUMBER, value);
			}
		}

		public int Overall
		{
			get
			{
				return intFields[OVERALL];
			}
			set
			{
				SetFieldWithBackup(OVERALL, value);
			}
		}

		public int Speed
		{
			get
			{
				return intFields[SPEED];
			}
			set
			{
				SetFieldWithBackup(SPEED, value);
			}
		}

		public int Strength
		{
			get
			{
				return intFields[STRENGTH];
			}
			set
			{
				SetFieldWithBackup(STRENGTH, value);
			}
		}

		public int Awareness
		{
			get
			{
				return intFields[AWARENESS];
			}
			set
			{
				SetFieldWithBackup(AWARENESS, value);
			}
		}

		public int Agility
		{
			get
			{
				return intFields[AGILITY];
			}
			set
			{
				SetFieldWithBackup(AGILITY, value);
			}
		}

		public int Acceleration
		{
			get
			{
				return intFields[ACCELERATION];
			}
			set
			{
				SetFieldWithBackup(ACCELERATION, value);
			}
		}

		public int Catching
		{
			get
			{
				return intFields[CATCHING];
			}
			set
			{
				SetFieldWithBackup(CATCHING, value);
			}
		}

		public int Carrying
		{
			get
			{
				return intFields[CARRYING];
			}
			set
			{
				SetFieldWithBackup(CARRYING, value);
			}
		}

		public int Jumping
		{
			get
			{
				return intFields[JUMPING];
			}
			set
			{
				SetFieldWithBackup(JUMPING, value);
			}
		}

		public int BreakTackle
		{
			get
			{
				return intFields[BREAK_TACKLE];
			}
			set
			{
				SetFieldWithBackup(BREAK_TACKLE, value);
			}
		}

		public int Tackle
		{
			get
			{
				return intFields[TACKLE];
			}
			set
			{
				SetFieldWithBackup(TACKLE, value);
			}
		}

		public int ThrowPower
		{
			get
			{
				return intFields[THROW_POWER];
			}
			set
			{
				SetFieldWithBackup(THROW_POWER, value);
			}
		}

		public int ThrowAccuracy
		{
			get
			{
				return intFields[THROW_ACCURACY];
			}
			set
			{
				SetFieldWithBackup(THROW_ACCURACY, value);
			}
		}

		public int PassBlocking
		{
			get
			{
				return intFields[PASS_BLOCKING];
			}
			set
			{
				SetFieldWithBackup(PASS_BLOCKING, value);
			}
		}

		public int RunBlocking
		{
			get
			{
				return intFields[RUN_BLOCKING];
			}
			set
			{
				SetFieldWithBackup(RUN_BLOCKING, value);
			}
		}

		public int KickPower
		{
			get
			{
				return intFields[KICK_POWER];
			}
			set
			{
				SetFieldWithBackup(KICK_POWER, value);
			}
		}

		public int KickAccuracy
		{
			get
			{
				return intFields[KICK_ACCURACY];
			}
			set
			{
				SetFieldWithBackup(KICK_ACCURACY, value);
			}
		}

		public int KickReturn
		{
			get
			{
				return intFields[KICK_RETURN];
			}
			set
			{
				SetFieldWithBackup(KICK_RETURN, value);
			}
		}

		public int Stamina
		{
			get
			{
				return intFields[STAMINA];
			}
			set
			{
				SetFieldWithBackup(STAMINA, value);
			}
		}

		public int Injury
		{
			get
			{
				return intFields[INJURY];
			}
			set
			{
				SetFieldWithBackup(INJURY, value);
			}
		}

		public int Toughness
		{
			get
			{
				return intFields[TOUGHNESS];
			}
			set
			{
				SetFieldWithBackup(TOUGHNESS, value);
			}
		}

		public int Morale
		{
			get
			{
				return intFields[MORALE];
			}
			set
			{
				SetFieldWithBackup(MORALE, value);
			}
		}

		public int Importance
		{
			get
			{
				return intFields[IMPORTANCE];
			}
			set
			{
				SetFieldWithBackup(IMPORTANCE, value);
			}
		}

		public int Weight
		{
			get
			{
				return intFields[WEIGHT];
			}
			set
			{
				SetFieldWithBackup(WEIGHT, value);
			}
		}

		public int Height
		{
			get
			{
				return intFields[HEIGHT];
			}
			set
			{
				SetFieldWithBackup(HEIGHT, value);
			}
		}

		public int BodyWeight
		{
			get
			{
				return (intFields[BODY_WEIGHT] < 100 ? intFields[BODY_WEIGHT] : 99);
			}
			set
			{
				SetFieldWithBackup(BODY_WEIGHT, value);
			}
		}

		public int BodyMuscle
		{
			get
			{
				return (intFields[BODY_MUSCLE] < 100 ? intFields[BODY_MUSCLE] : 99);
			}
			set
			{
				SetFieldWithBackup(BODY_MUSCLE, value);
			}
		}

		public int BodyFat
		{
			get
			{
				return (intFields[BODY_FAT] < 100 ? intFields[BODY_FAT] : 99);
			}
			set
			{
				SetFieldWithBackup(BODY_FAT, value);
			}
		}

		public int EquipmentShoes
		{
			get
			{
				return (intFields[EQP_SHOES] < 100 ? intFields[EQP_SHOES] : 99);
			}
			set
			{
				SetFieldWithBackup(EQP_SHOES, value);
			}
		}

		public int EquipmentPadHeight
		{
			get
			{
				return (intFields[EQP_PAD_HEIGHT] < 100 ? intFields[EQP_PAD_HEIGHT] : 99);
			}
			set
			{
				SetFieldWithBackup(EQP_PAD_HEIGHT, value);
			}
		}

		public int EquipmentPadWidth
		{
			get
			{
				return (intFields[EQP_PAD_WIDTH] < 100 ? intFields[EQP_PAD_WIDTH] : 99);
			}
			set
			{
				SetFieldWithBackup(EQP_PAD_WIDTH, value);
			}
		}

		public int EquipmentPadShelf
		{
			get
			{
				return (intFields[EQP_PAD_SHELF] < 100 ? intFields[EQP_PAD_SHELF] : 99);
			}
			set
			{
				SetFieldWithBackup(EQP_PAD_SHELF, value);
			}
		}

		public int EquipmentFlakJacket
		{
			get
			{
				return (intFields[EQP_FLAK_JACKET] < 100 ? intFields[EQP_FLAK_JACKET] : 99);
			}
			set
			{
				SetFieldWithBackup(EQP_FLAK_JACKET, value);
			}
		}

		public int ArmsMuscle
		{
			get
			{
				return (intFields[ARMS_MUSCLE] < 100 ? intFields[ARMS_MUSCLE] : 99);
			}
			set
			{
				SetFieldWithBackup(ARMS_MUSCLE, value);
			}
		}

		public int ArmsFat
		{
			get
			{
				return (intFields[ARMS_FAT] < 100 ? intFields[ARMS_FAT] : 99);
			}
			set
			{
				SetFieldWithBackup(ARMS_FAT, value);
			}
		}

		public int LegsThighMuscle
		{
			get
			{
				return (intFields[LEGS_THIGH_MUSCLE] < 100 ? intFields[LEGS_THIGH_MUSCLE] : 99);
			}
			set
			{
				SetFieldWithBackup(LEGS_THIGH_MUSCLE, value);
			}
		}

		public int LegsThighFat
		{
			get
			{
				return (intFields[LEGS_THIGH_FAT] < 100 ? intFields[LEGS_THIGH_FAT] : 99);
			}
			set
			{
				SetFieldWithBackup(LEGS_THIGH_FAT, value);
			}
		}

		public int LegsCalfMuscle
		{
			get
			{
				return (intFields[LEGS_CALF_MUSCLE] < 100 ? intFields[LEGS_CALF_MUSCLE] : 99);
			}
			set
			{
				SetFieldWithBackup(LEGS_CALF_MUSCLE, value);
			}
		}

		public int LegsCalfFat
		{
			get
			{
				return (intFields[LEGS_CALF_FAT] < 100 ? intFields[LEGS_CALF_FAT] : 99);
			}
			set
			{
				SetFieldWithBackup(LEGS_CALF_FAT, value);
			}
		}

		public int RearRearFat
		{
			get
			{
				return (intFields[REAR_REAR_FAR] < 100 ? intFields[REAR_REAR_FAR] : 99);
			}
			set
			{
				SetFieldWithBackup(REAR_REAR_FAR, value);
			}
		}

		public int RearShape
		{
			get
			{
				return (intFields[REAR_SHAPE] < 100 ? intFields[REAR_SHAPE] : 99);
			}
			set
			{
				SetFieldWithBackup(REAR_SHAPE, value);
			}
		}

		public int ContractLength
		{
			get
			{
				return intFields[CONTRACT_LENGTH];
			}
			set
			{
				SetFieldWithBackup(CONTRACT_LENGTH, value);
			}
		}

		public int ContractYearsLeft
		{
			get
			{
				return intFields[CONTRACT_YRS_LEFT];
			}
			set
			{
				SetFieldWithBackup(CONTRACT_YRS_LEFT, value);
			}
		}

		public int SigningBonus
		{
			get
			{
				return intFields[SIGNING_BONUS];
			}
			set
			{
				SetFieldWithBackup(SIGNING_BONUS, value);
			}
		}

		public int TotalSalary
		{
			get
			{
				return intFields[TOTAL_SALARY];
			}
			set
			{
				SetFieldWithBackup(TOTAL_SALARY, value);
			}
		}

		/*public int SkinType
		{
			get
			{
				return intFields[SKIN_COLOR];
			}
			set
			{
				SetFieldWithBackup(SKIN_COLOR, value);
			}
		}*/

		public int ThrowingStyle
		{
			get
			{
				return intFields[THROWING_STYLE];
			}
			set
			{
				SetFieldWithBackup(THROWING_STYLE, value);
			}
		}

		public int HairColor
		{
			get
			{
				return intFields[HAIR_COLOR];
			}
			set
			{
				SetFieldWithBackup(HAIR_COLOR, value);
			}
		}

		public int HelmetStyle
		{
			get
			{
				return intFields[HELMET_STYLE];
			}
			set
			{
				SetFieldWithBackup(HELMET_STYLE, value);
			}
		}

		public int FaceMask
		{
			get
			{
				return intFields[FACE_MASK];
			}
			set
			{
				SetFieldWithBackup(FACE_MASK, value);
			}
		}

		public void CalculateOverallRating()
		{
			double tempOverall = 0;

			switch (PositionId)
			{
				case (int)MaddenPositions.QB:
					tempOverall = (((double)ThrowPower - 50) / 10) * 4.9;
					tempOverall += (((double)ThrowAccuracy - 50) / 10) * 5.8;
					tempOverall += (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Awareness - 50) / 10) * 4.0;
					tempOverall += (((double)Speed - 50) / 10) * 2.0;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.HB:
					tempOverall = (((double)PassBlocking - 50) / 10) * 0.33;
					tempOverall += (((double)BreakTackle - 50) / 10) * 3.3;
					tempOverall += (((double)Carrying - 50) / 10) * 2.0;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall += (((double)Agility - 50) / 10) * 2.8;
					tempOverall += (((double)Awareness - 50) / 10) * 2.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.6;
					tempOverall += (((double)Speed - 50) / 10) * 3.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27,1);
					break;
				case (int)MaddenPositions.FB:
					tempOverall= (((double)PassBlocking - 50) / 10) * 1.0;
					tempOverall+= (((double)RunBlocking - 50) / 10) * 7.2;
					tempOverall+= (((double)BreakTackle - 50) / 10) * 1.8;
					tempOverall+= (((double)Carrying - 50) / 10) * 1.8;
					tempOverall+= (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall+= (((double)Agility - 50) / 10) * 1.0;
					tempOverall+= (((double)Awareness - 50) / 10) * 2.8;
					tempOverall+= (((double)Strength - 50) / 10) * 1.8;
					tempOverall+= (((double)Speed - 50) / 10) * 1.8;
					tempOverall+= (((double)Catching - 50) / 10) * 5.2;
					tempOverall= (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 39,1);
					break;
				case (int)MaddenPositions.WR:
					tempOverall = (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.3;
					tempOverall += (((double)Agility - 50) / 10) * 2.3;
					tempOverall += (((double)Awareness - 50) / 10) * 2.3;
					tempOverall += (((double)Strength - 50) / 10) * 0.8;
					tempOverall += (((double)Speed - 50) / 10) * 2.3;
					tempOverall += (((double)Catching - 50) / 10) * 4.75;
					tempOverall += (((double)Jumping - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.TE:
					tempOverall = (((double)Speed - 50) / 10) * 2.65;
					tempOverall += (((double)Strength - 50) / 10) * 2.65;
					tempOverall += (((double)Awareness - 50) / 10) * 2.65;
					tempOverall += (((double)Agility - 50) / 10) * 1.25;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.25;
					tempOverall += (((double)Catching - 50) / 10) * 5.4;
					tempOverall += (((double)BreakTackle - 50) / 10) * 1.2;
					tempOverall += (((double)PassBlocking - 50) / 10) * 1.2;
					tempOverall += (((double)RunBlocking - 50) / 10) * 5.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 35, 1);
					break;
				case (int)MaddenPositions.LT:
				case (int)MaddenPositions.RT:
					tempOverall = (((double)Speed - 50) / 10) * 0.8;
					tempOverall += (((double)Strength - 50) / 10) * 3.3;
					tempOverall += (((double)Awareness - 50) / 10) * 3.3;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 0.8;
					tempOverall += (((double)PassBlocking - 50) / 10) * 4.75;
					tempOverall += (((double)RunBlocking - 50) / 10) * 3.75;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.LG:
				case (int)MaddenPositions.RG:
				case (int)MaddenPositions.C:
					tempOverall = (((double)Speed - 50) / 10) * 1.7;
					tempOverall += (((double)Strength - 50) / 10) * 3.25;
					tempOverall += (((double)Awareness - 50) / 10) * 3.25;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)PassBlocking - 50) / 10) * 3.25;
					tempOverall += (((double)RunBlocking - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.LE:
				case (int)MaddenPositions.RE:
					tempOverall = (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.75;
					tempOverall += (((double)Awareness - 50) / 10) * 1.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.75;
					tempOverall += (((double)Acceleration - 50) / 10) * 3.8;
					tempOverall += (((double)Tackle - 50) / 10) * 5.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.DT:
					tempOverall = (((double)Speed - 50) / 10) * 1.8;
					tempOverall += (((double)Strength - 50) / 10) * 5.5;
					tempOverall += (((double)Awareness - 50) / 10) * 3.8;
					tempOverall += (((double)Agility - 50) / 10) * 1;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.8;
					tempOverall += (((double)Tackle - 50) / 10) * 4.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.LOLB:
				case (int)MaddenPositions.ROLB:
					tempOverall = (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 2.4;
					tempOverall += (((double)Awareness - 50) / 10) * 3.6;
					tempOverall += (((double)Agility - 50) / 10) * 2.4;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.3;
					tempOverall += (((double)Tackle - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.MLB:
					tempOverall = (((double)Speed - 50) / 10) * 0.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.4;
					tempOverall += (((double)Awareness - 50) / 10) * 5.2;
					tempOverall += (((double)Agility - 50) / 10) * 1.65;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.75;
					tempOverall += (((double)Tackle - 50) / 10) * 5.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
					break;
				case (int)MaddenPositions.CB:
					tempOverall = (((double)Speed - 50) / 10) * 3.85;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 3.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.55;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.35;
					tempOverall += (((double)Catching - 50) / 10) * 3;
					tempOverall += (((double)Jumping - 50) / 10) * 1.55;
					tempOverall += (((double)Tackle - 50) / 10) * 1.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.FS:
					tempOverall = (((double)Speed - 50) / 10) * 3.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 4.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.5;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.5;
					tempOverall += (((double)Catching - 50) / 10) * 3.0;
					tempOverall += (((double)Jumping - 50) / 10) * 1.5;
					tempOverall += (((double)Tackle - 50) / 10) * 2.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.SS:
					tempOverall = (((double)Speed - 50) / 10) * 3.2;
					tempOverall += (((double)Strength - 50) / 10) * 1.7;
					tempOverall += (((double)Awareness - 50) / 10) * 4.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.7;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)Catching - 50) / 10) * 3.2;
					tempOverall += (((double)Jumping - 50) / 10) * 0.9;
					tempOverall += (((double)Tackle - 50) / 10) * 3.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;


			}

			Overall = (tempOverall < 100 ? (int)tempOverall : 99);
		}
	}
}