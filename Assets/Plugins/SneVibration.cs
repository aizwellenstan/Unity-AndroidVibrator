/*------------------------------------------------------------*/
/// <summary>[Native Vibration Test] Main</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2017 Seibe TAKAHASHI.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

#if UNITY_ANDROID && !UNITY_EDITOR || true
#define UNITY_ANDROID_NATIVE
#endif

namespace SeibeNativeExtension.Vibration
{
    /// <summary>
    /// 振動API
    /// </summary>
    public static class Vibrator
    {
        /// <summary>
        /// 0.4 秒間、端末を振動させます。
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Vibrate()
        {
#if UNITY_ANDROID_NATIVE
            if (sVibrator != null)
            {
                sVibrator.Call("vibrate", 400L);
            }
#endif
        }

        /// <summary>
        /// 指定した秒数、端末を振動させます。
        /// </summary>
        /// <param name="milliseconds">振動時間 (ミリ秒)</param>
        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Vibrate(long milliseconds)
        {
#if UNITY_ANDROID_NATIVE
            if (sVibrator != null)
            {
                sVibrator.Call("vibrate", milliseconds);
            }
#endif
        }

        /// <summary>
        /// 振動に対応しているかどうか
        /// </summary>
        public static bool HasVibrator
        {
#if UNITY_ANDROID_NATIVE
            get
            {
                if (sVibrator == null) return false;
                return sVibrator.Call<bool>("hasVibrator");
            }
#else
            get { return false; }
#endif
        }

#if UNITY_ANDROID_NATIVE
        static Vibrator()
        {
            findVibrator();
        }

        private static void findVibrator()
        {
            using (var UnityPlayer = new UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = UnityPlayer.GetStatic<UnityEngine.AndroidJavaObject>("currentActivity"))
            {
                sVibrator = currentActivity.Call<UnityEngine.AndroidJavaObject>("getSystemService", "vibrator");
            }
        }

        private static UnityEngine.AndroidJavaObject sVibrator;
        private static bool sHasVibrator;
#endif
    }
}
