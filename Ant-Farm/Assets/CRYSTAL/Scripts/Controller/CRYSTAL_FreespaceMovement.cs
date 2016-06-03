using UnityEngine;
using System.Collections;

namespace CRYSTAL {
	/**
	* This is a small controller which moves a ship in 3D space
	*
	* The idea behind this script is to apply thrust forward, and rotate to 
	* shift which direction forward is in. This applies thrist for movement, 
	* so a rigidbody is required.
	*/
	[RequireComponent (typeof(Rigidbody))]
	public class CRYSTAL_FreespaceMovement : CRYSTAL_Script {
		
		/**
		* A class which wraps the input settings
		*/
		[System.Serializable]
		public class KeyBindings {
			
			/**
			* Rotates the ship left
			*/
			public KeyCode left = KeyCode.A;

			/**
			* Rotates the ship right
			*/
			public KeyCode right = KeyCode.D;

			/**
			* Rotates the ship up
			*/
			public KeyCode up = KeyCode.None;

			/**
			* Rotates the ship down
			*/
			public KeyCode down = KeyCode.None;
			
			/**
			* Rotates the ship counter clockwise around forward axis
			*/
			public KeyCode counterClockwise = KeyCode.Q;

			/**
			* Rotates the ship clockwise around forward axis
			*/
			public KeyCode clockwise = KeyCode.E;

			/**
			* Applies thrust to forward vector 
			*/
			public KeyCode thrust = KeyCode.W;
			
			/**
			* Applies thrust to back vector 
			*/
			public KeyCode oppThrust = KeyCode.X;
					
		}
		
		/**
		* The particle system used for engine effects
		*/
		public ParticleSystem particles = null;

		/**
		* The rate of rotation of the ship
		*
		* The number of degrees that can be rotated in one second
		*/
		public float rotationRate = 90;
		
		/**
		* The force put on the ship while thrust is active
		*/
		public float thrustRate = 1;
		
		/**
		* If true, enables input
		*/
		public bool inputEnabled = false;

		/**
		* The keys this will repond to
		*/
		public KeyBindings keybindings;
		
		// private tracking booleans
		public bool togLeft = false;
		public bool togRight = false;
		public bool togUp = false;
		public bool togDown = false;
		public bool togThrust = false;
		public bool oppTogThrust = false;
		public bool togCounterClockwise = false;
		public bool togClockwise = false;
		
		/**
		* This applies forces to the ship based on input
		*/
		override public void FixedStep() {
			
			float rotate = rotationRate * Time.deltaTime;

			// do stuff based on my input state
			if(togLeft) {
				transform.Rotate(0,0-rotate,0);
			}
			if(togRight) {
				transform.Rotate(0,rotate,0);
			}
			if(togUp) {
				transform.Rotate(rotate,0,0);
			}
			if(togDown) {
				transform.Rotate(0-rotate,0,0);
			}
			if(togClockwise) {
				transform.Rotate(0,0,0-rotate);
			}
			if(togCounterClockwise) {
				transform.Rotate(0,0,rotate);
			}
			if(togThrust) {
				// ( 0, 0, 1) -> ( 0, .5, 1)
				Vector3 thrustForce = transform.forward * thrustRate;
				GetComponent<Rigidbody>().AddForce(thrustForce);
			}
			if (oppTogThrust){
				if (GetComponent<Rigidbody>().velocity.magnitude <= 0 ) {
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					return;
				}
				Vector3 thrustForce = transform.forward * -thrustRate;
				GetComponent<Rigidbody>().AddForce(thrustForce);
			}
		}
		
		/**
		* In update, the input is handled to prepare for FixedUpdate
		*/
		override public void Step() {
			
			// only process if input is enabled
			if(!inputEnabled) return;

			// Handle my input
			if (Input.GetKeyDown(keybindings.counterClockwise)) {
				togCounterClockwise = true;
			}
			if (Input.GetKeyDown(keybindings.clockwise)) {
				togClockwise = true;
			}
			if (Input.GetKeyDown(keybindings.left)) {
				togLeft = true;
			}
			if (Input.GetKeyDown(keybindings.right)) {
				togRight = true;
			}
			if (Input.GetKeyDown(keybindings.up)) {
				togUp = true;
			}
			if (Input.GetKeyDown(keybindings.down)) {
				togDown = true;
			}
			if (Input.GetKeyDown(keybindings.thrust)) {
				togThrust = true;
				if (particles){
					particles.enableEmission = true;
					particles.Emit(100);
				}
			}
			if (Input.GetKeyDown(keybindings.oppThrust)){
				oppTogThrust = true;
			}

			if (Input.GetKeyUp(keybindings.left)) {
				togLeft = false;
			}
			if (Input.GetKeyUp(keybindings.right)) {
				togRight = false;
			}
			if (Input.GetKeyUp(keybindings.up)) {
				togUp = false;
			}
			if (Input.GetKeyUp(keybindings.down)) {
				togDown = false;
			}
			if (Input.GetKeyUp(keybindings.thrust)) {
				togThrust = false;
				if (particles)	particles.enableEmission = false;
			}
			if (Input.GetKeyUp(keybindings.oppThrust)) {
				oppTogThrust = false;
			}
			if (Input.GetKeyUp(keybindings.counterClockwise)) {
				togCounterClockwise = false;
			}
			if (Input.GetKeyUp(keybindings.clockwise)) {
				togClockwise = false;
			}	
		}
		
		/**
		* Stops all movement of the ship
		*
		* this works by turning off all toggles
		*/
		public void StopAll() {
			togThrust = false;
			oppTogThrust = false;
			particles.enableEmission = false;
			togUp = false;
			togDown = false;
			togLeft = false;
			togRight = false;
			togClockwise = false;
			togCounterClockwise = false;

		}
	}
}