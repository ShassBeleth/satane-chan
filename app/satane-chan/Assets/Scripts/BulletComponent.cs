using UnityEngine;

/// <summary>
/// プレゼント弾用のコンポーネント
/// </summary>
public class BulletComponent : MonoBehaviour
{
    /// <summary>
    /// 左の見えない壁
    /// </summary>
    private float LEFT_INVISIBLE_WALL = -10f;
    /// <summary>
    /// 右の見えない壁
    /// </summary>
    private float RIGHT_INVISIBLE_WALL = 10f;
    /// <summary>
    /// 上の見えない壁
    /// </summary>
    private float UP_INVISIBLE_WALL = 10f;
    /// <summary>
    /// 下の見えない壁
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -10f;
    /// <summary>
    /// 射出角度
    /// </summary>
    public float Angle { set; get; }
    /// <summary>
    /// 移動速度
    /// </summary>
    private float MOVE_SPEED = 0.025f;
    /// <summary>
    /// 当たり判定半径
    /// </summary>
    public static float COLLISION_RADIUS = 0.5f;
    /// <summary>
    /// プレゼントが回転する
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform bulletGraphicTransform in this.transform)
        {
            bulletGraphicTransform.Rotate(0.0f, 0.0f, -0.1f, Space.Self);
        }
    }
    /// <summary>
    /// 移動
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
    /// 壁の外か判定する
    /// </summary>
    /// <returns>壁の外かどうか</returns>
    private bool IsOutSideWall()
    {
        if( this.transform.localPosition.x < LEFT_INVISIBLE_WALL) { return true; }
        if( RIGHT_INVISIBLE_WALL < this.transform.localPosition.x) { return true; }
        if(UP_INVISIBLE_WALL < this.transform.localPosition.y ) { return true; }
        if( this.transform.localPosition.y < DOWN_INVISIBLE_WALL) { return true; }
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
