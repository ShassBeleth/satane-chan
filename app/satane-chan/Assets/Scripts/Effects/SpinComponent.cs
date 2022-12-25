using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// ���Ă�I�u�W�F�N�g����]����R���|�[�l���g
    /// </summary>
    public class SpinComponent : MonoBehaviour
    {
        /// <summary>
        /// ��]���x
        /// </summary>
        public float spinSpeed = 0f;

        void Update()
        {
            transform.Rotate(0.0f, 0.0f, spinSpeed, Space.Self);
        }
    }
}