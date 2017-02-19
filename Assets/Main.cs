/*------------------------------------------------------------*/
/// <summary>[Native Vibration Test] Main</summary>
/// <author>Seibe TAKAHASHI</author>
/// <remarks>
/// (c) 2017 Seibe TAKAHASHI.
/// This software is released under the MIT License.
/// http://opensource.org/licenses/mit-license.php
/// </remarks>
/*------------------------------------------------------------*/

using SeibeNativeExtension.Vibration;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        private void Start()
        {
            Vibrator.Vibrate(1000L);
        }
    }
}
