using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Items;
using Game.GameMode.Session.Game.Items.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class PlayerSpawnerConfigs : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject PlayerCharacterComponent { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PlayerCamera { get; private set; }
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
        [field: SerializeField] public SwordWeapon SwordWeapon { get; private set; }
    }
    
}