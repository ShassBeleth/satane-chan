using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// プレゼント弾用のコンポーネント
/// </summary>
public class BulletComponent : MonoBehaviour
{
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// 射出角度
    /// </summary>
    public float Angle { set; get; }
    /// <summary>
    /// 射出時のプレイヤーの座標
    /// </summary>
    public Vector3 shotPlayerPosition { set; get; }
    /// <summary>
    /// プレゼントが回転する
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform bulletGraphicTransform in this.transform)
        {
            bulletGraphicTransform.Rotate(0.0f, 0.0f, parameterManager.bullet.turnSpeed, Space.Self);
        }
    }
    /// <summary>
    /// 移動
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
    /// 壁の外か判定する
    /// </summary>
    /// <returns>壁の外かどうか</returns>
    private bool IsOutSideWall()
    {
        if( this.transform.localPosition.x < parameterManager.bulletWall.leftInvisibleWall) { return true; }
        if(parameterManager.bulletWall.rightInvisibleWall < this.transform.localPosition.x) { return true; }
        if(parameterManager.bulletWall.upInvisibleWall < this.transform.localPosition.y ) { return true; }
        if( this.transform.localPosition.y < parameterManager.bulletWall.downInvisibleWall) { return true; }
        return false;
    }
    /// <summary>
    /// 弾が壊れる
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
