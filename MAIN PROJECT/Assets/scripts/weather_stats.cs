using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;

public class weather_stats : MonoBehaviour
{
    public GameObject floor;
    public Material snowmat;
    public Material GreenMat;
    
    public float Downpour = 1;
    public float percentRain = 0;
    public float temperatureF = 70f;
    public float snowSpeed = 5f; // Changed to float as it's a speed value
    public float rainSpeed = 10f; // Changed to float as it's a speed value
    public float downpour = 10f;
    public bool toggle_wind;
    public bool toggle_rain;
    public bool isRaining;
    
    public Cloth Flag;
    public float windvelocity = 5;
    public Vector3 windSpeedv;
    

    public ParticleSystem rain;
    public ParticleSystem snow;


    private float maxtemp = 120;
    private float mintemp = -30;
    private float maxwind = 300;
    private float minwind = 0;
    private float maxpercent = 100;
    private float minpercent = 0;
    private float maxdownpour = 100;
    private float mindownpour = 0;

    public AudioSource rainsound;
    public AudioSource lowwind;
    public AudioSource Highwind;
    
    
    
    public List<GameObject> leverObjects = new List<GameObject>();
    public List<GameObject> buttonObjects = new List<GameObject>();
    
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flag.externalAcceleration = windSpeedv;
        if (toggle_rain == true && percentRain > 50)
        {
            if (temperatureF < 32)
            {
                ChangeToSnow();
                Stoprain();
                floor.GetComponent<MeshRenderer>().material = snowmat;
            }
            else
            {
                ChangeToRain();
                StopSnow();
                floor.GetComponent<MeshRenderer>().material = GreenMat;

            }
        }
        else
        {
            stopboth();
        }

        if (toggle_wind == true && windvelocity > 20 && windvelocity < 50)
        {
            smallwindnoise();
        }
        else if (toggle_wind == true && windvelocity > 50)
        {
            highwindnoise();
        }
        else
        {
            stopwindnoise();
        }

        

        changestatlever();

        


        
    }

    void ChangeToRain()
    {
        rain.Play();
        if (!rainsound.isPlaying)
        {
            rainsound.Play();
        }
       

    }

    void ChangeToSnow()
    {
        if(rain.isPlaying)
        {
            rain.Stop();
            rainsound.Stop();
        }
        snow.Play();
        
    }

    void Stoprain()
    {
       
        rain.Stop();
        rainsound.Stop();
    }

    void StopSnow()
    {
        snow.Stop();
    
    }

    void stopboth()
    {
        rain.Stop();
        snow.Stop();
    }

    void smallwindnoise()
    {
        
        if (!lowwind.isPlaying)
        {
            UnityEngine.Debug.Log("small wind is playing");
            lowwind.Play();
        }
        Highwind.Stop();
    }
    void highwindnoise()
    {
        if (!Highwind.isPlaying)
        {
            UnityEngine.Debug.Log("high wind is playing");
            Highwind.Play();
        }
        lowwind.Stop();
    }

    void stopwindnoise()
    {
        Highwind.Stop();
        lowwind.Stop();
    }

    void changestatlever()
    {
        float Lever1Rotate = leverObjects[1].transform.rotation.eulerAngles.x;
        float Lever2Rotate = leverObjects[2].transform.rotation.eulerAngles.x;
        float Lever3Rotate = leverObjects[3].transform.rotation.eulerAngles.x;
        float Lever4Rotate = leverObjects[4].transform.rotation.eulerAngles.x;
        // Lever2Rotate = leverObjects[2].transform.rotate.eulerangles.x
        //Lever3Rotate = leverObjects[3].transform.rotate.eulerangles.x
        if (Lever1Rotate > 30 && Lever1Rotate < 75 && temperatureF < maxtemp)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising temperature");

            temperatureF += .01f;

        }
        if (Lever1Rotate < 330 && Lever1Rotate > 280 && temperatureF > mintemp)

        {
            UnityEngine.Debug.Log("lowering temperature");
            temperatureF -= .01f;
        }
        if (Lever2Rotate > 30 && Lever2Rotate < 75 && windvelocity < maxwind)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising windspeed");

            windvelocity += .1f;
            windSpeedv = new Vector3(0, 0, windvelocity);
            Flag.externalAcceleration = windSpeedv;


        }
        if (Lever2Rotate < 330 && Lever2Rotate > 280 && windvelocity > minwind)

        {
            UnityEngine.Debug.Log("lowering windspeed");
            windvelocity -= .1f;
            windSpeedv = new Vector3(0, 0, windvelocity);
            Flag.externalAcceleration = windSpeedv;
        }
        if (Lever3Rotate > 30 && Lever3Rotate < 75 && percentRain < maxpercent)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising percentage of rain");

            percentRain += .1f;
        }
        if (Lever3Rotate < 330 && Lever3Rotate > 280 && percentRain > minpercent)

        {
            UnityEngine.Debug.Log("lowering percentage of rain");
            percentRain -= .1f;
        }
        if (Lever4Rotate > 30 && Lever4Rotate < 75 && downpour < maxdownpour)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising downpour");

            downpour += .01f;
        }
        if (Lever4Rotate < 330 && Lever4Rotate > 280 && downpour > mindownpour)

        {
            UnityEngine.Debug.Log("lowering downpour");
            downpour -= .01f;
        }
    }
}
