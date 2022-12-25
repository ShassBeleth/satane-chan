namespace Assets.Scripts.Managers.Parameters.Houses
{
    /// <summary>
    /// 家
    /// </summary>
    [System.Serializable]
    public class House
    {
        /// <summary>
        /// 初期位置X
        /// </summary>
        public float startPositionX = 5f;
        /// <summary>
        /// 家の移動スピード
        /// </summary>
        public float speed = 0.012f;
        /// <summary>
        /// 家が消失する左の壁
        /// </summary>
        public float leftInvisibleWall = -10f;
        /// <summary>
        /// 出現インターバル(1/フレーム数)
        /// </summary>
        public int occurInterval = 10;
        /// <summary>
        /// 出現頻度
        /// </summary>
        public float[] frequencyOfAppearance = { 0.25f, 0.5f, 0.75f };
    }
}
