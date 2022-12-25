using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// 障害物用コンポーネント
/// </summary>
public class ObstacleComponent : MonoBehaviour
{
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// 射出角度
    /// </summary>
    private float angle;
    /// <summary>
    /// 移動速度
    /// </summary>
    private float moveSpeed;

    /// <summary>
    /// 星の回転
    /// </summary>
    public Transform StartRotation;

    /// <summary>
    /// 移動
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
    /// 星が回転する
    /// </summary>
    private void TurnStar()
    {
        foreach (Transform starGraphicTransform in StartRotation)
        {
            starGraphicTransform.Rotate(0.0f, 0.0f, parameterManager.shootingStar.turnSpeed, Space.Self);
        }
    }
    /// <summary>
    /// 壁の外か判定する
    /// </summary>
    /// <returns>壁の外かどうか</returns>
    private bool IsOutSideWall()
    {
        if (this.transform.localPosition.x < parameterManager.shootingStarWall.leftInvisibleWall) { return true; }
        if (parameterManager.shootingStarWall.rightInvisibleWall < this.transform.localPosition.x) { return true; }
        if (parameterManager.shootingStarWall.upInvisibleWall < this.transform.localPosition.y) { return true; }
        if (this.transform.localPosition.y < parameterManager.shootingStarWall.downInvisibleWall) { return true; }
        return false;
    }
    /// <summary>
    /// 星が壊れる
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
