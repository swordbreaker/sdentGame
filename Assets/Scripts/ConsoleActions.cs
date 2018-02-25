using CommandConsole;
using Assets.Scripts.Movement;
using UnityEngine;
using CommandConsole.Attributes;

namespace Assets.Scripts
{
    public class ConsoleActions : MonoBehaviour
    {
        [SerializeField] GameObject _fpsCanvas;

        private bool _fpsEnabled;



        private void Start()
        {
            Console.Instance.RegisterClass<ConsoleActions>(this);
        }

        private void OnDestroy()
        {
            Console.Instance.DeregisterClass<ConsoleActions>(this);
        }

        [ConsoleCommand(Global = true)]
        public void OpenDoors()
        {
            foreach (var door in FindObjectsOfType<Door>())
            {
                door.IsOpen = true;
            }
        }

        [ConsoleCommand(Global = true)]
        public void RepairEngine()
        {
            for (int i = 0; i < 4; i++)
            {
                Fungus.Flowchart.BroadcastFungusMessage("Engineroom_Repair");
            }
        }

        [ConsoleCommand(Global = true)]
        public void MovementSpeed(float movementSpeed)
        {
            FindObjectOfType<MoveController>().Speed = movementSpeed;
        }

        [ConsoleCommand(Global = true)]
        public void BeginSlaughter()
        {
            Fungus.Flowchart.BroadcastFungusMessage("Engineroom_Exit");
        }

        [ConsoleCommand(Global = true)]
        public void BroadcastFungus(string message)
        {
            Fungus.Flowchart.BroadcastFungusMessage(message);
        }

        [ConsoleCommand(Global = true, ReturnMessage = null)]
        public void Goto(Vector3 pos)
        {
            FindObjectOfType<MoveController>().transform.position = pos;
            Console.Instance.Log(string.Format("Goto {0}", pos));
        }

        [ConsoleCommand(Global = true, ReturnMessage = null)]
        public void ToggleFps()
        {
            _fpsEnabled = !_fpsEnabled;
            _fpsCanvas.SetActive(_fpsEnabled);
            if (_fpsEnabled)
            {
                Console.Instance.Log("Fps Graph enabled");
            }
            else
            {
                Console.Instance.Log("Fps Graph disabled");
            }
        }
    }
}
