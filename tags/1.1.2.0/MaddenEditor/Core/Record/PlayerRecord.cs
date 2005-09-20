/******************************************************************************
 * Gommo's Madden Editor
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
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Core.Record
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

		public const string TENDANCY = "PTEN";
		public const string MORALE = "PMOR";
		public const string IMPORTANCE = "PIMP";
		public const string PORTRAIT_ID = "PSXP";
		public const string NFL_ICON = "PICN";
		public const string PRO_BOWL = "PFPB";
		public const string CONTRACT_LENGTH = "PCON";
		public const string CONTRACT_YRS_LEFT = "PCYL";
		public const string SIGNING_BONUS = "PSBO";
		public const string TOTAL_SALARY = "PTSA";

		public const string BODY_OVERALL = "PSBS";
		public const string LEGS_THIGH_PADS = "PTPS";
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

		//public const string SKIN_COLOR = "PCPH";
		//public const string FACE_SHAPE = "PFGE";
		public const string FACE_ID = "PFEx";
		public const string HAIR_COLOR = "PHCL";
		public const string HAIR_STYLE = "PHED";
		public const string EYE_PAINT = "PEYE";
		public const string NECK_ROLL = "PNEK";
		public const string VISOR = "PVIS";
		public const string MOUTHPIECE = "PMPC";
		public const string LEFT_HAND_A = "PLHA";
		public const string LEFT_HAND_B = "TLHA";
		public const string RIGHT_HAND_A = "PRHA";
		public const string RIGHT_HAND_B = "TRHA";
		public const string LEFT_ANKLE = "PLSH";
		public const string RIGHT_ANKLE = "PRSH";
		public const string LEFT_KNEE = "PLTH";
		public const string RIGHT_KNEE = "PRTH";
		public const string LEFT_ELBOW_A = "PLEL";
		public const string LEFT_ELBOW_B = "TLEL";
		public const string RIGHT_ELBOW_A = "PREL";
		public const string RIGHT_ELBOW_B = "TREL";
		public const string SLEEVES_A = "PGSL";
		public const string SLEEVES_B = "PTSL";
		public const string LEFT_WRIST_A = "PLWR";
		public const string LEFT_WRIST_B = "TLWR";
		public const string RIGHT_WRIST_A = "PRWR";
		public const string RIGHT_WRIST_B = "TRWR";
		public const string NASAL_STRIP = "PBRE";
		public const string LEFT_TATTOO = "PTAL";
		public const string RIGHT_TATTOO = "PTAR";

		public const string HELMET_STYLE = "PHLM";
		public const string FACE_MASK = "PFMK";

		//Salary constants
		public const string SALARY_YEAR_0 = "PSA0";
		public const string SIGNING_BONUS_YEAR_0 = "PSB0";
		public const string SALARY_YEAR_1 = "PSA1";
		public const string SIGNING_BONUS_YEAR_1 = "PSB1";
		public const string SALARY_YEAR_2 = "PSA2";
		public const string SIGNING_BONUS_YEAR_2 = "PSB2";
		public const string SALARY_YEAR_3 = "PSA3";
		public const string SIGNING_BONUS_YEAR_3 = "PSB3";
		public const string SALARY_YEAR_4 = "PSA4";
		public const string SIGNING_BONUS_YEAR_4 = "PSB4";
		public const string SALARY_YEAR_5 = "PSA5";
		public const string SIGNING_BONUS_YEAR_5 = "PSB5";
		public const string SALARY_YEAR_6 = "PSA6";
		public const string SIGNING_BONUS_YEAR_6 = "PSB6";

		public const string DRAFT_ROUND_INDEX = "PDPI";
		public const string DRAFT_ROUND = "PDRO";

		private bool calculatedCapHit = false;
		private int capHit = 0;
		private int capHitDifference = 0;
		double[] estYearlySalary = new double[7];
		double[] estSigningBonusArray = new double[7];

		public PlayerRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public override string ToString()
		{
			return FirstName + " " + LastName + " (" + Enum.GetNames(typeof(MaddenPositions))[PositionId].ToString() + ")";
		}

		public string FirstName
		{
			get
			{
				//The first time we access this record we should calculate this players cap hit
				if (!calculatedCapHit)
				{
					CalculateCapHit(false);
				}
				return GetStringField(FIRST_NAME);
			}
			set
			{
				SetField(FIRST_NAME, value);
			}
		}

		public string LastName
		{
			get
			{
				return GetStringField(LAST_NAME);
			}
			set
			{
				SetField(LAST_NAME, value);
			}
		}

		public int PositionId
		{
			get
			{
				return GetIntField(POSITION_ID);
			}
			set
			{
				SetField(POSITION_ID, value);
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

		public int PlayerId
		{
			get
			{
				return GetIntField(PLAYER_ID);
			}
			set
			{
				SetField(PLAYER_ID, value);
			}
		}

		public int CollegeId
		{
			get
			{
				return GetIntField(COLLEGE_ID);
			}
			set
			{
				SetField(COLLEGE_ID, value);
			}
		}

		public int Age
		{
			get
			{
				return GetIntField(AGE);
			}
			set
			{
				SetField(AGE, value);
			}
		}

		public int YearsPro
		{
			get
			{
				return GetIntField(YRS_PRO);
			}
			set
			{
				SetField(YRS_PRO, value);
			}
		}

		public int PortraitId
		{
			get
			{
				return GetIntField(PORTRAIT_ID);
			}
			set
			{
				SetField(PORTRAIT_ID, value);
			}
		}

		public bool NFLIcon
		{
			get
			{
				return (GetIntField(NFL_ICON) == 1);
			}
			set
			{
				SetField(NFL_ICON, Convert.ToInt32(value));
			}
		}

		public bool ProBowl
		{
			get
			{
				return (GetIntField(PRO_BOWL) == 1);
			}
			set
			{
				SetField(PRO_BOWL, Convert.ToInt32(value));
			}
		}

		public bool DominantHand
		{
			get
			{
				return (GetIntField(DOMINANT_HAND) == 1);
			}
			set
			{
				SetField(DOMINANT_HAND, Convert.ToInt32(value));
			}
		}

		public int JerseyNumber
		{
			get
			{
				return GetIntField(JERSEY_NUMBER);
			}
			set
			{
				SetField(JERSEY_NUMBER, value);
			}
		}

		public int Overall
		{
			get
			{
				return GetIntField(OVERALL);
			}
			set
			{
				SetField(OVERALL, value);
			}
		}

		public int Speed
		{
			get
			{
				return GetIntField(SPEED);
			}
			set
			{
				SetField(SPEED, value);
			}
		}

		public int Strength
		{
			get
			{
				return GetIntField(STRENGTH);
			}
			set
			{
				SetField(STRENGTH, value);
			}
		}

		public int Awareness
		{
			get
			{
				return GetIntField(AWARENESS);
			}
			set
			{
				SetField(AWARENESS, value);
			}
		}

		public int Agility
		{
			get
			{
				return GetIntField(AGILITY);
			}
			set
			{
				SetField(AGILITY, value);
			}
		}

		public int Acceleration
		{
			get
			{
				return GetIntField(ACCELERATION);
			}
			set
			{
				SetField(ACCELERATION, value);
			}
		}

		public int Catching
		{
			get
			{
				return GetIntField(CATCHING);
			}
			set
			{
				SetField(CATCHING, value);
			}
		}

		public int Carrying
		{
			get
			{
				return GetIntField(CARRYING);
			}
			set
			{
				SetField(CARRYING, value);
			}
		}

		public int Jumping
		{
			get
			{
				return GetIntField(JUMPING);
			}
			set
			{
				SetField(JUMPING, value);
			}
		}

		public int BreakTackle
		{
			get
			{
				return GetIntField(BREAK_TACKLE);
			}
			set
			{
				SetField(BREAK_TACKLE, value);
			}
		}

		public int Tackle
		{
			get
			{
				return GetIntField(TACKLE);
			}
			set
			{
				SetField(TACKLE, value);
			}
		}

		public int ThrowPower
		{
			get
			{
				return GetIntField(THROW_POWER);
			}
			set
			{
				SetField(THROW_POWER, value);
			}
		}

		public int ThrowAccuracy
		{
			get
			{
				return GetIntField(THROW_ACCURACY);
			}
			set
			{
				SetField(THROW_ACCURACY, value);
			}
		}

		public int PassBlocking
		{
			get
			{
				return GetIntField(PASS_BLOCKING);
			}
			set
			{
				SetField(PASS_BLOCKING, value);
			}
		}

		public int RunBlocking
		{
			get
			{
				return GetIntField(RUN_BLOCKING);
			}
			set
			{
				SetField(RUN_BLOCKING, value);
			}
		}

		public int KickPower
		{
			get
			{
				return GetIntField(KICK_POWER);
			}
			set
			{
				SetField(KICK_POWER, value);
			}
		}

		public int KickAccuracy
		{
			get
			{
				return GetIntField(KICK_ACCURACY);
			}
			set
			{
				SetField(KICK_ACCURACY, value);
			}
		}

		public int KickReturn
		{
			get
			{
				return GetIntField(KICK_RETURN);
			}
			set
			{
				SetField(KICK_RETURN, value);
			}
		}

		public int Stamina
		{
			get
			{
				return GetIntField(STAMINA);
			}
			set
			{
				SetField(STAMINA, value);
			}
		}

		public int Injury
		{
			get
			{
				return GetIntField(INJURY);
			}
			set
			{
				SetField(INJURY, value);
			}
		}

		public int Toughness
		{
			get
			{
				return GetIntField(TOUGHNESS);
			}
			set
			{
				SetField(TOUGHNESS, value);
			}
		}

		public int Morale
		{
			get
			{
				return GetIntField(MORALE);
			}
			set
			{
				SetField(MORALE, value);
			}
		}

		public int Importance
		{
			get
			{
				return GetIntField(IMPORTANCE);
			}
			set
			{
				SetField(IMPORTANCE, value);
			}
		}

		public int Weight
		{
			get
			{
				return GetIntField(WEIGHT);
			}
			set
			{
				SetField(WEIGHT, value);
			}
		}

		public int Height
		{
			get
			{
				return GetIntField(HEIGHT);
			}
			set
			{
				SetField(HEIGHT, value);
			}
		}

		public int BodyWeight
		{
			get
			{
				return (GetIntField(BODY_WEIGHT) < 100 ? GetIntField(BODY_WEIGHT) : 99);
			}
			set
			{
				SetField(BODY_WEIGHT, value);
			}
		}

		public int BodyMuscle
		{
			get
			{
				return (GetIntField(BODY_MUSCLE) < 100 ? GetIntField(BODY_MUSCLE) : 99);
			}
			set
			{
				SetField(BODY_MUSCLE, value);
			}
		}

		public int BodyFat
		{
			get
			{
				return (GetIntField(BODY_FAT) < 100 ? GetIntField(BODY_FAT) : 99);
			}
			set
			{
				SetField(BODY_FAT, value);
			}
		}

		public int EquipmentShoes
		{
			get
			{
				return (GetIntField(EQP_SHOES) < 100 ? GetIntField(EQP_SHOES) : 99);
			}
			set
			{
				SetField(EQP_SHOES, value);
			}
		}

		public int EquipmentPadHeight
		{
			get
			{
				return (GetIntField(EQP_PAD_HEIGHT) < 100 ? GetIntField(EQP_PAD_HEIGHT) : 99);
			}
			set
			{
				SetField(EQP_PAD_HEIGHT, value);
			}
		}

		public int EquipmentPadWidth
		{
			get
			{
				return (GetIntField(EQP_PAD_WIDTH) < 100 ? GetIntField(EQP_PAD_WIDTH) : 99);
			}
			set
			{
				SetField(EQP_PAD_WIDTH, value);
			}
		}

		public int EquipmentPadShelf
		{
			get
			{
				return (GetIntField(EQP_PAD_SHELF) < 100 ? GetIntField(EQP_PAD_SHELF) : 99);
			}
			set
			{
				SetField(EQP_PAD_SHELF, value);
			}
		}

		public int EquipmentFlakJacket
		{
			get
			{
				return (GetIntField(EQP_FLAK_JACKET) < 100 ? GetIntField(EQP_FLAK_JACKET) : 99);
			}
			set
			{
				SetField(EQP_FLAK_JACKET, value);
			}
		}

		public int ArmsMuscle
		{
			get
			{
				return (GetIntField(ARMS_MUSCLE) < 100 ? GetIntField(ARMS_MUSCLE) : 99);
			}
			set
			{
				SetField(ARMS_MUSCLE, value);
			}
		}

		public int ArmsFat
		{
			get
			{
				return (GetIntField(ARMS_FAT) < 100 ? GetIntField(ARMS_FAT) : 99);
			}
			set
			{
				SetField(ARMS_FAT, value);
			}
		}

		public int LegsThighMuscle
		{
			get
			{
				return (GetIntField(LEGS_THIGH_MUSCLE) < 100 ? GetIntField(LEGS_THIGH_MUSCLE) : 99);
			}
			set
			{
				SetField(LEGS_THIGH_MUSCLE, value);
			}
		}

		public int LegsThighFat
		{
			get
			{
				return (GetIntField(LEGS_THIGH_FAT) < 100 ? GetIntField(LEGS_THIGH_FAT) : 99);
			}
			set
			{
				SetField(LEGS_THIGH_FAT, value);
			}
		}

		public int LegsCalfMuscle
		{
			get
			{
				return (GetIntField(LEGS_CALF_MUSCLE) < 100 ? GetIntField(LEGS_CALF_MUSCLE) : 99);
			}
			set
			{
				SetField(LEGS_CALF_MUSCLE, value);
			}
		}

		public int LegsCalfFat
		{
			get
			{
				return (GetIntField(LEGS_CALF_FAT) < 100 ? GetIntField(LEGS_CALF_FAT) : 99);
			}
			set
			{
				SetField(LEGS_CALF_FAT, value);
			}
		}

		public int RearRearFat
		{
			get
			{
				return (GetIntField(REAR_REAR_FAR) < 100 ? GetIntField(REAR_REAR_FAR) : 99);
			}
			set
			{
				SetField(REAR_REAR_FAR, value);
			}
		}

		public int RearShape
		{
			get
			{
				return (GetIntField(REAR_SHAPE) < 100 ? GetIntField(REAR_SHAPE) : 99);
			}
			set
			{
				SetField(REAR_SHAPE, value);
			}
		}

		public int ContractLength
		{
			get
			{
				return GetIntField(CONTRACT_LENGTH);
			}
			set
			{
				SetField(CONTRACT_LENGTH, value);
				CalculateCapHit(true);
			}
		}

		public int ContractYearsLeft
		{
			get
			{
				return GetIntField(CONTRACT_YRS_LEFT);
			}
			set
			{
				SetField(CONTRACT_YRS_LEFT, value);
				CalculateCapHit(true);
			}
		}

		public int SigningBonus
		{
			get
			{
				return GetIntField(SIGNING_BONUS);
			}
			set
			{
				SetField(SIGNING_BONUS, value);
				CalculateCapHit(true);
			}
		}

		public int TotalSalary
		{
			get
			{
				return GetIntField(TOTAL_SALARY);
			}
			set
			{
				SetField(TOTAL_SALARY, value);
				CalculateCapHit(true);
			}
		}

		/*
		public int SkinType
		{
			get
			{
				return GetIntField(SKIN_COLOR);
			}
			set
			{
				SetField(SKIN_COLOR, value);
			}
		}

		public int FaceShape
		{
			get
			{
				return (GetIntField(FACE_SHAPE) < 21 ? GetIntField(FACE_SHAPE) : 20);
			}
			set
			{
				SetField(FACE_SHAPE, value);
			}
		}*/

		public int FaceId
		{
			get
			{
				return GetIntField(FACE_ID);
			}
			set
			{
				SetField(FACE_ID, value);
			}
		}

		public int EyePaint
		{
			get
			{
				return GetIntField(EYE_PAINT);
			}
			set
			{
				SetField(EYE_PAINT, value);
			}
		}

		public int ThrowingStyle
		{
			get
			{
				return GetIntField(THROWING_STYLE);
			}
			set
			{
				SetField(THROWING_STYLE, value);
			}
		}

		public int HairColor
		{
			get
			{
				return GetIntField(HAIR_COLOR);
			}
			set
			{
				SetField(HAIR_COLOR, value);
			}
		}

		public int HairStyle
		{
			get
			{
				return GetIntField(HAIR_STYLE);
			}
			set
			{
				SetField(HAIR_STYLE, value);
			}
		}

		public int HelmetStyle
		{
			get
			{
				return GetIntField(HELMET_STYLE);
			}
			set
			{
				SetField(HELMET_STYLE, value);
			}
		}

		public int FaceMask
		{
			get
			{
				return GetIntField(FACE_MASK);
			}
			set
			{
				SetField(FACE_MASK, value);
			}
		}

		public int NeckRoll
		{
			get
			{
				return (GetIntField(NECK_ROLL) < 3 ? GetIntField(NECK_ROLL) : 2);
			}
			set
			{
				SetField(NECK_ROLL, value);
			}
		}

		public int Visor
		{
			get
			{
				return GetIntField(VISOR);
			}
			set
			{
				SetField(VISOR, value);
			}
		}

		public int MouthPiece
		{
			get
			{
				return GetIntField(MOUTHPIECE);
			}
			set
			{
				SetField(MOUTHPIECE, value);
			}
		}

		public int LeftHand
		{
			get
			{
				return GetIntField(LEFT_HAND_A);
			}
			set
			{
				SetField(LEFT_HAND_A, value);
				SetField(LEFT_HAND_B, value);
			}
		}

		public int RightHand
		{
			get
			{
				return GetIntField(RIGHT_HAND_A);
			}
			set
			{
				SetField(RIGHT_HAND_A, value);
				SetField(RIGHT_HAND_B, value);
			}
		}

		public int LeftAnkle
		{
			get
			{
				return GetIntField(LEFT_ANKLE);
			}
			set
			{
				SetField(LEFT_ANKLE, value);
			}
		}

		public int RightAnkle
		{
			get
			{
				return GetIntField(RIGHT_ANKLE);
			}
			set
			{
				SetField(RIGHT_ANKLE, value);
			}
		}

		public int LeftKnee
		{
			get
			{
				return GetIntField(LEFT_KNEE);
			}
			set
			{
				SetField(LEFT_KNEE, value);
			}
		}

		public int RightKnee
		{
			get
			{
				return GetIntField(RIGHT_KNEE);
			}
			set
			{
				SetField(RIGHT_KNEE, value);
			}
		}

		public int LeftElbow
		{
			get
			{
				return GetIntField(LEFT_ELBOW_A);
			}
			set
			{
				SetField(LEFT_ELBOW_A, value);
				SetField(LEFT_ELBOW_B, value);
			}
		}

		public int RightElbow
		{
			get
			{
				return GetIntField(RIGHT_ELBOW_A);
			}
			set
			{
				SetField(RIGHT_ELBOW_A, value);
				SetField(RIGHT_ELBOW_B, value);
			}
		}

		public int Sleeves
		{
			get
			{
				return GetIntField(SLEEVES_A);
			}
			set
			{
				SetField(SLEEVES_A, value);
				SetField(SLEEVES_B, value);
			}
		}

		public int LeftWrist
		{
			get
			{
				return GetIntField(LEFT_WRIST_A);
			}
			set
			{
				SetField(LEFT_WRIST_A, value);
				SetField(LEFT_WRIST_B, value);
			}
		}

		public int RightWrist
		{
			get
			{
				return GetIntField(RIGHT_WRIST_A);
			}
			set
			{
				SetField(RIGHT_WRIST_A, value);
				SetField(RIGHT_WRIST_B, value);
			}
		}

		public int NasalStrip
		{
			get
			{
				return GetIntField(NASAL_STRIP);
			}
			set
			{
				SetField(NASAL_STRIP, value);
			}
		}

		public int LeftTattoo
		{
			get
			{
				return GetIntField(LEFT_TATTOO);
			}
			set
			{
				SetField(LEFT_TATTOO, value);
			}
		}

		public int RightTattoo
		{
			get
			{
				return GetIntField(RIGHT_TATTOO);
			}
			set
			{
				SetField(RIGHT_TATTOO, value);
			}
		}

		public int BodyOverall
		{
			get
			{
				return 99 - (GetIntField(BODY_OVERALL) > 99 ? 99 : GetIntField(BODY_OVERALL));
			}
			set
			{
				SetField(BODY_OVERALL, 99 - value);
			}
		}

		public int LegsThighPads
		{
			get
			{
				return 99 - (GetIntField(LEGS_THIGH_PADS) > 99 ? 99 : GetIntField(LEGS_THIGH_PADS));
			}
			set
			{
				SetField(LEGS_THIGH_PADS, 99 - value);
			}
		}

		public int Tendancy
		{
			get
			{
				return GetIntField(TENDANCY);
			}
			set
			{
				SetField(TENDANCY, value);
			}
		}

		public int DraftRoundIndex
		{
			get
			{
				return GetIntField(DRAFT_ROUND_INDEX);
			}
			set
			{
				SetField(DRAFT_ROUND_INDEX, value);
			}
		}

		public int DraftRound
		{
			get
			{
				return GetIntField(DRAFT_ROUND);
			}
			set
			{
				SetField(DRAFT_ROUND, value);
			}
		}

		public int CalculateOverallRating(int positionId)
		{
			double tempOverall = 0;

			switch (positionId)
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
				case (int)MaddenPositions.P:
					tempOverall = (double)(-183 + 0.218*Awareness + 1.5 * KickPower + 1.33 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;
				case (int)MaddenPositions.K:
					tempOverall = (double)(-177 + 0.218*Awareness + 1.28 * KickPower + 1.47 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;


			}

			if (tempOverall < 0)
			{
				tempOverall = 0;
			}
			if (tempOverall > 99)
			{
				tempOverall = 99;
			}

			return (int)tempOverall;
		}

		#region Salary Signing Bonus Functions

		public int GetSalaryAtYear(int year)
		{
			string key = "PSA" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)estYearlySalary[year];
			}
		}

		public int GetSigningBonusAtYear(int year)
		{
			string key = "PSB" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)estSigningBonusArray[year];
			}
		}

		public int CapHit
		{
			get
			{
				return capHit;
			}
		}

		public int CapHitDifference
		{
			get
			{
				return capHitDifference;
			}
		}

		private void CalculateCapHit(bool causeDirty)
		{
			double tempSigningBonus = SigningBonus;
			double tempSigningBonusPerYear = tempSigningBonus / ContractLength;
			double tempTotalSalary = TotalSalary;
			
			for (int i=0; i < 7; i++)
			{
				estYearlySalary[i] = 0;
				estSigningBonusArray[i] = 0;
			}

			if (ContractLength == 0)
			{
				return;
			}

			for (int i = ContractLength; i > 1; i--)
			{
				double specval = (tempSigningBonus / tempTotalSalary > 0.2 ? 1.3 : 1 + 1.5 * (tempSigningBonus / tempTotalSalary));
				double divisor = 1 + specval;
				
				for (int j = i - 1; j > 1; j--)
				{
					divisor += Math.Pow(specval, j);
				}

				double tempVal = (double)((tempTotalSalary - tempSigningBonus) / divisor);
				//Console.WriteLine("Value is " + tempVal);

				estYearlySalary[ContractLength-i] = tempVal;

				tempSigningBonus -= tempSigningBonusPerYear;
				tempTotalSalary -= (tempVal + tempSigningBonusPerYear);
			}
			//Calculate last year of contract
			//Round other years first
			estYearlySalary[ContractLength - 1] = TotalSalary - SigningBonus;
			estSigningBonusArray[ContractLength - 1] = SigningBonus;
			for (int i = 0; i < ContractLength - 1; i++)
			{
				estYearlySalary[i] = Math.Round(estYearlySalary[i]);
				estSigningBonusArray[i] = Math.Round(tempSigningBonusPerYear);

				estYearlySalary[ContractLength - 1] = estYearlySalary[ContractLength - 1] - estYearlySalary[i];
				estSigningBonusArray[ContractLength - 1] = estSigningBonusArray[ContractLength - 1] - estSigningBonusArray[i];
				//Console.WriteLine("Rounded = " + tempYearlySalary[i] + " SB: " + tempSigningBonusArray[i]);
			}
			//Console.WriteLine("Rounded = " + tempYearlySalary[ContractLength - 1] + " SB: " + tempSigningBonusArray[ContractLength - 1]);

			if (!calculatedCapHit)
			{
				capHit = (int)(estYearlySalary[ContractLength - ContractYearsLeft] + estSigningBonusArray[ContractLength - ContractYearsLeft]);
				calculatedCapHit = true;
			}
			else
			{
				int tempCapHit = (int)(estYearlySalary[ContractLength - ContractYearsLeft] + estSigningBonusArray[ContractLength - ContractYearsLeft]);
				capHitDifference = tempCapHit - capHit;
				capHit = tempCapHit;
			}
			
			Console.WriteLine("Cap hit = " + capHit);

			if (ContainsField(SALARY_YEAR_0))
			{
				//We are a franchise file so save back our yearly stuff
				for (int i = 0; i < 7; i++)
				{
					string key = "PSA" + i;
					SetField(key, (int)estYearlySalary[i], causeDirty);
					key = "PSB" + i;
					SetField(key, (int)estSigningBonusArray[i], causeDirty);
				}
			}
		}

		#endregion

		public DataGridViewRow GetDataRow(int positionId)
		{
			DataGridViewRow viewRow = new DataGridViewRow();

			DataGridViewTextBoxCell posCell = new DataGridViewTextBoxCell();
			posCell.Value = Enum.GetNames(typeof(MaddenPositions))[PositionId];
			DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
			nameCell.Value = FirstName + " " + LastName;
			DataGridViewTextBoxCell ovrCell = new DataGridViewTextBoxCell();
			ovrCell.Value = CalculateOverallRating(positionId);
			DataGridViewTextBoxCell playerCell = new DataGridViewTextBoxCell();
			playerCell.Value = this;
			viewRow.Cells.Add(posCell);
			viewRow.Cells.Add(nameCell);
			viewRow.Cells.Add(ovrCell);
			viewRow.Cells.Add(playerCell);

			posCell.ReadOnly = true;
			nameCell.ReadOnly = true;
			ovrCell.ReadOnly = true;
			playerCell.ReadOnly = true;

			return viewRow;
		}

	}
}