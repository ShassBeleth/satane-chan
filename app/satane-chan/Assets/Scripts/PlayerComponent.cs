using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// �v���C���[�Ǘ��p�R���|�[�l���g
/// </summary>
public class PlayerComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManagerComponent;
    /// <summary>
    /// �v���C���[�̒ʏ�̃X�P�[��
    /// </summary>
    private Vector3 PLAYER_DEFAULT_SCALE = Vector3.one;
    /// <summary>
    /// �v���C���[�̔��]�����X�P�[��
    /// </summary>
    private Vector3 PLAYER_REVERSE_SCALE = new Vector3(-1.0f, 1.0f, 1.0f);

    /// <summary>
    /// �v���C���[�̊p�x
    /// </summary>
    public float Angle { private set; get; }

    /// <summary>
    /// �v���C���[�̍��W
    /// </summary>
    public Transform PlayerPosition;
    /// <summary>
    /// �v���C���[�̃X�P�[��
    /// </summary>
    public Transform PlayerScale;
    /// <summary>
    /// �v���C���[�̊p�x
    /// </summary>
    public Transform PlayerRotation;
    /// <summary>
    /// �e�̃v���n�u
    /// </summary>
    public GameObject BulletPrefab;
    /// <summary>
    /// �e�̃v���n�u�i�[�p�I�u�W�F�N�g
    /// </summary>
    public GameObject BulletsListGameObject;
    /// <summary>
    /// �J�[�\���̍��W
    /// </summary>
    public Transform CursorPosition;

    /// <summary>
    /// ���ֈړ�����
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
    /// �E�ֈړ�����
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
    /// ��ֈړ�����
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
    /// ���ֈړ�����
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
    /// �w����W�𒍖ڂ���i��]������j
    /// </summary>
    /// <param name="target">�^�[�Q�b�g�̍��W</param>
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
    /// �e������
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
    /// �J�[�\���̍��W���X�V����
    /// </summary>
    /// <param name="targetPosition">�^�[�Q�b�g</param>
    public void UpdateCursorPosition(Vector3 targetPosition)
    {
        Vector3 screenToWorldMousePosition = Camera.main.ScreenToWorldPoint(targetPosition);
        screenToWorldMousePosition.z = 0.0f;
        Vector3 normalized = (screenToWorldMousePosition - PlayerPosition.position).normalized;
        Vector3 position = PlayerPosition.position + (normalized * parameterManagerComponent.player.distanceBetweenPlayerAndCursor);
        CursorPosition.position = position;
    }

}
