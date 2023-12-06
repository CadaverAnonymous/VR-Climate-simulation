using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class lockrotation : MonoBehaviour
{
    public float minrox = -45;
    public float maxrox = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Vector3 eulerrotation = transform.eulerAngles;
        eulerrotation.x = Mathf.Clamp(eulerrotation.x, minrox, maxrox);
        transform.eulerAngles = eulerrotation;
    }
}
