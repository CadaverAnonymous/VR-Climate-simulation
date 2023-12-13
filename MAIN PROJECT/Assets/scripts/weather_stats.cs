using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class weather_stats : MonoBehaviour
{
    public GameObject floor;
    public Material snowmat;
    public Material GreenMat;
    public int windSpeedv = 0;
    public int Downpour = 1;
    public int PercentRain = 0;
    public float temperatureF = 70f;
    public float snowSpeed = 5f; // Changed to float as it's a speed value
    public float rainSpeed = 10f; // Changed to float as it's a speed value
    public bool toggle_wind;
    public bool toggle_rain;
    public ParticleSystem particleSystem;
    public float maxtemp = 120;
    public float mintemp = -30;
    
    public List<GameObject> leverObjects = new List<GameObject>();
    public List<GameObject> buttonObjects = new List<GameObject>();
    
        

    // Start is called before the first frame update
    void Start()
    {
        if (toggle_rain == true && PercentRain > 0)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
        if (temperatureF < 32)
        {
            ChangeToSnow();
            floor.GetComponent<MeshRenderer>().material = snowmat;
        }
        else
        {
            ChangeToRain();
            floor.GetComponent<MeshRenderer>().material = GreenMat;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float Lever1Rotate = leverObjects[1].transform.rotation.eulerAngles.x;
       // Lever2Rotate = leverObjects[2].transform.rotate.eulerangles.x
        //Lever3Rotate = leverObjects[3].transform.rotate.eulerangles.x
        if (Lever1Rotate > 30 && Lever1Rotate < 75 && temperatureF < maxtemp)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising temperature");
            
            temperatureF += .01f;
        }
        if (Lever1Rotate < 330  && Lever1Rotate > 280 && temperatureF > mintemp)

        {
            UnityEngine.Debug.Log("lowering temperature");
            temperatureF -= .01f;
        }    

        if (toggle_rain == true && PercentRain > 0)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
        if (temperatureF < 32)
        {
            ChangeToSnow();
        }
        else
        {
            ChangeToRain();
        }
    }

    void ChangeToRain()
    {
        var mainMod = particleSystem.main; // Added '=' and changed var type
        mainMod.startSpeed = rainSpeed;
    }

    void ChangeToSnow()
    {
        var mainMod = particleSystem.main; // Added '=' and changed var type
        mainMod.startSpeed = snowSpeed;
    }
}
