using System;
using UnityEngine;

/// <summary>
/// ゲーム管理用コンポーネント
/// </summary>
public class GameManagerComponent : MonoBehaviour
{

    #region 定数
    /// <summary>
    /// 時間で増加する得点
    /// </summary>
    private int INCREASES_WITH_TIME_SCORE = 10;
    #endregion

    #region コンポーネント
    /// <summary>
    /// HPのグラフィック管理用コンポーネント
    /// </summary>
    public HpComponent HpComponent;
    /// <summary>
    /// スコア用コンポーネント
    /// </summary>
    public ScoreComponent ScoreComponent;
    /// <summary>
    /// プレイヤー管理用コンポーネント
    /// </summary>
    public PlayerComponent PlayerComponent;
    /// <summary>
    /// 障害物のプレハブ
    /// </summary>
    public GameObject ObstaclePrefab;
    /// <summary>
    /// 障害物のプレハブ格納用オブジェクト
    /// </summary>
    public GameObject ObstaclesListGameObject;
    #endregion
    /// <summary>
    /// 障害物が発生する
    /// </summary>
    private void OccurObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ObstaclesListGameObject.transform;
    }
    private void Awake()
    {
        this.HpComponent.Reset();
        ScoreComponent.Reset(100);
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

   }
    int count = 0;
    
    void Update()
    {
        // プレイヤーの移動
        if (Input.GetKey(KeyCode.A))
        {
            this.PlayerComponent.MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.PlayerComponent.MoveRight();
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.PlayerComponent.MoveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.PlayerComponent.MoveDown();
        }

        // カーソルの移動
        this.PlayerComponent.UpdateCursorPosition(Input.mousePosition);
        // プレイヤーの角度設定
        this.PlayerComponent.LookAt();

        // ショット
        if (Input.GetMouseButtonDown(0))
        {
            this.PlayerComponent.Shot();
        }

        foreach( Transform obstacleChildTransform in this.ObstaclesListGameObject.transform)
        {
            float distanceBetweenPlayerAndObstacle = (PlayerComponent.PlayerPosition.transform.position - obstacleChildTransform.position).magnitude;
            if ( distanceBetweenPlayerAndObstacle < PlayerComponent.COLLISION_RADIUS )
            {
                HpComponent.Damage();
                obstacleChildTransform.GetComponent<ObstacleComponent>().BreakStar();
            }
        }

        // スコアの更新
        if (count % 60 == 0 && count != 0)
        {
            ScoreComponent.AddScore(INCREASES_WITH_TIME_SCORE);
        }

        count++;
        if( count % 200 == 0)
        {
            OccurObstacle();
        }
    }

}