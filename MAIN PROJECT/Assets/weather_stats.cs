using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_stats : MonoBehaviour
{
    public int windSpeed;
    public int Downpour;
    public int PercentRain;
    public int temperature;
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


    }
}
