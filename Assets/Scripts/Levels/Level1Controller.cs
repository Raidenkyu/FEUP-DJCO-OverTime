using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    public GameObject Object;

    public void MoveObject(){
        Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z+3);
    }
}
