using UnityEngine;

namespace Assets.Scripts.Visuals
{
    public class ColorOverlay : MonoBehaviour
    {
        [SerializeField]
        private Shader shader;

        private Material material;

        [SerializeField]
        private Vector4 color = Vector4.zero;

        [SerializeField]
        private float colorStrength = 0.0f;

        public Vector4 Color
        {
            get { return color; }
            set { color = value; }
        }

        public float ColorStrength
        {
            get { return colorStrength; }
            set { colorStrength = value; }
        }

        public void Start()
        {
            material = new Material(shader)
            {
                name = "ImageEffectMaterial",
                hideFlags = HideFlags.HideAndDontSave
            };
        }

        public void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            material.SetVector("_Color", color);
            material.SetFloat("_ColorStrength", colorStrength);
            Graphics.Blit(src, dst, material);
        }

        public void OnDisable()
        {
            DestroyImmediate(material);
        }
    }
}
