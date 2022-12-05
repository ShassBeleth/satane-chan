using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleComponent : MonoBehaviour
{
    /// <summary>
    /// 操作説明用パネルゲームオブジェクト
    /// </summary>
    public GameObject DescriptionPanelGameObject;
    /// <summary>
    /// スタートボタン
    /// </summary>
    public Button StartButton;
    /// <summary>
    /// 操作説明ボタン
    /// </summary>
    public Button DescriptionButton;

    public void Awake()
    {
        DescriptionPanelGameObject.SetActive(false);
    }
    /// <summary>
    /// スタートボタン押下時イベント
    /// </summary>
    public void HandleClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// 操作説明ボタン押下時イベント
    /// </summary>
    public void HandleClickDescriptionButton()
    {
        DescriptionPanelGameObject.SetActive(true);
        StartButton.enabled = false;
        DescriptionButton.enabled = false;
    }
    /// <summary>
    /// 操作説明を閉じるボタン押下時イベント
    /// </summary>
    public void HandleClickCloseDescriptionButton()
    {
        DescriptionPanelGameObject.SetActive(false);
        StartButton.enabled = true;
        DescriptionButton.enabled = true;
    }
}
