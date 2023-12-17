using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonselect : MonoBehaviour
{

    public Button start;
    public Button options;
    public Button quit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start.interactable && Input.GetAxis("Jump") == 1)
        {

            start.onClick.Invoke();
        }
        else if (options.interactable && Input.GetAxis("Jump") == 1)
        {
            options.onClick.Invoke();
        }
        else if (quit.interactable && Input.GetAxis("Jump") == 1)
        {
            quit.onClick.Invoke();
        }
    }
}
