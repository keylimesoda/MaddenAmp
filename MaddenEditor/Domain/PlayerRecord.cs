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
 * http://gommo.homelinux.net             colin.goudie@gmail.com
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

		public const string MORALE = "PMOR";
		public const string IMPORTANCE = "PIMP";
		public const string XP_POINTS = "PSXP";
		public const string NFL_ICON = "PICN";
		public const string PRO_BOWL = "PFPB";
		public const string CONTRACT_LENGTH = "PCON";
		public const string CONTRACT_YRS_LEFT = "PCYL";
		public const string SIGNING_BONUS = "PSBO";

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
				return intFields[BODY_WEIGHT];
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
				return intFields[BODY_MUSCLE];
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
				return intFields[BODY_FAT];
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
				return intFields[EQP_SHOES];
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
				return intFields[EQP_PAD_HEIGHT];
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
				return intFields[EQP_PAD_WIDTH];
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
				return intFields[EQP_PAD_SHELF];
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
				return intFields[EQP_FLAK_JACKET];
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
				return intFields[ARMS_MUSCLE];
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
				return intFields[ARMS_FAT];
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
				return intFields[LEGS_THIGH_MUSCLE];
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
				return intFields[LEGS_THIGH_FAT];
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
				return intFields[LEGS_CALF_MUSCLE];
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
				return intFields[LEGS_CALF_FAT];
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
				return intFields[REAR_REAR_FAR];
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
				return intFields[REAR_SHAPE];
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
	}
}