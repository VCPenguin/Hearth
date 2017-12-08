using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBillboard : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        //Target look at position

        Vector3 distanceVector = this.transform.position - PlayerController.instance.firstPersonController.HorizontalTurntable.transform.position;

        transform.LookAt(transform.position + distanceVector);


        GetComponent<MeshRenderer>().enabled = false;

        //if its the right time of day
        if (PlayerController.instance.dayNightControl.currentTime < PlayerController.instance.dayNightControl.dayStart || PlayerController.instance.dayNightControl.currentTime > PlayerController.instance.dayNightControl.dayEnd)
        {
            //If your fire is burning enough
            if (PlayerController.instance.campFire.currentFireTime > PlayerController.instance.campFire.maxEffectiveTime)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
