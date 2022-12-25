namespace Assets.Scripts.Managers.Parameters.Imaginations
{
    /// <summary>
    /// 吹き出しの壁
    /// </summary>
    [System.Serializable]
    public class ImaginationWall
    {
        /// <summary>
        /// 出現位置最西端
        /// </summary>
        public float westernMost = -10f;
        /// <summary>
        /// 出現位置最東端
        /// </summary>
        public float easterMost = 10f;
        /// <summary>
        /// 出現位置最北端
        /// </summary>
        public float northernMost = 10f;
        /// <summary>
        /// 出現位置最南端
        /// </summary>
        public float southernMost = -10f;
    }
}
