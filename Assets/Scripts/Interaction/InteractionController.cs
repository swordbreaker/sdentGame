using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    private TextMeshProUGUI interactionText;

    public void Start ()
    {
        var interactionTextGameObject = GameObject.Find("InteractionText");
        if (interactionTextGameObject != null)
        {
            interactionText = interactionTextGameObject.GetComponent<TextMeshProUGUI>();
        }
    }
    
    void Update () 
    {
        Debug.DrawRay (transform.position, transform.forward, Color.yellow);
        RaycastHit hitInfo;
        if (interactionText != null)
            interactionText.text = "";
        if (Physics.Raycast (transform.position, transform.forward, out hitInfo, 2, LayerMask.GetMask ("PlayerInteraction")))
        {
            var interactions = hitInfo.transform.gameObject.GetComponents<IInteraction>().Where(x => x.Interactable).ToList();
            if (interactions.Any())
            {
                var interaction = interactions.First();
                if (interactionText != null)
                    interactionText.text = string.Format("[E] {0}", interaction.Name);
                if (Input.GetKeyDown (KeyCode.E)) 
                {
                    interaction.Interact (gameObject);
                }
            }
        }
    }
}
