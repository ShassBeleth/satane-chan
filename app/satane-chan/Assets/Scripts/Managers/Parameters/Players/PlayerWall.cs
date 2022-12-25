namespace Assets.Scripts.Managers.Parameters.Players
{
    /// <summary>
    /// プレイヤーの移動制限
    /// </summary>
    [System.Serializable]
    public class PlayerWall
    {
        /// <summary>
        /// 左の見えない壁
        /// </summary>
        public float leftInvisibleWall = -7f;
        /// <summary>
        /// 右の見えない壁
        /// </summary>
        public float rightInvisibleWall = 7f;
        /// <summary>
        /// 上の見えない壁
        /// </summary>
        public float upInvisibleWall = 4f;
        /// <summary>
        /// 下の見えない壁
        /// </summary>
        public float downInvisibleWall = -4f;
    }
}
