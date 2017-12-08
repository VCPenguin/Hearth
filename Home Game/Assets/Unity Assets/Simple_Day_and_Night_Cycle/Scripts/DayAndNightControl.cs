//2016 Spyblood Games

using UnityEngine;
using System.Collections;

public class DayAndNightControl : MonoBehaviour {

    public float dayStart;
    public float dayEnd;

    Mesh mesh;
	public GameObject StarDome;
	public int currentDay = 0; //day 8287... still stuck in this grass prison... no esacape... no freedom...
	public string DayState;
	public Light directionalLight; //the directional light in the scene we're going to work with
	public float SecondsInAFullDay = 120f; //in realtime, this is about two minutes by default. (every 1 minute/60 seconds is day in game)
	[Range(0,1)]
	public float currentTime = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
	[HideInInspector]
	public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. lower values will make the days go by longer, while higher values make it go faster. This may be useful if you're siumulating seasons where daylight and night times are altered.

	float lightIntensity; //static variable to see what the current light's insensity is in the inspector
	Material starMat;

    bool WipedAllGlue = false;

    public ReflectionProbe colourFixer;

    public Gradient dayDirectionalLightColour;
    public Gradient colourFixerLightColour;
    public Gradient skyboxColour;

	// Use this for initialization
	void Start () {
		lightIntensity = directionalLight.intensity; //what's the current intensity of the light
		starMat = StarDome.GetComponent<MeshRenderer> ().material;
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLight();
		CheckTimeOfDay ();
		currentTime += (Time.deltaTime / SecondsInAFullDay) * timeMultiplier;
		if (currentTime >= 1) {
			currentTime = 0;//once we hit "midnight"; any time after that sunrise will begin.
			currentDay++; //make the day counter go up
		}

        ////wipe all the glue at midnight
        //if(currentTime > 0.9f)
        //{
        //    if(WipedAllGlue == false)
        //    {
        //        Debug.Log("wipedGlue");
        //        PlayerController.instance.interactionController.WipeAllStructures();
        //        WipedAllGlue = true;
        //    }
        //}

        //if(currentTime > 0.1 && currentTime < 0.9f)
        //{
        //    if(WipedAllGlue == true)
        //    {
        //        Debug.Log("reset Glue");
        //        WipedAllGlue = false;
        //    }
        //}

        if (currentTime > dayStart && BGMController.instance.currentTrack == BGMController.CurrentTrack.night)
        {
            BGMController.instance.ChangeMusic(0);
        }

        if (currentTime > dayEnd && BGMController.instance.currentTrack == BGMController.CurrentTrack.day)
        {
            BGMController.instance.ChangeMusic(1);
        }
    }

	void UpdateLight()
	{



        //Morning
        if(currentTime < 0.5f)
        {
            //Directional Light
            //Intensity
            directionalLight.intensity = Mathf.Lerp(0, 1, currentTime * 2);
        }


        //Night
        else
        {
            //Directional Light
            //Intensity
            directionalLight.intensity = Mathf.Lerp(1, 0, (currentTime - 0.5f) * 2);
        }


        //Colour
        directionalLight.color = dayDirectionalLightColour.Evaluate(currentTime);// Color.Lerp(Color.black,Color.white, );

        //Colour Fixer
        //Background
        colourFixer.backgroundColor = colourFixerLightColour.Evaluate(currentTime);

        //Camera
        //Background
        Camera.main.backgroundColor = skyboxColour.Evaluate(currentTime);



        //directionalLight.intensity = lightIntensity * intensityMultiplier;
    }

	void CheckTimeOfDay ()
	{
	if (currentTime < dayStart || currentTime > 1f) {
			DayState = "Midnight";
		}
		if (currentTime > dayStart)
		{
			DayState = "Morning";

		}
		if (currentTime > dayStart && currentTime < 0.5f)
		{
			DayState = "Mid Noon";
		}
		if (currentTime > 0.5f && currentTime < dayEnd)
		{
			DayState = "Evening";

		}
		if (currentTime > dayEnd && currentTime < 1f)
		{
			DayState = "Night";
		}
	}

	void OnGUI()
	{
		//debug GUI on screen visuals
		//GUI.Box (new Rect (15, 15, 100, 25), "Day: " + currentDay);
		//GUI.Box (new Rect (40, 40, 200, 30), "" + DayState);
	}
}
