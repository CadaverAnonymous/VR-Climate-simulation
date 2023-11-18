using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        
        //RAYCASTING BLOCK____________________________________________________________________________________________________________
        //Make a ray assigned with no direction for raycasting
        Vector3 rayposition = Vector3.zero;

        // Put that vector in the middle of the camera. we do this by halfing the width and height of the camera pixels. This puts the ray right in the middle of the camera
        rayposition.x = Camera.main.pixelWidth / 2;
        rayposition.y = Camera.main.pixelHeight / 2;

        //now give the ray a direction. in this implementation it is shooting directly in front of the camera.
        Vector3 rayDirection = Camera.main.ScreenToWorldPoint(rayposition) - Camera.main.transform.position;

        //now we test if the ray is working as we need it. Ray is just pointing out at stuff and its not doing anything. TIME TO CHANGE THAT!!!!
        //RaycastHit for the win!!! (this object gets the ray to actually tell us if its colliding with an object)
        RaycastHit DING;

        //now we detect for collisions. Don't ask me the specifics of how this function works cause i dont know either lol. found it in Unity API.
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out DING) && Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("DING! " + DING.collider.gameObject.name);
        }
        else if(Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("no ding... D:");
        }
        //RAYCASTING BLOCK________________________________________________________________________________________________________________

        //CHARACTER MOVEMENT BLOCK________________________________________________________________________________________________________
        // time to apply movement based on camera rotation using WASD
        //movement variables horizontal is controlled by AD. A is negative and D is positive numbers
        float horizontal = Input.GetAxis("Horizontal");
        //verticle is controlled by WS. W is positive numbers, and S is negative numbers
        float vertical = Input.GetAxis("Vertical");
        

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




    }
}
