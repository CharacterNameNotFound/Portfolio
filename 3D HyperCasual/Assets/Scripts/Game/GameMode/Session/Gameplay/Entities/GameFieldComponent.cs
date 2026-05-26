using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.Entities
{
    public class GameFieldComponent : MonoBehaviour
    {
        public List<Transform> SpawnPoints;
        public List<HammerCubeComponent> HummerPoints;
    }
}