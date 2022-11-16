using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour

{
    float turnspeed = 100.0f;
    float movespeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * turnspeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        transform.Translate(0f, 0f, movespeed * Input.GetAxis("Vertical") * Time.deltaTime);

    }
}
