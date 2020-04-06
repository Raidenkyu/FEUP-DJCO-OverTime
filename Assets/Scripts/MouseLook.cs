using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;
    // Start is called before the first frame update

    float xRotation = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ///ATUALMENTE O MOUSE LOOK ESTÁ A USAR AS SETAS PARA MEXER A CÂMARA, MUDAR ESTES VALORES ENTRE PARÊNTESIS PARA Input.GetAxis("Mouse X") e Input.GetAxis("Mouse Y")
        float mouseX = (Convert.ToInt32(Input.GetKey("right"))-Convert.ToInt32(Input.GetKey("left"))) * mouseSensitivity * Time.deltaTime;
        float mouseY = (Convert.ToInt32(Input.GetKey("up"))-Convert.ToInt32(Input.GetKey("down"))) * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);

        playerBody.Rotate(Vector3.up*mouseX);
        
    }
}
