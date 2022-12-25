namespace Assets.Scripts.Managers.Parameters.Bullets
{
    /// <summary>
    /// 弾のパラメータ
    /// </summary>
    [System.Serializable]
    public class Bullet
    {
        /// <summary>
        /// 移動速度
        /// </summary>
        public float moveSpeed = 0.25f;
        /// <summary>
        /// 回転速度
        /// </summary>
        public float turnSpeed = -0.1f;
        /// <summary>
        /// 当たり判定半径
        /// </summary>
        public float collisionRadius = 0.5f;
    }
}
