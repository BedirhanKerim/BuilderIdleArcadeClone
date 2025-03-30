using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableArea : MonoBehaviour,IInteractable
{
    public event UnityAction OnInteracted;
    public event UnityAction OnInteractEnd;




    public void InteractEnd()
    {
        OnInteractEnd?.Invoke();
    }
    public void Interact()
    {
        OnInteracted?.Invoke();
    }
    
}
