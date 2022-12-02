using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコア用コンポーネント
/// </summary>
public class ScoreComponent : MonoBehaviour
{

    /// <summary>
    /// ハイスコア
    /// </summary>
    public Text HiScoreText;
    /// <summary>
    /// スコア
    /// </summary>
    public Text ScoreText;

    /// <summary>
    /// ハイスコア
    /// </summary>
    private int hiScore;
    /// <summary>
    /// スコア
    /// </summary>
    private int score;

    /// <summary>
    /// スコアのリセット
    /// </summary>
    /// <param name="hiScore">ハイスコア初期値</param>
    public void Reset( int hiScore )
    {
        this.hiScore = hiScore;
        this.score = 0;

        Draw();
    }

    /// <summary>
    /// スコア設定
    /// </summary>
    /// <param name="increaseScore">増加するスコア</param>
    public void AddScore( int increaseScore )
    {
        this.score += increaseScore;
        if( hiScore < score)
        {
            hiScore = score;
        }

        Draw();
    }

    /// <summary>
    /// 描画
    /// </summary>
    private void Draw()
    {
        HiScoreText.text = hiScore.ToString();
        ScoreText.text = score.ToString();
    }

}
