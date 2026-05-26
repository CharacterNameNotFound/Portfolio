using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs;
using Game.GameMode.Session.Gameplay.Inputs;
using Game.GameMode.Session.Gameplay.Pools.CubePooling;
using Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling;
using Game.GameMode.Session.UI;
using Game.Utilities.MusicControlling;
using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class HammersReactionSystem : ILoopedSystem
    {
        private AudioArchive _audioArchive;
        private InputBuffer _inputBuffer;
        private HammerSystemsConfig _hammerConfigs;
        private GameCubePool _cubePool;
        private PoppedCubeParticlePool _poppedCubeParticlePool;
        
        public HammersReactionSystem(
            AudioArchive audioArchive, 
            InputBuffer inputBuffer, 
            HammerSystemsConfig hammerConfigs, 
            GameCubePool cubePool, 
            PoppedCubeParticlePool poppedCubeParticlePool)
        {
            _audioArchive = audioArchive;
            _inputBuffer = inputBuffer;
            _hammerConfigs = hammerConfigs;
            _cubePool = cubePool;
            _poppedCubeParticlePool = poppedCubeParticlePool;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        { 
            await ProcessInputs(_inputBuffer, sessionRegistry, cancellationToken);
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
        
        private async UniTask ProcessInputs(InputBuffer inputBuffer, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            bool playSFX = false;
            for (int i = 0; i < inputBuffer.ActivatedLines.Length; i++)
            {
                if (!inputBuffer.ActivatedLines[i])
                {
                    continue;
                }
                
                HammerCubeComponent hammer = sessionRegistry.GameFieldComponent.HummerPoints[i];
                hammer.Transform.position = hammer.OriginalPosition + _hammerConfigs.ActivationAmplitude;

                ColorButton button = sessionRegistry.SessionScreen.ColorButtons[i];
                button.Transform.sizeDelta = _hammerConfigs.UIActivationAmplitude;

                await TryPopCube(hammer.Transform.position.z, i, sessionRegistry, cancellationToken);
                
                
                playSFX = true;
            }


            if (playSFX)
            {
                _audioArchive.PlayBit();
            }
            
        }

        private async UniTask TryPopCube(float activatedHammerZ, int activatedHammerLine, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            bool isAnyHit = false;
            for (int i = 0; i < sessionRegistry.ActiveCubes.Count; i++)
            {
                var cube = sessionRegistry.ActiveCubes[i];
                if (cube.Line != activatedHammerLine)
                {
                    continue;
                }

                float distanceToHammer = Mathf.Abs(cube.Transform.position.z - activatedHammerZ);

                if (distanceToHammer > _hammerConfigs.HammerSize)
                {
                    continue;
                }

                isAnyHit = true;
                
                sessionRegistry.Score++;
                sessionRegistry.ActiveCubes.RemoveAt(i);
                i--;

                PoppedParticlesComponent poppedParticle = await _poppedCubeParticlePool.GetObject(cancellationToken);
                poppedParticle.transform.position = cube.Transform.position;
                poppedParticle.Play(cube.Color, _poppedCubeParticlePool, cancellationToken).Forget();
                _cubePool.ReturnToPool(cube);
            }

            if (isAnyHit)
            {
                return;
            }

            sessionRegistry.Lives--;
        }
        
    }
}