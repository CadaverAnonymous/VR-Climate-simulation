using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_stats : MonoBehaviour
{
    public int windSpeed;
    public int Downpour;
    public int PercentRain;
    public int temperatureF;
    public float snowSpeed = 5f; // Changed to float as it's a speed value
    public float rainSpeed = 10f; // Changed to float as it's a speed value
    public bool toggle_wind;
    public bool toggle_rain;
    public ParticleSystem particleSystem;

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
        }
        else
        {
            ChangeToRain();
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
