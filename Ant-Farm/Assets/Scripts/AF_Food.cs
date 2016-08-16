using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Food : CRYSTAL_Script {

		[Range(10, 1000)] public int StartLifePoints = 10;

		private int p_CurrentLifePoints;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.p_CurrentLifePoints = this.StartLifePoints;

			TileItem tile = AF_STATIC.FloodFill.FindTileByPosition (this.transform.position);
			tile.Type = TILE_TYPE.FOOD;
			tile.Object = this.gameObject;
		}

		public void GatherFood () {
			CONSOLE.Log ("Gather Food");
			this.p_CurrentLifePoints--;

			if (this.p_CurrentLifePoints <= 0) {
				TileItem tile = AF_STATIC.FloodFill.FindTileByPosition (this.transform.position);
				tile.Type = TILE_TYPE.EMPTY;
				tile.Object = null;

				GameObject.Destroy (this.gameObject);
			}
		}
	}
}