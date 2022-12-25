using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// �摜���l�p�X�N���v�g
/// </summary>
public class NumberComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;

    /// <summary>
    /// ������ݒ肷��
    /// </summary>
    /// <param name="number">�w�萔</param>
    public void SetNumber(int number)
    {
        int digit = parameterManager.number.digit;
        foreach (Transform maskTransform in this.gameObject.transform)
        {
            int displayNumber = number / digit;
            foreach (RectTransform imageTransform in maskTransform)
            {
                Vector3 position = imageTransform.localPosition;
                position.y = parameterManager.number.numberPosition[displayNumber];
                imageTransform.localPosition = position;
            }
            number %= digit;
            digit /= 10;
        }
    }
}
