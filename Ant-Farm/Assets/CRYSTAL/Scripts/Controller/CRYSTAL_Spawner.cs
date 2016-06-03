using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public class CRYSTAL_Spawner : CRYSTAL_Script {

		private static int _SpawnIndex = 0;

		public bool SpawnEnabled = false;
		public bool Repeat = false;
		public float SpawnDelay = 0.0f;	// Seconds (0 - instant)
		public List<GameObject> Objects = new List<GameObject>();
		public List<GameObject> SpawnObjects = new List<GameObject>();
		public List<Vector3> SpawnPositions = null;
		private TimerStats p_CurrentTimer = null;
		
		public bool Enabled {
			set { 
				// if !value, check for existing timer and pause it
				if (value == true) {
					this.p_CurrentTimer = TIMER.AddTimer("CRYSTAL.Spawner." + _SpawnIndex++, this.SpawnDelay, true, false, this.OnSpawnTimer);
				} else if (this.p_CurrentTimer != null) {
					this.p_CurrentTimer.Stop();
				}
			}
		}

		override public void SetDefaultValues() {
			base.SetDefaultValues ();

			this.SpawnPositions = null;
			this.Enabled = this.SpawnEnabled;
		}

		virtual protected void OnSpawnTimer (string _tag, object[] _timerArguments) {

			this.p_CurrentTimer = null;
			this.Enabled = this.Repeat;

			List<GameObject> spawn = Spawn ();
			int i, n = spawn.Count;
			for (i = 0; i < n; ++i)	Objects.Add(spawn[i]);

			this.SpawnLogic();
		}

		virtual protected void SpawnLogic() {
			// ...
		}

		/// <summary>
		/// Set the spawn locations.
		/// </summary>
		/// <returns>The locations.</returns>
		virtual protected List<Vector3> SpawnLocations () {
			
			List<Vector3> spawnLocations = new List<Vector3>();
			int i, n = SpawnObjects.Count;
			for (i = 0; i < n; ++i)	spawnLocations.Add(this.transform.position);
			
			return spawnLocations;
		}
		
		/// <summary>
		/// Spawns the on trigger.
		/// </summary>
		virtual public List<GameObject> Spawn () {

			List<Vector3> spawnLocations = (this.SpawnPositions != null ? this.SpawnPositions : this.SpawnLocations());
			return Spawn(SpawnObjects, spawnLocations);
		}

		/// <summary>
		/// Spawn the specified _spawnObjects at specified _spawnLocations.
		/// </summary>
		/// <param name="_spawnObjects">_spawn objects.</param>
		/// <param name="_spawnLocations">_spawn locations.</param>
		virtual public List<GameObject> Spawn (List<GameObject> _spawnObjects, List<Vector3> _spawnLocations) {

			List<GameObject> spawnList = new List<GameObject>(); 
			int i, n = _spawnObjects.Count;
			for (i = 0; i < n; ++i) spawnList.Add(Spawn (_spawnObjects[i], _spawnLocations[i]));

			return spawnList;
		}

		/// <summary>
		/// Spawn the specified _spawnObject at specified _spawnLocation.
		/// </summary>
		/// <param name="_spawnObject">_spawn object.</param>
		/// <param name="_spawnLocation">_spawn location.</param>
		virtual public GameObject Spawn(GameObject _spawnObject, Vector3 _spawnLocation) {

			return Instantiate(_spawnObject, _spawnLocation, Quaternion.identity) as GameObject;
		}

	}
}