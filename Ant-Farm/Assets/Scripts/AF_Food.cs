using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Food : CRYSTAL_Script {

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			TileItem tile = AF_STATIC.FloodFill.FindTileByPosition (this.transform.position);
			tile.Type = TILE_TYPE.FOOD;
			tile.Object = this.gameObject;
		}
	}
}