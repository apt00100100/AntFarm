using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Ant : CRYSTAL_Script {

		static int COUNT = 0;

		private Vector3 p_Direction;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			GameObject antContainer = GameObject.Find ("AntContainer");
			this.transform.parent = antContainer.transform;

			this.p_Direction = Vector3.zero;

			CONSOLE.Log ("SPAWNED ANT!", ++COUNT);
		}

		public override void Step (){
			base.Step ();
		}
	}
}