using UnityEngine;
using System.Collections;

namespace AntFarm {
	
	public static class AF_STATIC {

		public static GameObject Player {
			get {
				return GameObject.FindGameObjectWithTag("Player");
			}
		}

		public static AF_Game Game {
			get {
				return (GameObject.FindGameObjectWithTag ("Game") as GameObject).GetComponent<AF_Game> ();
			}
		}

		public static AF_FloodFill FloodFill {
			get { 
				return GameObject.Find ("FloodFill").GetComponent<AF_FloodFill> (); 
			}
		}

		public static GUIStyle GetStyleFromSkin (GUISkin _skin, string _styleName) {

			if (_skin == null || _styleName == string.Empty) return null;

			GUIStyle[] styles = _skin.customStyles;
			int i, n = styles.Length;
			for (i = 0; i < n; ++i) {
				if (styles[i].name == _styleName) return styles[i];
			}

			return null;
		}

		/// <summary>
		/// Get a float representing a percentage of the screen width
		/// </summary>
		/// <param name="_percent">Percent from 0-1</param>
		/// <returns></returns>
		public static float PercentWidth(float _percent) { return Screen.width * _percent; }

		/// <summary>
		/// Get a float representing a percentage of the screen height
		/// </summary>
		/// <param name="_percent">Percent from 0-1</param>
		/// <returns></returns>
		public static float PercentHeight(float _percent) { return Screen.height * _percent; }

		/// <summary>
		/// Get a Rect in screen percentages
		/// </summary>
		/// <param name="_xPercent">0-1</param>
		/// <param name="_yPercent">0-1</param>
		/// <param name="_wPercent">0-1</param>
		/// <param name="_hPercent">0-1</param>
		/// <returns></returns>
		public static Rect PercentRect(float _xPercent, float _yPercent, float _wPercent, float _hPercent) {
			return new Rect(PercentWidth(_xPercent), PercentHeight(_yPercent), PercentWidth(_wPercent), PercentHeight(_hPercent));
		}

		public static Vector3 PercentVector3(float _xPercent, float _yPercent, float _wPercent, float _hPercent) {
			float height = 2 * Camera.main.orthographicSize;
			float width = height * Camera.main.aspect;
			Vector3 vector = Vector3.zero;

			vector.x = width * _xPercent; // / PercentWidth(_xPercent);
			vector.y = height * _yPercent; // / PercentHeight(_yPercent);

			return vector;
		}
	}
}