using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// プレイヤー管理用コンポーネント
/// </summary>
public class PlayerComponent : MonoBehaviour
{
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManagerComponent;
    /// <summary>
    /// プレイヤーの通常のスケール
    /// </summary>
    private Vector3 PLAYER_DEFAULT_SCALE = Vector3.one;
    /// <summary>
    /// プレイヤーの反転したスケール
    /// </summary>
    private Vector3 PLAYER_REVERSE_SCALE = new Vector3(-1.0f, 1.0f, 1.0f);

    /// <summary>
    /// プレイヤーの角度
    /// </summary>
    public float Angle { private set; get; }

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
        position.x -= parameterManagerComponent.player.amountOfPlayerMovement;
        if (position.x < parameterManagerComponent.playerWall.leftInvisibleWall)
        {
            position.x = parameterManagerComponent.playerWall.leftInvisibleWall;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 右へ移動する
    /// </summary>
    public void MoveRight()
    {
        Vector3 position = PlayerPosition.position;
        position.x += parameterManagerComponent.player.amountOfPlayerMovement;
        if (parameterManagerComponent.playerWall.rightInvisibleWall < position.x)
        {
            position.x = parameterManagerComponent.playerWall.rightInvisibleWall;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 上へ移動する
    /// </summary>
    public void MoveUp()
    {
        Vector3 position = PlayerPosition.position;
        position.y += parameterManagerComponent.player.amountOfPlayerMovement;
        if (parameterManagerComponent.playerWall.upInvisibleWall < position.y)
        {
            position.y = parameterManagerComponent.playerWall.upInvisibleWall;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// 下へ移動する
    /// </summary>
    public void MoveDown()
    {
        Vector3 position = PlayerPosition.position;
        position.y -= parameterManagerComponent.player.amountOfPlayerMovement;
        if (position.y < parameterManagerComponent.playerWall.downInvisibleWall)
        {
            position.y = parameterManagerComponent.playerWall.downInvisibleWall;
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
        CursorPosition.rotation = Quaternion.Euler(0.0f, 0.0f,this.Angle);
    }
    /// <summary>
    /// 弾を撃つ
    /// </summary>
    public void Shot()
    {
        if ( BulletsListGameObject.transform.childCount > 5)
        {
            return;
        }
        GameObject obj = Instantiate(BulletPrefab, PlayerPosition.position, Quaternion.identity);
        obj.transform.parent = BulletsListGameObject.transform;
        BulletComponent bullet = obj.GetComponent<BulletComponent>();
        bullet.Angle = this.Angle;
        bullet.parameterManager = parameterManagerComponent;
        bullet.shotPlayerPosition = PlayerPosition.position;
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
        Vector3 position = PlayerPosition.position + (normalized * parameterManagerComponent.player.distanceBetweenPlayerAndCursor);
        CursorPosition.position = position;
    }

}
