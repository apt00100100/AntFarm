using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public delegate void TriggerCallbackDelegate (Collider _Other);

	public class CRYSTAL_Trigger : CRYSTAL_Script {

		protected TriggerCallbackDelegate TriggerCallbackEnter;
		protected TriggerCallbackDelegate TriggerCallbackExit;

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.TriggerCallbackEnter	= null;
			this.TriggerCallbackExit = null;
		}

		/// <summary>
		/// UNITY - Raises the trigger enter event.
		/// </summary>
		/// <param name="_Other">Object that collided with this trigger.</param>
		void OnTriggerEnter (Collider _Other) { 
			if (TriggerCallbackEnter != null) TriggerCallbackEnter (_Other); 
		}

		/// <summary>
		/// UNITY - Raises the trigger exit event.
		/// </summary>
		/// <param name="_Other">_ other.</param>
		void OnTriggerExit(Collider _Other){
			if (TriggerCallbackExit != null) TriggerCallbackExit (_Other); 
		}
	}
}