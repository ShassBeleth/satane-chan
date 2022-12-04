using UnityEngine;

/// <summary>
/// 想像吹き出し用スクリプト
/// </summary>
public class ImaginationComponent : MonoBehaviour
{
    /// <summary>
    /// 出現位置最西端
    /// </summary>
    private float WESTERNMOST = -10f;
    /// <summary>
    /// 出現位置最東端
    /// </summary>
    private float EASTERNMOST = 10f;
    /// <summary>
    /// 出現位置最北端
    /// </summary>
    private float NORTHERNMOST = 10f;
    /// <summary>
    /// 出現位置最南端
    /// </summary>
    private float SOUTHERNMOST = -10f;

    /// <summary>
    /// 星が壊れる
    /// </summary>
    public void BreakStar()
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(WESTERNMOST, EASTERNMOST), Random.Range(SOUTHERNMOST, NORTHERNMOST), 0f);
    }
}
