using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameWideSystems.GameSceneManager.LoadingScreen
{
    public class BasicLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private Image _image;
        
        public UniTask Show(CancellationToken cancellationToken)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide(CancellationToken cancellationToken)
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void UpdateProgress(float operationProgress)
        {
            float color = 1 - operationProgress;
            _image.color = new Color(color, color, color, 1);
        }

        public UniTask Reset(CancellationToken cancellationToken)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }
    }
}