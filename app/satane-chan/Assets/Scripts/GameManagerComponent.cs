using Assets.Scripts.Managers.Parameters;
using RpgAtsumaruApiForUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���Ǘ��p�R���|�[�l���g
/// </summary>
public class GameManagerComponent : MonoBehaviour
{
    #region �R���|�[�l���g
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// HP�̃O���t�B�b�N�Ǘ��p�R���|�[�l���g
    /// </summary>
    public HpComponent HpComponent;
    /// <summary>
    /// �X�R�A�p�R���|�[�l���g
    /// </summary>
    public ScoreComponent ScoreComponent;
    /// <summary>
    /// �v���C���[�Ǘ��p�R���|�[�l���g
    /// </summary>
    public PlayerComponent PlayerComponent;
    /// <summary>
    /// ��Q���̃v���n�u
    /// </summary>
    public GameObject ObstaclePrefab;
    /// <summary>
    /// ��Q���̃v���n�u�i�[�p�I�u�W�F�N�g
    /// </summary>
    public GameObject ObstaclesListGameObject;
    /// <summary>
    /// �z�������o���̃v���n�u
    /// </summary>
    public GameObject ImaginationPrefab;
    /// <summary>
    /// �z�������o���̃v���n�u�i�[�p�I�u�W�F�N�g
    /// </summary>
    public GameObject ImaginationsListGameObject;
    /// <summary>
    /// �Q�[���I�[�o�[���̃p�l��
    /// </summary>
    public GameObject GameOverPanelGameObject;
    /// <summary>
    /// �Q�[���I�[�o�[���ɕ\�������p�l���̃��U���g
    /// </summary>
    public NumberComponent ResultNumberComponent;
    #endregion
    /// <summary>
    /// ��Q������������
    /// </summary>
    private void OccurObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ObstaclesListGameObject.transform;
        obj.GetComponent<ObstacleComponent>().parameterManager = parameterManager;
    }
    /// <summary>
    /// �z�������o������������
    /// </summary>
    private void OccurImagination()
    {
        GameObject obj = Instantiate(ImaginationPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ImaginationsListGameObject.transform;
        obj.GetComponent<ImaginationComponent>().parameterManager = parameterManager;
    }

    /// <summary>
    /// TODO ��
    /// �I������
    /// �Q�[���I�[�o�[����񂾂��������������߂̃t���O�ϐ�
    /// </summary>
    private bool finished = false;
    /// <summary>
    /// ������
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
        // �A�c�}�[���p�̃v���O�C��������N�����̂ݏ���������
        if (!RpgAtsumaruApi.Initialized)
        {
            RpgAtsumaruApi.Initialize();
        }

        Initialize();
    }
    int count = 0;

    async void Update()
    {

        // �Q�[���I�[�o�[������
        if (HpComponent.Hp == 0 )
        {
            if( !finished)
            {
                ResultNumberComponent.SetNumber(ScoreComponent.Score);
                GameOverPanelGameObject.SetActive(true);

                finished = true;

                // RPG�A�c�}�[���ɃX�R�A�𑗐M����
                await RpgAtsumaruApi.ScoreboardApi.SendScoreAsync(1, ScoreComponent.Score);
                await RpgAtsumaruApi.ScoreboardApi.ShowScoreboardAsync(1);

            }
            return;
        }

        // �v���C���[�̈ړ�
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

        // �J�[�\���̈ړ�
        this.PlayerComponent.UpdateCursorPosition(Input.mousePosition);
        // �v���C���[�̊p�x�ݒ�
        this.PlayerComponent.LookAt();

        // �V���b�g
        if (Input.GetMouseButtonDown(0))
        {
            this.PlayerComponent.Shot();
        }

        // �v���C���[�Ə�Q���Ƃ̓����蔻��
        foreach (Transform obstacleChildTransform in this.ObstaclesListGameObject.transform)
        {
            float distanceBetweenPlayerAndObstacle = (PlayerComponent.PlayerPosition.transform.position - obstacleChildTransform.position).magnitude;
            if (distanceBetweenPlayerAndObstacle < parameterManager.player.collisionRadius)
            {
                HpComponent.Damage();
                obstacleChildTransform.GetComponent<ObstacleComponent>().BreakStar();
            }
        }

        // �V���b�g�Ɛ����o���Ƃ̓����蔻��
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
        // �X�R�A�̍X�V
        if (count % parameterManager.score.timeScoreInterval == 0 && count != 0)
        {
            ScoreComponent.AddScore(parameterManager.score.timeScore);
        }

        count++;

        // ��Q���̏o��
        if (count % parameterManager.shootingStar.occurInterval == 0)
        {
            OccurObstacle();
        }
        // �����o���̏o��
        if (count % parameterManager.imagination.occurInterval == 50)
        {
            OccurImagination();
        }
    }
    /// <summary>
    /// ���X�^�[�g�{�^���������C�x���g
    /// </summary>
    public void HandleClickRestart()
    {
        Initialize();
    }
    /// <summary>
    /// �^�C�g���֖߂�{�^���������C�x���g
    /// </summary>
    public void HandleClickMoveToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}