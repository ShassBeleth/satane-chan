namespace Assets.Scripts.Managers.Parameters.Numbers
{
    /// <summary>
    /// 数字
    /// </summary>
    [System.Serializable]
    public class Number
    {
        /// <summary>
        /// 数字画像のトリミング座標配列
        /// </summary>
        public float[] numberPosition = new float[10] { -166f, -130f, -92f, -57f, -20f, 18f, 55f, 90f, 128f, 164f };
        /// <summary>
        /// 10×最大桁乗
        /// </summary>
        public int digit = 100000000;
    }
}
