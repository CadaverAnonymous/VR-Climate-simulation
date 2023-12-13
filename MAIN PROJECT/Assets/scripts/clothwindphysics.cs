using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulator : MonoBehaviour
{
    public float windStrength = 1f;
    public float windSpeed = 1f;
    public Cloth flag;
    void Start()
    {
        // Get the Cloth component from the flag mesh
        

        // Create a wind force that will affect the cloth
        
        // Create a coroutine to apply the wind force over time
       
    }
    void update()
    {

        Vector3 windForce = new Vector3(windStrength, 0, 0);
        flag.externalAcceleration = windForce;

    }

   
}
