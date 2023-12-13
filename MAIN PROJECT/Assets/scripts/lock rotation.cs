using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockrotation : MonoBehaviour
{
    public float minrox = -45f;
    public float maxrox = 45f;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {

        if (transform.parent == null)
        {
            UnityEngine.Debug.Log("lock rotation script activate");
            Vector3 eulerrotation = rb.rotation.eulerAngles;

            if (eulerrotation.x > maxrox)
            {
                eulerrotation.x = maxrox;
                rb.rotation = Quaternion.Euler(eulerrotation);
            }

            if (eulerrotation.x < minrox)
            {
                eulerrotation.x = minrox;
                rb.rotation = Quaternion.Euler(eulerrotation);
            }
        }

    }
}