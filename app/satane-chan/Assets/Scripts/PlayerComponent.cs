using UnityEngine;

/// <summary>
/// プレイヤー管理用コンポーネント
/// </summary>
public class PlayerComponent : MonoBehaviour
{

    /// <summary>
    /// 左の見えない壁
    /// </summary>
    private float LEFT_INVISIBLE_WALL = -7f;
    /// <summary>
    /// 右の見えない壁
    /// </summary>
    private float RIGHT_INVISIBLE_WALL = 7f;
    /// <summary>
    /// 上の見えない壁
    /// </summary>
    private float UP_INVISIBLE_WALL = 4f;
    /// <summary>
    /// 下の見えない壁
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -4f;
    /// <summary>
    /// プレイヤーの移動量
    /// </summary>
    private float AMOUNT_OF_PLAYER_MOVEMENT = 0.05f;
    /// <summary>
    /// プレイヤーの通常のスケール
    /// </summary>
    private Vector3 PLAYER_DEFAULT_SCALE = Vector3.one;
    /// <summary>
    /// プレイヤーの反転したスケール
    /// </summary>
    private Vector3 PLAYER_REVERSE_SCALE = new Vector3(-1.0f, 1.0f, 1.0f);
    /// <summary>
    /// プレイヤーとカーソルの距離
    /// </summary>
    private float DISTANCE_BETWEEN_PLAYER_AND_CURSOR = 3.5f;

    /// <summary>
    /// プレイヤーの角度
    /// </summary>
    public float Angle { private set; get; }

    /// <summary>
    /// 当たり判定半径
    /// </summary>
    public float COLLISION_RADIUS { private set; get; } = 0.60f;

    /// <summary>
    /// プレイヤーの座標
    /// </summary>
    public Transform PlayerPosition;
    /// <summary>
    /// プレイヤーのスケール
    /// </summary>
    public Transform PlayerScale;
    /// <summary>
    /// プレイヤーの角度
    /// </summary>
    public Transform PlayerRotation;
    /// <summary>
    /// 弾のプレハブ
    /// </summary>
    public GameObject BulletPrefab;
    /// <summary>
    /// 弾のプレハブ格納用オブジェクト
    /// </summary>
    public GameObject BulletsListGameObject;
    /// <summary>
    /// カーソルの座標
    /// </summary>
    public Transform CursorPosition;

    /// <summary>
    /// 左へ移動する
    /// </summary>
    public void MoveLeft()
    {
        Vector3 position = PlayerPosition.position;
        position.x -= AMOUNT_OF_PLAYER_MOVEMENT;
        if (position.x < LEFT_INVISIBLE_WALL)
        {
            position.x = LEFT_INVISIBLE_WALL;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 右へ移動する
    /// </summary>
    public void MoveRight()
    {
        Vector3 position = PlayerPosition.position;
        position.x += AMOUNT_OF_PLAYER_MOVEMENT;
        if (RIGHT_INVISIBLE_WALL < position.x)
        {
            position.x = RIGHT_INVISIBLE_WALL;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 上へ移動する
    /// </summary>
    public void MoveUp()
    {
        Vector3 position = PlayerPosition.position;
        position.y += AMOUNT_OF_PLAYER_MOVEMENT;
        if (UP_INVISIBLE_WALL < position.y)
        {
            position.y = UP_INVISIBLE_WALL;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 下へ移動する
    /// </summary>
    public void MoveDown()
    {
        Vector3 position = PlayerPosition.position;
        position.y -= AMOUNT_OF_PLAYER_MOVEMENT;
        if (position.y < DOWN_INVISIBLE_WALL)
        {
            position.y = DOWN_INVISIBLE_WALL;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 指定座標を注目する（回転させる）
    /// </summary>
    /// <param name="target">ターゲットの座標</param>
    public void LookAt()
    {
        Vector3 dt = CursorPosition.position - PlayerPosition.position;
        float rad = Mathf.Atan2(dt.y, dt.x);
        this.Angle = rad * Mathf.Rad2Deg;

        if (-90 < this.Angle && this.Angle < 90)
        {
            PlayerScale.localScale = PLAYER_DEFAULT_SCALE;
            PlayerRotation.rotation = Quaternion.Euler(0.0f, 0.0f, this.Angle);
        }
        else
        {
            PlayerScale.localScale = PLAYER_REVERSE_SCALE;
            PlayerRotation.rotation = Quaternion.Euler(0.0f, 0.0f, this.Angle - 180);
        }
    }
    /// <summary>
    /// 弾を撃つ
    /// </summary>
    public void Shot()
    {
        GameObject obj = Instantiate(BulletPrefab, PlayerPosition.position, Quaternion.identity);
        obj.transform.parent = BulletsListGameObject.transform;
        obj.GetComponent<BulletComponent>().Angle = this.Angle;
    }
    /// <summary>
    /// カーソルの座標を更新する
    /// </summary>
    /// <param name="targetPosition">ターゲット</param>
    public void UpdateCursorPosition(Vector3 targetPosition)
    {
        Vector3 screenToWorldMousePosition = Camera.main.ScreenToWorldPoint(targetPosition);
        screenToWorldMousePosition.z = 0.0f;
        Vector3 normalized = (screenToWorldMousePosition - PlayerPosition.position).normalized;
        CursorPosition.position = PlayerPosition.position + (normalized * DISTANCE_BETWEEN_PLAYER_AND_CURSOR);
    }

}
