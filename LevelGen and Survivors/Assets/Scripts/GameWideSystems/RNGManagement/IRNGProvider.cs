namespace GameWideSystems.RNGManagement
{
    public interface IRNGProvider
    {
        /// <summary>
        /// Generate integer [min, max)
        /// </summary>
        public int Range(int min, int max);
        
        /// <summary>
        /// Generate integer [min, max), returns min in case min == max
        /// </summary>
        public int RangeSafe(int min, int max);
    }
}