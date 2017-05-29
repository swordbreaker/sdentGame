using UnityEngine;

namespace Assets.Scripts.Visuals
{
    public class RadialBlur : MonoBehaviour
    {
        [SerializeField]
        private Shader shader;

        [SerializeField]
        private float blurStrength = 0;

        [SerializeField]
        private float blurWidth = 0.0f;

        private Material material;

        public float BlurStrength
        {
            get { return blurStrength; }
            set { blurStrength = value; }
        }

        public float BlurWidth
        {
            get { return blurWidth; }
            set { blurWidth = value; }
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
            material.SetFloat("_BlurStrength", blurStrength);
            material.SetFloat("_BlurWidth", blurWidth);
            Graphics.Blit(src, dst, material);
        }

        public void OnDisable()
        {
            DestroyImmediate(material);
        }
    }
}
