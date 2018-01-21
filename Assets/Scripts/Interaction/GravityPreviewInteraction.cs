using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class GravityPreviewInteraction : AbstractInteraction
    {
        [SerializeField] private GravityPreviewBall _gravityPreviewBall;

        public override string Name
        {
            get
            {
                return "Benutze";
            }
        }

        public override void Interact(GameObject interacter)
        {
            _gravityPreviewBall.ToggleDirection();
        }
    }
}
