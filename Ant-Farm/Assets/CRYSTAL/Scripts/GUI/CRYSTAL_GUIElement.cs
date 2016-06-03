using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public enum CONTENT_STATE { TEXT, IMAGE, CONTENT }
	public delegate void ButtonCallbackDelegate(CRYSTAL_GUIButton button);

	public class CRYSTAL_GUIElement {

		protected static uint ID_COUNT = 0;
		private uint p_ID;
		
		public string Tag {get; set;}
		protected bool RenderVisible;

		public CRYSTAL_GUIElement() { SetDefaultValues(); }

		virtual protected void SetDefaultValues() {
			p_ID = ID_COUNT++;
			RenderVisible = true;
		}
		virtual public void Destroy() {}

		virtual public void Render() {
			if (RenderVisible) RenderLogic();
		}
		virtual protected void RenderLogic() { }

		public uint ID { get { return p_ID; } }
	}

	#region GUI CONTROL

	public class CRYSTAL_GUIControl : CRYSTAL_GUIElement {
		public GUIContent content;
		public GUIStyle style;
		public CONTENT_STATE state;
		public Rect position;

		protected GUISkin MainSkin;
		protected uint SkinIndex;

		virtual public CRYSTAL_GUIControl Initialize(Rect _position, GUIContent _content, GUIStyle _style = null, CONTENT_STATE _state = CONTENT_STATE.CONTENT) {
			position = _position;
			content = _content;
			style = _style;
			state = _state;
			MainSkin = null;

			return this;
		}

		public override void Destroy() {
			base.Destroy();
			
			content = null;
			style = null;
		}

		protected override void RenderLogic() {
//			if (MainSkin && MainSkin.customStyles.Length > SkinIndex) {
//				GUI.skin = MainSkin.customStyles[SkinIndex];
//			}

			switch (state) {
				case CONTENT_STATE.CONTENT: RenderStateContent(); break;
				case CONTENT_STATE.IMAGE: RenderStateImage(); break;
				case CONTENT_STATE.TEXT: RenderStateText(); break;
			}
		}

//		virtual public void SetSkin(GUISkin _mainSkin, uint _skinIndex) {
//			MainSkin = _mainSkin;
//			SkinIndex = _skinIndex;
//		}


		virtual protected void RenderStateContent() { }
		virtual protected void RenderStateImage() { }
		virtual protected void RenderStateText() { }
	}

	#endregion

	#region GUI BUTTON

	/// <summary>
	/// 
	/// </summary>
	public class CRYSTAL_GUIButton : CRYSTAL_GUIControl {

		private ButtonCallbackDelegate p_callback;
		private bool p_repeat;

		public CRYSTAL_GUIButton(ButtonCallbackDelegate _callback, bool _repeat = false) : base() { 
			p_callback = _callback; 
			p_repeat = _repeat;
		}

		public override void Destroy() {
			base.Destroy();

			p_callback = null;
		}

		//protected override void RenderLogic() {
		//    if (style == null && GUI.skin.button != null) {
		//        style = GUI.skin.button;
		//    } else {
		//        GUIScript.print("NO STYLE FOR BUTTON");
		//    }
		//    base.RenderLogic();
		//}

		protected override void RenderStateContent() {
			base.RenderStateContent();

			if (p_repeat) {
				if (GUI.RepeatButton(position, content, (style != null ? style : "button"))) { p_callback(this); }
			} else {
				if (GUI.Button(position, content, (style != null ? style : "button"))) { p_callback(this); }
			}
		}

		protected override void RenderStateImage() {
			base.RenderStateImage();

			if (p_repeat) {
				if (GUI.RepeatButton(position, content.image, (style != null ? style : "button"))) { p_callback(this); }
			} else {
				if (GUI.Button(position, content.image, (style != null ? style : "button"))) { p_callback(this); }
			}
		}

		protected override void RenderStateText() {
			base.RenderStateText();
			if (p_repeat) {
				if (GUI.RepeatButton(position, content.text, (style != null ? style : "button"))) { p_callback(this); }
			} else {
				if (GUI.Button(position, content.text, (style != null ? style : "button"))) { p_callback(this); }
			}
		}
	}

	#endregion

	#region GUI LABEL

	/// <summary>
	/// 
	/// </summary>
	public class CRYSTAL_GUILabel : CRYSTAL_GUIControl {

		//protected override void RenderLogic() {
		//    if (style == null) style = GUI.skin.label;
		//    base.RenderLogic();
		//}

		protected override void RenderStateContent() {
			base.RenderStateContent();
			GUI.Label(position, content, (style != null ? style : "label"));
		}

		protected override void RenderStateImage() {
			base.RenderStateImage();
			GUI.Label(position, content.image, (style != null ? style : "label"));
		}

		protected override void RenderStateText() {
			base.RenderStateText();
			GUI.Label(position, content.text, (style != null ? style : "label"));
		}
	}

	#endregion

	#region GUI BOX

	public class CRYSTAL_GUIBox : CRYSTAL_GUIControl {

		//protected override void RenderLogic() {
		//    if (style == null) style = GUI.skin.box;
		//    base.RenderLogic();
		//}

		protected override void RenderStateContent() {
			base.RenderStateContent();
			GUI.Box(position, content, (style != null ? style : "box"));
		}

		protected override void RenderStateImage() {
			base.RenderStateImage();
			GUI.Box(position, content.image, (style != null ? style : "box"));
		}

		protected override void RenderStateText() {
			base.RenderStateText();
			GUI.Box(position, content.text, (style != null ? style : "box"));
		}
	}

	#endregion

}