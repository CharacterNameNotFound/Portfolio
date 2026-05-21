using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace GameWideSystems.InputManager
{
    public class InputHost : IInputHost
    {
        private readonly List<IInputHandlerLayer> inputLayers = new();

        private bool _ignoreInputs;

        public InputHost([Inject(Optional = true)] IInputHandlerLayer[] inputHandlers)
        {
            if (inputHandlers is not null && inputHandlers.Length > 0)
            {
                inputLayers.AddRange(inputHandlers);
            }
            
            inputLayers.Sort(CompareLayers);
        }

        public void AddInputLayer(IInputHandlerLayer inputHandlerLayer)
        {
            inputLayers.Add(inputHandlerLayer);
            
            inputLayers.Sort(CompareLayers);
        }

        public bool RemoveInputLayer(IInputHandlerLayer inputHandlerLayer)
        {
            return inputLayers.Remove(inputHandlerLayer);
        }

        public void HostInputEvent(IGesture gesture)
        {
            if (_ignoreInputs)
                return;
            
            
            if (inputLayers.Any(layer => layer.InputType == gesture.InputType && layer.TryHandle(gesture)))
            {
                return;
            }
        }

        public void SetIgnoreInputs(bool isLocked)
        {
            _ignoreInputs = isLocked;
        }

        private static int CompareLayers(IInputHandlerLayer a, IInputHandlerLayer b)
        {
            return a.Index.CompareTo(b.Index);
        }
    }
}