using MathNet.Numerics.Random;

namespace GameWideSystems.RNGManagement
{
    public class RandomSourceWrap : IRNGProvider
    {
        private RandomSource _randomSource;

        public RandomSourceWrap(RandomSource randomSource)
        {
            _randomSource = randomSource;
        }


        public int Range(int min, int max)
        {
            return _randomSource.Next(min, max);
        }
        
        public int RangeSafe(int min, int max)
        {
            if (min == max)
            {
                return min;
            }
            
            return _randomSource.Next(min, max);
        }
        
        
        
    }
}