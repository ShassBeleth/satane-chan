using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�R�A�p�R���|�[�l���g
/// </summary>
public class ScoreComponent : MonoBehaviour
{

    /// <summary>
    /// �n�C�X�R�A
    /// </summary>
    public Text HiScoreText;
    /// <summary>
    /// �X�R�A
    /// </summary>
    public Text ScoreText;

    /// <summary>
    /// �n�C�X�R�A
    /// </summary>
    private int hiScore;
    /// <summary>
    /// �X�R�A
    /// </summary>
    private int score;

    /// <summary>
    /// �X�R�A�̃��Z�b�g
    /// </summary>
    /// <param name="hiScore">�n�C�X�R�A�����l</param>
    public void Reset( int hiScore )
    {
        this.hiScore = hiScore;
        this.score = 0;

        Draw();
    }

    /// <summary>
    /// �X�R�A�ݒ�
    /// </summary>
    /// <param name="increaseScore">��������X�R�A</param>
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
    /// �`��
    /// </summary>
    private void Draw()
    {
        HiScoreText.text = hiScore.ToString();
        ScoreText.text = score.ToString();
    }

}
