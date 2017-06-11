using UnityEngine;

namespace Assets.Scripts.MainMenu
{
    public class FlyingObject : MonoBehaviour
    {
        [SerializeField] private Transform _forcePos;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForceAtPosition(Vector3.forward * 10, _forcePos.position);
            GetComponent<Rigidbody>().AddTorque(new Vector3(0, 50, 0));
        }
    }
}
