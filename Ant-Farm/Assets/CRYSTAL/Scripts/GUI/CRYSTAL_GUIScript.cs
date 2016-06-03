using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public class CRYSTAL_GUIScript : CRYSTAL_Script {

		protected Rect Position;

		protected CRYSTAL_GUIScript caller;
		protected GUISkin CustomSkin;
		public bool RenderVisible;

		protected List<CRYSTAL_GUIButton> buttons;
		protected List<CRYSTAL_GUILabel> labels;
		protected List<CRYSTAL_GUIBox> boxes;
		
		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			this.buttons 	= new List<CRYSTAL_GUIButton>();
			this.labels 	= new List<CRYSTAL_GUILabel>();
			this.boxes 		= new List<CRYSTAL_GUIBox>();

			SetPosition();
		}

		protected virtual void SetPosition() {
			this.Position = new Rect(0, 0, Screen.width, Screen.height);
		}

		public override void Destroy (){
		
			this.ClearGUI ();

			base.Destroy ();
		}


		public override void StepGUI (){
			base.StepGUI ();
		
			if (this.RenderVisible) {
				Render ();
				Handle ();
			}
		}

		private void Render() {
			GUISkin oldSkin = GUI.skin;

			if (this.CustomSkin) {
				GUI.skin = this.CustomSkin;
			}

			GUI.BeginGroup(Position);
			this.RenderLogic();
			GUI.EndGroup();

			GUI.skin = oldSkin;
		}

		virtual protected void RenderLogic() {
			for (int i = 0; i < boxes.Count; i++) 	boxes[i].Render();
			for (int i = 0; i < buttons.Count; i++) buttons[i].Render();
			for (int i = 0; i < labels.Count; i++) 	labels[i].Render();
		}

		/// <summary>
		/// Handle all GUI controls here.
		/// </summary>
		virtual protected void Handle() { }
		
		/// <summary>
		/// Perform a check to see if the mouse is over a specified Rect
		/// </summary>
		/// <param name="_rect"></param>
		/// <returns>Boolean</returns>
		virtual protected bool MouseOverRect(Rect _rect) {
			return _rect.Contains(Input.mousePosition);
		}

		virtual protected void ClearGUI () {

			int i, n;
			for (i = 0, n = this.buttons.Count; i < n; i++)	if (buttons[i] != null)	buttons[i].Destroy ();
			for (i = 0, n = this.labels.Count;  i < n; i++)	if (labels[i]  != null)	labels[i].Destroy ();
			for (i = 0, n = this.boxes.Count;   i < n; i++)	if (boxes[i]   != null)	boxes[i].Destroy ();
			
			this.buttons.Clear ();
			this.labels.Clear ();
			this.boxes.Clear ();
		}

		#region ADD/GET/REMOVE BUTTON

		public CRYSTAL_GUIButton AddButton(string _tag, CRYSTAL_GUIButton _button) {
			CRYSTAL_GUIButton button = GetButton(_tag);

			if (button == null) {
				_button.Tag = _tag;
				buttons.Add(_button);
			} else {
				buttons[buttons.IndexOf(button)] = _button;
			}
			return _button;
		}
		public CRYSTAL_GUIButton GetButton(string _tag) {
			for (int i = 0; i < buttons.Count; i++) {
				if (buttons[i].Tag == _tag) return buttons[i];
			}
			return null;
		}
		public bool RemoveButton(string _tag) {
			CRYSTAL_GUIButton button = GetButton(_tag);

			if (button != null) {
				buttons.RemoveAt(buttons.IndexOf(button));
				return true;
			}
			return false;
		}

		#endregion

		#region ADD/GET/REMOVE LABEL

		public CRYSTAL_GUILabel AddLabel(string _tag, CRYSTAL_GUILabel _label) {
			CRYSTAL_GUILabel label = GetLabel(_tag);

			if (label == null) {
				_label.Tag = _tag;
				labels.Add(_label);
			} else {
				labels[labels.IndexOf(label)] = _label;
			}
			return _label;
		}
		public CRYSTAL_GUILabel GetLabel(string _tag) {
			for (int i = 0; i < labels.Count; i++) {
				if (labels[i].Tag == _tag) return labels[i];
			}
			return null;
		}
		public bool RemoveLabel(string _tag) {
			CRYSTAL_GUILabel label = GetLabel(_tag);

			if (label != null) {
				labels.RemoveAt(labels.IndexOf(label));
				return true;
			}
			return false;
		}

		#endregion

		#region ADD/GET/REMOVE BOX

		public CRYSTAL_GUIBox AddBox(string _tag, CRYSTAL_GUIBox _box) {
			CRYSTAL_GUIBox box = GetBox(_tag);

			if (box == null) {
				_box.Tag = _tag;
				boxes.Add(_box);
			} else {
				boxes[boxes.IndexOf(box)] = _box;
			}
			return _box;
		}
		public CRYSTAL_GUIBox GetBox(string _tag) {
			for (int i = 0; i < boxes.Count; i++) {
				if (boxes[i].Tag == _tag) return boxes[i];
			}
			return null;
		}
		public bool RemoveBox(string _tag) {
			CRYSTAL_GUIBox box = GetBox(_tag);

			if (box != null) {
				boxes.RemoveAt(boxes.IndexOf(box));
				return true;
			}
			return false;
		}

		#endregion

	}
}
