﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {


	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (GetComponentInParent<Attractor>().active)
		{
			return;
		}
		else
		{		
			float translation = Input.GetAxis("Vertical") * speed;
			float strafe = Input.GetAxis("Horizontal") * speed;
			translation *= Time.deltaTime;
			strafe *= Time.deltaTime;

			transform.Translate(strafe, 0, translation);
		}*/

        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
