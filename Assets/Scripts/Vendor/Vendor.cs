using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacting with vendor");
    }

    public void StopInteract()
    {
        Debug.Log("Stop Interacting with vendor");
    }
    
}
