using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightController : MonoBehaviour
{
    public GameObject spotlight;
    public PickUpController pickUpCon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && pickUpCon.equipped == true)
        {
            spotlight.SetActive(!spotlight.activeSelf);
        }

        else if (pickUpCon.equipped == false)
        {
            spotlight.SetActive(false);
        }
    }
}
