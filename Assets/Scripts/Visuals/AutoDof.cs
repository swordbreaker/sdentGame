using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts.Visuals
{
    

    public class AutoDof : MonoBehaviour
    {
        [SerializeField]
        private PostProcessProfile _postProcessrofile;

        private Camera _camera;

        private int _transparentFxLayer;

        private void Start()
        {
            _camera = Camera.main;
            _transparentFxLayer = LayerMask.NameToLayer("TransparentFX");
        }

        private void FixedUpdate()
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            //Quaternion.Slerp(transform.localRotation, newRotation, 5 * Time.deltaTime);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 200, ~0, QueryTriggerInteraction.Ignore)) //all layer expept Transparent FX layer
            {
                DepthOfField setting;
                if(_postProcessrofile.TryGetSettings<DepthOfField>(out setting))
                {
                    var dist = Mathf.Lerp(setting.focusDistance.value, Vector3.Distance(transform.position, hit.point), 0.5f);
                    //setting.focusDistance.value = Vector3.Distance(transform.position, hit.point);
                    setting.focusDistance.Override(dist);
                }
            }
        }

    }
}
