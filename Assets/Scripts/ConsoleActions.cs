using Assets.Scripts.Console.Attributes;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConsoleActions : MonoBehaviour
    {
        private void Start()
        {
            Console.Console.Instance.RegisterClass<ConsoleActions>(this);
        }

        private void OnDestroy()
        {
            Console.Console.Instance.DeregisterClass<ConsoleActions>(this);
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
            Console.Console.Instance.Log(string.Format("Goto {0}", pos));
        }
    }
}
