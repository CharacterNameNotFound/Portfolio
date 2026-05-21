using System.Collections.Generic;
using GameWideSystems.RNGManagement;

namespace Game.Utilities.Shuffling
{
    public static class ListShuffling
    {
        // Using Durstenfeld variant of Fisher–Yates shuffle
        public static void ShuffleListDurstenfeld<T>(this List<T> list, IRNGProvider rngProvider)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = rngProvider.Range(0, i + 1);

                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
            
        }
        
        
    }
}