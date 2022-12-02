using UnityEngine;

/// <summary>
/// ��Q���p�R���|�[�l���g
/// </summary>
public class ObstacleComponent : MonoBehaviour
{

    /// <summary>
    /// ���̌����Ȃ���
    /// </summary>
    private float LEFT_INVISIBLE_WALL = -10f;
    /// <summary>
    /// �E�̌����Ȃ���
    /// </summary>
    private float RIGHT_INVISIBLE_WALL = 10f;
    /// <summary>
    /// ��̌����Ȃ���
    /// </summary>
    private float UP_INVISIBLE_WALL = 7f;
    /// <summary>
    /// ���̌����Ȃ���
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -10f;
    /// <summary>
    /// �ˏo�p�x
    /// </summary>
    private float angle;
    /// <summary>
    /// �ړ����x
    /// </summary>
    private float moveSpeed;

    /// <summary>
    /// ���̉�]
    /// </summary>
    public Transform StartRotation;

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        Vector3 dt = Vector3.zero;
        float rad = angle * Mathf.Deg2Rad;
        dt.x = Mathf.Cos(rad) * moveSpeed;
        dt.y = Mathf.Sin(rad) * moveSpeed;
        transform.localPosition += dt;
    }
    /// <summary>
    /// �v���[���g����]����
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform starGraphicTransform in StartRotation)
        {
            starGraphicTransform.Rotate(0.0f, 0.0f, 0.3f, Space.Self);
        }
    }
    /// <summary>
    /// �ǂ̊O�����肷��
    /// </summary>
    /// <returns>�ǂ̊O���ǂ���</returns>
    private bool IsOutSideWall()
    {
        if (this.transform.localPosition.x < LEFT_INVISIBLE_WALL) { return true; }
        if (RIGHT_INVISIBLE_WALL < this.transform.localPosition.x) { return true; }
        if (UP_INVISIBLE_WALL < this.transform.localPosition.y) { return true; }
        if (this.transform.localPosition.y < DOWN_INVISIBLE_WALL) { return true; }
        return false;
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void BreakStar()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-9f, 9f), 6f, 0f);
        angle = Random.Range(210f, 330f);
        moveSpeed = Random.Range(0.01f, 0.03f);
    }

    void Update()
    {
        TurnPresent();
        Move();
        if (IsOutSideWall())
        {
            Destroy(this.gameObject);
        }
    }
}
