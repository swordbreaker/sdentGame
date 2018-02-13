using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts.Visuals
{
    

    public class AutoDof : MonoBehaviour
    {
        [SerializeField]
        private PostProcessProfile _postProcessrofile;

        private Camera _camera;

        private int _playerLayer;

        private void Start()
        {
            _camera = Camera.main;
            _playerLayer = LayerMask.GetMask("Player", "TransparentFX");
        }

        private void FixedUpdate()
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            ray.origin = ray.origin + ray.direction * -0.2f;

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            //Quaternion.Slerp(transform.localRotation, newRotation, 5 * Time.deltaTime);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 200, ~_playerLayer, QueryTriggerInteraction.Ignore)) //all layer except Player layer
            {
                DepthOfField setting;

                if (_postProcessrofile.TryGetSettings<DepthOfField>(out setting))
                {
                    var dist = Mathf.Lerp(setting.focusDistance.value, Vector3.Distance(transform.position, hit.point), 0.5f);
                    //setting.focusDistance.value = Vector3.Distance(transform.position, hit.point);
                    setting.focusDistance.Override(dist);
                }
            }
        }

    }
}
