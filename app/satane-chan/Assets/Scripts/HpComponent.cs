using UnityEngine;

/// <summary>
/// HP用コンポーネント
/// </summary>
public class HpComponent : MonoBehaviour
{
    /// <summary>
    /// 最大HP
    /// </summary>
    private int MAX_HP = 6;
    /// <summary>
    /// HP1のハート満タンオブジェクト
    /// </summary>
    public GameObject Hp1FullHeartGameObject;
    /// <summary>
    /// HP1のハート半分オブジェクト
    /// </summary>
    public GameObject Hp1HalfHeartGameObject;
    /// <summary>
    /// HP2のハート満タンオブジェクト
    /// </summary>
    public GameObject Hp2FullHeartGameObject;
    /// <summary>
    /// HP2のハート半分オブジェクト
    /// </summary>
    public GameObject Hp2HalfHeartGameObject;
    /// <summary>
    /// HP3のハート満タンオブジェクト
    /// </summary>
    public GameObject Hp3FullHeartGameObject;
    /// <summary>
    /// HP3のハート半分オブジェクト
    /// </summary>
    public GameObject Hp3HalfHeartGameObject;

    /// <summary>
    /// 現在のHP
    /// </summary>
    public int Hp { private set; get; } = int.MaxValue;
    /// <summary>
    /// HPリセット
    /// </summary>
    public void Reset()
    {
        Hp = MAX_HP;

        Hp3FullHeartGameObject.SetActive(true);
        Hp3HalfHeartGameObject.SetActive(true);
        Hp2FullHeartGameObject.SetActive(true);
        Hp2HalfHeartGameObject.SetActive(true);
        Hp1FullHeartGameObject.SetActive(true);
        Hp1HalfHeartGameObject.SetActive(true);
    }
    /// <summary>
    /// HPを設定する
    /// </summary>
    public void Damage()
    {
        Hp--;

        Hp3FullHeartGameObject.SetActive(6 <= Hp);
        Hp3HalfHeartGameObject.SetActive(5 <= Hp);

        Hp2FullHeartGameObject.SetActive(4 <= Hp);
        Hp2HalfHeartGameObject.SetActive(3 <= Hp);

        Hp1FullHeartGameObject.SetActive(2 <= Hp);
        Hp1HalfHeartGameObject.SetActive(1 <= Hp);
    }
}
