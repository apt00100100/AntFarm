using UnityEngine;
using System.Collections;

namespace CRYSTAL{

	[ExecuteInEditMode]
	public class CRYSTAL_FollowScript : CRYSTAL_Script {

		public Transform TargetTransform;
		public bool FaceForward = false;
		public Vector3 Offset;

		// Update is called once per frame
		override public void Step () {
			base.Step ();
										//Vector3 is a struct so can NEVER be null
			if (TargetTransform != null /* && Offset != null */ )
				this.transform.position = TargetTransform.position + Offset;

			if ( FaceForward )
				this.transform.forward = TargetTransform.forward;
		}
	}
}