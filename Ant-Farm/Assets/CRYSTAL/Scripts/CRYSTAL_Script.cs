using UnityEngine;
using System.Collections;

namespace CRYSTAL {
	
	public class CRYSTAL_Script : MonoBehaviour {

		#region Variable Declarations

		public bool StepEnabled 		= true,
					FixedStepEnabled	= true;

		public void EnableUpdates()	{ this.StepEnabled = true;  this.FixedStepEnabled = true;  }
		public void DisableUpdates(){ this.StepEnabled = false; this.FixedStepEnabled = false; }

		#endregion

		#region Get / Set

		protected virtual string FileName {
			get { return "NoFileName("+this.name+")"; }
		}

		#endregion

		#region Unity Overrides

		/// <summary>
		/// UNITY - Start this instance.
		/// </summary>
		void Start () 				{ this.SetDefaultValues(); }

		/// <summary>
		/// UNITY - Raises the destroy event.
		/// </summary>
		void OnDestroy() 			{ this.Destroy(); }

		/// <summary>
		/// UNITY - Update this instance.
		/// </summary>
		void Update() 				{ if (StepEnabled) this.Step(); }

		/// <summary>
		/// UNITY - Fixeds the update.
		/// </summary>
		void FixedUpdate()			{ if (FixedStepEnabled)	this.FixedStep (); }	

		/// <summary>
		/// UNITY - Awake is called when the script instance is being loaded.
		/// <summary>
		void Awake() 				{ this.CRYSTAL_Awake (); }

		/// <summary>
		/// UNITY - Raises the application pause event.
		/// </summary>
		void OnApplicationPause()	{}

		/// <summary>
		/// UNITY - Raises the application quit event.
		/// </summary>
		void OnApplicationQuit()	{}	

		/// <summary>
		/// UNITY - Raises the application focus event.
		/// </summary>
		void OnApplicationFocus()	{}

		/// <summary>
		/// UNITY - Raises the became visible event.
		/// </summary>
		void OnBecameVisible() 		{}

		/// <summary>
		/// UNITY - Raises the became invisible event.
		/// </summary>
		void OnBecameInvisible() 	{}

		/// <summary>
		/// UNITY - Raises the disable event.
		/// </summary>
		void OnDisable() 			{} 

		/// <summary>
		/// UNITY - This function is called when the object becomes enabled and active.
		/// </summary>
		void OnEnable() 			{}

		/// <summary>
		/// UNITY - Raises the mouse down event.
		/// </summary>
		void OnMouseDown()			{}

		/// <summary>
		/// UNITY - Raises the On-GUI event.
		/// </summary>
		void OnGUI () 				{ this.StepGUI (); }

		#endregion

		#region CRYSTAL Methods

		/// <summary>
		/// CRYSTAL - Sets the default values.
		/// </summary>
		virtual public void SetDefaultValues() {
			this.LoadInitialData ();
		}

		/// <summary>
		/// CRYSTAL - Destroy this instance.
		/// </summary>
		virtual public void Destroy() {}

		/// <summary>
		/// CRYSTAL - Step this instance.
		/// </summary>
		virtual public void Step() {}

		/// <summary>
		/// CRYSTAL - Fixeds the step.
		/// </summary>
		virtual public void FixedStep() {}

		/// <summary>
		/// CRYSTAL - Raises the awake event.
		/// </summary>
		virtual public void CRYSTAL_Awake(){}

		/// <summary>
		/// CRYSTAL - On GUI raised
		/// </summary>
		virtual public void StepGUI() {}

		/// <summary>
		/// CRYSTAL - Loads the initial data.
		/// </summary>
		virtual protected void LoadInitialData() {}

		#endregion
	}
}
