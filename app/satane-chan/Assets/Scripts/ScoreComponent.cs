using UnityEngine;

/// <summary>
/// �X�R�A�p�R���|�[�l���g
/// </summary>
public class ScoreComponent : MonoBehaviour
{

    /// <summary>
    /// �n�C�X�R�A
    /// </summary>
    public NumberComponent HiScoreNumberComponent;
    /// <summary>
    /// �X�R�A
    /// </summary>
    public NumberComponent ScoreNumberComponent;

    /// <summary>
    /// �n�C�X�R�A
    /// </summary>
    public int HiScore { private set; get; }
    /// <summary>
    /// �X�R�A
    /// </summary>
    public int Score { private set; get; }

    /// <summary>
    /// �X�R�A�̃��Z�b�g
    /// </summary>
    /// <param name="hiScore">�n�C�X�R�A�����l</param>
    public void Reset( int hiScore )
    {
        this.HiScore = hiScore;
        this.Score = 0;

        Draw();
    }

    /// <summary>
    /// �X�R�A�ݒ�
    /// </summary>
    /// <param name="increaseScore">��������X�R�A</param>
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
    /// �`��
    /// </summary>
    private void Draw()
    {
        HiScoreNumberComponent.SetNumber(HiScore);
        ScoreNumberComponent.SetNumber(Score);
    }

}
