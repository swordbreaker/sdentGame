using Assets.Scripts.Interaction.Capsule;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleDoor : MonoBehaviour
    {
        private Transform leftTransform;
        private Transform rightTransform;
        private AudioSource doorAudio;

        private float xLeftStart;
        private float xRightStart;
        [SerializeField] private bool isOpen = true;

        public bool IsOpen { get; set; }


        private void Start ()
        {
            leftTransform = transform.Find("door_001");
            rightTransform = transform.Find("door");
            doorAudio = GetComponent<AudioSource>();

            xLeftStart = leftTransform.localPosition.x;
            xRightStart = rightTransform.localPosition.x;

            IsOpen = isOpen;

            CapsuleDisplayInteraction.OnUndock += sender =>
            {
                isOpen = false;
                doorAudio.Play();
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!isOpen) return;
            if (other.tag == "Player")
            {
                doorAudio.Play();
                print("open");
                Open();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isOpen) return;
            if (other.tag == "Player")
            {
                doorAudio.Play();
                Close();
            }
        }

        private void Open()
        {
            leftTransform.DOLocalMoveX(-4f, 2f);
            rightTransform.DOLocalMoveX(4f, 2f);
        }

        private void Close()
        {
            leftTransform.DOLocalMoveX(xLeftStart, 2f);
            rightTransform.DOLocalMoveX(xRightStart, 2f);
        }

    }
}
