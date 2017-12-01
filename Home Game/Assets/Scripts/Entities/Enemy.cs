﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;

    PlayerController player;

    public float upwardForce;
    public float outwardForce;

    public AudioClip EnemyExplosionClip;

    private void Awake()
    {
        player = PlayerController.instance;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMovement();

        if(player.dayNightControl.currentTime > 0.25f && player.dayNightControl.currentTime < 0.75f)
        {
            Destroy(this.gameObject);
        }
	}

    void UpdateMovement()
    {
        transform.Translate((player.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Colided with player");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 explosionForce = (player.transform.position - gameObject.transform.position).normalized * outwardForce;
            explosionForce.y = upwardForce;

            rb.AddForce(explosionForce, ForceMode.Impulse);

            other.gameObject.GetComponent<InteractionController>().TryDrop();

            SFXController.instance.SpawnAudioBomb(this.transform.position, EnemyExplosionClip, 1);

            Destroy(this.gameObject);
        }
    }

}
