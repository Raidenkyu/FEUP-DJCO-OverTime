using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
   
    public bool activated_already;
    public bool activate;
    public GameObject player;
    public GameObject button;

    public GameObject Object;
     public Material NewMaterial;
 
    // Update is called once per frame

    void Start(){
        activated_already = false;
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, button.transform.position) < 2.5 )
        {   
            
 
            activate = true;
        }
    
        if(activate && !activated_already){
           //change material
           
            GameObject.Find("button/Sphere").GetComponent<Renderer>().material = NewMaterial;
           
            Debug.Log("aaa");
            moveObject();
            activated_already = true;
        }
        
    }

    void moveObject(){
         Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z+3);
    }
}
