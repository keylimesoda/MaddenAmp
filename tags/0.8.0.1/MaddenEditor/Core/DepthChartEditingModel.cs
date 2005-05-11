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
