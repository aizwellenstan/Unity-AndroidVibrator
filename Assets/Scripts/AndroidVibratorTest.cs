
#if DEBUG
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AndroidVibratorTest
{
    [UnityTest]
    public IEnumerator SimpleViberate()
    {
        yield return new MonoBehaviourTest<SimpleBehavior>();
    }

    [UnityTest]
    public IEnumerator PatternViberate()
    {
        yield return new MonoBehaviourTest<PatternBehavior>();
    }

    private class SimpleBehavior : MonoBehaviour, IMonoBehaviourTest
    {
        private bool _isFinish = false;
        public bool IsTestFinished { get { return _isFinish; } }

        IEnumerator Start()
        {
            yield return null;
#if UNITY_ANDROID
            Debug.Log("HasVibrator");
            if (Android.OS.Vibrator.HasVibrator)
            {
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Vibrate(500)");
                Android.OS.Vibrator.Vibrate(500);
            }
#endif //UNITY_ANDROID

            _isFinish = true;
        }

        private void OnDestroy()
        {
            Debug.Log("Dispose()");
            Android.OS.Vibrator.Dispose();
        }
    }

    private class PatternBehavior : MonoBehaviour, IMonoBehaviourTest
    {
        private bool _isFinish = false;
        public bool IsTestFinished { get { return _isFinish; } }

        IEnumerator Start()
        {
            yield return null;
#if UNITY_ANDROID
            Debug.Log("HasVibrator");
            if (Android.OS.Vibrator.HasVibrator)
            {
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Vibrate([400, 400, 800, 800], 1)");
                var pattern = new long[] { 400, 400, 800, 800 };
                Android.OS.Vibrator.Vibrate(pattern, 1);

                yield return new WaitForSeconds(10f);
                Debug.Log("Cancel()");
                Android.OS.Vibrator.Cancel();
            }
#endif //UNITY_ANDROID

            _isFinish = true;
        }

        private void OnDestroy()
        {
            Debug.Log("Dispose()");
            Android.OS.Vibrator.Dispose();
        }
    }
}
#endif //DEBUG
