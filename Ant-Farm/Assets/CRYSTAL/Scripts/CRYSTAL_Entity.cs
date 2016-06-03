using UnityEngine;
using System.Collections;

namespace CRYSTAL {

	public class CRYSTAL_Entity : CRYSTAL_Script {

		public override void Destroy () {

			Object.Destroy (this.gameObject);

			base.Destroy ();
		}
	}
}