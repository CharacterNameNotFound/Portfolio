using UnityEngine;

namespace Utils.UtilityTypes.Splines.CatmullRom
{
    public class CatmullRomModel
    {
        public Vector2[] Points;
        
        public float Alpha;

        public CatmullRomModel(Vector2[] points, float alpha)
        {
            Points = points;
            Alpha = alpha;
        }

        public void SetUniform(float alpha)
        {
            SetAlpha(0);
        }

        public void SetCentripetal(float alpha)
        {
            SetAlpha(0.5f);
        }

        public void SetChordal(float alpha)
        {
            SetAlpha(1f);
        }
        
        public void SetAlpha(float alpha)
        {
            Alpha = alpha;
        }
        
    }
}