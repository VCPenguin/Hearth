using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    

    public float currentFireTime;
    public float maxFireTime;
    public float maxEffectiveTime;

    public GameObject particleSystem;

    public float minFlamePosition;
    public float maxFlamePosition;

    public Light fireLight;

    public float minLightIntensity;
    public float maxLightIntensity;

    public float maxLightRange;
    public float minLightRange;

    public float sleepBurnPercentageCost;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateFireTime();
	}

    void UpdateFireTime()
    {
        if(currentFireTime > 0)
        {
            currentFireTime -= Time.deltaTime;
        }
        else
        {

        }

        //Determine range of the light based on fire time
        fireLight.range = Mathf.Lerp(fireLight.range, Mathf.Lerp(minLightRange, maxLightRange, (currentFireTime * 2) / maxFireTime), 0.05f);

        //Clamp range
        //If the fireTime is less than 0 make radius 0
        if(currentFireTime < 0)
        {
            fireLight.range = 0;
        }
        //If the fireTime is greater than maxEffectiveTime make radius max
        if(currentFireTime > maxEffectiveTime)
        {
            fireLight.range = maxLightRange;
        }

        //Determine intensity of the light based on fire time
        fireLight.intensity = Mathf.Lerp(fireLight.intensity,  Mathf.Lerp(minLightIntensity,  maxLightIntensity, (currentFireTime * 2) / maxFireTime), 0.05f);

        //Clamp intensity
        //If the fireTime is less than 0 make the intensity 0
        if (currentFireTime < 0)
        {
            fireLight.intensity = 0;
        }
        //If the fireTime is greater than maxEffectiveTime make intensity max
        if (currentFireTime > maxEffectiveTime)
        {
            fireLight.intensity = maxLightIntensity;
        }

        particleSystem.transform.localPosition = new Vector3(particleSystem.transform.position.x, Mathf.Lerp(maxFlamePosition,  minFlamePosition, currentFireTime/maxFireTime), particleSystem.transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.GetComponent<EntityParent>() != null)
        {
            Debug.Log("Collided with fire");
            GameObject parentObject = collision.gameObject;

            if(parentObject.transform.childCount > 1)
            {
                //Get all burnable chield objects
                List<GameObject> burnableChildren = new List<GameObject>();
                List<GameObject> allEntities = new List<GameObject>();
                for (int i = 0; i < parentObject.transform.childCount; i++)
                {
                    allEntities.Add(parentObject.transform.GetChild(i).gameObject);
                    if(parentObject.transform.GetChild(i).gameObject.GetComponent<Entity>().Burnable == true)
                    {
                        burnableChildren.Add(parentObject.transform.GetChild(i).gameObject);
                    }
                }

                foreach(GameObject child in burnableChildren)
                {
                    foreach (GameObject entity in allEntities)
                    {
                        entity.transform.parent = null;
                        entity.GetComponent<Collider>().isTrigger = false;
                        entity.GetComponent<Entity>().InstantiateEntityParent();

                        //Disable all burnable objects so they dont stick to other objects again
                        if(burnableChildren.Contains(entity))
                        {
                            Destroy(entity);
                            //entity.GetComponent<Collider>().enabled = false;
                        }
                        else
                        {
                            entity.GetComponent<Entity>().MakeTempSticky(0.1f);
                        }
                            
                    }
                }
            }
            else
            {
                GameObject childObject = parentObject.transform.GetChild(0).gameObject;
                if(childObject.GetComponent<Entity>().Burnable)
                {
                    SFXController.instance.SpawnAudioBomb(this.transform.position, SFXController.instance.FireWoosh, 0.3f);
                    currentFireTime += childObject.GetComponent<Entity>().BurnTime;
                    Destroy(parentObject);
                }
                
            }
        }
    }
}
