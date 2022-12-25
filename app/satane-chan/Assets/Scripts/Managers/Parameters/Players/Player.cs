namespace Assets.Scripts.Managers.Parameters.Players
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    [System.Serializable]
    public class Player
    {
        /// <summary>
        /// プレイヤーの移動量
        /// </summary>
        public float amountOfPlayerMovement = 0.25f;
        /// <summary>
        /// プレイヤーとカーソルの距離
        /// </summary>
        public float distanceBetweenPlayerAndCursor = 2.0f;
        /// <summary>
        /// 当たり判定半径
        /// </summary>
        public float collisionRadius = 0.60f;
    }
}
