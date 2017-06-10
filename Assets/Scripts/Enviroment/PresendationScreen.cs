using UnityEngine;

namespace Assets.Scripts.Enviroment
{
    public class PresendationScreen : AbstractInteraction
    {
        [SerializeField] private Texture[] _presendationTextures;
        [SerializeField] private int screenMaterialId;

        private MeshRenderer _renderer;
        private Material _material;
        private int presendationId = 0;

        public override string Name
        {
            get { return null; }
        }

        public override bool Interactable { get; set; }

        public override void Interact(GameObject interacter)
        {
            if (presendationId >= _presendationTextures.Length)
            {
                Interactable = false;
                return;
            }

            _material.SetTexture("_MainTex", _presendationTextures[++presendationId]);
            _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
        }

        private void Start()
        {
            Interactable = true;
            _renderer = GetComponent<MeshRenderer>();
            _material = _renderer.materials[screenMaterialId];

            _material.SetTexture("_MainTex", _presendationTextures[presendationId]);
            _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (presendationId <= 0) return;

                Interactable = true;
                _material.SetTexture("_MainTex", _presendationTextures[--presendationId]);
                _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
            }
        }
    }
}
