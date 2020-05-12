using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float range = 100f;
    public GameObject gun;
    public float totalTime = 60;
    private float currentTime;

    public float totalShifts = 2;
    public float currentShifts;

    
    public TextMeshPro tmp;

    // Update is called once per frame
    void Start(){
        currentShifts = totalShifts;
        currentTime = totalTime;
    }
    void Update() {

    }

    private void FixedUpdate() {
        if(currentTime > 0)
        clockTicks();


        lightUpWires();
    }

    public void clockTicks(){
        currentTime--;
        tmp.text = currentTime.ToString();
    }

    public void lightUpWires(){

    }

    public void Shoot() {
        FreezeMonster();
        ResetTime();
    }

    void FreezeMonster() {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 13;
        
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
