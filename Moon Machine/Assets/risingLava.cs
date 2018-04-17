using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingLava : MonoBehaviour {

	public bool isRising;
	public float rate;

	// Use this for initialization
	void Start () {
		isRising = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRising)
		{
			this.transform.position += this.transform.up * rate * Time.deltaTime;
		}
	}
}
