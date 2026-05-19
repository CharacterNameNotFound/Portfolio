using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class PlayerCameraMovement : ILoopedSystem
    {
        public void Update(float deltaTime, SessionRegistry sessionRegistry)
        {
            Vector3 position = sessionRegistry.PlayerCharacterComponent.Transform.position;
            Bounds cameraBound = sessionRegistry.PlayerCameraComponent.CameraBounds;
            Bounds levelBound = sessionRegistry.GameField.Bounds;

            position.x = Mathf.Clamp(position.x, 
                levelBound.min.x + cameraBound.min.x,
                levelBound.max.x - cameraBound.max.x);
            
            position.y = Mathf.Clamp(position.y, 
                levelBound.min.y + cameraBound.min.y,
                levelBound.max.y - cameraBound.max.y);

            position.z = -10;
            
            sessionRegistry.PlayerCameraComponent.Transform.position = position;
        }
        
        
    }
}