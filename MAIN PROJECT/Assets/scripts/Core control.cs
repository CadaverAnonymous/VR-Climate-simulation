using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System.Runtime.InteropServices;

public class NewBehaviourScript : MonoBehaviour
{

    //WASSUP!!! this is the character core controller which houses pretty much the entire character rig. 

    //variables that we need for the implementation.
    public float speed = 5f;
    // public CharacterController controller;
    public Transform cameraTransform;
    public float rotationSpeed = 5.0f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Vector3 Raycast;
    
    public float globeSpinningForce = 1000.0f;
    public float rotationspeed = 10f;
    
    //referenced from weather stats to mess with UI
    public weather_stats stats;

    public GameObject uiEmptymain;
    public GameObject uiEmptypause;
    

    private bool interacting = false;
    public bool devmode = true;

    public TextMeshProUGUI Texty;
    public TextMeshProUGUI tempnum;
    public TextMeshProUGUI windnum;
    public TextMeshProUGUI downpour;
    public TextMeshProUGUI percentRain;
    public TextMeshProUGUI rainTog;
    public TextMeshProUGUI windtog;

    public List<GameObject> buttonObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        float windspeed = stats.windvelocity;
        float temp = stats.temperatureF;
        float rainchance = stats.percentRain;
        float pour = stats.Downpour;
        bool togwind = stats.toggle_wind;
        bool tograin = stats.toggle_rain;

        tempnum.text = Mathf.RoundToInt(temp).ToString();
        windnum.text = Mathf.RoundToInt(windspeed).ToString();
        rainTog.text = tograin.ToString();
        windtog.text = togwind.ToString();
        downpour.text = Mathf.RoundToInt(pour).ToString();
        percentRain.text = Mathf.RoundToInt(rainchance).ToString();




        Texty.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //MOUSE MOVEMENT BLOCK_____________________________________________________________________________________________________
        // mouse variables

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //UnityEngine.Debug.Log("interacting before: " + interacting);


        // MOUSE MOVEMENT BLOCK________________________________________________________________________________________________


        //Make a ray assigned with no direction for raycasting
        Vector3 rayposition = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, Camera.main.nearClipPlane));

        //now give the ray a direction. in this implementation it is shooting directly in front of the camera.
        Vector3 rayDirection = (rayposition - Camera.main.transform.position).normalized;

        //now we test if the ray is working as we need it. Ray is just pointing out at stuff and its not doing anything. TIME TO CHANGE THAT!!!!
        //RaycastHit for the win!!! (this object gets the ray to actually tell us if its colliding with an object)
        RaycastHit DING;
        UnityEngine.Debug.Log("Interacting = " + interacting);
        //text feedback
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING))
        {
           
            if (DING.collider.gameObject.CompareTag("lever"))
            {
                Texty.enabled = true;
                Texty.text = "Click and Drag to rotate lever.";
            }
            if (DING.collider.gameObject.CompareTag("Globe"))
            {
                Texty.enabled = true;
                Texty.text = "Click and Drag to rotate Globe.";
            }
            if (DING.collider.gameObject.CompareTag("button"))
            {
                Texty.enabled = true;
                Texty.text = "press to change weather";
            }

        }

        if (!uiEmptypause.activeSelf)
        {
            if (devmode == false && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu(uiEmptymain, uiEmptypause);
            }
        }
        else
        {
            if (devmode == false && Input.GetKeyDown(KeyCode.Escape))
            {
                RestoreControls(uiEmptymain, uiEmptypause);
            }
        }
        
        
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("Globe"))
            {
                interacting = true;
                //UnityEngine.Debug.DrawLine(rayposition, DING.point, Color.red, 5.0f);
                //UnityEngine.Debug.Log("DING! " + DING.collider.gameObject.name);
                Rigidbody rb = DING.collider.gameObject.GetComponent<Rigidbody>();
                //UnityEngine.Debug.Log("Spin globe" + interacting);

                rb.AddTorque(Vector3.up * -mouseX);
                rb.AddTorque(Vector3.right * -mouseY);
            }
            else if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("lever"))
            {
                interacting = true;
                GameObject parentObject = DING.collider.transform.parent.gameObject;
                Rigidbody rb = parentObject.GetComponent<Rigidbody>();

                float rotation = -mouseX * rotationspeed * Time.deltaTime;
                rb.AddTorque(Vector3.left * mouseY);
                //UnityEngine.Debug.Log("Toggle lever" + interacting);
            }
            

            else if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("button"))
            {
                GameObject Buttonhit = DING.transform.gameObject;
                string Buttonname = Buttonhit.name;
                if (Buttonname == "Button 1")
                {
                    stats.temperatureF = 101f;
                    stats.percentRain = 30;
                    stats.windvelocity = 7;
                    stats.Downpour = 10;
                }
                if (Buttonname == "Button 2")
                {
                    stats.temperatureF = 60f;
                    stats.percentRain = 100;
                    stats.windvelocity = 7;
                    stats.Downpour = 100;
                }
                if (Buttonname == "Button 3")
                {
                    stats.temperatureF = 80f;
                    stats.percentRain = 0;
                    stats.windvelocity = 30;
                    stats.Downpour = 10;
                }
                if (Buttonname == "Button 4")
                {
                    stats.temperatureF = 40f;
                    stats.percentRain = 0;
                    stats.windvelocity = 30;
                    stats.Downpour = 10;
                }
            }
            else 
            {
                UnityEngine.Debug.Log("no ding... D:");
            }
        }
        else
        {
            interacting = false;
        }

        if (!interacting)
        {
            //UnityEngine.Debug.Log("rotating camera");
            rotatecamera(mouseX, mouseY);
        }




        //UnityEngine.Debug.Log("interacting after: " + interacting);

        //CHARACTER MOVEMENT BLOCK________________________________________________________________________________________________________
        // time to apply movement based on camera rotation using WASD
        //movement variables horizontal is controlled by AD. A is negative and D is positive numbers
        float horizontal = Input.GetAxisRaw("Horizontal");
        //verticle is controlled by WS. W is positive numbers, and S is negative numbers
        float vertical = Input.GetAxisRaw("Vertical");


        // movement based on camera rotation

        // movement variable to simulate 3d FPS movement
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // make movement change positional direction based on camera transform rotation
        movement = cameraTransform.TransformDirection(movement);
        movement.y = 0;
        movement.Normalize();
        // WE HAVE MOVEMENT!!!! (this line allows us to change the position of the character in real time based on WASD directions)
        transform.position += movement * speed * Time.deltaTime;

        //CHARACTER MOVEMENT BLOCK________________________________________________________________________________________________________
        UnityEngine.Debug.Log("Interacting = " + interacting);


    }

    void FixedUpdate()
    { 
    
    }

    void rotatecamera(float mouseX, float mouseY)
    {
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, mouseY * rotationSpeed * Time.deltaTime);

        // lock z axis so camera doesnt get wonky
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }

    void ToggleLever(GameObject lever)
    {
        UnityEngine.Debug.Log("Toggle lever");
        // Add lever toggling functionality here


    }

    void PressButton(GameObject button)
    {
        UnityEngine.Debug.Log("Press button");
        // Add button pressing functionality here
    }

    void PauseMenu(GameObject uiEmptymain, GameObject uiEmptypause)
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        uiEmptymain.SetActive(false);
        uiEmptypause.SetActive(true);
    }
    void RestoreControls(GameObject uiEmptymain, GameObject uiEmptypause)
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        uiEmptymain.SetActive(true);
        uiEmptypause.SetActive(false);
    }
    void ButtonPress()
    { 
    
    
    }
}
