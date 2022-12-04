using RpgAtsumaruApiForUnity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    /// <summary>
    /// プレゼントが吹き出しに当たったときに増加する点数
    /// </summary>
    private int INCREASES_HIT_SCORE = 1000;
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
    /// <summary>
    /// 想像吹き出しのプレハブ
    /// </summary>
    public GameObject ImaginationPrefab;
    /// <summary>
    /// 想像吹き出しのプレハブ格納用オブジェクト
    /// </summary>
    public GameObject ImaginationsListGameObject;
    /// <summary>
    /// ゲームオーバー時のパネル
    /// </summary>
    public GameObject GameOverPanelGameObject;
    /// <summary>
    /// ゲームオーバー時に表示されるパネルのリザルトテキスト
    /// </summary>
    public Text ResultText;
    #endregion
    /// <summary>
    /// 障害物が発生する
    /// </summary>
    private void OccurObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ObstaclesListGameObject.transform;
    }
    /// <summary>
    /// 想像吹き出しが発生する
    /// </summary>
    private void OccurImagination()
    {
        GameObject obj = Instantiate(ImaginationPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ImaginationsListGameObject.transform;
    }

    /// <summary>
    /// TODO 仮
    /// 終了処理
    /// ゲームオーバー時一回だけ処理したいためのフラグ変数
    /// </summary>
    private bool finished = false;
    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        this.HpComponent.Reset();
        ScoreComponent.Reset(100);
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        foreach (Transform bullet in PlayerComponent.BulletsListGameObject.transform)
        {
            Destroy(bullet.gameObject);
        }
        foreach (Transform obstacle in ObstaclesListGameObject.transform)
        {
            Destroy(obstacle.gameObject);
        }
        foreach (Transform imagination in ImaginationsListGameObject.transform)
        {
            Destroy(imagination.gameObject);
        }
        GameOverPanelGameObject.SetActive(false);
        finished = false;
    }
    private void Awake()
    {
        // アツマール用のプラグインを初回起動時のみ初期化する
        if (!RpgAtsumaruApi.Initialized)
        {
            RpgAtsumaruApi.Initialize();
        }

        Initialize();
    }
    int count = 0;

    async void Update()
    {

        // ゲームオーバー時処理
        if (HpComponent.Hp == 0 )
        {
            if( !finished)
            {
                ResultText.text = ScoreComponent.ScoreText.text;
                GameOverPanelGameObject.SetActive(true);

                finished = true;

                // RPGアツマールにスコアを送信する
                await RpgAtsumaruApi.ScoreboardApi.SendScoreAsync(1, ScoreComponent.Score);
                await RpgAtsumaruApi.ScoreboardApi.ShowScoreboardAsync(1);


            }
            return;
        }

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

        // プレイヤーと障害物との当たり判定
        foreach (Transform obstacleChildTransform in this.ObstaclesListGameObject.transform)
        {
            float distanceBetweenPlayerAndObstacle = (PlayerComponent.PlayerPosition.transform.position - obstacleChildTransform.position).magnitude;
            if (distanceBetweenPlayerAndObstacle < PlayerComponent.COLLISION_RADIUS)
            {
                HpComponent.Damage();
                obstacleChildTransform.GetComponent<ObstacleComponent>().BreakStar();
            }
        }

        // ショットと吹き出しとの当たり判定
        foreach (Transform bulletChildTransform in this.PlayerComponent.BulletsListGameObject.transform)
        {
            foreach (Transform imaginationChildTransform in this.ImaginationsListGameObject.transform)
            {
                float distanceBetweenBulletAndImagination = (bulletChildTransform.position - imaginationChildTransform.position).magnitude;
                if( distanceBetweenBulletAndImagination < BulletComponent.COLLISION_RADIUS )
                {
                    ScoreComponent.AddScore(INCREASES_HIT_SCORE);
                    imaginationChildTransform.GetComponent<ImaginationComponent>().BreakStar();
                    bulletChildTransform.GetComponent<BulletComponent>().BreakBullet();
                    break;
                }
            }
        }
        // スコアの更新
        if (count % 60 == 0 && count != 0)
        {
            ScoreComponent.AddScore(INCREASES_WITH_TIME_SCORE);
        }

        count++;

        // 障害物の出現
        if (count % 200 == 0)
        {
            OccurObstacle();
        }
        // 吹き出しの出現
        if (count % 400 == 50)
        {
            OccurImagination();
        }
    }
    /// <summary>
    /// リスタートボタン押下時イベント
    /// </summary>
    public void HandleClickRestart()
    {
        Debug.Log("RESTART");
        Initialize();
    }
    /// <summary>
    /// タイトルへ戻るボタン押下時イベント
    /// </summary>
    public void HandleClickMoveToTitle()
    {
        Debug.Log("MOVE TO TITLE");
        Initialize();
    }
}