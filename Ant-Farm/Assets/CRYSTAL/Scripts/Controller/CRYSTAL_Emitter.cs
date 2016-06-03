using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public class CRYSTAL_Emitter : CRYSTAL_Script {

		public bool Enabled = true;

		public GameObject SpawningObject;

		public float SpawnFriequency = 0.0f; //+- Seconds on SpawnTime
		public float SpawnTime = 0.0f; 	// Seconds (0 - instant)

		private float p_timeCounter;

		override public void SetDefaultValues() {
			base.SetDefaultValues ();

			p_timeCounter = 0.0f;
		}

		public override void Step () {
			base.Step ();

			if (Enabled) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.EDITOR, p_timeCounter);
				p_timeCounter += Time.deltaTime;
				if (p_timeCounter > SpawnTime) {
					p_timeCounter	= 0.0f;

					Spawn ();
				}
			}
		}

		virtual public void Spawn () {
			if (SpawningObject != null) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.EDITOR, "Spawn GameObjects", SpawningObject);
				Instantiate(SpawningObject, this.transform.position, Quaternion.identity);
			}
		}

	}
}