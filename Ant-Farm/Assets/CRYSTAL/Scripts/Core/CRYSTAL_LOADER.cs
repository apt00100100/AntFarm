using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.IO;

namespace CRYSTAL {

	public delegate void LoadCallback (object _data);
	public delegate void SaveCallback (bool _success);

	public static class IO {

		#region Generic Loaders

		public static Texture LoadTexture(string _fileName) {
			return Resources.Load<Texture>(_fileName);
		}
		public static Texture2D LoadTexture2D(string _fileName) {
			return Resources.Load<Texture2D>(_fileName);
		}
		public static GUISkin LoadGUISkin(string _fileName) {
			return Resources.Load<GUISkin>(_fileName);
		}
		public static GameObject LoadGameObject(string _fileName) {
			return Resources.Load<GameObject>(_fileName);
		}

		#endregion

		#region Save Data

		/// <summary>
		/// Save the data with a specified file name.
		/// </summary>
		/// <param name="_objToSave">_obj to save.</param>
		/// <param name="_fileName">_file name.</param>
		public static void Save(object _objToSave, string _fileName, SaveCallback _callback = null) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + _fileName);
			bool isSuccessful = true;
			
			try {
				bf.Serialize (file, _objToSave);
			}
			catch (System.ArgumentNullException _anExc) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "NULL:", _anExc.Message);
				isSuccessful = false;
			}
			catch (EndOfStreamException _eofExc) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "End Of File:", _eofExc.Message);
				isSuccessful = false;
			}
			catch (SerializationException _serExc) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "Serialize:", _serExc.Message);
				isSuccessful = false;
			}
			catch(SecurityException _secExc) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "Security:", _secExc.Message);
				isSuccessful = false;
			}
			catch (IOException _ioExc) {
				CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "IO:", _ioExc.Message);
				isSuccessful = false;
			}
			finally {
				file.Close ();

				if (_callback != null) _callback(isSuccessful);
			}
		}

		#endregion

		#region Load Data

		/// <summary>
		/// Load data with a specified file name.
		/// </summary>
		/// <param name="_fileName">_file name.</param>
		public static void Load(string _fileName, LoadCallback _callback = null ) {
			object data = null;

			if (_fileName != null && File.Exists (Application.persistentDataPath + _fileName)) {
				
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + _fileName, FileMode.Open);

				try {
					data = bf.Deserialize (file);
				}
				catch(System.ArgumentNullException _anExc) {
					CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "NULL:", _anExc.Message);
				}
				catch(SecurityException _secExc) {
					CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "Security:", _secExc.Message);
				}
				catch (EndOfStreamException _eofExc) {
					CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "End Of File:", _eofExc.Message);
				}
				catch (SerializationException _serExc) {
					CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "Serialize:", _serExc.Message);
				}
				catch (IOException _exc) {
					CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "IO:", _exc.Message);
				}
				finally {
					file.Close ();

					if (_callback != null) _callback(data);
				}
			}
			else if (_callback != null) _callback(null);
		}

		#endregion

	}
}
