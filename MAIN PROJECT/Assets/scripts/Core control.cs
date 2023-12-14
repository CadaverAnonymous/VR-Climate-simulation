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
    public bool devmode = true;
    public float globeSpinningForce = 1000.0f;
    public float rotationspeed = 10f;
    
    //referenced from weather stats to mess with UI
    public weather_stats stats;
    

    private bool interacting = false;

    public TextMeshProUGUI Texty;
    public TextMeshProUGUI tempnum;
    public TextMeshProUGUI windnum;
    public TextMeshProUGUI rainTog;
    public TextMeshProUGUI windtog;
   
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
        bool togwind = stats.toggle_wind;
        bool tograin = stats.toggle_rain;

        tempnum.text = Mathf.RoundToInt(temp).ToString();
        windnum.text = Mathf.RoundToInt(windspeed).ToString();
        rainTog.text = tograin.ToString();
        windtog.text = togwind.ToString();
        
        
        
            
        
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
                Texty.text = "Click to press button";
            }

        }
        
        
        //now we detect for collisions. Don't ask me the specifics of how this function works cause i dont know either lol. found it in Unity API.
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("Globe"))
            {
                interacting = true;
                //UnityEngine.Debug.DrawLine(rayposition, DING.point, Color.red, 5.0f);
                //UnityEngine.Debug.Log("DING! " + DING.collider.gameObject.name);
                Rigidbody rb = DING.collider.gameObject.GetComponent<Rigidbody>();
                //UnityEngine.Debug.Log("Spin globe" + interacting);

                rb.AddTorque(Vector3.up * -mouseX * rotationSpeed);
                rb.AddTorque(Vector3.right * -mouseY * rotationSpeed);
            }
            else if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("lever"))
            {
                interacting = true;
                GameObject parentObject = DING.collider.transform.parent.gameObject;
                Rigidbody rb = parentObject.GetComponent<Rigidbody>();
                
                float rotation = -mouseX * rotationspeed * Time.deltaTime;
                rb.AddTorque(Vector3.left * -mouseY );
                //UnityEngine.Debug.Log("Toggle lever" + interacting);
            }

            else if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && DING.collider.gameObject.CompareTag("button"))
            {
                UnityEngine.Debug.Log("Press button");
            }
            else 
            {
                UnityEngine.Debug.Log("no ding... D:");
            }
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
        // WE HAVE MOVEMENT BITCHES!!!! (this line allows us to change the position of the character in real time based on WASD directions)
        transform.position += movement * speed * Time.deltaTime;

        //CHARACTER MOVEMENT BLOCK________________________________________________________________________________________________________

        //DEVMODE BLOCK__________________________________________________________________________________________________________________
        //escape testing if devmode is enabled for my conveniance in testing.
        if (devmode == true && Input.GetKeyDown(KeyCode.Escape))
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
        }

        //DEVMODE BLOCK__________________________________________________________________________________________________________________
        interacting = false;

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

}
