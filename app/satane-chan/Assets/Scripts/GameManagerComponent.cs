using Assets.Scripts.Managers.Parameters;
using RpgAtsumaruApiForUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム管理用コンポーネント
/// </summary>
public class GameManagerComponent : MonoBehaviour
{
    #region コンポーネント
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManager;
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
    /// ゲームオーバー時に表示されるパネルのリザルト
    /// </summary>
    public NumberComponent ResultNumberComponent;
    #endregion
    /// <summary>
    /// 障害物が発生する
    /// </summary>
    private void OccurObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ObstaclesListGameObject.transform;
        obj.GetComponent<ObstacleComponent>().parameterManager = parameterManager;
    }
    /// <summary>
    /// 想像吹き出しが発生する
    /// </summary>
    private void OccurImagination()
    {
        GameObject obj = Instantiate(ImaginationPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ImaginationsListGameObject.transform;
        obj.GetComponent<ImaginationComponent>().parameterManager = parameterManager;
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
        ScoreComponent.Reset(parameterManager.score.defaultHiScore);
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
                ResultNumberComponent.SetNumber(ScoreComponent.Score);
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
            if (distanceBetweenPlayerAndObstacle < parameterManager.player.collisionRadius)
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
                if( distanceBetweenBulletAndImagination < parameterManager.bullet.collisionRadius )
                {
                    BulletComponent bulletComponent = bulletChildTransform.GetComponent<BulletComponent>();
                    float distanceBetweenPlayerAndImagination = (bulletComponent.shotPlayerPosition - imaginationChildTransform.position).magnitude;
                    float ratio = (16 - distanceBetweenPlayerAndImagination) / 16;
                    if( ratio < 0f)
                    {
                        ratio = 0f;
                    }
                    ScoreComponent.AddScore((int)(parameterManager.score.hitScore * ratio));
                    imaginationChildTransform.GetComponent<ImaginationComponent>().BreakStar();
                    bulletComponent.BreakBullet();
                    break;
                }
            }
        }
        // スコアの更新
        if (count % parameterManager.score.timeScoreInterval == 0 && count != 0)
        {
            ScoreComponent.AddScore(parameterManager.score.timeScore);
        }

        count++;

        // 障害物の出現
        if (count % parameterManager.shootingStar.occurInterval == 0)
        {
            OccurObstacle();
        }
        // 吹き出しの出現
        if (count % parameterManager.imagination.occurInterval == 50)
        {
            OccurImagination();
        }
    }
    /// <summary>
    /// リスタートボタン押下時イベント
    /// </summary>
    public void HandleClickRestart()
    {
        Initialize();
    }
    /// <summary>
    /// タイトルへ戻るボタン押下時イベント
    /// </summary>
    public void HandleClickMoveToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}