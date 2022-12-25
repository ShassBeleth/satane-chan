using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// ついてるオブジェクトが回転するコンポーネント
    /// </summary>
    public class SpinComponent : MonoBehaviour
    {
        /// <summary>
        /// 回転速度
        /// </summary>
        public float spinSpeed = 0f;

        void Update()
        {
            transform.Rotate(0.0f, 0.0f, spinSpeed, Space.Self);
        }
    }
}