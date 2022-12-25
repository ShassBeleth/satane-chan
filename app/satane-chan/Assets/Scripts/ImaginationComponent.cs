using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// �z�������o���p�X�N���v�g
/// </summary>
public class ImaginationComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// ��������
    /// </summary>
    public void BreakStar()
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        this.transform.position = new Vector3(
            Random.Range(
                parameterManager.imaginationWall.westernMost,
                parameterManager.imaginationWall.easterMost
                ),
            Random.Range(
                parameterManager.imaginationWall.southernMost,
                parameterManager.imaginationWall.northernMost
                ), 
            0f
            );
    }
}
