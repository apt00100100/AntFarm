using UnityEngine;
using System.Collections;

namespace CRYSTAL {
	public class CRYSTAL_SwitchListener : CRYSTAL_Entity {

		void ToggleSwitch (GameObject _switch) {
			this.OnToggleSwitch (_switch);
		}

		/// <summary>
		/// Override this function when extending this class.
		/// </summary>
		/// <param name="_switch">The switch that's been toggled.</param>
		protected virtual void OnToggleSwitch(GameObject _switch) {
			CONSOLE.Log (CONSOLE_LOG_TYPE.INPUT, "Switch:", _switch);
		}
	}
}