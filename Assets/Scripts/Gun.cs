using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Debug.Log("SHOOT");
            Shoot();
        }
    }

    void Shoot(){
        RaycastHit Hit;
        if(Physics.Raycast(gun.transform.position,gun.transform.forward, out Hit, range)){
            Debug.Log(Hit.transform.name);
        }
    }
}
