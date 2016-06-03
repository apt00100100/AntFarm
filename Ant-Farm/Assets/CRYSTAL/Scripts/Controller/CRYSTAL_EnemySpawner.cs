using UnityEngine;
using System.Collections;

namespace CRYSTAL {
	
	public class CRYSTAL_EnemySpawner : CRYSTAL_Spawner {

		#region Variable Declaration

		[Range(3, 10)] public int SpawnCount;
		public int p_spawnsRemaining;

		#endregion


		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.p_spawnsRemaining = this.SpawnCount;
		}

		void OnTriggerEnter(Collider _collider) {

			if (_collider.gameObject.tag == "Player") {
				this.SpawnEnemy ();
			}
		}

		void OnTriggerExit (Collider _collider) {

			if (_collider.gameObject.tag == "Player") {
				this.Enabled = false;
			}
		}

		protected override void SpawnLogic (){
			base.SpawnLogic ();

			SpawnEnemy ();
		}

		protected virtual void SpawnEnemy() {

			if (this.p_spawnsRemaining > 0) {
				this.p_spawnsRemaining--;
				this.Enabled = true;
			}
		}
	}
}