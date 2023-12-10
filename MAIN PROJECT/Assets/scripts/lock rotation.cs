using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;


public class lockrotation : MonoBehaviour
{
    public float minrox = -45;
    public float maxrox = 45;





    // Update is called once per frame
    void Update()
    {

        if (transform.parent == null)
        {
            UnityEngine.Debug.Log("lock rotation script activate");
        Vector3 eulerrotation = transform.eulerAngles;
        eulerrotation.x = Mathf.Clamp(eulerrotation.x, minrox, maxrox);
        transform.eulerAngles = eulerrotation;
        }

    }
}
