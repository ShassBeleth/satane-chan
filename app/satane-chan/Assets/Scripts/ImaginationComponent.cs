using Assets.Scripts.Managers.Parameters;
using UnityEngine;

/// <summary>
/// 想像吹き出し用スクリプト
/// </summary>
public class ImaginationComponent : MonoBehaviour
{
    /// <summary>
    /// パラメータ管理
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// 星が壊れる
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
