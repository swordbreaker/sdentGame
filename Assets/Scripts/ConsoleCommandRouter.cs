using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Movement;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Assets.Scripts
{
    public class ConsoleCommandRouter : MonoBehaviour
    {
        private DialogEventManagement _dialogEventManagement;
        private ConsoleCommandsRepository _repo;

        void Start()
        {
            _dialogEventManagement = FindObjectOfType<DialogEventManagement>();

            _repo = ConsoleCommandsRepository.Instance;
            _repo.RegisterCommand("help", Help);
            _repo.RegisterCommand("timeScale", TimeScale);
            _repo.RegisterCommand("openDoors", OpenDoors);
            _repo.RegisterCommand("repairEngine", RepairEngine);
            _repo.RegisterCommand("movementSpeed", MovementSpeed);
            _repo.RegisterCommand("movementSpeed", MovementSpeed);
            _repo.RegisterCommand("beginSlaughter", BeginSlaughter);
            _repo.RegisterCommand("broadcastFungus", BroadcastFungus);
        }

        public string Help(params string[] args)
        {
            var sb = new StringBuilder();
            _repo.Repository.ForEach(pair => sb.Append(pair.Key + "\n\r"));

            return sb.ToString();
        }

        public string TimeScale(params string[] args)
        {
            if (args.Length < 1)
            {
                return "usage timeScale [float]";
            }
            var scale = args[0];
            float timeScale = 0f;
            if (float.TryParse(scale, out timeScale))
            {
                Time.timeScale = timeScale;
                return "Set Timescale to " + timeScale;
            }
            else
            {
                return "Cannot parse " + args[0] + " to a number";
            }
        }

        public string OpenDoors(params string[] args)
        {
            foreach (var door in FindObjectsOfType<Door>())
            {
                door.IsOpen = true;
            }

            return "All doors are open";
        }

        public string RepairEngine(params string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                Fungus.Flowchart.BroadcastFungusMessage("Engineroom_Repair");
            }

            return "Engine repaired";
        }

        public string MovementSpeed(params string[] args)
        {
            if (args.Length < 1)
            {
                return "usage movementSpeed [float]";
            }

            var speed = args[0];
            float speedF = 0f;
            if (float.TryParse(speed, out speedF))
            {
                FindObjectOfType<MoveController>().Speed = speedF;
                return "Set movementspeed to " + speedF;
            }
            else
            {
                return "Cannot parse " + args[0] + " to a number";
            }
        }

        public string BeginSlaughter(params string[] args)
        {
            Fungus.Flowchart.BroadcastFungusMessage("Engineroom_Exit");
            return "May the Slaughter begin";
        }

        public string BroadcastFungus(params string[] args)
        {
            if (args.Length < 1)
            {
                return "usage broadcastFungus [string]";
            }

            Fungus.Flowchart.BroadcastFungusMessage(args[0]);
            return "Fungus message broadcasted";
        }
    }
}
