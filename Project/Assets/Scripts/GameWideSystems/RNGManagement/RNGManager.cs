using System.Collections.Generic;
using MathNet.Numerics.Random;

namespace GameWideSystems.RNGManagement
{
    // ToDo: properly create, save and load in future
    public class RNGManager : IRNGManager
    {
        private Dictionary<RNGGroup, IRNGProvider> _rngProviders;
        
        public RNGManager()
        {
            _rngProviders = new();
            
            MersenneTwister mersenneTwister = new MersenneTwister();
            _rngProviders.Add(RNGGroup.Default, new RandomSourceWrap(mersenneTwister));
            
            mersenneTwister = new MersenneTwister();
            _rngProviders.Add(RNGGroup.CardShuffler, new RandomSourceWrap(mersenneTwister));
            
            mersenneTwister = new MersenneTwister();
            _rngProviders.Add(RNGGroup.Battle, new RandomSourceWrap(mersenneTwister));
            
            mersenneTwister = new MersenneTwister();
            _rngProviders.Add(RNGGroup.Encounter, new RandomSourceWrap(mersenneTwister));
        }


        public IRNGProvider GetRandomProvider(RNGGroup rngGroup)
        {
            if (!_rngProviders.TryGetValue(rngGroup, out IRNGProvider value))
            {
                var mersenneTwister = new MersenneTwister();
                value = new RandomSourceWrap(mersenneTwister);
                 _rngProviders.Add(rngGroup, value);
            }
            
            return value;
        }
        
    }
}