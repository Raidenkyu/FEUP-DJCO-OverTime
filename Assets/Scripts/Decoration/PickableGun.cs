using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableGun : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact() {
        Activated.Invoke();
        Destroy(gameObject);
    }
}
