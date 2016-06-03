using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public class CRYSTAL_Switch : CRYSTAL_Script {

		public float HoverDistance = 4.0f;
		public bool IsSwitchActive = false;
		public float ResetSwitchTimer = 0.0f;
		public List<GameObject> Collection = new List<GameObject>();
		public GameObject InteractionIndicatorPrefab;
		public KeyCode ActionKey = KeyCode.Return;
		private GameObject p_InteractionIndicator;


		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.p_InteractionIndicator = null;
			this.StepEnabled = false;
		}

		public override void Destroy (){

			this.DestroyInteractionIndicator ();
			base.Destroy ();
		}

		public override void Step (){
			base.Step ();

			if (this.p_InteractionIndicator != null) {
				this.p_InteractionIndicator.transform.LookAt(Camera.main.transform.position, -Vector3.up);
			}

			// When the user activates the switch
			if (Input.GetKeyDown (this.ActionKey)) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.INPUT, "Activated Switch");

				this.DestroyInteractionIndicator ();
				this.StepEnabled = false;
				this.IsSwitchActive = false;
				this.ToggleSwitch();

				if (this.ResetSwitchTimer > 0.0f) {
					TIMER.AddTimer("CRYSTAL.Switch." + this.gameObject.GetInstanceID(), this.ResetSwitchTimer, true, false, this.OnSwitchResetTimer);
				}
			}
		}

		protected virtual void ToggleSwitch() {

			int i, n = this.Collection.Count;
			for (i = 0; i < n; ++i) {
				this.Collection[i].SendMessage("ToggleSwitch", this.gameObject, SendMessageOptions.DontRequireReceiver);
			}

			CRYSTAL_SwitchListener listener = this.gameObject.GetComponentInChildren<CRYSTAL_SwitchListener> ();
			if (listener != null) {
				listener.gameObject.SendMessage("ToggleSwitch", this.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}

		void OnTriggerEnter (Collider _collider) {

			if (!this.IsSwitchActive || _collider.gameObject.tag != "Player")	return;

			this.CreateInteractionIndicator ();
			this.StepEnabled = true;
		}

		void OnTriggerExit (Collider _collider) {
			
			if (!this.IsSwitchActive || _collider.gameObject.tag != "Player")	return;

			this.DestroyInteractionIndicator ();
			this.StepEnabled = false;
		}

		private void CreateInteractionIndicator() {

			if (this.InteractionIndicatorPrefab) {
				this.p_InteractionIndicator = SPAWNER.Spawn(this.InteractionIndicatorPrefab, this.IndicatorPosition);
				this.p_InteractionIndicator.transform.parent = this.transform.parent;
			}
		}

		private void DestroyInteractionIndicator() {

			if (this.p_InteractionIndicator) {
				Object.Destroy(this.p_InteractionIndicator);
			}
		}

		private void OnSwitchResetTimer (string _tag, object[] _timerArguments) {
			this.IsSwitchActive = true;

			// Activate the interaction indicator if the player is close enough to the switch
			SphereCollider collider = this.gameObject.GetComponent<SphereCollider> ();
			GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
			if (collider != null && player != null) {
				float length = (this.transform.position - player.transform.position).magnitude;

				if (length <= collider.radius) {
					this.CreateInteractionIndicator ();
					this.StepEnabled = true;
				}
			}
		}


		private Vector3 IndicatorPosition {
			get {
				return this.transform.position + (this.transform.up * this.HoverDistance);
			}
		}
	}
}