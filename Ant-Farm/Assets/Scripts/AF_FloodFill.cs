using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CRYSTAL;

namespace AntFarm {

	public enum TILE_TYPE {
		EMPTY,
		FOOD,
		HOME
	}

	public class AF_FloodFill : CRYSTAL_Script {
		const int MapSize = 10;
		//
		public float Size = 10;
		public float FoodCountScale = 1;
		//
		private TileItem p_Home;
		private List<List<TileItem>> p_TileItems = new List<List<TileItem>> ();
		//
		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.ResetMap ();
			this.CalculateMap ();

			// DEBUG
			this.Print ();
		}

		/// <summary>
		/// DEBUG: Print the mapped grid values
		/// </summary>
		private void Print() {
			for (int i = 0; i < this.Size; ++i) {
				var line = string.Empty;
				for (int j = 0; j < this.Size; ++j) line += this.p_TileItems [i] [j].Value.ToString() + "\t";
				CONSOLE.Log ("Row " + i.ToString(), line);
			}
		}

		public override void FixedStep (){
			base.FixedStep ();

			// Update all of the tiles
			this.MapTiles.ForEach (delegate(TileItem _tile) { _tile.Update(); });
		}

		public void ResetMap () {
			this.p_TileItems.Clear ();

			float tileSize	= MapSize / this.Size;
			float offset 	= -1f * (MapSize * 0.5f)+(tileSize*0.5f);
			Vector3 pos 	= Vector3.zero;
			TileItem tile;
			int i, j;

			// Populate Tile List & Food Items
			for (i = 0; i < this.Size; ++i) {
				var list = new List<TileItem> ();
				pos.x = (i * tileSize) + offset;
				for (j = 0; j < this.Size; ++j) {
					pos.z = (j * tileSize) + offset;
					tile = new TileItem (new Vector3(pos.x, pos.y, pos.z), tileSize);
					list.Add (tile);
				}
				this.p_TileItems.Add (list);
			}
		}

		public void CalculateMap () {
			TileItem tile;
			for (int i = 0; i < this.Size; ++i)
				for (int j = 0; j < this.Size; ++j) {
					tile = this.p_TileItems [i][j];
					// Check if the current tile is NOT a food item
					switch (tile.Type) {
					case TILE_TYPE.EMPTY:
						List<TileItem> foodTiles = this.FoodTiles;
						foodTiles.ForEach (delegate(TileItem _tile) {
							tile.Value += (_tile.Position.normalized - tile.Position.normalized).magnitude * 100;
						});
						tile.Value /= foodTiles.Count;
						break;
					case TILE_TYPE.HOME:
						this.p_Home = tile;
						tile.Value = float.MaxValue;
						break;
					}
				}
		}

		public void DropPheromone (TileItem _tile) {
			_tile.DropPheromone (TileItem.PHEROMONE_UP);

			List<TileItem> surroundingTiles = this.FindSurroundingTiles (_tile);
			surroundingTiles.ForEach (delegate(TileItem x) {
				x.DropPheromone(TileItem.PHEROMONE_UP * 0.5f);
			});
		}

		public TileItem NearestTile (TileItem _tile, BEHAVIOR _behavior, int _variable = 0) {
			List<TileItem> nearTiles = this.FindSurroundingTiles (_tile);

			switch (_behavior) {
				case BEHAVIOR.FOOD:
					// Exclude any squares occupied by another ant
					nearTiles.RemoveAll (delegate(TileItem x) {
						return x.Type == TILE_TYPE.EMPTY && x.Object != null;
					});

					nearTiles.Sort(delegate(TileItem x, TileItem y) {
						if (x.Value < y.Value) return -1;
						return 1;
					});
				
					break;

				case BEHAVIOR.HOME:
					nearTiles.Sort (delegate(TileItem x, TileItem y) {
						Vector3 dX = x.Position - this.p_Home.Position;
						Vector3 dY = y.Position - this.p_Home.Position;
						if (dX.magnitude < dY.magnitude) return -1;
						return 1;
					});

					return nearTiles.Count > 0 ? nearTiles [0] : null;
			}

			TileItem result = null;

			// Return the first food item if any exist
			nearTiles.ForEach (delegate(TileItem x) {
				if (x.Type == TILE_TYPE.FOOD) {
					result = x;
					return;
				}
			});

			if (result == null) {
				// Check pheromone levels
				nearTiles.Sort (delegate(TileItem x, TileItem y) {
					if (x.Pheromone > 0) return (x.Pheromone > x.Value) ? -1 : 0;
					return 1;
				});

				if (nearTiles.Count > 0 && nearTiles [0].Pheromone > 0)
					result = nearTiles [0];
			}

			// Default, return a random tile if any are left, else return null
			if (result == null)
				result = nearTiles.Count > 0 ? nearTiles [Random.Range (0, nearTiles.Count)] : null;

			return result;
		}

