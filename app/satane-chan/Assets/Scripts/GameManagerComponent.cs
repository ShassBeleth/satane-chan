using RpgAtsumaruApiForUnity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// �Q�[���Ǘ��p�R���|�[�l���g
/// </summary>
public class GameManagerComponent : MonoBehaviour
{

    #region �萔
    /// <summary>
    /// ���Ԃő������链�_
    /// </summary>
    private int INCREASES_WITH_TIME_SCORE = 10;
    /// <summary>
    /// �v���[���g�������o���ɓ��������Ƃ��ɑ�������_��
    /// </summary>
    private int INCREASES_HIT_SCORE = 1000;
    #endregion

    #region �R���|�[�l���g
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
    /// �Q�[���I�[�o�[���ɕ\�������p�l���̃��U���g�e�L�X�g
    /// </summary>
    public Text ResultText;
    #endregion
    /// <summary>
    /// ��Q������������
    /// </summary>
    private void OccurObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ObstaclesListGameObject.transform;
    }
    /// <summary>
    /// �z�������o������������
    /// </summary>
    private void OccurImagination()
    {
        GameObject obj = Instantiate(ImaginationPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = ImaginationsListGameObject.transform;
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
                ResultText.text = ScoreComponent.ScoreText.text;
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
            if (distanceBetweenPlayerAndObstacle < PlayerComponent.COLLISION_RADIUS)
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
                if( distanceBetweenBulletAndImagination < BulletComponent.COLLISION_RADIUS )
                {
                    ScoreComponent.AddScore(INCREASES_HIT_SCORE);
                    imaginationChildTransform.GetComponent<ImaginationComponent>().BreakStar();
                    bulletChildTransform.GetComponent<BulletComponent>().BreakBullet();
                    break;
                }
            }
        }
        // �X�R�A�̍X�V
        if (count % 60 == 0 && count != 0)
        {
            ScoreComponent.AddScore(INCREASES_WITH_TIME_SCORE);
        }

        count++;

        // ��Q���̏o��
        if (count % 200 == 0)
        {
            OccurObstacle();
        }
        // �����o���̏o��
        if (count % 400 == 50)
        {
            OccurImagination();
        }
    }
    /// <summary>
    /// ���X�^�[�g�{�^���������C�x���g
    /// </summary>
    public void HandleClickRestart()
    {
        Debug.Log("RESTART");
        Initialize();
    }
    /// <summary>
    /// �^�C�g���֖߂�{�^���������C�x���g
    /// </summary>
    public void HandleClickMoveToTitle()
    {
        Debug.Log("MOVE TO TITLE");
        Initialize();
    }
}