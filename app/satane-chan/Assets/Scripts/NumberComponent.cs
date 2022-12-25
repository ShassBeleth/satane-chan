using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// 画像数値用スクリプト
/// </summary>
public class NumberComponent : MonoBehaviour
{
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManager;

    /// <summary>
    /// 数字を設定する
    /// </summary>
    /// <param name="number">指定数</param>
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
