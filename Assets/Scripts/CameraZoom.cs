using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraZoom : MonoBehaviour
    {
        private float _defaultFov;
        private bool _active;


        private void Start()
        {
            _defaultFov = Camera.main.fieldOfView;
        }


        public void Update()
        {
            if(Input.GetMouseButtonDown(1))
            {
                Camera.main.fieldOfView = _defaultFov - 20;
                _active = true;
            }

            if(_active)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    Camera.main.fieldOfView = _defaultFov;
                }
            }
        }
    }
}
