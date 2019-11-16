using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundscript : MonoBehaviour
{
    public GameObject cameraobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraobj.transform.position.x, cameraobj.transform.position.y, 3);
    }
}
