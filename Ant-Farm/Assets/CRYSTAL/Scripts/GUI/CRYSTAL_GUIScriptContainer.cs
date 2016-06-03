using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public class CRYSTAL_GUIScriptContainer : CRYSTAL_Script {
		
//		public bool RenderGUI;
//		protected Texture2D gameCursor;
//		protected List<CRYSTAL_GUIScript> regions;
//
//		public CRYSTAL_GUIScriptContainer() {
//			regions = new List<CRYSTAL_GUIScript>();
//			RenderGUI = true;
//		}
//
//		/// <summary>
//		/// UNITY - OnGUI is called for rendering and handling GUI events.
//		/// </summary>
//		protected void OnGUI() {
//			RenderCRYSTAL_GUIControls();
//			HandleCRYSTAL_GUIControls();
//		}
//
//		public override void Step() {
////			for (int i = 0; i < this.regions.Count; ++i) this.regions[i].Update();		
//		}
//
//		/// <summary>
//		/// UNITY - This function is called when the MonoBehaviour will be destroyed.
//		/// </summary>
//		protected void OnDestroy() {
//			for (var i = 0; i < regions.Count; i++) regions[i].Destroy();
//			regions = null;
//		}
//
//		/// <summary>
//		/// UNITY - When the unity app is closing, this will be called.
//		/// </summary>
//		protected void OnApplicationQuit() {
//			DATA.Destroy();
//		}
//
//		public override void SetDefaultValues() {
//			base.SetDefaultValues();
//
//			//	Show/Hide the GUI
//			useGUILayout = RenderGUI;
//
//			//	Load in the game cursor if one is not already loaded
//			if (gameCursor == null) gameCursor = LOADER.LoadTexture2D("GameCursor");
//
//			//	Set the in game cursor
//			Cursor.SetCursor(gameCursor, Vector2.zero, CursorMode.ForceSoftware);
//		}
//
//		/// <summary>
//		/// Create all GUI controls here.
//		/// </summary>
//		protected void RenderCRYSTAL_GUIControls() {
//			//	Render the main menu window
//			for (int i = 0; i < regions.Count; i++) regions[i].Render();
//		}
//
//		/// <summary>
//		/// Handle all GUI controls here.
//		/// </summary>
//		virtual protected void HandleCRYSTAL_GUIControls() { }
//
//		/// <summary>
//		/// Perform a check to see if the mouse is over a specified Rect
//		/// </summary>
//		/// <param name="_rect"></param>
//		/// <returns>Boolean</returns>
//		virtual protected bool MouseOverRect(Rect _rect) {
//			return _rect.Contains(Input.mousePosition);
//		}
//
//		/// <summary>
//		/// Add a new region to the GUI Script
//		/// </summary>
//		/// <param name="_tag"></param>
//		/// <param name="_region"></param>
//		/// <returns></returns>
//		protected CRYSTAL_GUIRegion AddRegion(string _tag, CRYSTAL_GUIRegion _region) {
//			CRYSTAL_GUIRegion region = GetRegion(_tag);
//
//			if (region == null) {
//				_region.Tag = _tag;
//				regions.Add(_region);
//			} else {
//				regions[regions.IndexOf(region)] = _region;
//			}
//
//			return _region;
//		}
//
//		/// <summary>
//		/// Get a region reference from the GUI Script
//		/// </summary>
//		/// <param name="tag"></param>
//		/// <returns></returns>
//		protected CRYSTAL_GUIRegion GetRegion(string _tag) {
//			for (int i = 0; i < regions.Count; i++) {
//				if (regions[i].Tag == _tag) return regions[i];
//			}
//
//			return null;
//		}
//
//		/// <summary>
//		/// Remove a region reference from the GUI Script
//		/// </summary>
//		/// <param name="_tag"></param>
//		/// <returns></returns>
//		protected bool RemoveRegion(string _tag) {
//			CRYSTAL_GUIRegion region = GetRegion(_tag);
//
//			if (region != null) {
//				regions.Remove(region);
//				return true;
//			}
//
//			return false;
//		}
	}
}
