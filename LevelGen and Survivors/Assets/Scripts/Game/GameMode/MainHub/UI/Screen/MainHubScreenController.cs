using System.Globalization;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Controller;
using Game.GameMode.Session.WorldGeneration;
using Game.Utilities.MusicControlling;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameMode.MainHub.UI.Screen
{
    // There is generic state machine for complex UI initially I avoided using it, and that was mistake...
    public class MainHubScreenController : UIScreen<MainHubScreenParams, MainHubScreenDependencies>
    {
        [field: SerializeField] private Button _playSelectionButton;

        [Header("Level gen configs")] 
        [SerializeField] private TMP_InputField _worldSizeX;
        [SerializeField] private TMP_InputField _worldSizeY;
        
        [SerializeField] private TMP_InputField _noiseFrequencyX;
        [SerializeField] private TMP_InputField _noiseFrequencyY;
        
        [SerializeField] private TMP_InputField _generationChunkSizeX;
        [SerializeField] private TMP_InputField _generationChunkSizeY;
        
        [SerializeField] private TMP_InputField _decorationGrassThreshold;
        [SerializeField] private TMP_InputField _roadSplineVerts;
        
        [SerializeField] private TMP_InputField _roadMaxRadius;
        [SerializeField] private TMP_InputField _roadMinRadius;
        
        [SerializeField] private TMP_InputField _roadTileSpawnResolution;
        [SerializeField] private TMP_InputField _catmullRomAlpha;

        [SerializeField] private TMP_InputField _decorationChunkSizeX;
        [SerializeField] private TMP_InputField _decorationChunkSizeY;
        
        [SerializeField] private TMP_InputField _decorationsPerChunk;
        [SerializeField] private TMP_InputField _minimumDistanceBetweenDecos;
        [SerializeField] private TMP_InputField _generationSanity;
        
        
        
        
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;

        private WorldGenerationConfigs _worldGenerationConfigs;
        
        public override async UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);
            _worldGenerationConfigs = ((MainHubScreenParams) screenParams).WorldGenerationConfigs;

            await Dependencies.AudioArchive.PlayMusic(MusicGroup.Menu, cancellationToken);
            
            _playSelectionButton.onClick.AddListener(() => Play(Application.exitCancellationToken).Forget());

            BindInputFields();
            
            return result;
        }


        private async UniTask Play(CancellationToken cancellationToken)
        {
            Dependencies.AudioArchive.PlayButton();
            
            SessionInitializationParameters initializationParameters = new SessionInitializationParameters(_worldGenerationConfigs);
            
            await Dependencies.GameStateManager.AppendGameState(Dependencies.SessionFactory.Create(), true, initializationParameters, cancellationToken: cancellationToken);
        }
        
        
        private void BindInputFields()
        {
            _worldSizeX.SetTextWithoutNotify(_worldGenerationConfigs.WorldSize.x.ToString());
            _worldSizeX.onEndEdit.AddListener((value) => _worldGenerationConfigs.WorldSize = new Vector2Int(int.Parse(value), _worldGenerationConfigs.WorldSize.y));
            
            _worldSizeY.SetTextWithoutNotify(_worldGenerationConfigs.WorldSize.y.ToString());
            _worldSizeY.onEndEdit.AddListener((value) => _worldGenerationConfigs.WorldSize = new Vector2Int(_worldGenerationConfigs.WorldSize.x, int.Parse(value)));
            
            _noiseFrequencyX.SetTextWithoutNotify(_worldGenerationConfigs.FrequencyX.ToString(CultureInfo.InvariantCulture));
            _noiseFrequencyX.onEndEdit.AddListener((value) => _worldGenerationConfigs.FrequencyX = float.Parse(value));
            
            _noiseFrequencyY.SetTextWithoutNotify(_worldGenerationConfigs.FrequencyY.ToString(CultureInfo.InvariantCulture));
            _noiseFrequencyY.onEndEdit.AddListener((value) => _worldGenerationConfigs.FrequencyY = float.Parse(value));
            
            _generationChunkSizeX.SetTextWithoutNotify(_worldGenerationConfigs.ChunkSize.x.ToString());
            _generationChunkSizeX.onEndEdit.AddListener((value) => _worldGenerationConfigs.ChunkSize = new Vector2Int(int.Parse(value), _worldGenerationConfigs.ChunkSize.y));
            
            _generationChunkSizeY.SetTextWithoutNotify(_worldGenerationConfigs.ChunkSize.y.ToString());
            _generationChunkSizeY.onEndEdit.AddListener((value) => _worldGenerationConfigs.ChunkSize = new Vector2Int(_worldGenerationConfigs.ChunkSize.x, int.Parse(value)));
            
            _decorationGrassThreshold.SetTextWithoutNotify(_worldGenerationConfigs.WorldSchemaGenerationConfigs.Configs[1].Threshold.ToString(CultureInfo.InvariantCulture));
            _decorationGrassThreshold.onEndEdit.AddListener((value) => _worldGenerationConfigs.WorldSchemaGenerationConfigs.Configs[1].Threshold = float.Parse(value));
            
            _roadSplineVerts.SetTextWithoutNotify(_worldGenerationConfigs.RoadGeneratorConfigs.SplineSectionsCount.ToString());
            _roadSplineVerts.onEndEdit.AddListener((value) => _worldGenerationConfigs.RoadGeneratorConfigs.SplineSectionsCount = int.Parse(value));
            
            _roadMaxRadius.SetTextWithoutNotify(_worldGenerationConfigs.RoadGeneratorConfigs.MaxRoadRadius.ToString());
            _roadMaxRadius.onEndEdit.AddListener((value) => _worldGenerationConfigs.RoadGeneratorConfigs.MaxRoadRadius = int.Parse(value));
            
            
            _roadMinRadius.SetTextWithoutNotify(_worldGenerationConfigs.RoadGeneratorConfigs.MinRoadRadius.ToString());
            _roadMinRadius.onEndEdit.AddListener((value) => _worldGenerationConfigs.RoadGeneratorConfigs.MinRoadRadius = int.Parse(value));
            
            
            _roadTileSpawnResolution.SetTextWithoutNotify(_worldGenerationConfigs.RoadGeneratorConfigs.RoadResolution.ToString(CultureInfo.InvariantCulture));
            _roadTileSpawnResolution.onEndEdit.AddListener(value => _worldGenerationConfigs.RoadGeneratorConfigs.RoadResolution = float.Parse(value));
            
            _catmullRomAlpha.SetTextWithoutNotify(_worldGenerationConfigs.RoadGeneratorConfigs.CatmullRomAlpha.ToString(CultureInfo.InvariantCulture));
            _catmullRomAlpha.onEndEdit.AddListener(value => _worldGenerationConfigs.RoadGeneratorConfigs.CatmullRomAlpha = float.Parse(value));
            
            _decorationChunkSizeX.SetTextWithoutNotify(_worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize.x.ToString());
            _decorationChunkSizeX.onEndEdit.AddListener(
                value => _worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize 
                    = new Vector2Int(int.Parse(value), _worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize.y));
            
            _decorationChunkSizeY.SetTextWithoutNotify(_worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize.y.ToString());
            _decorationChunkSizeY.onEndEdit.AddListener(
                value => _worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize 
                    = new Vector2Int(_worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkSize.x, int.Parse(value)));
            
            _decorationsPerChunk.SetTextWithoutNotify(_worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkDecorationCount.ToString());
            _decorationsPerChunk.onEndEdit.AddListener(value => _worldGenerationConfigs.DecorationGeneratorConfigs.DecoratingChunkDecorationCount = int.Parse(value));
            
            
            _minimumDistanceBetweenDecos.SetTextWithoutNotify(_worldGenerationConfigs.DecorationGeneratorConfigs.MinimumDistanceBetweenDecos.ToString());
            _minimumDistanceBetweenDecos.onEndEdit.AddListener(value => _worldGenerationConfigs.DecorationGeneratorConfigs.MinimumDistanceBetweenDecos = float.Parse(value));
            
            
            _generationSanity.SetTextWithoutNotify(_worldGenerationConfigs.DecorationGeneratorConfigs.SanityCount.ToString());
            _generationSanity.onEndEdit.AddListener(value => _worldGenerationConfigs.DecorationGeneratorConfigs.SanityCount = int.Parse(value));
            
            
        }
        
        
    }
}