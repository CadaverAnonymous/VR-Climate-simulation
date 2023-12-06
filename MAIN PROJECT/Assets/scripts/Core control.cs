using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;


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
    public float rotationspeed = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //MOUSE MOVEMENT BLOCK_____________________________________________________________________________________________________
        // mouse variables
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // this line enables camera rotation movement based on mouse movementmove on y and x axis
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, mouseY * rotationSpeed * Time.deltaTime);

        // lock z axis so camera doesnt get wonky
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        // MOUSE MOVEMENT BLOCK________________________________________________________________________________________________

      
        //Make a ray assigned with no direction for raycasting
        Vector3 rayposition = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, Camera.main.nearClipPlane));

        //now give the ray a direction. in this implementation it is shooting directly in front of the camera.
        Vector3 rayDirection = (rayposition - Camera.main.transform.position).normalized;

        //now we test if the ray is working as we need it. Ray is just pointing out at stuff and its not doing anything. TIME TO CHANGE THAT!!!!
        //RaycastHit for the win!!! (this object gets the ray to actually tell us if its colliding with an object)
        RaycastHit DING;

        //now we detect for collisions. Don't ask me the specifics of how this function works cause i dont know either lol. found it in Unity API.
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && Input.GetMouseButton(0) && DING.collider.gameObject.CompareTag("Globe"))
        {
            //UnityEngine.Debug.DrawLine(rayposition, DING.point, Color.red, 5.0f);
            //UnityEngine.Debug.Log("DING! " + DING.collider.gameObject.name);
            Rigidbody rb = DING.collider.gameObject.GetComponent<Rigidbody>();
            UnityEngine.Debug.Log("Spin globe");

            rb.AddTorque(Vector3.up * -mouseX * rotationSpeed);
            rb.AddTorque(Vector3.right * -mouseY * rotationSpeed);
        }
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && Input.GetMouseButton(0) && DING.collider.gameObject.CompareTag("lever"))
        {
            GameObject parentObject = DING.collider.gameObject.transform.parent.gameObject;
            Rigidbody rb = parentObject.GetComponent<Rigidbody>();
            

            float rotation = -mouseX * rotationspeed * Time.deltaTime;
            rb.AddTorque(rotation,0, 0);
            UnityEngine.Debug.Log("Toggle lever");
        }
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && Input.GetMouseButton(0) && DING.collider.gameObject.CompareTag("button"))
        {
            UnityEngine.Debug.Log("Press button");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("no ding... D:");
        }


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


    }

    void FixedUpdate()
    { 
    
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
