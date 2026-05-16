using UnityEngine;
using Utils.UtilityTypes.Splines.CatmullRom;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect
{
    public class FlyingParticleParameters : ScriptedVisualEffectParams
    {
        public float Duration;
        public CatmullRomModel CatmullRomModel;
        public Color Color;
        
    }
}