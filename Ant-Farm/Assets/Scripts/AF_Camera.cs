using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Camera : CRYSTAL_Script {

		public override void SetDefaultValues () {
			base.SetDefaultValues ();

			this.transform.position = new Vector3 (0, 9f, 0);
			this.transform.Rotate (new Vector3 (90f, 0f, 0f));
		}
	}
}