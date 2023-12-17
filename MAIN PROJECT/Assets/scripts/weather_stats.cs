using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class weather_stats : MonoBehaviour
{


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

    //for lever rotation if statements. these variables make my life easier when i gotta move stuff around
    public float Highmin = 300;
    public float Highmax = 360;
    public float lowmin = 180;
    public float lowmax = 230;

    public List<GameObject> leverObjects = new List<GameObject>();
    




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

            }
            else
            {
                ChangeToRain();
                StopSnow();


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
        if (rain.isPlaying)
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
    void changesky()
    { 
    
    
    }

    void changestatlever()
    {
        float Lever1Rotate = leverObjects[1].transform.rotation.eulerAngles.z;
        float Lever2Rotate = leverObjects[2].transform.rotation.eulerAngles.z;
        float Lever3Rotate = leverObjects[3].transform.rotation.eulerAngles.z;
        float Lever4Rotate = leverObjects[4].transform.rotation.eulerAngles.z;
        // Lever2Rotate = leverObjects[2].transform.rotate.eulerangles.x
        //Lever3Rotate = leverObjects[3].transform.rotate.eulerangles.x
        if (Lever1Rotate > Highmin && Lever1Rotate < Highmax && temperatureF < maxtemp)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising temperature");

            temperatureF += .01f;

        }
        if (Lever1Rotate < lowmax && Lever1Rotate > lowmin && temperatureF > mintemp)

        {
            UnityEngine.Debug.Log("lowering temperature");
            temperatureF -= .01f;
        }
        if (Lever2Rotate > Highmin && Lever2Rotate < Highmax && windvelocity < maxwind)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising windspeed");

            windvelocity += .1f;
            windSpeedv = new Vector3(0, 0, windvelocity);
            Flag.externalAcceleration = windSpeedv;


        }
        if (Lever2Rotate < lowmax && Lever2Rotate > lowmin && windvelocity > minwind)

        {
            UnityEngine.Debug.Log("lowering windspeed");
            windvelocity -= .1f;
            windSpeedv = new Vector3(0, 0, windvelocity);
            Flag.externalAcceleration = windSpeedv;
        }
        if (Lever3Rotate > Highmin && Lever3Rotate < Highmax && percentRain < maxpercent)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);

            UnityEngine.Debug.Log("raising percentage of rain");

            percentRain += .1f;
        }
        if (Lever3Rotate < lowmax && Lever3Rotate > lowmin && percentRain > minpercent)

        {
            UnityEngine.Debug.Log("lowering percentage of rain");
            percentRain -= .1f;
        }
        if (Lever4Rotate > Highmin && Lever4Rotate < Highmax && downpour < maxdownpour)
        {
            //UnityEngine.Debug.Log("Current Lever Rotation: " + Lever1Rotate);
            //UnityEngine.Debug.Log("Current Temperature: " + temperatureF);
            var rainmain = rain.emission;
            var snowmain = snow.emission;

            UnityEngine.Debug.Log("raising downpour");

            downpour += .01f;
            rainmain.rateOverTime = downpour;
            snowmain.rateOverTime = downpour;
        }
        if (Lever4Rotate < lowmax && Lever4Rotate > lowmin && downpour > mindownpour)

        {
            var rainmain = rain.emission;
            var snowmain = snow.emission;
            UnityEngine.Debug.Log("lowering downpour");
            downpour -= .01f;

            rainmain.rateOverTime = downpour;
            snowmain.rateOverTime = downpour;
        }
    }
}
