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
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MaddenEditor.Core.Manager
{
    public class Salary
    {
        // 2004 values default
        public int NFL_cap = 75000000;
        public int inflation;

        public int Min_0 = 225000;
        public int Min_1 = 300000;
        public int Min_2 = 375000;
        public int Min_3 = 450000;
        public int Min_46 = 530000;
        public int Min_79 = 655000;
        public int Min_10 = 755000;

        public int RFA_franchise;
        public int RFA_transition;

    }
}
