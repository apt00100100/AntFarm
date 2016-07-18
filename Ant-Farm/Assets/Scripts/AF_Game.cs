using UnityEngine;
using System.Collections;
using CRYSTAL;

namespace AntFarm {

	public class AF_Game : CRYSTAL_Game {

		public override void SetDefaultValues (){
			base.SetDefaultValues ();

			AF_STATIC.FloodFill.CalculateMap ();
		}
	}
}
