using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.DatEditor;
using MaddenEditor.Core.Record;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace MaddenEditor.Forms
{
    public partial class DATOptions : Form
    {
        private DATConfig _config;
        private EditorModel _model = null;        
        
        public bool isInitializing = true;
        public repository PlayerRepo;

        public DATConfig datconfig
        {
            get { return _config; }
            set { _config = value; }
        }
        public EditorModel model
        {
            get { return _model; }
            set { _model = value; }
        }
                
        public DATOptions(DATConfig config)
        {
            InitializeComponent();
            this.datconfig = config;
            
            PlayerRepo = new repository();

            Init();
            isInitializing = false;
            Faq();
            
        }
        
        public void Init()
        {
            isInitializing = true;
            
            DefaultPlayerPort_Checkbox.Checked = datconfig.AutoLoad_PlayerPort;
            PlayerPortLocation_Textbox.Text = datconfig.Madden08_PlayerPortFile;
            CurrentPlayerPort_Textbox.Text = datconfig.PlayerDAT.loadfile;
            if (CurrentPlayerPort_Textbox.Text == "")
            {
                PlayerMakeDefault_Button.Visible = false;
                SavePlayerDAT_Button.Visible = false;
                groupBox6.Visible = false;
            }

            else
            {
                PlayerMakeDefault_Button.Visible = true;
                SavePlayerDAT_Button.Visible = true;
                groupBox6.Visible = true;
            }

            DefaultCoachPort_Checkbox.Checked = datconfig.AutoLoad_CoachPort;
            CoachPortLocation_Textbox.Text = datconfig.Madden08_CoachPortFile;
            CurrentCoachPort_Textbox.Text = datconfig.CoachDAT.loadfile;
            if (CurrentCoachPort_Textbox.Text == "")
            {
                CoachMakeDefault_Button.Visible = false;
                SaveCoachDat_Button.Visible = false;
                groupBox5.Visible = false;
            }
            else
            {
                CoachMakeDefault_Button.Visible = true;
                SaveCoachDat_Button.Visible = true;
                groupBox5.Visible = true;
            }

            AskForPlayerSave_Checkbox.Checked = datconfig.AskPlayerSave;
            AskForCoachSave_Checkbox.Checked = datconfig.AskCoachSave;

            isInitializing = false;
        }

        public void InitPlayerRepo()
        {
            if (File.Exists(PlayerRepo.playerrepofile))
            {
                PlayerRepo.Read();
            }

            if (model != null)
            {
                foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    //  Skip New Player, nfc/afc probowl members
                    if (rec.FirstName == "New" || rec.TeamId == 1010 || rec.TeamId == 1011) 
                        continue;
                    
                    PlayerRepo.Add(rec);
                }
            }

            PlayerRepo.Write();
        }
                
        public void Faq()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.Portfaq.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }

        public void PortPackFaq()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.PackFaq.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }

        public void Defaultdat()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.DefaultDat.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }

        #region Form Functions

        private void DefaultPlayerPort_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (DefaultPlayerPort_Checkbox.Checked == true)
                {
                    if (PlayerPortLocation_Textbox.Text == "")
                    {
                        string filename = "";
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.RestoreDirectory = true;
                        dialog.Title = "Load Madden DAT";
                        dialog.InitialDirectory = @"%USERPROFILE%\My Documents\";
                        dialog.Filter = "Madden DAT File (*.DAT)|*.DAT|All Files (*.*)|*.*";
                        dialog.FilterIndex = 1;
                        dialog.Multiselect = false;

                        if (dialog.ShowDialog() == DialogResult.OK)
                            filename = dialog.FileName;

                        if (filename != "")
                        {
                            datconfig.Madden08_PlayerPortFile = filename;
                            datconfig.AutoLoad_PlayerPort = true;                            
                        }
                    }
                    else
                    {
                        if(model.FileVersion == MaddenFileVersion.Ver2008)
                            datconfig.Madden08_PlayerPortFile = PlayerPortLocation_Textbox.Text;
                        if (model.FileVersion == MaddenFileVersion.Ver2007)
                            datconfig.Madden07_PlayerPortFile = PlayerPortLocation_Textbox.Text;
                        if (model.FileVersion == MaddenFileVersion.Ver2006)
                            datconfig.Madden06_PlayerPortFile = PlayerPortLocation_Textbox.Text;
                        if (model.FileVersion == MaddenFileVersion.Ver2005)
                            datconfig.Madden05_PlayerPortFile = PlayerPortLocation_Textbox.Text;
                        if (model.FileVersion == MaddenFileVersion.Ver2004)
                            datconfig.Madden04_PlayerPortFile = PlayerPortLocation_Textbox.Text;

                        datconfig.AutoLoad_PlayerPort = true;
                    }
                }
                else
                {
                    datconfig.AutoLoad_PlayerPort = false;
                }

                datconfig.changed = true;
                if (!datconfig.PlayerDAT.isterf)
                {
                    if (model.FileVersion == MaddenFileVersion.Ver2008)
                        datconfig.PlayerDAT.loadfile = datconfig.Madden08_PlayerPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2007)
                        datconfig.PlayerDAT.loadfile = datconfig.Madden07_PlayerPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2006)
                        datconfig.PlayerDAT.loadfile = datconfig.Madden06_PlayerPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2005)
                        datconfig.PlayerDAT.loadfile = datconfig.Madden05_PlayerPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2004)
                        datconfig.PlayerDAT.loadfile = datconfig.Madden04_PlayerPortFile;

                    datconfig.PlayerDAT.Load();
                }
                Init();
            }
        }

        private void DefaultCoachPort_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (DefaultCoachPort_Checkbox.Checked == true)
                {
                    if (CoachPortLocation_Textbox.Text == "")
                    {
                        string filename = "";

                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.RestoreDirectory = true;
                        dialog.Title = "Load Madden DAT";
                        dialog.InitialDirectory = @"%USERPROFILE%\My Documents\";
                        dialog.Filter = "Madden DAT File (*.DAT)|*.DAT|All Files (*.*)|*.*";
                        dialog.FilterIndex = 1;
                        dialog.Multiselect = false;

                        if (dialog.ShowDialog() == DialogResult.OK)
                            filename = dialog.FileName;

                        if (filename != "")
                        {
                            datconfig.Madden08_CoachPortFile = filename;
                            datconfig.AutoLoad_CoachPort = true;
                        }
                    }
                    else
                    {
                        if (model.FileVersion == MaddenFileVersion.Ver2008)
                            datconfig.Madden08_CoachPortFile = CoachPortLocation_Textbox.Text;
                        if (model.FileVersion == MaddenFileVersion.Ver2007)
                            datconfig.Madden07_CoachPortFile = CoachPortLocation_Textbox.Text; 
                        if (model.FileVersion == MaddenFileVersion.Ver2006)
                            datconfig.Madden06_CoachPortFile = CoachPortLocation_Textbox.Text; 
                        if (model.FileVersion == MaddenFileVersion.Ver2005)
                            datconfig.Madden05_CoachPortFile = CoachPortLocation_Textbox.Text; 
                        if (model.FileVersion == MaddenFileVersion.Ver2004)
                            datconfig.Madden04_CoachPortFile = CoachPortLocation_Textbox.Text;

                        datconfig.AutoLoad_CoachPort = true;
                    }
                }
                else
                {
                    datconfig.AutoLoad_PlayerPort = false;
                }

                datconfig.changed = true;
                if (!datconfig.CoachDAT.isterf)
                {
                    if (model.FileVersion == MaddenFileVersion.Ver2008)
                        datconfig.CoachDAT.loadfile = datconfig.Madden08_CoachPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2007)
                        datconfig.CoachDAT.loadfile = datconfig.Madden07_CoachPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2006)
                        datconfig.CoachDAT.loadfile = datconfig.Madden06_CoachPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2005)
                        datconfig.CoachDAT.loadfile = datconfig.Madden05_CoachPortFile;
                    if (model.FileVersion == MaddenFileVersion.Ver2004)
                        datconfig.CoachDAT.loadfile = datconfig.Madden04_CoachPortFile;

                    datconfig.CoachDAT.Load();
                }
                Init();               

            }
        }
        

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._config.changed)
                this._config.Save();
        }

        private void ExportPlayerPack_Button_Click(object sender, EventArgs e)
        {
            int datsize = datconfig.PlayerDAT.ParentTerf.Data.GetSize(datconfig.PlayerDAT.ParentTerf);            

            if (!isInitializing)
            {
                string dirname = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Choose Player Portraits Save Folder";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    dirname = dialog.SelectedPath;

                if (dirname == "")
                    return;

                DATProgress.Visible = true;
                DATProgress.Minimum = 1;
                DATProgress.Maximum = model.TableModels[EditorModel.PLAYER_TABLE].RecordCount;
                DATProgress.Value = 1;
                DATProgress.Step = 1;
                DATComment.Text = "Exporting Player Portraits, this will take some time.";

                foreach (PlayerRecord rec in _model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    if (rec.FirstName == "New")
                        continue;
                    string filename = dirname + "\\" + rec.FirstName + rec.LastName;
                    if (ExportPlayerID_Checkbox.Checked)
                        filename += "_" + rec.PortraitId.ToString("00000") + "." + rec.PlayerId.ToString("00000") + ".BMP";
                    else filename += "_" + rec.NFLID.ToString("00000") + ".BMP";

                    Image image = datconfig.PlayerDAT.ParentTerf.Data.DataFiles[rec.PortraitId + 1].mmap_data.GetPortraitDisplay();

                    image.Save(filename, ImageFormat.Bmp);
                    DATProgress.PerformStep();
                }

                DATComment.Text = "Done Exporting Player Portraits.";
                DATProgress.Visible = false;
            }
        }

        private void ImportPlayerPortPack_Button_Click(object sender, EventArgs e)
        {
            if (!datconfig.PlayerDAT.isterf)
            {
                MessageBox.Show("No DAT loaded", "Invalid DAT", MessageBoxButtons.OK);
                return;
            }
            string dirname = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Choose Player Portraits Location";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                dirname = dialog.SelectedPath;

            if (dirname == "")
                return;

            List<string> playernames = new List<string>();
            List<string> lastnames = new List<string>();
            List<int> playerids = new List<int>();
            List<int> Portraitids = new List<int>();

            string[] files = Directory.GetFiles(dirname);
            List<string> filenames = new List<string>();
            Dictionary<int, string> name_errors = new Dictionary<int, string>();

            for (int c = 0; c < files.Count(); c++)
                filenames.Add(files[c].Remove(0, dirname.Count() + 1));

            char[] underscore = { '_' };
            char[] period = { '.' };


            for (int c = 0; c < filenames.Count; c++)
            {
                try
                {
                    string[] origwords = filenames[c].Split(underscore);                                      

                    playernames.Add(origwords[0]);

                    string[] subwords = origwords[origwords.Count() - 1].Split(period);

                    if (subwords.Count() == 3)
                    {
                        int portid = Convert.ToInt32(subwords[0]);
                        int playid = Convert.ToInt32(subwords[1]);
                        Portraitids.Add(portid);
                        playerids.Add(playid);
                    }
                    else
                    {
                        Portraitids.Add(Convert.ToInt32(subwords[0]));
                    }
                }
                catch (Exception)
                {
                    name_errors.Add(c, "Invalid Filename");
                    continue;
                }
            }

            int total = 0;
            foreach (int high in Portraitids)
                if (high > total)
                    total = high;
            datconfig.PlayerDAT.ParentTerf.Expand(total);

            for (int c = 0; c < filenames.Count; c++)
            {
                if (name_errors.ContainsKey(c))
                    continue;
                else
                {
                    datconfig.PlayerDAT.grfx = new CustomBitmap(dirname + "\\" + filenames[c], Color.FromArgb(255, 255, 255, 255));
                    datconfig.PlayerDAT.ParentTerf.Data.DataFiles[Portraitids[c] + 1].mmap_data.ImportGraphic(datconfig.PlayerDAT.grfx.fixed_dds);
                }
            }

            if (playerids.Count > 0)
            {
                for (int pos = 0; pos < playerids.Count; pos++)
                {
                    foreach (PlayerRecord rec in _model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        if (rec.NFLID == playerids[pos])
                            if (rec.PortraitId != Portraitids[pos])
                                rec.PortraitId = Portraitids[pos];
                    }
                }   
            }

            datconfig.PlayerDAT.changed = true;
        }

        private void SaveDAT_Button_Click(object sender, EventArgs e)
        {
            if (AskForPlayerSave_Checkbox.Checked)
            {
                datconfig.PlayerDAT.SaveFileName();
            }
            else
            {
                string renamed = datconfig.PlayerDAT.loadfile + ".BAK";
                if (File.Exists(renamed))
                    File.Delete(renamed);
                System.IO.File.Move(datconfig.PlayerDAT.loadfile, renamed);
                datconfig.PlayerDAT.savefile = datconfig.PlayerDAT.loadfile;
            }
            DATComment.Text = "Saving Player Portrait DAT";
            DATProgress.Visible = true;
            DATProgress.Minimum = 1;
            DATProgress.Maximum = datconfig.PlayerDAT.ParentTerf.files;
            DATProgress.Value = 1;
            DATProgress.Step = 1;
            datconfig.PlayerDAT.Write(DATProgress);

            DATProgress.Visible = false;
            DATComment.Text = "Player Portrait DAT Saved";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Faq();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PortPackFaq();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Defaultdat();
        }

        private void ImportCoachPortPack_Button_Click(object sender, EventArgs e)
        {
            if (!datconfig.CoachDAT.isterf)
            {
                MessageBox.Show("No DAT loaded", "Invalid DAT", MessageBoxButtons.OK);
                return;
            }
            string dirname = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Choose Coach Portraits Location";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                dirname = dialog.SelectedPath;

            if (dirname == "")
                return;

            List<string> coachnames = new List<string>();            
            List<int> coachids = new List<int>();
            List<int> Portraitids = new List<int>();

            string[] files = Directory.GetFiles(dirname);
            List<string> filenames = new List<string>();
            Dictionary<int, string> name_errors = new Dictionary<int, string>();

            for (int c = 0; c < files.Count(); c++)
                filenames.Add(files[c].Remove(0, dirname.Count() + 1));

            char[] underscore = { '_' };
            char[] period = { '.' };


            for (int c = 0; c < filenames.Count; c++)
            {
                try
                {
                    string[] origwords = filenames[c].Split(underscore);
                    coachnames.Add(origwords[0]);

                    string[] subwords = origwords[origwords.Count() - 1].Split(period);

                    if (subwords.Count() == 3)
                    {
                        int portid = Convert.ToInt32(subwords[0]);
                        int playid = Convert.ToInt32(subwords[1]);
                        Portraitids.Add(portid);
                        coachids.Add(playid);
                    }
                    else
                    {
                        Portraitids.Add(Convert.ToInt32(subwords[0]));
                    }
                }
                catch (Exception)
                {
                    name_errors.Add(c, "Invalid Filename");
                    continue;
                }
            }

            int total = 0;
            foreach (int high in Portraitids)
                if (high > total)
                    total = high;
            datconfig.CoachDAT.ParentTerf.Expand(total);

            for (int c = 0; c < filenames.Count; c++)
            {
                if (name_errors.ContainsKey(c))
                    continue;
                else
                {
                    datconfig.CoachDAT.grfx = new CustomBitmap(dirname + "\\" + filenames[c], Color.FromArgb(255, 255, 255, 255));
                    datconfig.CoachDAT.ParentTerf.Data.DataFiles[Portraitids[c] + 1].mmap_data.ImportGraphic(datconfig.PlayerDAT.grfx.fixed_dds);
                }
            }
            if (coachids.Count > 0)
            {
                for (int pos = 0; pos < coachids.Count; pos++)
                {
                    foreach (CoachRecord rec in _model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        if (rec.CoachId == coachids[pos])
                            if (rec.Coachpic != Portraitids[pos])
                                rec.Coachpic = Portraitids[pos];
                    }
                }
            }
            datconfig.CoachDAT.changed = true;
        }

        private void ExportCoachPortPack_Button_Click(object sender, EventArgs e)
        {
            int datsize = datconfig.CoachDAT.ParentTerf.Data.GetSize(datconfig.CoachDAT.ParentTerf);

            if (!isInitializing)
            {
                string dirname = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Choose Coach Portraits Save Folder";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    dirname = dialog.SelectedPath;

                if (dirname == "")
                    return;
                foreach (CoachRecord rec in _model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    string filename = dirname + "\\" + rec.Name;
                    if (ExportCoachID_Checkbox.Checked)
                        filename += "_" + rec.Coachpic.ToString("00000") + "." + rec.CoachId.ToString("00000") + ".BMP";
                    else filename += "_" + rec.Coachpic.ToString("00000") + ".BMP";

                    Image image = datconfig.CoachDAT.ParentTerf.Data.DataFiles[rec.Coachpic + 1].mmap_data.GetPortraitDisplay();

                    image.Save(filename, ImageFormat.Bmp);
                }
            }
        }

        private void PlayerRepoLoad_Button_Click(object sender, EventArgs e)
        {
            InitPlayerRepo();
        }
        
        private void LoadPlayerDAT_Button_Click(object sender, EventArgs e)
        {            
            datconfig.PlayerDAT.LoadFileName();            
            datconfig.PlayerDAT.Load();
            CurrentPlayerPort_Textbox.Text = datconfig.PlayerDAT.loadfile; 
            Init();
        }

        private void LoadCoachDAT_Button_Click(object sender, EventArgs e)
        {
            datconfig.CoachDAT.LoadFileName();
            datconfig.CoachDAT.Load();
            CurrentCoachPort_Textbox.Text = datconfig.CoachDAT.loadfile;
            Init();
        }

        private void AskForPlayerSave_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            datconfig.AskPlayerSave = AskForPlayerSave_Checkbox.Checked;
            datconfig.changed = true;
        }

        private void AskForCoachSave_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            datconfig.AskCoachSave = AskForCoachSave_Checkbox.Checked;
            datconfig.changed = true;
        }

        private void SaveCoachDat_Button_Click(object sender, EventArgs e)
        {
            if (AskForCoachSave_Checkbox.Checked)
            {
                datconfig.CoachDAT.SaveFileName();
            }
            else
            {
                string renamed = datconfig.CoachDAT.loadfile + ".BAK";
                if (File.Exists(renamed))
                    File.Delete(renamed);
                System.IO.File.Move(datconfig.CoachDAT.loadfile, renamed);
                datconfig.CoachDAT.savefile = datconfig.CoachDAT.loadfile;
            }

            DATComment.Text = "Saving Coach Portrait DAT";
            DATProgress.Visible = true;
            DATProgress.Minimum = 1;
            DATProgress.Maximum = datconfig.CoachDAT.ParentTerf.files;
            DATProgress.Value = 1;
            DATProgress.Step = 1;
            
            datconfig.CoachDAT.Write(DATProgress);

            DATProgress.Visible = false;
            DATComment.Text = "Coach Portrait DAT Saved.";
        }

        private void PlayerMakeDefault_Button_Click(object sender, EventArgs e)
        {
            if(model.FileVersion == MaddenFileVersion.Ver2008)
                datconfig.Madden08_PlayerPortFile = CurrentPlayerPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2007)
                datconfig.Madden07_PlayerPortFile = CurrentPlayerPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2006)
                datconfig.Madden06_PlayerPortFile = CurrentPlayerPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2005)
                datconfig.Madden05_PlayerPortFile = CurrentPlayerPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2004)
                datconfig.Madden04_PlayerPortFile = CurrentPlayerPort_Textbox.Text;

            datconfig.changed = true;
            Init();
        }

        private void CoachMakeDefault_Button_Click(object sender, EventArgs e)
        {
            if (model.FileVersion == MaddenFileVersion.Ver2008)
                datconfig.Madden08_CoachPortFile = CurrentCoachPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2007)
                datconfig.Madden07_CoachPortFile = CurrentCoachPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2006)
                datconfig.Madden06_CoachPortFile = CurrentCoachPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2005)
                datconfig.Madden05_CoachPortFile = CurrentCoachPort_Textbox.Text;
            if (model.FileVersion == MaddenFileVersion.Ver2004)
                datconfig.Madden04_CoachPortFile = CurrentCoachPort_Textbox.Text;

            datconfig.changed = true;
            Init();
        }

        #endregion

       
    }

    public class person
    {        
        public string first;
        public string last;
        public int college;

        public person()
        {
            first = "";
            last = "";
            college = 0;
        }
        public person(PlayerRecord record)
        {
            first = record.FirstName;
            last = record.LastName;
            college = record.CollegeId;
        }
    }

    public class repository
    {
        public string playerrepofile = Application.StartupPath + @"\PlayerRepo.AMP";
        public string playerdupesfile = Application.StartupPath + @"\PlayerDupes.AMP";

        public Dictionary<int, person> Repo;
        public List<person> duplicates;

        public repository()
        {
            Repo = new Dictionary<int, person>();
            duplicates = new List<person>();
        }
                
        public void Add(PlayerRecord record)
        {
            if (!Repo.ContainsKey(record.NFLID))
            {
                person person = new person();
                person.first = record.FirstName;
                person.last = record.LastName;
                person.college = record.CollegeId;

                Repo.Add(record.NFLID, person);
            }

            else
            {
                if (Repo[record.NFLID].first == record.FirstName)
                    if (Repo[record.NFLID].last == record.LastName && !record.LastName.Contains("#"))
                        if (Repo[record.NFLID].college == record.CollegeId)
                            return;
                        else if (Repo[record.NFLID].college == 264 || Repo[record.NFLID].college == 265)
                        {
                            if (record.CollegeId != 264 && record.CollegeId != 265)
                            {
                                Repo[record.NFLID].college = record.CollegeId;
                                return;
                            }
                        }
                        else if(record.CollegeId == 264 || record.CollegeId == 265)
                        {
                            if (Repo[record.NFLID].college != 264 && Repo[record.NFLID].college != 265)
                                return;
                        }
                        else
                        {
                            foreach (person check in duplicates)
                            {
                                if (check.first == record.FirstName)
                                    if (check.last == record.LastName)
                                        if (check.college == record.CollegeId)
                                            return;
                            }

                            duplicates.Add(new person(record));
                        }
            }
        }
        
        #region IO

        public void Read()
        {
            BinaryReader binreader = new BinaryReader(File.Open(playerrepofile, FileMode.Open));
            int count = binreader.ReadUInt16();
            for (int c = 0; c < count; c++)
            {
                int dbid = binreader.ReadUInt16();
                person e = new person();
                e.first = binreader.ReadString();
                e.last = binreader.ReadString();
                e.college = binreader.ReadUInt16();

                Repo.Add(dbid, e);
            }

            binreader.Close();

            if (File.Exists(playerdupesfile))
            {
                binreader = new BinaryReader(File.Open(playerdupesfile, FileMode.Open));
                count = binreader.ReadUInt16();
                for (int c = 0; c < count; c++)
                {
                    person e = new person();
                    e.first = binreader.ReadString();
                    e.last = binreader.ReadString();
                    e.college = binreader.ReadUInt16();

                    duplicates.Add(e);
                }
                binreader.Close();
            }
        }
         
        public void Write()
        {
            BinaryWriter binwriter = new BinaryWriter(File.Open(playerrepofile, FileMode.Create));

            binwriter.Write((UInt16)Repo.Count);
            foreach (KeyValuePair<int, person> entry in Repo)
            {
                binwriter.Write((UInt16)entry.Key);
                binwriter.Write(entry.Value.first);
                binwriter.Write(entry.Value.last);
                binwriter.Write((UInt16)entry.Value.college);
            }

            binwriter.Close();

            if (duplicates.Count > 0)
            {
                binwriter = new BinaryWriter(File.Open(playerdupesfile, FileMode.Create));
                binwriter.Write((UInt16)duplicates.Count);
                foreach (person entry in duplicates)
                {                    
                    binwriter.Write(entry.first);
                    binwriter.Write(entry.last);
                    binwriter.Write((UInt16)entry.college);
                }

                binwriter.Close();
            }

        }

        #endregion
    }

}


