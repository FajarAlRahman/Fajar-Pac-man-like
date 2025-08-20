using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] public PickableType PickableType;
    public Action<Pickable> OnPicked;

    /// Called when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
           Debug.Log("Pickup :" + PickableType);
            if (OnPicked != null)
            {
                OnPicked(this);
            }
            // Destroy(gameObject);
        }
    }
}
