using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CRYSTAL;

namespace AntFarm {
	
	public class AF_AntHill : CRYSTAL_Script {

		public GameObject AntPrefab;

		[Range(1000, 2000)] public int SpawnDelayMin	= 1000;	// Time between spawns
		[Range(2001, 3000)] public int SpawnDelayMax	= 2001;	// Time between spawns
		[Range(1, 5)] public int SpawnCountMin			= 1;	// Number to spawn per interval
		[Range(5, 10)] public int SpawnCountMax			= 5;	// Number to spawn per interval
		[Range(100, 500)] public int MaxSpawnCount		= 100;	// Max number of ants to be spawned

		private List<GameObject> p_Ants;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			// Initialize Variables
			this.p_Ants = new List<GameObject> ();

			// Spawn ants!!
			this.SpawnAnts ();
		}

		public void SpawnAnts () {

			double spawnDelay	= (double)Random.Range (this.SpawnDelayMin, this.SpawnDelayMax);
			int spawnCount = Random.Range (this.SpawnCountMin, this.SpawnCountMax);

			if (this.p_Ants.Count + spawnCount > this.MaxSpawnCount) {
				spawnCount -= (this.p_Ants.Count + spawnCount) - this.MaxSpawnCount;
			}

			for (int i = 0; i < spawnCount; ++i) 
				this.p_Ants.Add( SPAWNER.Spawn (this.AntPrefab, this.transform.position) );
			
			TIMER.AddTimer ("AntSpawner", spawnDelay, true, false, this.SpawnTimerCallback);
		}

		private void SpawnTimerCallback (string _tag, object[] _args = null) {

			// Remove the timer so we can reuse it.
			TIMER.RemoveTimer ("AntSpawner");

			if (this.p_Ants.Count < this.MaxSpawnCount) this.SpawnAnts ();
		}
	}
}