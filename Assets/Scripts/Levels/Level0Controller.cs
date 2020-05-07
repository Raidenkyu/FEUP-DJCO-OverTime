using UnityEngine;

public class Level0Controller : MonoBehaviour
{
    public GameObject Object;

    public void MoveObject(){
        Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z+3);
    }
}
