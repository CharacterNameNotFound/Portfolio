using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Items;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class ItemInitializer : IItemInitializer
    {
        private IItem[] _items;

        public ItemInitializer(IItem[] items)
        {
            _items = items;
        }


        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                await _items[i].Initialize(cancellationToken);
            }
        }

        public void CleanUp()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].CleanUp();
            }
        }
        
        
    }
}