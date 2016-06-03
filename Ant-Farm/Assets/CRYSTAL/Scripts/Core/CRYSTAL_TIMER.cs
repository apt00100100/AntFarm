using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CRYSTAL {

	public delegate void TimerCallbackDelegate (string _tag, object[] _timerArguments);
	
	public static class TIMER {

		private static List<TimerStats> p_Timers = new List<TimerStats>();

		/// <summary>
		/// Step the timer.
		/// </summary>
		public static void Step (){

			int i, n = p_Timers.Count;
			for (i = 0; i < n; ++i) {
				if (p_Timers[i].Update()) {

					if (p_Timers[i].RepeatTimer) {
						p_Timers[i].Start (true);
					}
					else {
						RemoveTimer(p_Timers[i]);
						i--;
						n = p_Timers.Count;
					}
				}
			}
		}

		/// <summary>
		/// Adds new timer to object.
		/// </summary>
		/// <returns>The timer.</returns>
		/// <param name="_tag">_tag.</param>
		/// <param name="_delayTime">_delay time.</param>
		/// <param name="_startNow">If set to <c>true</c> _start now.</param>
		/// <param name="_repeat">If set to <c>true</c> _repeat.</param>
		/// <param name="_callback">_callback.</param>
		/// <param name="_timerArguments">_timer arguments.</param>
		public static TimerStats AddTimer(string _tag, 
		                             double _delayTime, 
		                             bool _startNow = true, 
		                             bool _repeat = false, 
		                             TimerCallbackDelegate _callback = null, 
		                             params object[] _timerArguments) {

			if (!HasTimer(_tag)) {

				// Make the list of callback functions
				List<TimerCallbackDelegate> timerCallbackFunctions = new List<TimerCallbackDelegate> ();
				if (_callback != null) timerCallbackFunctions.Add(_callback);

				// Create the timer
				p_Timers.Add( new TimerStats(_tag + Time.time, _delayTime, _startNow, _repeat, timerCallbackFunctions, _timerArguments) );

				return p_Timers[p_Timers.Count - 1];
			}

			CONSOLE.Log(CONSOLE_LOG_TYPE.ERROR, "Cannot create timer: " + _tag + ". The Timer already exists.");
			return null;
		}

		public static void RemoveTimer(TimerStats _timer) {
			if (_timer != null) {
				_timer.Destroy();
				p_Timers.RemoveAt( p_Timers.IndexOf(_timer) );
			}
		}

		public static void RemoveTimer(string _tag) {
			TimerStats timer = GetTimer (_tag);
			RemoveTimer (timer);
		}

		public static TimerStats GetTimer(string _tag) {
			int i, n = p_Timers.Count;
			for (i = 0; i < n; ++i) if (p_Timers[i].Tag.Equals(_tag)) return p_Timers[i];
			return null;
		}

		public static bool HasTimer(string _tag) { return GetTimer (_tag) != null; }
	}



	/// ======>
	/// 
	/// Timer Stats Class
	/// 
	/// #################
	/// Start, Stop, and Update Timer
	/// Calls callback functions (w/ without argument list)
	/// 
	/// TODO:
 	/// ...
	/// 
	/// ======>
	#region Timer Stats Class

	public class TimerStats {

		public string Tag;
		public bool IsRunning;
		public double DelayTime;
		public bool RepeatTimer;
		private List<TimerCallbackDelegate> p_CallbackList;
		private object[] p_TimerArguments;
		private double p_DeltaTime;
		
		public TimerStats(string _tag, 
		                double _delayTime, 
		                bool _startNow = true, 
		                bool _repeat = false, 
		                List<TimerCallbackDelegate> _callbackList = null, 
		                params object[] _timerArguments) {

			this.Tag = _tag;
			this.DelayTime = _delayTime;
			this.IsRunning = _startNow;
			this.RepeatTimer = _repeat;
			this.p_CallbackList = _callbackList;
			this.p_TimerArguments = _timerArguments;
			this.p_DeltaTime = 0.0;
		}

		public void Destroy() {
			this.p_CallbackList.Clear();
			this.p_CallbackList = null;
		}

		public bool Update() {
			if (IsRunning) {
				this.p_DeltaTime += Time.deltaTime * 1000;

				if (this.p_DeltaTime >= this.DelayTime) {

					int i, n = this.p_CallbackList.Count;
					for (i = 0; i < n; ++i) {
						(this.p_CallbackList[i] as TimerCallbackDelegate)(this.Tag, this.p_TimerArguments);
					}

					return true;
				}
			}

			return false;
		}

		public void Start(bool _reset = false) {
			this.IsRunning = true;

			if (_reset) this.p_DeltaTime = 0.0;
		}

		public void Stop() {
			this.IsRunning = false;
		}
    }

	#endregion
}

