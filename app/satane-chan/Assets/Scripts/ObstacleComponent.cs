using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// ��Q���p�R���|�[�l���g
/// </summary>
public class ObstacleComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;
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
    /// ������]����
    /// </summary>
    private void TurnStar()
    {
        foreach (Transform starGraphicTransform in StartRotation)
        {
            starGraphicTransform.Rotate(0.0f, 0.0f, parameterManager.shootingStar.turnSpeed, Space.Self);
        }
    }
    /// <summary>
    /// �ǂ̊O�����肷��
    /// </summary>
    /// <returns>�ǂ̊O���ǂ���</returns>
    private bool IsOutSideWall()
    {
        if (this.transform.localPosition.x < parameterManager.shootingStarWall.leftInvisibleWall) { return true; }
        if (parameterManager.shootingStarWall.rightInvisibleWall < this.transform.localPosition.x) { return true; }
        if (parameterManager.shootingStarWall.upInvisibleWall < this.transform.localPosition.y) { return true; }
        if (this.transform.localPosition.y < parameterManager.shootingStarWall.downInvisibleWall) { return true; }
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
        this.transform.position = new Vector3(
            Random.Range(
                parameterManager.shootingStar.horizontalOccurPosMin,
                parameterManager.shootingStar.horizontalOccurPosMax
                ), 
            parameterManager.shootingStar.occurPosUp, 
            0f
            );
        angle = Random.Range(
            parameterManager.shootingStar.minAngle, 
            parameterManager.shootingStar.maxAngle
            );
        moveSpeed = Random.Range(
            parameterManager.shootingStar.minSpeed,
            parameterManager.shootingStar.maxSpeed
            );
    }

    void Update()
    {
        TurnStar();
        Move();
        if (IsOutSideWall())
        {
            Destroy(this.gameObject);
        }
    }
}
