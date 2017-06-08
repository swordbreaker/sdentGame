using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class ColorExtention
    {
        public static Color SetAlpha(this Color color, float value)
        {
            return new Color(color.r, color.g, color.b, value);
        }

        public static float Distance(this Color color, Color otherColor)
        {
            float dist = 0f;
            for (int i = 0; i < 4; i++)
            {
                dist = Mathf.Abs(color[i] - otherColor[i]);
            }
            return dist;
        }
    }
}
