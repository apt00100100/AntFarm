using UnityEngine;
using System.Collections;

namespace CRYSTAL {

	public class CRYSTAL_Launcher : CRYSTAL_Script {

		public float MinRange = 1f;
		public float Angle = 30f;
		public GameObject ObjectStartPosition;

		public void LaunchProjectile(Vector3 _clickPosition, GameObject _object, bool _useObjectPosition = false) {
			//Vector3 is a struct so can NEVER null
			if (/*_clickPosition != null && */_object != null) {

				// source and target positions
				Rigidbody rigidBody	= _object.GetComponent<Rigidbody>();
				Vector3 pos 		= (!_useObjectPosition && ObjectStartPosition != null) ? ObjectStartPosition.transform.position : _object.transform.position;
				Vector3 target 		= _clickPosition;
				float dist 			= Vector3.Distance(pos, target);

				// Initial Checks
				if (dist < MinRange || rigidBody == null) return;
				
				// rotate & position the object
				_object.transform.position = pos;
				_object.transform.LookAt(target);
				
				// calculate initial velocity required to land the cube on target using the formula (9)
				float angleScale = Mathf.Sin(Mathf.Deg2Rad * Angle * 2);
				float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / angleScale);
				float Vy = (Vi * Mathf.Sin(Mathf.Deg2Rad * Angle)) * angleScale,
				Vz = (Vi * Mathf.Cos(Mathf.Deg2Rad * Angle)) * angleScale;

				Vector3 localVelocity 	= new Vector3(0, Vy, Vz);
				Vector3 launchVelocity 	= _object.transform.TransformDirection(localVelocity);
					
				// launch the cube by setting its initial velocity
				rigidBody.velocity = launchVelocity;
				_object.SendMessage("OnLaunch", launchVelocity, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

}
