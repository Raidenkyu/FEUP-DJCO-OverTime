using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public GameObject gun;

    // Update is called once per frame
    void Update() {

    }

    public void Shoot() {
        FreezeMonster();
        ResetTime();
    }

    void FreezeMonster() {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 10;
        
        Vector3 src = gun.transform.position;
        Vector3 dest = gun.transform.forward;

        RaycastHit Hit;        
        if (Physics.Raycast(src, dest, out Hit, range, layerMask)) {
            Debug.DrawRay(src, dest * Hit.distance, Color.white);
            Debug.Log(Hit.collider.tag);
            Hit.collider.gameObject.GetComponent<MonsterBehaviour>().Freeze();
        }
        else {
            Debug.DrawRay(src, dest * range, Color.white);
        }
    }

    void ResetTime() {

    }
}
