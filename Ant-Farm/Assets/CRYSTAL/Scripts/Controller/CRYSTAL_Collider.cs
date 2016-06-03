using UnityEngine;
using System.Collections;

namespace CRYSTAL {

	public delegate void CollisionCallbackDelegate (Collision _collision);

	public class CRYSTAL_Collider : CRYSTAL_Script {

		protected CollisionCallbackDelegate ColliderCallback;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			ColliderCallback = null;
		}

		void OnCollisionEnter(Collision _collision) {
			if (ColliderCallback != null) ColliderCallback (_collision);
		}
	}

}