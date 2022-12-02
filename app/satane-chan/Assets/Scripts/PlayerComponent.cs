using UnityEngine;

/// <summary>
/// �v���C���[�Ǘ��p�R���|�[�l���g
/// </summary>
public class PlayerComponent : MonoBehaviour
{

    /// <summary>
    /// ���̌����Ȃ���
    /// </summary>
    private float LEFT_INVISIBLE_WALL = -7f;
    /// <summary>
    /// �E�̌����Ȃ���
    /// </summary>
    private float RIGHT_INVISIBLE_WALL = 7f;
    /// <summary>
    /// ��̌����Ȃ���
    /// </summary>
    private float UP_INVISIBLE_WALL = 4f;
    /// <summary>
    /// ���̌����Ȃ���
    /// </summary>
    private float DOWN_INVISIBLE_WALL = -4f;
    /// <summary>
    /// �v���C���[�̈ړ���
    /// </summary>
    private float AMOUNT_OF_PLAYER_MOVEMENT = 0.05f;
    /// <summary>
    /// �v���C���[�̒ʏ�̃X�P�[��
    /// </summary>
    private Vector3 PLAYER_DEFAULT_SCALE = Vector3.one;
    /// <summary>
    /// �v���C���[�̔��]�����X�P�[��
    /// </summary>
    private Vector3 PLAYER_REVERSE_SCALE = new Vector3(-1.0f, 1.0f, 1.0f);
    /// <summary>
    /// �v���C���[�ƃJ�[�\���̋���
    /// </summary>
    private float DISTANCE_BETWEEN_PLAYER_AND_CURSOR = 3.5f;

    /// <summary>
    /// �v���C���[�̊p�x
    /// </summary>
    public float Angle { private set; get; }

    /// <summary>
    /// �����蔻�蔼�a
    /// </summary>
    public float COLLISION_RADIUS { private set; get; } = 0.60f;

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
        position.x -= AMOUNT_OF_PLAYER_MOVEMENT;
        if (position.x < LEFT_INVISIBLE_WALL)
        {
            position.x = LEFT_INVISIBLE_WALL;
        }
        PlayerPosition.position = position;
    }
    /// <summary>
    /// �E�ֈړ�����
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
    /// ��ֈړ�����
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
    /// ���ֈړ�����
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
    }
    /// <summary>
    /// �e������
    /// </summary>
    public void Shot()
    {
        GameObject obj = Instantiate(BulletPrefab, PlayerPosition.position, Quaternion.identity);
        obj.transform.parent = BulletsListGameObject.transform;
        obj.GetComponent<BulletComponent>().Angle = this.Angle;
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
        CursorPosition.position = PlayerPosition.position + (normalized * DISTANCE_BETWEEN_PLAYER_AND_CURSOR);
    }

}
