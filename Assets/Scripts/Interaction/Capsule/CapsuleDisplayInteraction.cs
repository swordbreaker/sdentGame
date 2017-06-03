using System;
using Assets.Scripts.Capsule;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Interaction.Capsule
{
    public abstract class DisplayState
    {
        protected CapsuleDisplayInteraction DisplayInteraction { get; private set; }
        public string Name { get; private set; }

        protected DisplayState(CapsuleDisplayInteraction displayInteraction, string name)
        {
            DisplayInteraction = displayInteraction;
            Name = name;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public abstract void Interact();
    }

    public sealed class UndockInteraction : DisplayState
    {
        public delegate void Undock();
        public static event Undock OnUndock;

        public UndockInteraction(CapsuleDisplayInteraction displayInteraction) : base(displayInteraction, "Kapsel abdocken")
        {
        }

        public override void Enter()
        {
            DisplayInteraction.Interactable = true;
        }

        public override void Interact()
        {
            DisplayInteraction.State = new UndockingInteraction(DisplayInteraction);
            if (OnUndock != null) OnUndock();
        }
    }

    public sealed class UndockingInteraction : DisplayState
    {
        public UndockingInteraction(CapsuleDisplayInteraction displayInteraction) : base(displayInteraction, "Am Abdocken ...")
        {
        }

        public override void Enter()
        {
            DisplayInteraction.Interactable = true;
            CapsuleDoor.OnUndockingCompleted += OnUndockingComplete;
        }

        public override void Exit()
        {
            CapsuleDoor.OnUndockingCompleted -= OnUndockingComplete;
        }

        public void OnUndockingComplete()
        {
            DisplayInteraction.State = new BoostInteraction(DisplayInteraction);

        }

        public override void Interact()
        {
        }
    }

    public sealed class BoostInteraction : DisplayState
    {
        public delegate void BoosterIgnition();
        public static event BoosterIgnition OnBoosterIgnition;

        public BoostInteraction(CapsuleDisplayInteraction displayInteraction) : base(displayInteraction, "Antrieb starten")
        {
        }

        public override void Enter()
        {
            DisplayInteraction.Interactable = true;
        }

        public override void Interact()
        {
            DisplayInteraction.State = new IdleInteraction(DisplayInteraction);
            if (OnBoosterIgnition != null) OnBoosterIgnition();
        }
    }

    public sealed class IdleInteraction : DisplayState
    {
        public IdleInteraction(CapsuleDisplayInteraction displayInteraction) : base(displayInteraction, "")
        {
        }

        public override void Enter()
        {
            DisplayInteraction.Interactable = false;
            CapsuleSeatInteraction.OnTakeCapsuleSeat += OnTakeCapsuleSeat;
        }

        public override void Exit()
        {
            CapsuleSeatInteraction.OnTakeCapsuleSeat -= OnTakeCapsuleSeat;
        }

        public void OnTakeCapsuleSeat()
        {
            DisplayInteraction.State = new UndockInteraction(DisplayInteraction);
        }

        public override void Interact()
        {
        }
    }

    public sealed class CapsuleDisplayInteraction : MonoBehaviour, IInteraction
    {
        private DisplayState state;
        public DisplayState State {
            get { return state; }
            internal set
            {
                if (state != null) state.Exit();
                if (value != null) value.Enter();
                state = value;
            }
        }

        public bool Interactable { get; set; }

        public string Name
        {
            get { return State.Name; }
        }

        public void Start()
        {
            State = new IdleInteraction(this);
        }

        public void Interact(GameObject interacter)
        {
            State.Interact();
        }
    }
}
