namespace Assets.Scripts.Managers.Parameters.Scores
{
    /// <summary>
    /// スコア用パラメータ
    /// </summary>
    [System.Serializable]
    public class Score
    {
        /// <summary>
        /// 時間で増加する得点
        /// </summary>
        public int timeScore = 10;
        /// <summary>
        /// プレゼントが吹き出しに当たったときに増加する点数
        /// </summary>
        public int hitScore = 1000;
        /// <summary>
        /// デフォルトのハイスコア
        /// </summary>
        public int defaultHiScore = 100;
        /// <summary>
        /// 時間で増加する得点のインターバル（1/フレーム数）
        /// </summary>
        public int timeScoreInterval = 60;
    }
}
