using System.Collections;
using Assets.Scripts.Interaction.Capsule;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleDoor : MonoBehaviour
    {
        public delegate void UndockingCompleted();
        public static event UndockingCompleted OnUndockingCompleted;

        private Transform leftTransform;
        private Transform rightTransform;

        private AudioSource audioSource;

        [SerializeField] private AudioClip openCloseAudioClip;

        [SerializeField] private AudioClip undockAudioClip;

        private float xLeftStart;
        private float xRightStart;

        public bool IsOpen { get; set; }

        [SerializeField]
        private bool isLocked = false;
        public bool IsLocked
        {
            get { return isLocked; }
            private set { isLocked = value; }
        }


        private void Start ()
        {
            leftTransform = transform.Find("door_001");
            rightTransform = transform.Find("door");
            audioSource = GetComponent<AudioSource>();

            xLeftStart = leftTransform.localPosition.x;
            xRightStart = rightTransform.localPosition.x;

            UndockInteraction.OnUndock += OnUndock;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(IsLocked) return;
            if (other.tag == "Player")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(openCloseAudioClip);
                Open();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsLocked) return;
            if (other.tag == "Player")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(openCloseAudioClip);
                Close();
            }
        }

        private void Open()
        {
            leftTransform.DOLocalMoveX(-4f, 2f);
            rightTransform.DOLocalMoveX(4f, 2f);
            IsOpen = true;
        }

        private void Close()
        {
            leftTransform.DOLocalMoveX(xLeftStart, 2f);
            rightTransform.DOLocalMoveX(xRightStart, 2f);
            IsOpen = false;
        }

        private void OnUndock()
        {
            IsLocked = true;
            StartCoroutine(Undock());
        }

        private IEnumerator Undock()
        {
            if (IsOpen)
            {
                Close();
                yield return new WaitForSeconds(2);
            }
            audioSource.Stop();
            audioSource.PlayOneShot(undockAudioClip);
            yield return new WaitForSeconds(undockAudioClip.length + 1);
            if (OnUndockingCompleted != null) OnUndockingCompleted();
        }

    }
}
