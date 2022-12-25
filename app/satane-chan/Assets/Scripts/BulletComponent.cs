using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// �v���[���g�e�p�̃R���|�[�l���g
/// </summary>
public class BulletComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// �ˏo�p�x
    /// </summary>
    public float Angle { set; get; }
    /// <summary>
    /// �ˏo���̃v���C���[�̍��W
    /// </summary>
    public Vector3 shotPlayerPosition { set; get; }
    /// <summary>
    /// �v���[���g����]����
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform bulletGraphicTransform in this.transform)
        {
            bulletGraphicTransform.Rotate(0.0f, 0.0f, parameterManager.bullet.turnSpeed, Space.Self);
        }
    }
    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        Vector3 dt = Vector3.zero;
        float rad = Angle * Mathf.Deg2Rad;
        dt.x = Mathf.Cos(rad) * parameterManager.bullet.moveSpeed;
        dt.y = Mathf.Sin(rad) * parameterManager.bullet.moveSpeed;
        transform.localPosition += dt;
    }
    /// <summary>
    /// �ǂ̊O�����肷��
    /// </summary>
    /// <returns>�ǂ̊O���ǂ���</returns>
    private bool IsOutSideWall()
    {
        if( this.transform.localPosition.x < parameterManager.bulletWall.leftInvisibleWall) { return true; }
        if(parameterManager.bulletWall.rightInvisibleWall < this.transform.localPosition.x) { return true; }
        if(parameterManager.bulletWall.upInvisibleWall < this.transform.localPosition.y ) { return true; }
        if( this.transform.localPosition.y < parameterManager.bulletWall.downInvisibleWall) { return true; }
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
