using UnityEngine;

/// <summary>
/// HP�p�R���|�[�l���g
/// </summary>
public class HpComponent : MonoBehaviour
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    private int MAX_HP = 6;
    /// <summary>
    /// HP1�̃n�[�g���^���I�u�W�F�N�g
    /// </summary>
    public GameObject Hp1FullHeartGameObject;
    /// <summary>
    /// HP1�̃n�[�g�����I�u�W�F�N�g
    /// </summary>
    public GameObject Hp1HalfHeartGameObject;
    /// <summary>
    /// HP2�̃n�[�g���^���I�u�W�F�N�g
    /// </summary>
    public GameObject Hp2FullHeartGameObject;
    /// <summary>
    /// HP2�̃n�[�g�����I�u�W�F�N�g
    /// </summary>
    public GameObject Hp2HalfHeartGameObject;
    /// <summary>
    /// HP3�̃n�[�g���^���I�u�W�F�N�g
    /// </summary>
    public GameObject Hp3FullHeartGameObject;
    /// <summary>
    /// HP3�̃n�[�g�����I�u�W�F�N�g
    /// </summary>
    public GameObject Hp3HalfHeartGameObject;

    /// <summary>
    /// ���݂�HP
    /// </summary>
    public int Hp { private set; get; } = int.MaxValue;
    /// <summary>
    /// HP���Z�b�g
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
    /// HP��ݒ肷��
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
