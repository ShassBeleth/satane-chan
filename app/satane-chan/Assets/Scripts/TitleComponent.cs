using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleComponent : MonoBehaviour
{
    /// <summary>
    /// ��������p�p�l���Q�[���I�u�W�F�N�g
    /// </summary>
    public GameObject DescriptionPanelGameObject;
    /// <summary>
    /// �X�^�[�g�{�^��
    /// </summary>
    public Button StartButton;
    /// <summary>
    /// ��������{�^��
    /// </summary>
    public Button DescriptionButton;

    public void Awake()
    {
        DescriptionPanelGameObject.SetActive(false);
    }
    /// <summary>
    /// �X�^�[�g�{�^���������C�x���g
    /// </summary>
    public void HandleClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// ��������{�^���������C�x���g
    /// </summary>
    public void HandleClickDescriptionButton()
    {
        DescriptionPanelGameObject.SetActive(true);
        StartButton.enabled = false;
        DescriptionButton.enabled = false;
    }
    /// <summary>
    /// ������������{�^���������C�x���g
    /// </summary>
    public void HandleClickCloseDescriptionButton()
    {
        DescriptionPanelGameObject.SetActive(false);
        StartButton.enabled = true;
        DescriptionButton.enabled = true;
    }
}
