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
    public int HiScore { private set; get; }
    /// <summary>
    /// スコア
    /// </summary>
    public int Score { private set; get; }

    /// <summary>
    /// スコアのリセット
    /// </summary>
    /// <param name="hiScore">ハイスコア初期値</param>
    public void Reset( int hiScore )
    {
        this.HiScore = hiScore;
        this.Score = 0;

        Draw();
    }

    /// <summary>
    /// スコア設定
    /// </summary>
    /// <param name="increaseScore">増加するスコア</param>
    public void AddScore( int increaseScore )
    {
        this.Score += increaseScore;
        if( HiScore < Score)
        {
            HiScore = Score;
        }

        Draw();
    }

    /// <summary>
    /// 描画
    /// </summary>
    private void Draw()
    {
        HiScoreText.text = HiScore.ToString();
        ScoreText.text = Score.ToString();
    }

}
