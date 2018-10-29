/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 StingRay68
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
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using System.Linq;
using System.Text;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Db;

namespace MaddenEditor.Core
{
    public class ProfileModel
    {
        private EditorModel model;
        private FB _Frostbyte;

        public FB fb
        {
            get { return _Frostbyte; }
            set { _Frostbyte = value; }
        }

        public ProfileModel(EditorModel Model)
        {
            model = Model;
            fb = new FB();
        }
        
    }
}
