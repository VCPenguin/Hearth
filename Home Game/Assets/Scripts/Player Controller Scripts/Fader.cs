using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    public float AlphaTarget;
    public float fadeSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Create a tamp colour
        Color tempColour = GetComponent<Image>().color;

        //End the colour's alpha
        tempColour.a = Mathf.Lerp(tempColour.a, AlphaTarget, fadeSpeed);

        //apply new colour
        GetComponent<Image>().color = tempColour;
	}
}
