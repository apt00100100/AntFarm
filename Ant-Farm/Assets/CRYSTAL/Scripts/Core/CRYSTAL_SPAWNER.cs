using UnityEngine;
using System.Collections;

namespace CRYSTAL {

	public static class SPAWNER {

		/// <summary>
		/// Spawn the specified GameObject.
		/// </summary>
		/// <param name="_spawnObject">_spawn object.</param>
		/// <param name="_spawnLocation">_spawn location.</param>
		static public GameObject Spawn(GameObject _spawnObject, Vector3 _spawnLocation) {

			return Object.Instantiate(_spawnObject, _spawnLocation, Quaternion.identity) as GameObject;
		}
	}
}
