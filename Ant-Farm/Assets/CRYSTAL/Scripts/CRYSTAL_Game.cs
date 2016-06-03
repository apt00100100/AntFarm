using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace CRYSTAL {

	public class CRYSTAL_Game : CRYSTAL_Script  {

		#region Base Method Overrides

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			/// CONFIG CONSOLE LOGS
			CONSOLE.Log ("Persistent Data Path:", Application.persistentDataPath);
		}

		public override void Step (){
			base.Step ();

			TIMER.Step ();
		}

		#endregion

		#region Virtual Methods 

		public virtual int GameIndex { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Start the game.
		/// </summary>
		public virtual void StartGame () {

			CONSOLE.Log(CONSOLE_LOG_TYPE.INITIALIZE, "Start Game:", this.GameIndex);
			this.Broadcast("StartGame", this.GameIndex);
		}

		public virtual void EndGame () {

			this.Broadcast("OnEndGame", this.GameIndex);
		}

		/// <summary>
		/// Saves the game data.
		/// </summary>
		public virtual void SaveGameData() {

			CONSOLE.Log (CONSOLE_LOG_TYPE.SAVEDATA, "CRYSTAL_Game.SaveGameData() -> Saving Data...");
			this.Broadcast("SaveData", this.GameIndex);
		}

		/// <summary>
		/// Loads the game data.
		/// </summary>
		public virtual void LoadGameData() { 
			CONSOLE.Log (CONSOLE_LOG_TYPE.LOADDATA, "Loading Data...");
			this.Broadcast("LoadData", this.GameIndex);
		}

		/// <summary>
		/// Deletes the game data.
		/// </summary>
		public virtual void DeleteGameData() {

			CONSOLE.Log (CONSOLE_LOG_TYPE.DELETEDATA, "Deleting Data...");
			this.Broadcast("DeleteData", this.GameIndex);
		}

		/// <summary>
		/// Quits the application.
		/// </summary>
		/// <param name="_save">If set to <c>true</c> _save.</param>
		public virtual void QuitApplication(bool _save = true) {
			if (_save) this.SaveGameData();
			
			Application.Quit();
		}

		/// <summary>
		/// Broadcast message to all object scripts.
		/// </summary>
		/// <param name="_method">_method.</param>
		/// <param name="_argument">_argument.</param>
		/// <param name="_optionType">_option type.</param>
		public void Broadcast(string _method, System.Object _argument = null, SendMessageOptions _optionType = SendMessageOptions.DontRequireReceiver) {

			this.gameObject.BroadcastMessage (_method, _argument, _optionType);
		}

		#endregion
	}
}