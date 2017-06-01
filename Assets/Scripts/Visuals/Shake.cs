using UnityEngine;

namespace Assets.Scripts.Visuals
{
    public class Shake : MonoBehaviour
    {
        [SerializeField]
        private Shader shader;

        [SerializeField]
        private float shakeStrength = 0.0f;

        private Material material;

        public float ShakeStrength
        {
            get { return shakeStrength; }
            set { shakeStrength = value; }
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
            material.SetFloat("_ShakeStrength", shakeStrength); 
            Graphics.Blit(src, dst, material);
        }

        public void OnDisable()
        {
            DestroyImmediate(material);
        }
    }
}
