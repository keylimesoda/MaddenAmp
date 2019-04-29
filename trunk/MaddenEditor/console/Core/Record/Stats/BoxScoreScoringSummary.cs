using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    //  BSCS

    public class BoxScoreScoringSummary : TableRecordModel
    {
        public const string GAME_QUARTER = "GQTR";
        public const string TIME_LEFT = "GTIM";
        public const string WEEK_NUMBER = "SEWN";
        public const string GAME_NUMBER = "SGNM";
        public const string DRIVE_YARDS = "SSCS";           //  50 - yards
        public const string TOP_SECS = "SSDE";
        public const string PLAYS = "SSDP";
        public const string WHICH_QB = "SSFI";
        public const string XTRA_PT = "SSPT";
        public const string PLAY_YARDAGE = "SSPY";
        public const string SCORER_ID = "SSTI";
        public const string SCORE_TYPE = "SSTY";
        public const string TEAM_ID = "TGID";

        public BoxScoreScoringSummary(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
        }

        public int Quarter
        {
            get { return GetIntField(GAME_QUARTER); }
            set { SetField(GAME_QUARTER, value); }
        }
        public int TimeLeft
        {
            get { return GetIntField(TIME_LEFT); }
            set { SetField(TIME_LEFT, value); }
        }
        public int WeekNumber
        {
            get { return GetIntField(WEEK_NUMBER); }
            set { SetField(WEEK_NUMBER, value); }
        }
        public int GameNumber
        {
            get { return GetIntField(GAME_NUMBER); }
            set { SetField(GAME_NUMBER, value); }
        }
        public int DriveYards
        {
            get { return GetIntField(DRIVE_YARDS + 50); }
            set { SetField(DRIVE_YARDS, 50 - value); }                
        }
        public int TimeOfPossession
        {
            get { return GetIntField(TOP_SECS); }
            set { SetField(TOP_SECS, value); }            
        }
        public int Plays
        {
            get { return GetIntField(PLAYS); }
            set { SetField(PLAYS, value); }
        }
        public int WhichQB
        {
            get { return GetIntField(WHICH_QB); }
            set { SetField(WHICH_QB, value); }
        }
        public int XtraPoint
        {
            get { return GetIntField(XTRA_PT); }
            set { SetField(XTRA_PT, value); }
        }
        public int PlayYardage
        {
            get { return GetIntField(PLAY_YARDAGE); }
            set { SetField(PLAY_YARDAGE, value); }
        }
        public int ScorerID
        {
            get { return GetIntField(SCORER_ID); }
            set { SetField(SCORER_ID, value); }
        }
        public int ScoreType
        {
            get { return GetIntField(SCORE_TYPE); }
            set { SetField(SCORE_TYPE, value); }
        }
        public int TeamId
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }

    }
}
