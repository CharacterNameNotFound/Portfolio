using System;
using UnityEngine;

namespace Game.GameMode.Session.WorldGeneration.SchemaGeneration
{
    [Serializable]
    public class LayerGenerationConfig
    {
        [field: SerializeField] public float Threshold { get; set; }
    }
}