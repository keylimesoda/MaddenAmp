/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.ConSole
{
    class ConsoleDraft
    {
        // Only data stored here is the reference back to the EditorModel.  
        // The point of this object is just to call
        // functions that exist in DraftModel.  This is more or less a Console
        // front-end for the draft, to be replaced by a GUI by someone who knows
        // something about GUI's. :)
        //

        private EditorModel model = null;
        private DraftModel draftModel = null;

        public ConsoleDraft(EditorModel editorModel)
        {
            model = editorModel;

            draftModel = new DraftModel(model);
            Console.WriteLine("# PGID Team   Pos  OVR INJ");
            draftModel.DumpDraftResults();
            Console.WriteLine("\n\n");

            // InitializeDraft makes all of the mandatory operations
            // to run a draft.  Other optional actions that need to
            // be run at the start appear below.  The thought is that
            // a settings tab will allow the user to toggle off some features
            // but everything in InitializeDraft MUST be run at the start.
            draftModel.InitializeDraft(19);


            draftModel.FixDraftOrder();

            /*
            draftModel.DumpRookies((int)RookieRecord.Attribute.OVR);
            draftModel.DumpRookies((int)RookieRecord.Attribute.INJ);
            */

            for (int i = 0; i < 224; i++)
            {
                draftModel.MakeSelection(i, null);
            }

            Console.WriteLine("# PGID Team   Pos  OVR INJ PlayerValue PickValue");
            draftModel.DumpDraftResults();

        }
    }
}
