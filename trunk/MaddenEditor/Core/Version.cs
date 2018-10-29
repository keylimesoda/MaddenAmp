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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core
{
	/// <summary>
	/// This simple class just gives us easier access to the version number of MaddenAmp
	/// </summary>
	public class Version
	{
		protected Version() 
		{

		}

		public static String VersionString
		{
			get
			{
                string title =  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
                if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build > 0)
                {
                    title += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
                    if (System.Reflection.Assembly.GetCallingAssembly().GetName().Version.Revision > 0)
                        title += " Revision " + System.Reflection.Assembly.GetCallingAssembly().GetName().Version.Revision;
                }
                
                return title;
			}
		}
	}
}
