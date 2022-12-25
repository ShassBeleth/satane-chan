namespace Assets.Scripts.Managers.Parameters.Bullets
{
    /// <summary>
    /// 弾の消失位置
    /// </summary>
    [System.Serializable]
    public class BulletWall
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
        public float upInvisibleWall = 10f;
        /// <summary>
        /// 下の見えない壁
        /// </summary>
        public float downInvisibleWall = -10f;
    }
}
