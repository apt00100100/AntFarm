using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public enum BEHAVIOR {
		FOOD,
		HOME
	}

	public class AF_Ant : CRYSTAL_Script {
		
		static int COUNT = 0;

		const float TOLERANCE = 0.1f;

		public float Speed = 0.5f;
		public TileItem Tile = null;
		public GameObject DEBUG_Tile;
		public BEHAVIOR Behavior = BEHAVIOR.FOOD;

		private int AntId;
		private TileItem NextTile = null;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			GameObject parent 		= GameObject.Find ("AntContainer");
			this.transform.parent 	= parent.transform;
			this.AntId 				= ++COUNT;
			this.Tile 				= AF_STATIC.FloodFill.FindTileByPosition (this.transform.position);
			this.NextTile 			= null;
		}

		public override void Step (){
			base.Step();

			if (this.NextTile == null) {
				this.Next ();
			} else {
				Vector3 offset = (this.NextTile.Position - this.transform.position);
				this.transform.position += offset.normalized * this.Speed * Time.deltaTime;
				this.transform.LookAt (this.NextTile.Position);

				// Update nearest tile when within tolerance.
				if (offset.magnitude <= TOLERANCE) {
					this.Next ();

					if (this.NextTile != null) {
						MeshRenderer renderer = this.GetComponentsInChildren<MeshRenderer> ()[1];

						switch (this.Tile.Type) {
						case TILE_TYPE.FOOD:
							renderer.enabled = true;
							this.Behavior = BEHAVIOR.HOME; 
							break;
						case TILE_TYPE.HOME:
							renderer.enabled = false;
							this.Behavior = BEHAVIOR.FOOD;
							break;
						}

						this.Tile.Object	= null;
						this.Tile 			= this.NextTile;
						this.name			= this.AntId + "_Ant-" + this.Behavior.ToString ();

						if (this.Behavior == BEHAVIOR.HOME) AF_STATIC.FloodFill.DropPheromone (this.Tile);
					}
				}
			}
		}

		private void Next () {
			TileItem next = AF_STATIC.FloodFill.NearestTile (this.Tile, this.Behavior, 3);

			if (next != null) {
				this.NextTile			= next;
				this.NextTile.Object	= this.gameObject;
			}
		}
	}
}