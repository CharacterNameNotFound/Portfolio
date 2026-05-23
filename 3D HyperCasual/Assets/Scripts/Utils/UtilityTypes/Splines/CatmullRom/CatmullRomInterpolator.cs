
using UnityEngine;

namespace Utils.UtilityTypes.Splines.CatmullRom
{
    public static class CatmullRomInterpolator
    {
        
        /// <param name="t"> decides section, 0 to 1 is first section, 1 to 2 is second section </param>
        public static Vector2 GetPoint(CatmullRomModel model, float t)
        {
            int p0 = (int) t;
            int p1 = p0 + 1;
            int p2 = p1 + 1;
            int p3 = p2 + 1;

            t %= 1;
            
            // calculate knots
            const float k0 = 0;
            float k1 = GetKnotInterval(model.Points[p0], model.Points[p1], model.Alpha);
            float k2 = GetKnotInterval(model.Points[p1], model.Points[p2], model.Alpha) + k1;
            float k3 = GetKnotInterval(model.Points[p2], model.Points[p3], model.Alpha) + k2;

            // evaluate the point
            float u = Mathf.LerpUnclamped(k1, k2, t);
            Vector2 a1 = Remap(k0, k1, model.Points[p0], model.Points[p1], u);
            Vector2 a2 = Remap(k1, k2, model.Points[p1], model.Points[p2], u);
            Vector2 a3 = Remap(k2, k3, model.Points[p2], model.Points[p3], u);
            Vector2 b1 = Remap(k0, k2, a1, a2, u);
            Vector2 b2 = Remap(k1, k3, a2, a3, u);
            return Remap(k1, k2, b1, b2, u);
        }

        private static Vector2 Remap(float a, float b, Vector2 c, Vector2 d, float u)
        {
            return Vector2.LerpUnclamped(c, d, (u - a) / (b - a));
        }

        private static float  GetKnotInterval(Vector2 a, Vector2 b, float alpha)
        {
            return Mathf.Pow(Vector2.SqrMagnitude(a - b), 0.5f * alpha);
        }
    }
}