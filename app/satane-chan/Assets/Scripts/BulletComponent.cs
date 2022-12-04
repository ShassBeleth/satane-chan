using UnityEngine;

/// <summary>
/// �v���[���g�e�p�̃R���|�[�l���g
/// </summary>
public class BulletComponent : MonoBehaviour
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
    private float UP_INVISIBLE_WALL = 10f;
    /// <summary>
    /// ���̌����Ȃ���
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -10f;
    /// <summary>
    /// �ˏo�p�x
    /// </summary>
    public float Angle { set; get; }
    /// <summary>
    /// �ړ����x
    /// </summary>
    private float MOVE_SPEED = 0.025f;
    /// <summary>
    /// �����蔻�蔼�a
    /// </summary>
    public static float COLLISION_RADIUS = 0.5f;
    /// <summary>
    /// �v���[���g����]����
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform bulletGraphicTransform in this.transform)
        {
            bulletGraphicTransform.Rotate(0.0f, 0.0f, -0.1f, Space.Self);
        }
    }
    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        Vector3 dt = Vector3.zero;
        float rad = Angle * Mathf.Deg2Rad;
        dt.x = Mathf.Cos(rad) * MOVE_SPEED;
        dt.y = Mathf.Sin(rad) * MOVE_SPEED;
        transform.localPosition += dt;
    }
    /// <summary>
    /// �ǂ̊O�����肷��
    /// </summary>
    /// <returns>�ǂ̊O���ǂ���</returns>
    private bool IsOutSideWall()
    {
        if( this.transform.localPosition.x < LEFT_INVISIBLE_WALL) { return true; }
        if( RIGHT_INVISIBLE_WALL < this.transform.localPosition.x) { return true; }
        if(UP_INVISIBLE_WALL < this.transform.localPosition.y ) { return true; }
        if( this.transform.localPosition.y < DOWN_INVISIBLE_WALL) { return true; }
        return false;
    }
    /// <summary>
    /// �e������
    /// </summary>
    public void BreakBullet()
    {
        Destroy(this.gameObject);
    }
    void Update()
    {
        TurnPresent();
        Move();
        if( IsOutSideWall())
        {
            BreakBullet();
        }
    }
}
