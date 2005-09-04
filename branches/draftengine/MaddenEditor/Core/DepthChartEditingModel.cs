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

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class DepthPlayerValueObject
	{
		public PlayerRecord playerObject = null;
		public DepthChartRecord depthObject = null;
	}

	public class DepthChartEditingModel
	{
		/** Reference to our EditorModel */
		private EditorModel model = null;

		public DepthChartEditingModel(EditorModel model)
		{
			this.model = model;
		}

		public SortedList<int, DepthPlayerValueObject> GetPlayers(int teamId, int positionId)
		{
			SortedList<int, DepthPlayerValueObject> playerIdList = new SortedList<int, DepthPlayerValueObject>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
			{
				if (((DepthChartRecord)record).TeamId == teamId && ((DepthChartRecord)record).PositionId == positionId)
				{
					//Skip this record if marked for deletion
					if (record.Deleted)
					{
						continue;
					}
					DepthPlayerValueObject valObject = new DepthPlayerValueObject();
					valObject.depthObject = (DepthChartRecord)record;
					valObject.playerObject = model.PlayerModel.GetPlayerByPlayerId(valObject.depthObject.PlayerId);
					playerIdList.Add(valObject.depthObject.DepthOrder, valObject);	
				}
			}

			return playerIdList;
		}

		public List<PlayerRecord> GetAllPlayersOnTeamByOvr(int teamId, int positionId)
		{
			List<PlayerRecord> playerList = new List<PlayerRecord>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (((PlayerRecord)record).TeamId == teamId)
				{
					playerList.Add((PlayerRecord)record);
				}
			}

			return playerList;
		}

	}
}
