using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Enviroment
{
    public class PresendationScreen : AbstractInteraction
    {
        [SerializeField] private Texture[] _presendationTextures;
        [SerializeField] private int screenMaterialId;
        [SerializeField] private MoveController _moveController;

        private MeshRenderer _renderer;
        private Material _material;
        private int presendationId = 0;
        private Camera _camera;
        private bool _isActive = false;

        public override string Name
        {
            get { return null; }
        }

        public override bool Interactable { get; set; }

        public override void Interact(GameObject interacter)
        {
            //if (presendationId >= _presendationTextures.Length)
            //{
            //    Interactable = false;
            //    return;
            //}

            if (_isActive)
            {
                _moveController.CanJump = true;
                _moveController.CanMove = true;
                AudioListener.volume = 1;
                _camera.gameObject.SetActive(false);
                _isActive = false;
            }
            else
            {
                _moveController.CanJump = false;
                _moveController.CanMove = false;
                _camera.gameObject.SetActive(true);
                AudioListener.volume = 0;
                _isActive = true;
            }

            //_material.SetTexture("_MainTex", _presendationTextures[++presendationId]);
            //_material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
        }

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
            _camera.gameObject.SetActive(false);
            Interactable = true;
            _renderer = GetComponent<MeshRenderer>();
            _material = _renderer.materials[screenMaterialId];

            _material.SetTexture("_MainTex", _presendationTextures[presendationId]);
            _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (presendationId >= _presendationTextures.Length - 1) return;

                _material.SetTexture("_MainTex", _presendationTextures[++presendationId]);
                _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (presendationId <= 0) return;

                Interactable = true;
                _material.SetTexture("_MainTex", _presendationTextures[--presendationId]);
                _material.SetTexture("_EmissionMap", _presendationTextures[presendationId]);
            }
        }
    }
}
