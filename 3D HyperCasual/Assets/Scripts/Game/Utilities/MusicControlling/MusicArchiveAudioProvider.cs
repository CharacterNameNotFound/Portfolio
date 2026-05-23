using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Utilities.MusicControlling
{
    public class MusicArchiveAudioProvider : ScriptableObject
    {
        [SerializeField] private List<AssetReference> _menuMusic;
        [SerializeField] private List<AssetReference> _sessionMusic;
        [SerializeField] private List<AssetReference> _buttonClick;

        public IReadOnlyList<AssetReference> MenuList => _menuMusic;
        public IReadOnlyList<AssetReference> SessionList => _sessionMusic;
        public IReadOnlyList<AssetReference> ButtonList => _buttonClick;

        
        public IReadOnlyList<AssetReference> GetAssetList(MusicGroup musicGroup)
        {
            return musicGroup switch
            {
                MusicGroup.Menu => MenuList,
                MusicGroup.Session => SessionList,
                _ => throw new ArgumentOutOfRangeException(nameof(musicGroup), musicGroup, null)
            };
        }
        
    }
}