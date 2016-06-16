using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Sun : CRYSTAL_Script {

		public float SunSpeed = 0.0f;

		public override void Step (){
			base.Step ();

			this.transform.position += new Vector3(0, 0, this.SunSpeed) * Time.deltaTime;
			this.transform.LookAt (Vector3.zero);
		}
	}
}