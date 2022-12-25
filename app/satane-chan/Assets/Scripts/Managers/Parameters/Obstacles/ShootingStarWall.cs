namespace Assets.Scripts.Managers.Parameters.Obstacles
{
    /// <summary>
    /// 障害物（流れ星）の壁
    /// </summary>
    [System.Serializable]
    public class ShootingStarWall
    {
        /// <summary>
        /// 左の見えない壁
        /// </summary>
        public float leftInvisibleWall = -10f;
        /// <summary>
        /// 右の見えない壁
        /// </summary>
        public float rightInvisibleWall = 10f;
        /// <summary>
        /// 上の見えない壁
        /// </summary>
        public float upInvisibleWall = 7f;
        /// <summary>
        /// 下の見えない壁
        /// </summary>
        public float downInvisibleWall = -10f;
    }
}
