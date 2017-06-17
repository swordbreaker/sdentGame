using Assets.Scripts.Console.Attributes;
using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConsoleCommandRouter : MonoBehaviour
    {
        private void Start()
        {

            //_repo = ConsoleCommandsRepository.Instance;
            //_repo.RegisterCommand("help", Help);
            //_repo.RegisterCommand("timeScale", TimeScale);
            //_repo.RegisterCommand("openDoors", OpenDoors);
            //_repo.RegisterCommand("repairEngine", RepairEngine);
            //_repo.RegisterCommand("movementSpeed", MovementSpeed);
            //_repo.RegisterCommand("movementSpeed", MovementSpeed);
            //_repo.RegisterCommand("beginSlaughter", BeginSlaughter);
            //_repo.RegisterCommand("broadcastFungus", BroadcastFungus);
            //_repo.RegisterCommand("goto", Goto);

            Console.Console.Instance.RegisterClass<ConsoleCommandRouter>(this);
        }

        //public string Help()
        //{
        //    var sb = new StringBuilder();
        //    _repo.Repository.ForEach(pair => sb.Append(pair.Key + "\n\r"));

        //    return sb.ToString();
        //}

        [ConsoleCommand(Global = true)]
        public void TimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
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

        [ConsoleCommand(Global = true)]
        public void Goto(Vector3 pos)
        {
            FindObjectOfType<MoveController>().transform.position = pos;
        }
    }
}
