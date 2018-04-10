using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public GameObject g;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0))
		{
			this.gameObject.transform.RotateAround(g.transform.position, Vector3.up, 1);

		}

		else
			this.gameObject.transform.RotateAround(g.transform.position, Vector3.up, 0);

	}
}
