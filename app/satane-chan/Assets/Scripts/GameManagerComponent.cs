using System;
using UnityEngine;

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
    #endregion
    /// <summary>
    /// ��Q������������
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

        foreach( Transform obstacleChildTransform in this.ObstaclesListGameObject.transform)
        {
            float distanceBetweenPlayerAndObstacle = (PlayerComponent.PlayerPosition.transform.position - obstacleChildTransform.position).magnitude;
            if ( distanceBetweenPlayerAndObstacle < PlayerComponent.COLLISION_RADIUS )
            {
                HpComponent.Damage();
                obstacleChildTransform.GetComponent<ObstacleComponent>().BreakStar();
            }
        }

        // �X�R�A�̍X�V
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