using Game.GameMode.Session.Game.Data;
using Game.GameMode.Session.Inputs;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class PlayerMovement : ILoopedSystem
    {
        private JoystickBuffer _joystickBuffer;

        public PlayerMovement(JoystickBuffer joystickBuffer)
        {
            _joystickBuffer = joystickBuffer;
        }
        

        public void Update(float deltaTime, SessionRegistry sessionRegistry)
        {
            PlayerCharacterComponent playerCharacter = sessionRegistry.PlayerCharacterComponent;

            Vector3 movementVector = _joystickBuffer.Joystick * sessionRegistry.PlayerStats.MoveSpeed;
            Vector3 newPosition = playerCharacter.transform.position + movementVector * deltaTime;

            Bounds gameFieldBounds = sessionRegistry.GameField.Bounds;
            
            // preventing crossing bound
            newPosition = new Vector2(Mathf.Clamp(newPosition.x, gameFieldBounds.min.x, gameFieldBounds.max.x), 
                Mathf.Clamp(newPosition.y, gameFieldBounds.min.y, gameFieldBounds.max.y));
            
            playerCharacter.RigidBody.MovePosition(newPosition);
        }
    }
}