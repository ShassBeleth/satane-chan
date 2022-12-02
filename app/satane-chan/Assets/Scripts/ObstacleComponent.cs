using UnityEngine;

/// <summary>
/// 障害物用コンポーネント
/// </summary>
public class ObstacleComponent : MonoBehaviour
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
    private float UP_INVISIBLE_WALL = 7f;
    /// <summary>
    /// 下の見えない壁
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -10f;
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
    /// プレゼントが回転する
    /// </summary>
    private void TurnPresent()
    {
        foreach (Transform starGraphicTransform in StartRotation)
        {
            starGraphicTransform.Rotate(0.0f, 0.0f, 0.3f, Space.Self);
        }
    }
    /// <summary>
    /// 壁の外か判定する
    /// </summary>
    /// <returns>壁の外かどうか</returns>
    private bool IsOutSideWall()
    {
        if (this.transform.localPosition.x < LEFT_INVISIBLE_WALL) { return true; }
        if (RIGHT_INVISIBLE_WALL < this.transform.localPosition.x) { return true; }
        if (UP_INVISIBLE_WALL < this.transform.localPosition.y) { return true; }
        if (this.transform.localPosition.y < DOWN_INVISIBLE_WALL) { return true; }
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
