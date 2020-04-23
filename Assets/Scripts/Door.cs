using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    bool open;

    void Start()
    {
        anim = GetComponent<Animator>();
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !open){
            anim.SetTrigger("PlayerOpen");
            anim.ResetTrigger("PlayerClose");
            open = true;
        }
        else if(Input.GetButtonDown("Jump") && open){
             anim.SetTrigger("PlayerClose");
             anim.ResetTrigger("PlayerOpen");
             open = false;
        }

       
        
    }

}
