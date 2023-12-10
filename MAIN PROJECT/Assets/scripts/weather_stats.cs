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
    public int temperatureF = 70;
    public float snowSpeed = 5f; // Changed to float as it's a speed value
    public float rainSpeed = 10f; // Changed to float as it's a speed value
    public bool toggle_wind;
    public bool toggle_rain;
    public ParticleSystem particleSystem;
    
    public List<GameObject> gameObjects = new List<GameObject>();

    


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
