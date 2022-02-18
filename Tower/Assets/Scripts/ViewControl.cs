using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{

    public float speed = 1f;
    public float mouseSpeed = 90f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel"); // -1 to 1;
        //Debug.Log(mouse);
        transform.Translate(new Vector3(h, mouse*mouseSpeed, v) * Time.deltaTime*speed, Space.World);
  


    }
}
