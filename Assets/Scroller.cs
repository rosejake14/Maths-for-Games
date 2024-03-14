using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y > 0 && this.GetComponent<Camera>().fieldOfView > 20)
        {
            this.GetComponent<Camera>().fieldOfView -= 2;
        }
        if (Input.mouseScrollDelta.y < 0 && this.GetComponent<Camera>().fieldOfView < 140)
        {
            this.GetComponent<Camera>().fieldOfView += 2;
        }

    }
}
