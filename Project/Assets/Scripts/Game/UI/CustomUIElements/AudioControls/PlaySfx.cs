using Game.Utilities.MusicControlling;
using GameWideSystems.AudioManager;
using UnityEngine;
using Zenject;

namespace Game.UI.CustomUIElements.AudioControls
{
    public class PlaySfx : MonoBehaviour
    {
        private AudioArchive _audioArchive;

        public PlaySfx(AudioArchive audioArchive)
        {
            _audioArchive = audioArchive;
        }

        private void Awake()
        {
            _audioArchive = ProjectContext.Instance.Container.Resolve<AudioArchive>();
        }

        public void PlayButton()
        {
            _audioArchive.PlayButton();
        }
        
    }
}