		public TileItem FindTileByPosition (Vector3 _target) {
			List<TileItem> tileItems = this.MapTiles;
			tileItems.Sort(delegate(TileItem x, TileItem y) {
				Vector3 dX = x.Position - _target;
				Vector3 dY = y.Position - _target;
				if (dX.magnitude < dY.magnitude) return -1;
				return 0;
			});
			return tileItems [0];
		}

		public TileItem FindHomeTile () {
			List<TileItem> tileItems = this.MapTiles;
			tileItems.RemoveAll(delegate(TileItem tile) {
				return tile.Type != TILE_TYPE.HOME;
			});

			return tileItems [0];
		}

		public List<TileItem> FindSurroundingTiles (TileItem _tile) {
			List<TileItem> nearTiles = new List<TileItem>();
			int i, j;
			for (i = 0; i < this.Size; ++i)
				for (j = 0; j < this.Size; ++j)
					if (this.p_TileItems [i] [j] == _tile) {
						// UP
						if (j > 0) nearTiles.Add(this.p_TileItems[i][j-1]);
						// UP-RIGHT
						if (j > 0 && i < this.Size - 1) nearTiles.Add (this.p_TileItems [i + 1] [j - 1]);
						// RIGHT
						if (i < this.Size - 1) nearTiles.Add(this.p_TileItems[i+1][j]);
						// RIGHT-DOWN
						if (i < this.Size - 1 && j < this.Size - 1) nearTiles.Add(this.p_TileItems[i+1][j+1]);
						// DOWN
						if (j < this.Size - 1) nearTiles.Add(this.p_TileItems[i][j+1]);
						// DOWN-LEFT
						if (i > 0 && j < this.Size - 1) nearTiles.Add(this.p_TileItems[i-1][j+1]);
						// LEFT
						if (i > 0) nearTiles.Add(this.p_TileItems[i-1][j]);
						// UP-LEFT
						if (i > 0 && j > 0) nearTiles.Add(this.p_TileItems[i-1][j-1]);
						break;
					}
			
			return nearTiles;
		}

		public List<TileItem> FoodTiles {
			get {
				List<TileItem> tiles = this.MapTiles;
				tiles.RemoveAll (delegate(TileItem tile) { 
					return tile.Type != TILE_TYPE.FOOD; 
				});
				return tiles;
			}
		}

		public List<TileItem> MapTiles {
			get {
				List<TileItem> tiles = new List<TileItem>();
				for (int i = 0; i < this.Size; ++i)
					for (int j = 0; j < this.Size; ++j)
						tiles.Add(this.p_TileItems[i][j]);
				
				return tiles;
			}
		}
	}

	public class TileItem {

		public static float PHEROMONE_UP = 100f;
		public static float PHEROMONE_DOWN = 0.2f;

		public GameObject Object;
		public Vector3 Position;
		public float Size;
		public float Value;
		public float Pheromone;
		public TILE_TYPE Type;

		public TileItem(Vector3 _position, float _size) {
			this.Position = _position;
			this.Size = _size;
			this.Value = 0f;
			this.Pheromone = 0f;
			this.Type = AntFarm.TILE_TYPE.EMPTY;
		}

		public void Update () {
			if (this.Pheromone > 0) {
				this.Pheromone -= PHEROMONE_DOWN * Time.deltaTime;

				if (this.Pheromone < 0) this.Pheromone = 0;
			}
		}

		public void DropPheromone (float _value) {
			this.Pheromone += _value;
		}
	}
}