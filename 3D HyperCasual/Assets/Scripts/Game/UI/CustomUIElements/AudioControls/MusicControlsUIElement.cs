using GameWideSystems.AudioManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.CustomUIElements.AudioControls
{
    public class MusicControlsUIElement : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Slider _slider;

        private AudioManager _audioManager;

        // not really nice way, but fast
        private void Awake()
        {
            _audioManager = ProjectContext.Instance.Container.Resolve<AudioManager>();
            

            _slider.SetValueWithoutNotify(_audioManager.MasterVolume);
            _toggle.SetIsOnWithoutNotify(_audioManager.IsMute);
            
            _slider.onValueChanged.AddListener(OnVolumeChange);
            _toggle.onValueChanged.AddListener(ToggleMute);
        }

        private void ToggleMute(bool value)
        {
            _audioManager.ToggleMute(value);
        }

        private void OnVolumeChange(float value)
        {
            _audioManager.SetMasterVolume(value);
        }
        
    }
}