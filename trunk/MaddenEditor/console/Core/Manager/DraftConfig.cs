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
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Core.Manager
{
    #region Enums
    public enum Operator
    {
        None = 0,
        Add = 1,
        Subtract = 2,
        Multiply = 3,
        Divide = 4,
        Equal = 5,
        Less = 6,
        More = 7,
        NotEqual = 8,
        And = 9,
        Or = 10,
        Diff = 11,
        Next = 12
    }
    public enum RatingType
    {
        Skill = 0,
        Potential = 1,
        Both = 2
    }
    public enum TendBonus
    {
        Skill = 0,
        Potential = 1,
        Both = 2
    }
    public enum TendApply
    {
        Rand = 0,
        Thresh = 1,
        Final = 2,
        RandLow = 3,
        ThrLow = 4,
        RandHigh = 5,
        ThrHigh = 6,
        RSplit = 7,
        TSplit = 8
    }
    #endregion

    #region Config Sub Classes
    public class ModMethod
    {
        public int operation = 0;
        public double op_value = 0;
        
        public ModMethod()
        {
            operation = 0;
            op_value = 0;
        }
        public ModMethod(int a, int b)
        {
            operation = a;
            op_value = b;
        }
        public ModMethod(int a, double b)
        {
            operation = a; 
            op_value = b;
        }
        public ModMethod(Operator op, int b)
        {
            operation = (int)op;
            op_value = b;
        }
        public ModMethod(Operator op, double b)
        {
            operation = (int)op;
            op_value = b;
        }

    }
    
    //  These are processed AFTER the random generator is finished.
    public class Modifier
    {
        #region Members
        public int trait;
        public List<ModMethod> functions;
        #endregion

        public Modifier()
        {
            trait = 0;
            functions = new List<ModMethod>();
        }
        public Modifier(Trait trait, Operator op, double op_value)
        {
            functions = new List<ModMethod>();
            this.trait = (int)trait;
            functions.Add(new ModMethod(op, op_value));
        }

        public void AddNewFunction(Operator op, double op_value)
        {
            functions.Add(new ModMethod(op, op_value));
        }
        public void RemoveFunction(Operator op, double op_value)
        {
            for (int c = this.functions.Count; c > 0; c-- )
                if (this.functions[c].operation == (int)op && this.functions[c].op_value == op_value)
                    this.functions.Remove(this.functions[c]);
        }


        #region File IO
        public void Read(BinaryReader binreader)
        {
            this.trait = binreader.ReadByte();
            int total = binreader.ReadByte();
            this.functions = new List<ModMethod>();
            for (int c = 0; c < total; c++)            
                this.functions.Add(new ModMethod(binreader.ReadByte(), binreader.ReadDouble()));            
        }
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)this.trait);
            binwriter.Write((byte)this.functions.Count);
            foreach (ModMethod mod in this.functions)
            {
                binwriter.Write((byte)mod.operation);
                binwriter.Write(mod.op_value);
            }            
        }
        #endregion
    }
        
    public class Tendency
    {
        public int tend0 = 33;
        public int tend1 = 33;

        public Tendency()
        {
            this.tend0 = 33;
            this.tend1 = 33;
        }
        public Tendency(int ten0, int ten1)
        {
            this.tend0 = ten0;
            this.tend1 = ten1;            
        }
        
        public void Read(BinaryReader binreader)
        {
            tend0 = binreader.ReadByte();
            tend1 = binreader.ReadByte();
        }
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)tend0);
            binwriter.Write((byte)tend1);
        }
    }

    public class Rating
    {
        #region Members
        public int Low = 40;
        public int High = 99;
        public int Tend0 = 0;
        public int Tend1 = 0;
        public int Tend2 = 0;
        public int apply = 0;
        public bool mods_active = true;
        public List<Modifier> modifiers = new List<Modifier>();
        #endregion
        #region Constructors
        public Rating()
        {
            Low = 40;
            High = 99;
            Tend0 = 0;
            Tend1 = 0;
            Tend2 = 0;
            mods_active = true;
            modifiers = new List<Modifier>();
        }
        public Rating(int low, int high, int ten0, int ten1, int ten2)
        {
            this.Low = low;
            this.High = high;
            this.Tend0 = ten0;
            this.Tend1 = ten1;
            this.Tend2 = ten2;
            this.apply = 0;
            mods_active = true;
            modifiers = new List<Modifier>();
        }
        public Rating(int low, int high, int ten0, int ten1, int ten2, int app)
        {
            this.Low = low;
            this.High = high;
            this.Tend0 = ten0;
            this.Tend1 = ten1;
            this.Tend2 = ten2;
            this.apply = app;
            mods_active = true;
            modifiers = new List<Modifier>();
        }
        
        #endregion
        public int GetRandomInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public void AddNewModifier(Trait trait, Operator op, double op_value)
        {
            bool skillexists = false;

            foreach (Modifier mod in this.modifiers)
            {
                if (mod.trait == (int)trait)
                {
                    skillexists = true;
                    mod.functions.Add(new ModMethod(op, op_value));
                }
            }
            if (!skillexists)
                this.modifiers.Add(new Modifier(trait, op, op_value));
        }
        
        public int GetSkillBonus(Player player)
        {
            Player_Ratings pr = player.Original_Ratings;
            double bonus = 0;

            for (int c = 0; c < this.modifiers.Count; c++)
            {
                List<bool> changes = new List<bool>();
                bool check = false;
                Trait trait = (Trait)this.modifiers[c].trait;
                for (int f = 0; f < 4; f++)
                {
                    changes.Add(true);
                    if (f > 1)
                    {
                        changes[f] = changes[f - 1];
                        // use previous change result...if previous was AND OR check, set the change to true/false based on expression before
                        // if it was OR, then it doesnt matter, set it it true and let the current expression handle the change                        
                        if ((Operator)this.modifiers[c].functions[f - 1].op_value == Operator.And)
                            changes[f] = changes[f - 2];
                        else if ((Operator)this.modifiers[c].functions[f - 1].op_value == Operator.Or)
                            check = true;
                    }

                    if (changes[f] || check)
                    {
                        //  Expressions setting a value, if previous expression was OR and the expression was true before that
                        //  set the value anyway (check=true)
                        if ((Operator)this.modifiers[c].functions[f].operation == Operator.None)
                            continue;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Add)
                            bonus += this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Subtract)
                            bonus -= this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Multiply)
                            bonus = bonus * this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Divide)
                            bonus = bonus / this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Diff)
                            bonus += player.GetPlayerTrait(trait) - this.modifiers[c].functions[f].op_value;

                            // Expression testing a value   
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Less)
                            changes[f] = player.GetPlayerTrait(trait) < this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.More)
                            changes[f] = player.GetPlayerTrait(trait) > this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.Equal)
                            changes[f] = player.GetPlayerTrait(trait) == this.modifiers[c].functions[f].op_value;
                        else if ((Operator)this.modifiers[c].functions[f].operation == Operator.NotEqual)
                            changes[f] = player.GetPlayerTrait(trait) != this.modifiers[c].functions[f].op_value;
                    }
                }
            }

            return (int)bonus;
        }

        public int GetPlayerSkill(Player player)
        {
            int low = 1;
            int high = 1000001;
            if (this.apply == (int)TendApply.Thresh)
            {
                if (player.Info.TENDENCY == 0)
                {
                    low += (this.Low + this.Tend0) * 10000;
                    high += (this.High + this.Tend0) * 10000;
                }
                else if (player.Info.TENDENCY == 1)
                {
                    low += (this.Low + this.Tend1) * 10000;
                    high += (this.High + this.Tend1) * 10000;
                }
                else
                {
                    low += (this.Low + this.Tend2) * 10000;
                    high += (this.Low + this.Tend2) * 10000;
                }
            }
            else
            {
                low = 1;
                high = 1000001;
            }

            int rand = GetRandomInt(low, high);

            if (apply == (int)TendApply.Rand)
            {
                if (player.Info.TENDENCY == 0)
                    rand += this.Tend0 * 10000;
                else if (player.Info.TENDENCY == 1)
                    rand += this.Tend1 * 10000;
                else rand += this.Tend2 * 10000;
            }

            int val = rand / 10000;
            if (val < this.Low)
                val = this.Low;
            if (val > this.High)
                val = this.High;

            return val;
        }
    }

    #endregion
    
    public class SkillGen
    {
        #region Members
        public int skill_low = 40;
        public int skill_high = 99;
        public int skill_tend0_bonus = 0;
        public int skill_tend1_bonus = 0;
        public int skill_tend2_bonus = 0;
        public int skill_tend_type = 0;
        public int potent_low = 40;
        public int potent_high = 99;
        public int potent_tend0_bonus = 0;
        public int potent_tend1_bonus = 0;
        public int potent_tend2_bonus = 0;
        public int potent_tend_type = 1;
        public bool mod_active = true;
        public List<Modifier> skill_modifiers;
        public List<Modifier> pot_modifiers;
        #endregion

        public SkillGen()
        {
            skill_low = 40;
            skill_high = 99;
            skill_tend0_bonus = 0;
            skill_tend1_bonus = 0;
            skill_tend2_bonus = 0;
            skill_tend_type = 1;
            potent_low = 40;
            potent_high = 99;
            potent_tend0_bonus = 0;
            potent_tend1_bonus = 0;
            potent_tend2_bonus = 0;
            potent_tend_type = 0;
            mod_active = true;
            skill_modifiers = new List<Modifier>();
            pot_modifiers = new List<Modifier>();
        }
        public SkillGen(int sklow, int skhi, int st0, int st1, int st2, int plow, int phi, int pt0, int pt1, int pt2)
        {
            this.skill_low = sklow;
            this.skill_high = skhi;
            this.skill_tend0_bonus = st0;
            this.skill_tend1_bonus = st1;
            this.skill_tend2_bonus = st2;
            this.potent_low = plow;
            this.potent_high = phi;
            this.potent_tend0_bonus = pt0;
            this.potent_tend1_bonus = pt1;
            this.potent_tend2_bonus = pt2;
            mod_active = true;
            this.skill_modifiers = new List<Modifier>();
            this.pot_modifiers = new List<Modifier>();
        }

        public void AddNewModifier(Trait trait, RatingType type, Operator op, double op_value)
        {
            bool skillexists = false;
            bool potexists = false;
            if (type == RatingType.Skill || type == RatingType.Both)
            foreach(Modifier mod in this.skill_modifiers)
                if (mod.trait == (int)trait)
                {
                    skillexists = true;
                    mod.functions.Add(new ModMethod(op, op_value));
                }
            if (!skillexists)
                this.skill_modifiers.Add(new Modifier(trait, op, op_value));

            if (type == RatingType.Potential || type == RatingType.Both)
                foreach (Modifier mod in this.pot_modifiers)
                    if (mod.trait == (int)trait)
                    {
                        potexists = true;
                        mod.functions.Add(new ModMethod(op, op_value));
                    }
            if (!potexists)
                this.pot_modifiers.Add(new Modifier(trait, op, op_value));

            mod_active = true;
        }

        public int GetBonus(TendBonus type, Player player)
        {
            if (type == TendBonus.Skill)
                return GetSkillBonus(player);
            else if (type == TendBonus.Potential)
                return GetPotentialBonus(player);
            else return 0;
        }
        public int GetSkillBonus(Player player)
        {
            Player_Ratings pr = player.Original_Ratings;

            double bonus = 0;
            for (int c = 0; c < this.skill_modifiers.Count; c++)
            {
                List<bool> changes = new List<bool>();
                bool check = false;
                Trait trait = (Trait)this.skill_modifiers[c].trait;
                for (int f = 0; f < 4; f++)
                {
                    changes.Add(true);
                    if (f > 1)
                    {
                        changes[f] = changes[f - 1];
                        // use previous change result...if previous was AND OR check, set the change to true/false based on expression before
                        // if it was OR, then it doesnt matter, set it it true and let the current expression handle the change                        
                        if ((Operator)this.skill_modifiers[c].functions[f - 1].op_value == Operator.And)                        
                            changes[f] = changes[f - 2];                        
                        else if ((Operator)this.skill_modifiers[c].functions[f - 1].op_value == Operator.Or)
                            check = true;                        
                    }

                    if (changes[f] || check)
                    {
                        //  Expressions setting a value, if previous expression was OR and the expression was true before that
                        //  set the value anyway (check=true)
                        if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.None)
                            continue;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Add)
                            bonus += this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Subtract)
                            bonus -= this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Multiply)
                            bonus = bonus * this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Divide)
                            bonus = bonus / this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Diff)
                            bonus += pr.GetPlayerTrait(trait) - this.skill_modifiers[c].functions[f].op_value;

                            // Expression testing a value   
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Less)
                            changes[f] = pr.GetPlayerTrait(trait) < this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.More)
                            changes[f] = pr.GetPlayerTrait(trait) > this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.Equal)
                            changes[f] = pr.GetPlayerTrait(trait) == this.skill_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.skill_modifiers[c].functions[f].operation == Operator.NotEqual)
                            changes[f] = pr.GetPlayerTrait(trait) != this.skill_modifiers[c].functions[f].op_value;
                    }                    
                }
            }

            return (int)bonus;
        }
        
        
        public int GetPotentialBonus(Player player)
        {
            Player_Ratings pr = player.Original_Ratings;
            double bonus = 0;
            for (int c = 0; c < this.pot_modifiers.Count; c++)
            {
                List<bool> changes = new List<bool>();
                bool check = false;
                Trait trait = (Trait)this.pot_modifiers[c].trait;
                for (int f = 0; f < 4; f++)
                {
                    changes.Add(true);
                    if (f > 1)
                    {
                        changes[f] = changes[f - 1];
                        // use previous change result...if previous was AND OR check, set the change to true/false based on expression before
                        // if it was OR, then it doesnt matter, set it it true and let the current expression handle the change                        
                        if ((Operator)this.pot_modifiers[c].functions[f - 1].op_value == Operator.And)
                            changes[f] = changes[f - 2];
                        else if ((Operator)this.pot_modifiers[c].functions[f - 1].op_value == Operator.Or)
                            check = true;
                    }

                    if (changes[f] || check)
                    {
                        //  Expressions setting a value, if previous expression was OR and the expression was true before that
                        //  set the value anyway (check=true)
                        if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.None)
                            continue;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Add)
                            bonus += this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Subtract)
                            bonus -= this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Multiply)
                            bonus = bonus * this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Divide)
                            bonus = bonus / this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Diff)
                            bonus += pr.GetPlayerTrait(trait) - this.pot_modifiers[c].functions[f].op_value;

                            // Expression testing a value   
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Less)
                            changes[f] = pr.GetPlayerTrait(trait) < this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.More)
                            changes[f] = pr.GetPlayerTrait(trait) > this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.Equal)
                            changes[f] = pr.GetPlayerTrait(trait) == this.pot_modifiers[c].functions[f].op_value;
                        else if ((Operator)this.pot_modifiers[c].functions[f].operation == Operator.NotEqual)
                            changes[f] = pr.GetPlayerTrait(trait) != this.pot_modifiers[c].functions[f].op_value;
                    }
                }
            }

            return (int)bonus;
        }
               
        #region File IO
        #region Read
        public void Read(BinaryReader binreader)
        {
            skill_low = binreader.ReadByte();
            skill_high = binreader.ReadByte();
            skill_tend0_bonus = binreader.ReadByte();
            skill_tend1_bonus = binreader.ReadByte();
            skill_tend2_bonus = binreader.ReadByte();
            potent_low = binreader.ReadByte();
            potent_high = binreader.ReadByte();
            potent_tend0_bonus = binreader.ReadByte();
            potent_tend1_bonus = binreader.ReadByte();
            potent_tend2_bonus = binreader.ReadByte();
            this.skill_modifiers = new List<Modifier>();
            int total = binreader.ReadByte();            
            for (int c = 0; c < total; c++)
            {
                Modifier mod = new Modifier();
                mod.Read(binreader);
                this.skill_modifiers.Add(mod);
            }
            this.pot_modifiers = new List<Modifier>();
            total = binreader.ReadByte();
            for (int c = 0; c < total; c++)
            {
                Modifier mod = new Modifier();
                mod.Read(binreader);
                this.pot_modifiers.Add(mod);
            }
        }
        #endregion
        #region Write
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)skill_low);
            binwriter.Write((byte)skill_high);
            binwriter.Write((byte)skill_tend0_bonus);
            binwriter.Write((byte)skill_tend1_bonus);
            binwriter.Write((byte)skill_tend2_bonus);
            binwriter.Write((byte)potent_low);
            binwriter.Write((byte)potent_high);
            binwriter.Write((byte)potent_tend0_bonus);
            binwriter.Write((byte)potent_tend1_bonus);
            binwriter.Write((byte)potent_tend2_bonus);
            binwriter.Write((byte)this.skill_modifiers.Count);
            for (int c = 0; c < this.skill_modifiers.Count; c++)
                this.skill_modifiers[c].Write(binwriter);
            binwriter.Write((byte)this.pot_modifiers.Count);
            for (int c = 0; c < this.pot_modifiers.Count; c++)
                this.pot_modifiers[c].Write(binwriter);
        }
        #endregion
        #endregion
    }    
    


    public class DraftConfig
    {
        #region Members
        private EditorModel model = null;
        private EditorModel streamedmodel = null;
        public Dictionary<int, Tendency> Tendencies;
        public Dictionary<int, List<int>> PositionRace;        
        public Dictionary<int, Dictionary<int, SkillGen>> playerskills;
        public Dictionary<int, Dictionary<int, Dictionary<int, Rating>>> Ratings;
        #endregion

        public DraftConfig()
        {
            model = null;
            streamedmodel = null;
            Tendencies = new Dictionary<int, Tendency>();
            playerskills = new Dictionary<int, Dictionary<int, SkillGen>>();
            PositionRace = new Dictionary<int, List<int>>();
            Ratings = new Dictionary<int, Dictionary<int, Dictionary<int, Rating>>>();
        }

        public void Setmodel(EditorModel emodel)
        {
            model = emodel;
        }
        public void SetStreamedmodel(EditorModel smodel)
        {
            streamedmodel = smodel;
        }
                
        public void AddRace(Position position, int light, int med, int dark)
        {
            if (!PositionRace.ContainsKey((int)position))            
                PositionRace.Add((int)position, new List<int> { light, med, dark });
            else PositionRace[(int)position] = new List<int>{light,med,dark};
        }
        public void InitRace()
        {
            PositionRace = new Dictionary<int, List<int>>();
            AddRace(Position.QB, 82, 9, 9);
            AddRace(Position.HB, 12, 44, 44);
            AddRace(Position.WR, 14, 43, 43);
            AddRace(Position.FB, 50, 25, 25);
            AddRace(Position.TE, 54, 23, 23);
            AddRace(Position.LT, 50, 25, 25);
            AddRace(Position.RT, 50, 25, 25);
            AddRace(Position.LG, 50, 25, 25);
            AddRace(Position.RG, 50, 25, 25);
            AddRace(Position.C, 86, 7, 7);
            AddRace(Position.CB, 2, 49, 49);
            AddRace(Position.SS, 16, 42, 42);
            AddRace(Position.FS, 16, 42, 42);
            AddRace(Position.MLB, 26, 37, 37);
            AddRace(Position.ROLB, 26, 37, 37);
            AddRace(Position.LOLB, 26, 37, 37);
            AddRace(Position.RE, 20, 40, 40);
            AddRace(Position.LE, 20, 40, 40);
            AddRace(Position.DT, 12, 44, 44);
            AddRace(Position.P, 90, 5, 5);
            AddRace(Position.K, 98, 1, 1);
        }
        public int SetRace(Position position)
        {
            Random rand = new Random();
            int test = rand.Next(1, 1000001) / 10000;
            if (test <= PositionRace[(int)position][0])
                return 0;
            if (test >= 100 - PositionRace[(int)position][2])
                return 2;
            else return 1;
        }

        public void SetTendencies(Position position, int Tend0, int Tend1)
        {
            this.Tendencies[(int)position].tend0 = Tend0;
            this.Tendencies[(int)position].tend1 = Tend1;
        }
        public void AddPosition(Position position)
        {
            if (!this.playerskills.ContainsKey((int)position))
                playerskills.Add((int)position, new Dictionary<int, SkillGen>());
            if (!this.Tendencies.ContainsKey((int)position))
                Tendencies.Add((int)position, new Tendency());
            if (!this.Ratings.ContainsKey((int)position))
                Ratings.Add((int)position, new Dictionary<int, Dictionary<int, Rating>>());
        }
        public void SetRating(Position position, Trait trait, RatingType type, int low, int high, int t0, int t1, int t2, TendApply apply)
        {
            int start = 0;
            int end = 1;
            if (type == RatingType.Potential)
                start = 1;
            else if (type == RatingType.Skill)
                end = 0;

            if (!this.Ratings.ContainsKey((int)position))            
                this.Ratings.Add((int)position, new Dictionary<int, Dictionary<int, Rating>>());
            
            if (!this.Ratings[(int)position].ContainsKey((int)trait))            
                this.Ratings[(int)position].Add((int)trait, new Dictionary<int, Rating>());

            for (int t = start; t <= end; t++)
            {
                if (!this.Ratings[(int)position][(int)trait].ContainsKey(t))
                    this.Ratings[(int)position][(int)trait].Add(t, new Rating(low, high, t0, t1, t2, (int)apply));
                else this.Ratings[(int)position][(int)trait][t] = new Rating(low, high, t0, t1, t2, (int)apply);
            }
        }
        public Rating GetRating(Position position, Trait trait, RatingType type)
        {
            return this.Ratings[(int)position][(int)trait][(int)type];
        }
        public void SetModifier(Position position, Trait trait, RatingType type, Trait basetrait, Operator op, double op_val)
        {
            int start = 0;
            int end = 1;
            if (type == RatingType.Potential)
                start = 1;
            else if (type == RatingType.Skill)
                end = 0;
            for (int c = start; c < end; c++)
                this.Ratings[(int)position][(int)trait][c].AddNewModifier(basetrait, op, op_val);                
        }


        #region Old Methods
        /*
        public void SetRating(Position position, Trait trait, TendBonus type, int low, int high, int t0, int t1, int t2, TendApply tend)
        {
            if (type == TendBonus.Skill || type == TendBonus.Both)
            {
                if (!this.playerskills.ContainsKey((int)position))
                    this.playerskills.Add((int)position, new Dictionary<int, SkillGen>());
                if (!this.playerskills[(int)position].ContainsKey((int)trait))
                    this.playerskills[(int)position].Add((int)trait, new SkillGen());

                this.playerskills[(int)position][(int)trait].skill_low = low;
                this.playerskills[(int)position][(int)trait].skill_high = high;
                this.playerskills[(int)position][(int)trait].skill_tend0_bonus = t0;
                this.playerskills[(int)position][(int)trait].skill_tend1_bonus = t1;
                this.playerskills[(int)position][(int)trait].skill_tend2_bonus = t2;
                this.playerskills[(int)position][(int)trait].skill_tend_type = (int)tend;

            }
            if (type == TendBonus.Potential || type == TendBonus.Both)
            {
                if (!this.playerskills.ContainsKey((int)position))
                    this.playerskills.Add((int)position, new Dictionary<int, SkillGen>());
                if (!this.playerskills[(int)position].ContainsKey((int)trait))
                    this.playerskills[(int)position].Add((int)trait, new SkillGen());

                this.playerskills[(int)position][(int)trait].potent_low = low;
                this.playerskills[(int)position][(int)trait].potent_high = high;
                this.playerskills[(int)position][(int)trait].potent_tend0_bonus = t0;
                this.playerskills[(int)position][(int)trait].potent_tend1_bonus = t1;
                this.playerskills[(int)position][(int)trait].potent_tend2_bonus = t2;
                this.playerskills[(int)position][(int)trait].potent_tend_type = (int)tend;
            }
        }

        public SkillGen GetPlayerRating(Position position, Trait trait)
        {
            Dictionary<int, SkillGen> skills = playerskills[(int)position];
            return skills[(int)trait];
        }
        public void AddModifier(Position position, Trait trait, RatingType modtype, Trait basetrait, Operator op, double op_value)
        {
            this.playerskills[(int)position][(int)trait].AddNewModifier(basetrait, modtype, op, op_value);
        }
        */

        #endregion


        public void InitSkillSets()
        {
            Ratings = new Dictionary<int, Dictionary<int, Dictionary<int, Rating>>>();
            playerskills = new Dictionary<int, Dictionary<int, SkillGen>>();
                        
            #region QB
            AddPosition(Position.QB);
            SetTendencies(Position.QB, 43, 37);
            SetRating(Position.QB, Trait.ACC, RatingType.Both, 35, 60, 0, 30, 15, TendApply.Thresh);                // ACC 35-60 BAL +15 SCR +30
            SetRating(Position.QB, Trait.AGI, RatingType.Both, 35, 60, 0, 30, 15, TendApply.Thresh);                // AGI 35-60 BAL +15 SCR +30            
            SetRating(Position.QB, Trait.AWR, RatingType.Skill, 40, 60, 20, 0, 10, TendApply.Rand);                 // AWR 40-80 POC +20 BAL +10
            SetRating(Position.QB, Trait.AWR, RatingType.Potential, 74, 82, 18, 11, 20, TendApply.Thresh);          // potential 92-99, SCR 85-92
            SetModifier(Position.QB, Trait.AWR, RatingType.Skill, Trait.HT, Operator.NotEqual, 9);                  // Height not 6'2"
            SetModifier(Position.QB, Trait.AWR, RatingType.Skill, Trait.HT, Operator.Diff, 1);                      // +/- 1pt per inch difference            
            SetRating(Position.QB, Trait.BTK, RatingType.Both, 28, 82, 0, 0, 0, TendApply.Rand);            
            SetModifier(Position.QB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Subtract, 50);             // bonus = (strength-50) / 5
            SetModifier(Position.QB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Divide, 5.0);
            SetModifier(Position.QB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Subtract, 50);             // bonus = (agility-50) / 5
            SetModifier(Position.QB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Divide, 5.0);
            SetRating(Position.QB, Trait.CAR, RatingType.Both, 25, 80, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.CTH, RatingType.Both, 9, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.EGO, RatingType.Both, 50, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.INJ, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.JMP, RatingType.Skill, 40, 88, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.JMP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.KAC, RatingType.Both, 9, 40, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.KPW, RatingType.Both, 4, 49, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.KRT, RatingType.Both, 0, 20, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.PBK, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.RBK, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.SPD, RatingType.Skill, 38, 63, 0, 30, 12, TendApply.Thresh);                // low/high affected by tendency
            SetRating(Position.QB, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                 // physical, pot +(0-5)
            SetRating(Position.QB, Trait.STA, RatingType.Both, 75, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.STR, RatingType.Skill, 45, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.STR, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);
            SetRating(Position.QB, Trait.TAK, RatingType.Both, 10, 45, 0, 0, 0, TendApply.Rand);
            SetRating(Position.QB, Trait.TGH, RatingType.Both, 45, 99, 0, 0, 0, TendApply.Rand);            
            SetRating(Position.QB, Trait.THA, RatingType.Both, 70, 99, 6, -2, 2, TendApply.Rand);                    // THA 70-99 POT 70-99

            SetRating(Position.QB, Trait.THP, RatingType.Skill, 70, 99, 0, 6, 2, TendApply.Rand);                    // THP 70-99  
            SetRating(Position.QB, Trait.THP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                 // POT +(0-5)
            SetModifier(Position.QB, Trait.THP, RatingType.Both, Trait.STY, Operator.NotEqual, 0);                  // side arm thrower 
            SetModifier(Position.QB, Trait.THP, RatingType.Both, Trait.STY, Operator.Subtract, 5);                  // THP -5 skill and potential
            #endregion

            #region HB
            AddPosition(Position.HB);
            SetTendencies(Position.HB, 25, 25);
            SetRating(Position.HB, Trait.ACC, RatingType.Skill, 75, 90, 0, 15, 5, TendApply.Thresh);
            SetRating(Position.HB, Trait.ACC, RatingType.Potential, 85, 99, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.HB, Trait.AGI, RatingType.Both, 70, 85, 0, 18, 5, TendApply.Thresh);
            SetRating(Position.HB, Trait.AWR, RatingType.Skill, 50, 70, 0, 0, 0, TendApply.Thresh);                 // AWR 50-70, RB is tough position to learn in NFL
            SetRating(Position.HB, Trait.AWR, RatingType.Potential, 80, 99, 20, 0, 10, TendApply.Rand);             // potential 80-99
            SetRating(Position.HB, Trait.BTK, RatingType.Both, 50, 70, 0, 25, 18, TendApply.Rand);
            SetModifier(Position.HB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Subtract, 50);                 // bonus = (strength-50) / 5
            SetModifier(Position.HB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Divide, 5.0);
            SetModifier(Position.HB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Subtract, 50);                 // bonus = (agility-50) / 5
            SetModifier(Position.HB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Divide, 5.0);
            SetRating(Position.HB, Trait.CAR, RatingType.Skill, 60, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.CAR, RatingType.Potential, 85, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.CTH, RatingType.Both, 45, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.EGO, RatingType.Both, 50, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.INJ, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.JMP, RatingType.Skill, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.JMP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Rand);                 // Jumping is physcial skill for RB
            SetRating(Position.HB, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.KPW, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.KRT, RatingType.Both, 0, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.PBK, RatingType.Both, 15, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.RBK, RatingType.Both, 20, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.SPD, RatingType.Skill, 75, 85, 0, 15, 7, TendApply.Rand);                  // SPD 70-85, +7 BAL +15 SPD
            SetRating(Position.HB, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                // physical, pot +(0-5)
            SetRating(Position.HB, Trait.STA, RatingType.Both, 65, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.STR, RatingType.Skill, 30, 50, 35, 20, 30, TendApply.Rand);
            SetRating(Position.HB, Trait.STR, RatingType.Potential, 0, 10, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.TAK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.TGH, RatingType.Both, 45, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.THA, RatingType.Both, 10, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.HB, Trait.THP, RatingType.Skill, 10, 80, 0, 6, 2, TendApply.Rand);
            SetRating(Position.HB, Trait.THP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Rand);
            SetModifier(Position.HB, Trait.THP, RatingType.Both, Trait.STY, Operator.NotEqual, 0);                  // side arm thrower 
            SetModifier(Position.HB, Trait.THP, RatingType.Both, Trait.STY, Operator.Subtract, 5);                  // THP -5 skill and potential
            #endregion

            #region FB
            AddPosition(Position.FB);
            SetTendencies(Position.FB, 50, 25);
            SetRating(Position.FB, Trait.ACC, RatingType.Both, 50, 70, 5, 20, 10, TendApply.Rand);
            SetRating(Position.FB, Trait.AGI, RatingType.Both, 50, 65, 0, 16, 5, TendApply.Rand);
            SetRating(Position.FB, Trait.AWR, RatingType.Skill, 45, 70, 0, 0, 0, TendApply.Rand);                   // AWR
            SetRating(Position.FB, Trait.AWR, RatingType.Potential, 80, 99, 20, 0, 10, TendApply.Rand);             // potential 80-99
            SetRating(Position.FB, Trait.BTK, RatingType.Both, 55, 95, 0, 25, 18, TendApply.Thresh);
            SetModifier(Position.FB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Subtract, 50);                 // bonus = (strength-50) / 5
            SetModifier(Position.FB, Trait.BTK, RatingType.Both, Trait.STR, Operator.Divide, 5.0);
            SetModifier(Position.FB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Subtract, 50);                 // bonus = (agility-50) / 5
            SetModifier(Position.FB, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Divide, 5.0);
            SetRating(Position.FB, Trait.CAR, RatingType.Skill, 45, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.CAR, RatingType.Potential, 70, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.CTH, RatingType.Skill, 30, 50, 10, 30, 22, TendApply.Rand);
            SetRating(Position.FB, Trait.CTH, RatingType.Potential, 40, 60, 5, 25, 20, TendApply.Thresh);
            SetRating(Position.FB, Trait.EGO, RatingType.Both, 50, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.INJ, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.JMP, RatingType.Skill, 40, 80, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.JMP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Rand);                 // Jumping is physcial skill
            SetRating(Position.FB, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.KPW, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.KRT, RatingType.Both, 0, 0, 25, 80, 60, TendApply.RandHigh);               // 0-25 BLK 0-80 REC 0-60 BAL
            SetRating(Position.FB, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.PBK, RatingType.Both, 35, 68, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.RBK, RatingType.Skill, 40, 60, 0, 25, 20, TendApply.Rand);
            SetRating(Position.FB, Trait.RBK, RatingType.Potential, 5, 10, 65, 85, 85, TendApply.Final);            // 5-10 progression, set highs 85-BAL/BLK 65-REC
            SetRating(Position.FB, Trait.SPD, RatingType.Skill, 45, 60, 5, 23, 20, TendApply.Rand);                 // SPD
            SetRating(Position.FB, Trait.SPD, RatingType.Potential, 0, 5, 65, 85, 85, TendApply.Final);             // physical, pot +(0-5) set highs
            SetRating(Position.FB, Trait.STA, RatingType.Both, 45, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.STR, RatingType.Skill, 50, 60, 23, 12, 20, TendApply.Rand);
            SetRating(Position.FB, Trait.STR, RatingType.Potential, 0, 7, 88, 70, 88, TendApply.Final);             // 0-7  set highs
            SetRating(Position.FB, Trait.TAK, RatingType.Both, 10, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.TGH, RatingType.Both, 50, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.THA, RatingType.Both, 10, 37, 0, 0, 0, TendApply.Rand);
            SetRating(Position.FB, Trait.THP, RatingType.Both, 10, 40, 0, 6, 2, TendApply.Rand); 
            #endregion

            #region WR
            AddPosition(Position.WR);
            SetTendencies(Position.WR, 20, 40);
            SetRating(Position.WR, Trait.ACC, RatingType.Skill, 70, 99, 0, 15, 5, TendApply.Rand);
            SetRating(Position.WR, Trait.ACC, RatingType.Potential, 85, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.AGI, RatingType.Skill, 60, 84, 10, 20, 15, TendApply.Rand);
            SetRating(Position.WR, Trait.AGI, RatingType.Skill, 0, 8, 92, 99, 99, TendApply.Final);                 // physical trait, add limits
            SetRating(Position.WR, Trait.AWR, RatingType.Skill, 50, 60, 15, 5, 10, TendApply.Rand);                 // AWR lower for rookie WRs
            SetRating(Position.WR, Trait.AWR, RatingType.Potential, 80, 99, 0, 0, 0, TendApply.Rand);               // potential 80-99
            SetRating(Position.WR, Trait.BTK, RatingType.Both, 20, 70, 0, 0, 0, TendApply.Rand);
            SetModifier(Position.WR, Trait.BTK, RatingType.Both, Trait.STR, Operator.Subtract, 50);                 // bonus = (strength-50) / 5
            SetModifier(Position.WR, Trait.BTK, RatingType.Both, Trait.STR, Operator.Divide, 5.0);
            SetModifier(Position.WR, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Subtract, 50);                 // bonus = (agility-50) / 5
            SetModifier(Position.WR, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Divide, 5.0);
            SetRating(Position.WR, Trait.CAR, RatingType.Skill, 35, 77, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.CAR, RatingType.Skill, 35, 77, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.WR, Trait.CTH, RatingType.Skill, 60, 80, 5, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.CTH, RatingType.Potential, 85, 99, 7, 0, 5, TendApply.Thresh);
            SetRating(Position.WR, Trait.EGO, RatingType.Both, 50, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.INJ, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.JMP, RatingType.Skill, 65, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.JMP, RatingType.Potential, 0, 8, 0, 0, 0, TendApply.Final);                // Jumping is physcial skill
            SetRating(Position.WR, Trait.KAC, RatingType.Both, 5, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.KPW, RatingType.Both, 5, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.KRT, RatingType.Both, 0, 99, 0, 0, 0, TendApply.Rand);                     // 0-99
            SetRating(Position.WR, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.PBK, RatingType.Both, 10, 44, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.RBK, RatingType.Skill, 0, 35, 30, 0, 20, TendApply.Rand);
            SetRating(Position.WR, Trait.RBK, RatingType.Potential, 5, 10, 70, 45, 70, TendApply.Final);            // 5-10 progression, set highs 85-BAL/BLK 65-REC            
            SetRating(Position.WR, Trait.SPD, RatingType.Skill, 75, 85, 4, 15, 8, TendApply.Rand);                  // SPD
            SetRating(Position.WR, Trait.SPD, RatingType.Potential, 0, 5, 89, 100, 95, TendApply.Final);            // physical, pot +(0-5) set highs
            SetRating(Position.WR, Trait.STA, RatingType.Skill, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.STA, RatingType.Skill, 85, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.STR, RatingType.Skill, 30, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.STR, RatingType.Potential, 0, 7, 75, 75, 75, TendApply.Final);             // 0-7  set highs
            SetRating(Position.WR, Trait.TAK, RatingType.Both, 5, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.TGH, RatingType.Both, 40, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.THA, RatingType.Both, 10, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.WR, Trait.THP, RatingType.Both, 10, 80, 0, 6, 2, TendApply.Rand);
            SetRating(Position.WR, Trait.THP, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);
            #endregion

            #region TE
            AddPosition(Position.TE);
            SetTendencies(Position.TE, 33, 25);
            SetRating(Position.TE, Trait.ACC, RatingType.Skill, 50, 65, 5, 20, 10, TendApply.Rand);
            SetRating(Position.TE, Trait.ACC, RatingType.Potential, 0, 5, 70, 90, 80, TendApply.Rand);              // Physical for TE, set limits
            SetRating(Position.TE, Trait.AGI, RatingType.Skill, 50, 65, 0, 20, 5, TendApply.Rand);
            SetRating(Position.TE, Trait.AGI, RatingType.Potential, 60, 75, 0, 10, 3, TendApply.Thresh);
            SetRating(Position.TE, Trait.AWR, RatingType.Skill, 40, 62, 5, 5, 10, TendApply.Thresh);                // AWR 45-65  50-72 BAL
            SetRating(Position.TE, Trait.AWR, RatingType.Potential, 80, 99, 92, 92, 99, TendApply.Thresh);          // potential 80-92 80-99 BAL            
            SetRating(Position.TE, Trait.BTK, RatingType.Skill, 45, 80, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.TE, Trait.BTK, RatingType.Potential, 60, 85, 0, 0, 0, TendApply.Thresh);
            SetModifier(Position.TE, Trait.BTK, RatingType.Both, Trait.STR, Operator.Subtract, 50);                 // bonus = (strength-50) / 5
            SetModifier(Position.TE, Trait.BTK, RatingType.Both, Trait.STR, Operator.Divide, 5.0);
            SetModifier(Position.TE, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Subtract, 50);                 // bonus = (agility-50) / 5
            SetModifier(Position.TE, Trait.BTK, RatingType.Both, Trait.AGI, Operator.Divide, 5.0);
            SetRating(Position.TE, Trait.CAR, RatingType.Skill, 40, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.CAR, RatingType.Potential, 70, 78, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.CTH, RatingType.Skill, 55, 65, 0, 15, 10, TendApply.Rand);
            SetRating(Position.TE, Trait.CTH, RatingType.Potential, 0, 10, 75, 92, 88, TendApply.Final);
            SetRating(Position.TE, Trait.EGO, RatingType.Both, 50, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.INJ, RatingType.Both, 50, 95, 0, 20, 10, TendApply.Rand);
            SetRating(Position.TE, Trait.JMP, RatingType.Skill, 35, 60, 8, 30, 25, TendApply.Thresh);
            SetRating(Position.TE, Trait.JMP, RatingType.Potential, 0, 5, 70, 92, 88, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.TE, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.KPW, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.KRT, RatingType.Both, 0, 0, 25, 80, 60, TendApply.RandHigh);               // 0-25 BLK 0-80 REC 0-60 BAL
            SetRating(Position.TE, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.PBK, RatingType.Skill, 28, 50, 32, 0, 22, TendApply.Rand);
            SetRating(Position.TE, Trait.PBK, RatingType.Potential, 0, 10, 85, 65, 78, TendApply.Final);            
            SetRating(Position.TE, Trait.RBK, RatingType.Skill, 40, 50, 28, 20, 15, TendApply.Rand);
            SetRating(Position.TE, Trait.RBK, RatingType.Potential, 5, 10, 80, 65, 70, TendApply.Final);            // 5-10 progression, set highs            
            SetRating(Position.TE, Trait.SPD, RatingType.Skill, 50, 62, 0, 30, 20, TendApply.RSplit);               // low + tend/2  high + tend            
            SetRating(Position.TE, Trait.SPD, RatingType.Potential, 2, 5, 65, 92, 85, TendApply.Final);             // physical set highs
            SetRating(Position.TE, Trait.STA, RatingType.Both, 45, 90, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.STR, RatingType.Skill, 55, 80, 23, 12, 20, TendApply.Thresh);
            SetRating(Position.TE, Trait.STR, RatingType.Potential, 0, 7, 85, 85, 85, TendApply.Final);             // 0-7  set highs
            SetRating(Position.TE, Trait.TAK, RatingType.Both, 10, 55, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.TGH, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.THA, RatingType.Both, 10, 48, 0, 0, 0, TendApply.Rand);
            SetRating(Position.TE, Trait.THP, RatingType.Both, 10, 52, 0, 0, 0, TendApply.Rand);
            #endregion

            #region LT
            AddPosition(Position.LT);
            SetTendencies(Position.LT, 41, 31);
            SetRating(Position.LT, Trait.ACC, RatingType.Skill, 45, 80, 0, 0, 0, TendApply.Rand);                   // skill 45-80
            SetRating(Position.LT, Trait.ACC, RatingType.Potential, 74, 89, 0, 0, 0, TendApply.Thresh);             // Potential 74-89
            SetRating(Position.LT, Trait.AGI, RatingType.Skill, 40, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.AGI, RatingType.Potential, 55, 75, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.AWR, RatingType.Skill, 45, 80, 0, 0, 0, TendApply.Rand);                  // AWR 45-80
            SetRating(Position.LT, Trait.AWR, RatingType.Potential, 85, 99, 0, 0, 0, TendApply.Thresh);             // potential 85-99    
            SetRating(Position.LT, Trait.BTK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.CAR, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.CTH, RatingType.Both, 10, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.EGO, RatingType.Both, 50, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.INJ, RatingType.Both, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.JMP, RatingType.Skill, 5, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.JMP, RatingType.Potential, 0, 5, 75, 75, 75, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.LT, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.KPW, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.KRT, RatingType.Both, 0, 15, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.PBK, RatingType.Skill, 70, 80, 5, 12, 7, TendApply.Rand);
            SetRating(Position.LT, Trait.PBK, RatingType.Potential, 5, 10, 85, 95, 90, TendApply.Final);            // POTs lower, LTs already have advantages?
            SetRating(Position.LT, Trait.RBK, RatingType.Skill, 70, 80, 12, 5, 7, TendApply.Rand);
            SetRating(Position.LT, Trait.RBK, RatingType.Potential, 5, 10, 99, 88, 93, TendApply.Final);            // pot +5-10 set highs           
            SetRating(Position.LT, Trait.SPD, RatingType.Skill, 40, 70, 0, 0, 0, TendApply.Thresh);                 //            
            SetRating(Position.LT, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                // physical pot +(0-5)
            SetRating(Position.LT, Trait.STA, RatingType.Skill, 45, 80, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LT, Trait.STA, RatingType.Potential, 45, 80, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.STR, RatingType.Skill, 80, 92, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.STR, RatingType.Potential, 5, 10, 0, 0, 0, TendApply.Final);               // 5-10            
            SetRating(Position.LT, Trait.TAK, RatingType.Both, 10, 50, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.TGH, RatingType.Both, 50, 99, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.THA, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LT, Trait.THP, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            #endregion
                        
            #region LG
            AddPosition(Position.LG);
            SetTendencies(Position.LG, 54, 18);
            SetRating(Position.LG, Trait.ACC, RatingType.Skill, 45, 84, 0, 0, 0, TendApply.Thresh);                   // skill 45-84
            SetRating(Position.LG, Trait.ACC, RatingType.Potential, 68, 84, 0, 0, 0, TendApply.Thresh);             // Potential 68-84            
            SetRating(Position.LG, Trait.AGI, RatingType.Skill, 35, 70, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.AGI, RatingType.Potential, 52, 70, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.AWR, RatingType.Skill, 45, 75, 0, 0, 0, TendApply.Thresh);                  // AWR 45-80
            SetRating(Position.LG, Trait.AWR, RatingType.Potential, 85, 99, 95, 92, 99, TendApply.Thresh);             // potential 85-99                
            SetRating(Position.LG, Trait.BTK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.CAR, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.CTH, RatingType.Both, 10, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.EGO, RatingType.Both, 50, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.INJ, RatingType.Both, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.JMP, RatingType.Skill, 5, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.JMP, RatingType.Potential, 0, 5, 75, 75, 75, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.LG, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.KPW, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.KRT, RatingType.Both, 0, 15, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.LG, Trait.PBK, RatingType.Skill, 70, 87, 0, 7, 3, TendApply.Thresh);
            SetRating(Position.LG, Trait.PBK, RatingType.Potential, 5, 10, 92, 99, 99, TendApply.Final);            //            
            SetRating(Position.LG, Trait.RBK, RatingType.Skill, 63, 82, 12, 7, 10, TendApply.Thresh);
            SetRating(Position.LG, Trait.RBK, RatingType.Potential, 5, 10, 99, 90, 93, TendApply.Final);            // pot +5-10 set highs
            SetRating(Position.LG, Trait.SPD, RatingType.Skill, 40, 63, 0, 0, 0, TendApply.Thresh);                 //            
            SetRating(Position.LG, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                // physical pot +(0-5)            
            SetRating(Position.LG, Trait.STA, RatingType.Skill, 38, 90, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.STA, RatingType.Potential, 70, 90, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.STR, RatingType.Skill, 80, 88, 7, 2, 4, TendApply.Thresh);
            SetRating(Position.LG, Trait.STR, RatingType.Potential, 5, 10, 99, 92, 94, TendApply.Final);            // 5-10            
            SetRating(Position.LG, Trait.TAK, RatingType.Both, 10, 50, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.TGH, RatingType.Both, 45, 99, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.THA, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.LG, Trait.THP, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            #endregion

            #region C
            AddPosition(Position.C);
            SetTendencies(Position.C, 50, 20);
            SetRating(Position.C, Trait.ACC, RatingType.Skill, 45, 80, 0, 0, 0, TendApply.Thresh);                  // skill 45-80
            SetRating(Position.C, Trait.ACC, RatingType.Potential, 72, 85, 0, 0, 0, TendApply.Thresh);              // Potential 72-85
            SetRating(Position.C, Trait.AGI, RatingType.Skill, 40, 65, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.AGI, RatingType.Potential, 55, 70, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.AWR, RatingType.Skill, 45, 75, 0, 0, 0, TendApply.Thresh);                  // AWR 45-80
            SetRating(Position.C, Trait.AWR, RatingType.Potential, 75, 99, 96, 93, 99, TendApply.Thresh);           // potential 75-99 set limits
            SetRating(Position.C, Trait.BTK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.CAR, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.CTH, RatingType.Both, 10, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.EGO, RatingType.Both, 50, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.INJ, RatingType.Both, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.JMP, RatingType.Skill, 5, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.JMP, RatingType.Potential, 0, 5, 75, 75, 75, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.C, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.KPW, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.KRT, RatingType.Both, 0, 15, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.C, Trait.PBK, RatingType.Skill, 70, 80, 3, 10, 6, TendApply.Thresh);
            SetRating(Position.C, Trait.PBK, RatingType.Potential, 5, 10, 90, 99, 95, TendApply.Final);            //                        
            SetRating(Position.C, Trait.RBK, RatingType.Skill, 70, 80, 10, 3, 6, TendApply.Thresh);
            SetRating(Position.C, Trait.RBK, RatingType.Potential, 5, 10, 99, 90, 95, TendApply.Final);            // pot +5-10 set highs
            SetRating(Position.C, Trait.SPD, RatingType.Skill, 40, 65, 0, 0, 0, TendApply.Thresh);                 //            
            SetRating(Position.C, Trait.SPD, RatingType.Potential, 0, 5, 65, 65, 65, TendApply.Final);             // physical pot +(0-5)           
            SetRating(Position.C, Trait.STA, RatingType.Skill, 38, 85, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.STA, RatingType.Potential, 70, 85, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.STR, RatingType.Skill, 75, 90, 7, 2, 4, TendApply.Thresh);
            SetRating(Position.C, Trait.STR, RatingType.Potential, 5, 10, 95, 90, 92, TendApply.Final);            // 5-10            
            SetRating(Position.C, Trait.TAK, RatingType.Both, 10, 50, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.TGH, RatingType.Both, 45, 99, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.THA, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.C, Trait.THP, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            #endregion

            #region RG
            AddPosition(Position.RG);
            SetTendencies(Position.RG, 71, 8);
            SetRating(Position.RG, Trait.ACC, RatingType.Skill, 45, 84, 0, 0, 0, TendApply.Thresh);                 // skill 45-84
            SetRating(Position.RG, Trait.ACC, RatingType.Potential, 68, 84, 0, 0, 0, TendApply.Thresh);             // Potential 68-84
            SetRating(Position.RG, Trait.AGI, RatingType.Skill, 45, 85, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.AGI, RatingType.Potential, 68, 85, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.AWR, RatingType.Skill, 45, 75, 0, 0, 0, TendApply.Thresh);                 // AWR 45-75
            SetRating(Position.RG, Trait.AWR, RatingType.Potential, 78, 99, 99, 90, 95, TendApply.Thresh);          // potential 78-99 set limits
            SetRating(Position.RG, Trait.BTK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.CAR, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.CTH, RatingType.Both, 10, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.EGO, RatingType.Both, 50, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.INJ, RatingType.Both, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.JMP, RatingType.Skill, 5, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.JMP, RatingType.Potential, 0, 5, 75, 75, 75, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.RG, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.KPW, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.KRT, RatingType.Both, 0, 15, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RG, Trait.PBK, RatingType.Skill, 70, 80, 0, 5, 2, TendApply.Thresh);
            SetRating(Position.RG, Trait.PBK, RatingType.Potential, 5, 10, 89, 96, 92, TendApply.Final);            //            
            SetRating(Position.RG, Trait.RBK, RatingType.Skill, 70, 80, 10, 0, 5, TendApply.Thresh);
            SetRating(Position.RG, Trait.RBK, RatingType.Potential, 5, 10, 99, 93, 96, TendApply.Final);            // pot +5-10 set highs            
            SetRating(Position.RG, Trait.SPD, RatingType.Skill, 36, 65, 0, 0, 0, TendApply.Thresh);                 //            
            SetRating(Position.RG, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                // physical pot +(0-5)
            SetRating(Position.RG, Trait.STA, RatingType.Skill, 38, 90, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.STA, RatingType.Potential, 70, 90, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.STR, RatingType.Skill, 80, 90, 5, 0, 2, TendApply.Thresh);                 // 80-95
            SetRating(Position.RG, Trait.STR, RatingType.Potential, 5, 10, 99, 92, 96, TendApply.Final);            // +5,10 set limits
            SetRating(Position.RG, Trait.TAK, RatingType.Both, 10, 50, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.TGH, RatingType.Both, 45, 99, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.THA, RatingType.Both, 5, 40, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RG, Trait.THP, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            #endregion

            #region RT
            AddPosition(Position.RT);
            SetTendencies(Position.RT, 50, 20);
            SetRating(Position.RT, Trait.ACC, RatingType.Skill, 40, 80, 0, 0, 0, TendApply.Rand);                   // skill 40-80
            SetRating(Position.RT, Trait.ACC, RatingType.Potential, 70, 85, 0, 0, 0, TendApply.Thresh);             // Potential 70-85            
            SetRating(Position.RT, Trait.AGI, RatingType.Skill, 40, 70, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.AGI, RatingType.Potential, 52, 70, 0, 0, 0, TendApply.Thresh);            
            SetRating(Position.RT, Trait.AWR, RatingType.Skill, 42, 70, 0, 0, 0, TendApply.Rand);                  // AWR 45-80
            SetRating(Position.RT, Trait.AWR, RatingType.Potential, 75, 99, 95, 95, 99, TendApply.Thresh);         // potential 85-99 set limits            
            SetRating(Position.RT, Trait.BTK, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.CAR, RatingType.Both, 10, 60, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.CTH, RatingType.Both, 10, 65, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.EGO, RatingType.Both, 50, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.INJ, RatingType.Both, 55, 95, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.JMP, RatingType.Skill, 5, 75, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.JMP, RatingType.Potential, 0, 5, 75, 75, 75, TendApply.Rand);              // Jumping is physcial skill            
            SetRating(Position.RT, Trait.KAC, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.KPW, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.KRT, RatingType.Both, 0, 15, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.MOR, RatingType.Both, 70, 100, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.PBK, RatingType.Skill, 70, 80, 5, 10, 7, TendApply.Rand);
            SetRating(Position.RT, Trait.PBK, RatingType.Potential, 5, 10, 93, 99, 95, TendApply.Final);            // Higher ratings that LTs            
            SetRating(Position.RT, Trait.RBK, RatingType.Skill, 70, 80, 10, 5, 7, TendApply.Rand);
            SetRating(Position.RT, Trait.RBK, RatingType.Potential, 5, 10, 99, 95, 93, TendApply.Final);            // pot +5-10 set highs           
            SetRating(Position.RT, Trait.SPD, RatingType.Skill, 40, 61, 0, 0, 0, TendApply.Thresh);                 //            
            SetRating(Position.RT, Trait.SPD, RatingType.Potential, 0, 5, 0, 0, 0, TendApply.Final);                // physical pot +(0-5)            
            SetRating(Position.RT, Trait.STA, RatingType.Skill, 35, 80, 0, 0, 0, TendApply.Rand);
            SetRating(Position.RT, Trait.STA, RatingType.Potential, 45, 80, 0, 0, 0, TendApply.Thresh);            
            SetRating(Position.RT, Trait.STR, RatingType.Skill, 80, 88, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.STR, RatingType.Potential, 2, 8, 0, 0, 0, TendApply.Final);               // 2-8            
            SetRating(Position.RT, Trait.TAK, RatingType.Both, 10, 50, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.TGH, RatingType.Both, 45, 98, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.THA, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            SetRating(Position.RT, Trait.THP, RatingType.Both, 5, 35, 0, 0, 0, TendApply.Thresh);
            #endregion
        }

        public void SetPlayerSkillSets(Player player)
        {
            // Set skills and potentials
            foreach (Trait trait in Enum.GetValues(typeof(Trait)))
            {
                for (int type = 0; type < 2; type++)
                {
                    int val = this.Ratings[player.Info.POSITION_ID][(int)trait][type].GetPlayerSkill(player);
                    if (type == 0)
                        player.Original_Ratings.SetPlayerTrait(trait, val);
                    else 
                    {
                        if (this.Ratings[player.Info.POSITION_ID][(int)trait][type].apply == (int)TendApply.Final)
                            val += player.Original_Ratings.GetPlayerTrait(trait);
                        player.PlayerPotential.SetPlayerTrait(trait, val);
                    }
                }
            }
            // Now go back and add any bonus from modifiers

        }
      
    }
}
