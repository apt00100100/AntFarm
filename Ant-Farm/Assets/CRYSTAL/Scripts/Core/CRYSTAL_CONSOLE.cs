using UnityEngine;
using System.Collections.Generic;

namespace CRYSTAL {

	/// <summary>
	/// Enum values correspond with colors in list.
	/// </summary>
	public enum CONSOLE_LOG_TYPE {
		EDITOR 		= 0,	//BLACK
		ERROR		= 1,	//RED
		WARNING		= 2,	//ORANGE
		LOADDATA	= 3,	//BLUE
		SAVEDATA	= 4,	//LIGHTBLUE
		DELETEDATA	= 5,	//FUSCIA
		INITIALIZE	= 6,	//LIGHTPURPLE
		INPUT		= 7,	//GREEN
		CLONE		= 8		//DARKBLUE
	};

	public static class CONSOLE {

		public static bool ACTIVE = true;

		private static List<string> colors = new List<string>() {
			"000000",	//BLACK			->	EDITOR
			"FF0000",	//RED			->	ERROR
			"FF9933",	//ORANGE		->	WARNING
			"0000CC",	//BLUE			->	LOADDATA
			"0099FF",	//LIGHTBLUE		->	SAVEDATA
			"cc0066",	//FUSCIA		->	DELETEDATA
			"9900FF",	//LIGHTPURPLE	->	INITIALIZE
			"007e40",	//GREEN			->	INPUT
			"000066"	//DARK BLUE		->	CLONE
		};

		#region Log

		public static void Log(CONSOLE_LOG_TYPE _type, string _message) {
			
			//	If the CONSOLE is not active, we need to return
			if (!ACTIVE) {
				Debug.Log("LOGGING DISABLED: Turn this log off when you get this shit working!! T_T");
				return;
			}
			
			string color	= colors[(int)_type];
			string result	=	
				"<color=#"+colors[0]+">CRYSTAL [</color>" +
				"<color=#"+color+">"+_type.ToString()+"</color>" +
				"<color=#"+colors[0]+">] ||==>\t</color>" +
				"<color=#"+color+">"+_message+"</color>";

			if (_type == CONSOLE_LOG_TYPE.ERROR)		Debug.LogError(result);
			else if (_type == CONSOLE_LOG_TYPE.WARNING)	Debug.LogWarning(result);
			else										Debug.Log(result);
		}

		public static void Log(string _message)									{ Log(CONSOLE_LOG_TYPE.EDITOR, _message);	}
		public static void Log(params object[] _args)							{ Log(CONSOLE_LOG_TYPE.EDITOR, _args);		}
		public static void Log(CONSOLE_LOG_TYPE _type, params object[] _args) 	{ 
			int i, n = _args.Length;
			string message = string.Empty;
			
			for (i = 0; i < n; ++i) {
				if (_args[i] != null) {
					message += _args[i].ToString() + '\t';
				} else {
					message += "NULL";
				}
			}
			Log (_type, message);
		}

		#endregion
	}
}