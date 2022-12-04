using UnityEngine;

/// <summary>
/// �z�������o���p�X�N���v�g
/// </summary>
public class ImaginationComponent : MonoBehaviour
{
    /// <summary>
    /// �o���ʒu�Ő��[
    /// </summary>
    private float WESTERNMOST = -10f;
    /// <summary>
    /// �o���ʒu�œ��[
    /// </summary>
    private float EASTERNMOST = 10f;
    /// <summary>
    /// �o���ʒu�Ŗk�[
    /// </summary>
    private float NORTHERNMOST = 10f;
    /// <summary>
    /// �o���ʒu�œ�[
    /// </summary>
    private float SOUTHERNMOST = -10f;

    /// <summary>
    /// ��������
    /// </summary>
    public void BreakStar()
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(WESTERNMOST, EASTERNMOST), Random.Range(SOUTHERNMOST, NORTHERNMOST), 0f);
    }
}
