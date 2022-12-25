namespace Assets.Scripts.Managers.Parameters.Obstacles
{
    /// <summary>
    /// 障害物(星)用パラメータ
    /// </summary>
    [System.Serializable]
    public class ShootingStar
    {
        /// <summary>
        /// 障害物出現インターバル(1/フレーム数)
        /// </summary>
        public int occurInterval = 75;
        /// <summary>
        /// 回転速度
        /// </summary>
        public float turnSpeed = 0.3f;
        /// <summary>
        /// 出現最大位置（左）
        /// </summary>
        public float horizontalOccurPosMin = -9f;
        /// <summary>
        /// 出現最大位置（右）
        /// </summary>
        public float horizontalOccurPosMax = 9f;
        /// <summary>
        /// 出現位置（上）
        /// </summary>
        public float occurPosUp = 6f;
        /// <summary>
        /// 射出最低角度
        /// </summary>
        public float minAngle = 210f;
        /// <summary>
        /// 射出最大角度
        /// </summary>
        public float maxAngle = 330f;
        /// <summary>
        /// 最低速度
        /// </summary>
        public float minSpeed = 0.15f;
        /// <summary>
        /// 最大速度
        /// </summary>
        public float maxSpeed = 0.30f;
    }
}
