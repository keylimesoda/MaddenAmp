/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Forms;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Forms
{
    public partial class ManagerDraftConfig : Form
    {
        #region Members
        #region Private
        private EditorModel _model = null;
        private EditorModel _streamed = null;
        private MGMT _manager = null;
        private DraftConfig _config = null;
        #endregion

        #region Public
        public EditorModel model
        {
            get { return _model; }
            set { _model = value; }
        }
        public EditorModel streamed
        {
            get { return _streamed; }
            set { _streamed = value; }
        }
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public DraftConfig config
        {
            get { return _config; }
            set { _config = value; }
        }

        bool isInitializing = false;
        #endregion
        #endregion

        #region Constructors
        public ManagerDraftConfig()
        {
            InitializeComponent();
            InitUI();
        }

        #endregion

        #region Methods
        #region Inits
        public void InitUI()
        {
            isInitializing = true;

            for (int p = 0; p < 21; p++)
            {
                string pos = Enum.GetName(typeof(MaddenPositions), p);
                PlayerPositionCombo.Items.Add(pos.ToString());
            }

            for (int c = 0; c < 3; c++)
            {
                string name = "Mod" + c + "_Base";

                ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                
                foreach (MaddenEditor.Core.Manager.Trait att in Enum.GetValues(typeof(MaddenEditor.Core.Manager.Trait)))
                    com.Items.Add(att.ToString());
                com.SelectedIndex = 32;
            }

            for (int m = 0; m < 3; m++)
            {
                string newname = "Mod" + m + "_Func";
                for (int f = 0; f < 5; f++)
                {
                    string name = newname + f;
                    ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                    foreach (MaddenEditor.Core.Manager.Operator op in Enum.GetValues(typeof(MaddenEditor.Core.Manager.Operator)))
                        com.Items.Add(op.ToString());
                    com.SelectedIndex = 0;
                }
            }

            for (int t = 0; t < 21; t++)
            {
                string sn = "Skill" + t + "_Apply";
                string pn = "Pot" + t + "_Apply";
                ComboBox skillcom = this.Controls.Find(sn, true).First() as ComboBox;
                ComboBox potcom = this.Controls.Find(pn, true).First() as ComboBox;
                foreach (TendApply apply in Enum.GetValues(typeof(TendApply)))
                {
                    skillcom.Items.Add(apply.ToString());
                    potcom.Items.Add(apply.ToString());
                }
                skillcom.SelectedIndex = 0;
                potcom.SelectedIndex = 0;
            }

            Modifier_Type.Items.Add("Skill");
            Modifier_Type.Items.Add("Potential");
            Modifier_Type.SelectedIndex = 0;

            isInitializing = false;
        }

        public void DisplayPosition(MaddenPositions position)
        {
            if (position == MaddenPositions.QB)
            {
                Tendency0_Textbox.Text = "Pocket";
                Tendency1_Textbox.Text = "Scrambler";
                Skill_Ten0_Label.Text = "POC";
                Skill_Ten1_Label.Text = "SCR";
                Pot_Ten0_label.Text = "POC";
                Pot_Ten1_label.Text = "SCR";
            }
            else if (position == MaddenPositions.HB)
            {
                Tendency0_Textbox.Text = "Power";
                Tendency1_Textbox.Text = "Speed";
                Skill_Ten0_Label.Text = "POW";
                Skill_Ten1_Label.Text = "SPD";
                Pot_Ten0_label.Text = "POW";
                Pot_Ten1_label.Text = "SPD";
            }
            else if (position == MaddenPositions.FB || position==MaddenPositions.TE)
            {
                Tendency0_Textbox.Text = "Blocking";
                Tendency1_Textbox.Text = "Receiving";
                Skill_Ten0_Label.Text = "BLK";
                Skill_Ten1_Label.Text = "REC";
                Pot_Ten0_label.Text = "BLK";
                Pot_Ten1_label.Text = "REC";
            }
            else if (position == MaddenPositions.WR)
            {
                Tendency0_Textbox.Text = "Possession";
                Tendency1_Textbox.Text = "Speed";
                Skill_Ten0_Label.Text = "POS";
                Skill_Ten1_Label.Text = "SPD";
                Pot_Ten0_label.Text = "POS";
                Pot_Ten1_label.Text = "SPD";
            }
            else if (position == MaddenPositions.LT || position == MaddenPositions.LG || position == MaddenPositions.C || position == MaddenPositions.RG
                || position == MaddenPositions.RT)
            {
                Tendency0_Textbox.Text = "Run Block";
                Tendency1_Textbox.Text = "Pass Block";
                Skill_Ten0_Label.Text = "RUN";
                Skill_Ten1_Label.Text = "PAS";
                Pot_Ten0_label.Text = "RUN";
                Pot_Ten1_label.Text = "PAS";
            }





            Tendency0_Perc.Value = config.Tendencies[(int)position].tend0;
            Tendency1_Perc.Value = config.Tendencies[(int)position].tend1;
            Balanced_Perc.Value = 100 - (config.Tendencies[(int)position].tend0 + config.Tendencies[(int)position].tend1);

            for (int s = 0; s < 21; s++)
            {
                string skill = "Skill" + s;
                string pot = "Pot" + s;
                string skillapply = skill + "_Apply";
                string potapply = pot + "_Apply";
                Trait trait = (Trait)Enum.ToObject(typeof(Trait), (byte)s);
                NumericUpDown skillnum = this.Controls.Find(skill + "_Low", true).First() as NumericUpDown;
                NumericUpDown potnum = this.Controls.Find(pot + "_Low", true).First() as NumericUpDown;
                CheckBox skillmod = this.Controls.Find(skill + "_ModOn", true).First() as CheckBox;
                ComboBox skillapp = this.Controls.Find(skillapply, true).First() as ComboBox;
                ComboBox potapp = this.Controls.Find(potapply, true).First() as ComboBox;
                skillapp.SelectedIndex = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].apply;
                potapp.SelectedIndex = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].apply;

                skillmod.Checked = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].mods_active;                
                skillnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].Low;
                skillnum = this.Controls.Find(skill + "_High", true).First() as NumericUpDown;
                skillnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].High;
                potnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].Low;
                potnum = this.Controls.Find(pot + "_High", true).First() as NumericUpDown;
                potnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].High;

                for (int t = 0; t < 3; t++)
                {
                    string skilltend = skill + "_Tend" + t;
                    string pottend = pot + "_Tend" + t;
                    skillnum = this.Controls.Find(skilltend, true).First() as NumericUpDown;
                    potnum = this.Controls.Find(pottend, true).First() as NumericUpDown;
                    if (t == 0)
                    {
                        skillnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].Tend0;
                        potnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].Tend0;
                    }
                    else if (t == 1)
                    {
                        skillnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].Tend1;
                        potnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].Tend1;
                    }
                    else
                    {
                        skillnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Skill].Tend2;
                        potnum.Value = config.Ratings[(int)position][(int)trait][(int)RatingType.Potential].Tend2;
                    }
                }
            }

            DisplayModifier(position, (Trait)Enum.ToObject(typeof(Trait), 0), 0);
        }

        public void DisplayModifier(MaddenPositions position, Trait trait, int modtype)
        {
            //clear everything out first
            Modifier_Textbox.Text = "";
            for (int c = 0; c < 3; c++)
            {
                string name = "Mod" + c;
                ComboBox com = this.Controls.Find(name + "_Base", true).First() as ComboBox;
                com.SelectedIndex = 32;

                for (int m = 0; m < 5; m++)
                {
                    string method = name + "_Func" + m;
                    string methodvalue = name + "_Value" + m;
                    ComboBox methbox = this.Controls.Find(method, true).First() as ComboBox;
                    methbox.SelectedIndex = 0;
                    NumericUpDown methval = this.Controls.Find(methodvalue, true).First() as NumericUpDown;
                    methval.Value = 0;
                }
            }

            Modifier_Textbox.Text = trait.ToString();

            for (int c = 0; c < config.Ratings[(int)position][(int)trait][modtype].modifiers.Count; c++)
            {
                Modifier_Textbox.Text = trait.ToString();
                Modifier_Type.SelectedIndex = modtype;
                string name = "Mod" + c;

                for (int m = 0; m < config.Ratings[(int)position][(int)trait][modtype].modifiers[c].functions.Count; m++)
                {
                    if (m == 0)
                    {
                        ComboBox com = this.Controls.Find(name + "_Base", true).First() as ComboBox;
                        com.SelectedIndex = config.Ratings[(int)position][(int)trait][modtype].modifiers[c].trait;
                    }

                    string method = name + "_Func" + m;
                    string methodvalue = name + "_Value" + m;
                    ComboBox methbox = this.Controls.Find(method, true).First() as ComboBox;
                    NumericUpDown methnum = this.Controls.Find(methodvalue, true).First() as NumericUpDown;
                    methbox.SelectedIndex = config.Ratings[(int)position][(int)trait][modtype].modifiers[c].functions[m].operation;
                    methnum.Value = (decimal)config.Ratings[(int)position][(int)trait][modtype].modifiers[c].functions[m].op_value;
                }
            }
        }
            
        

        public void InitModifier(int skill, int modtype)
        {
            DisplayModifier((MaddenPositions)Enum.ToObject(typeof(MaddenPositions), (byte)PlayerPositionCombo.SelectedIndex), (Trait)Enum.ToObject(typeof(Trait), skill), modtype);
        }


        #endregion



        #endregion

        #region Form Functions

        private void PlayerPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayPosition((MaddenPositions)PlayerPositionCombo.SelectedIndex);
        }
        
        private void Modifier_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                Trait trait = (Trait)Enum.Parse(typeof(Trait), this.Modifier_Textbox.Text);
                InitModifier((int)trait, Modifier_Type.SelectedIndex);
            }
        }
        
        


        #region Skill Mod Buttons
        private void Skill0_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(0, 0);
        }
        private void Skill1_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(1, 0);
        }
        private void Skill2_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(2, 0);
        }
        private void Skill3_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(3, 0);
        }
        private void Skill4_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(4, 0);
        }
        private void Skill5_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(5, 0);
        }
        private void Skill6_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(6, 0);
        }
        private void Skill7_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(7, 0);
        }
        private void Skill8_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(8, 0);
        }
        private void Skill9_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(9, 0);
        }
        private void Skill10_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(10, 0);
        }
        private void Skill11_Button_Click(object sender, EventArgs e)
        {
            InitModifier(11, 0);
        }
        private void Skill12_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(12, 0);
        }
        private void Skill13_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(13, 0);
        }
        private void Skill14_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(14, 0);
        }
        private void Skill15_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(15, 0);
        }
        private void Skill16_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(16, 0);
        }
        private void Skill17_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(17, 0);
        }
        private void Skill18_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(18, 0);
        }
        private void Skill19_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(19, 0);
        }
        private void Skill20_Mod_Button_Click(object sender, EventArgs e)
        {
            InitModifier(20, 0);
        }
        #endregion

        #endregion


    }
}
