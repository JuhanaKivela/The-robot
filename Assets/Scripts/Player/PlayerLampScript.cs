using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLampScript : MonoBehaviour {

    private bool usingLamp = false;
    public GameObject lampObject;
	
	void Update ()
    {
		if(Input.GetButtonDown("Lamp"))
        {
            if (usingLamp == false)
            {
                lampObject.SetActive(true);
                usingLamp = true;
            }
            else
            {
                lampObject.SetActive(false);
                usingLamp = false;
            }
        }
	}
}
