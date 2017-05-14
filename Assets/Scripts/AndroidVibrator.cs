
#if UNITY_ANDROID && !UNITY_EDITOR
#define UNITY_ANDROID_NATIVE
#endif //UNITY_ANDROID && !UNITY_EDITOR

using UnityEngine;
using System;

namespace Android.OS
{
    /// <summary>
    /// android.os.Vibrator
    /// </summary>
    public static class Vibrator
    {
#if UNITY_ANDROID_NATIVE
        private static AndroidJavaObject _vibrator = null;
        private static IntPtr _methodPtr_Cancel = IntPtr.Zero;
        private static IntPtr _methodPtr_HasVibrator = IntPtr.Zero;
        private static IntPtr _methodPtr_Vibrate1 = IntPtr.Zero;
        private static IntPtr _methodPtr_Vibrate2 = IntPtr.Zero;
        private static bool? _hasVibrator = null;
#endif //UNITY_ANDROID_NATIVE

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Initialize()
        {
#if UNITY_ANDROID_NATIVE
            using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = player.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                _vibrator = activity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                if (_vibrator != null)
                {
                    var objPtr = _vibrator.GetRawObject();
                    _methodPtr_Cancel = AndroidJNI.GetMethodID(objPtr, "cancel", "()Z");
                    _methodPtr_HasVibrator = AndroidJNI.GetMethodID(objPtr, "hasVibrator", "()Z");
                    _methodPtr_Vibrate1 = AndroidJNI.GetMethodID(objPtr, "vibrate", "(J)V");
                    _methodPtr_Vibrate2 = AndroidJNI.GetMethodID(objPtr, "vibrate", "([JI)V");
                }
            }
#endif //UNITY_ANDROID_NATIVE
        }

        public static bool IsInitialized
        {
#if UNITY_ANDROID_NATIVE
            get { return (_vibrator != null); }
#else
            get { return false; }
#endif //UNITY_ANDROID_NATIVE
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Dispose()
        {
#if UNITY_ANDROID_NATIVE
            if (_vibrator != null)
            {
                _vibrator.Dispose();
                _vibrator = null;
            }
#endif //UNITY_ANDROID_NATIVE
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Cancel()
        {
#if UNITY_ANDROID_NATIVE
            if (!IsInitialized) Initialize();
            if (_vibrator != null && _methodPtr_Cancel != IntPtr.Zero)
            {
                AndroidJNI.CallVoidMethod(_vibrator.GetRawObject(), _methodPtr_Cancel, null);
            }
#endif //UNITY_ANDROID_NATIVE
        }

        public static bool HasVibrator
        {
            get
            {
#if UNITY_ANDROID_NATIVE
                if (!_hasVibrator.HasValue)
                {
                    if (!IsInitialized) Initialize();
                    if (_vibrator != null && _methodPtr_HasVibrator != IntPtr.Zero)
                    {
                        _hasVibrator = AndroidJNI.CallBooleanMethod(_vibrator.GetRawObject(), _methodPtr_HasVibrator, null);
                    }
                }
                return _hasVibrator.Value;
#else
                return false;
#endif //UNITY_ANDROID_NATIVE
            }
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Vibrate(long milliseconds)
        {
#if UNITY_ANDROID_NATIVE
            if (!IsInitialized) Initialize();
            if (_vibrator != null && _methodPtr_Vibrate1 != IntPtr.Zero)
            {
                AndroidJNI.CallVoidMethod(_vibrator.GetRawObject(), _methodPtr_Vibrate1, new[] { new jvalue() { j = milliseconds } });
            }
#endif //UNITY_ANDROID_NATIVE
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        public static void Vibrate(long[] pattern, int repeat)
        {
#if UNITY_ANDROID_NATIVE
            if (!IsInitialized) Initialize();
            if (_vibrator != null && _methodPtr_Vibrate1 != IntPtr.Zero)
            {
                var patternPtr = AndroidJNI.ToLongArray(pattern);
                AndroidJNI.CallVoidMethod(_vibrator.GetRawObject(), _methodPtr_Vibrate2, new[] {
                    new jvalue() { l = patternPtr },
                    new jvalue() { i = repeat }
                });
                AndroidJNI.DeleteLocalRef(patternPtr);
            }
#endif //UNITY_ANDROID_NATIVE
        }
    }
}
