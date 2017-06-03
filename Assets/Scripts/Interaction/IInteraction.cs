using UnityEngine;

public interface IInteraction
{
    bool Interactable { get; set; }
    string Name { get; }

    void Interact(GameObject interacter);
}