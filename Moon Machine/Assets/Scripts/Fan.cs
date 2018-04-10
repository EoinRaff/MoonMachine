using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    GameObject fan;

	void Start () {
        Transform fanOne = fan.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, 0, Time.deltaTime*3);
	}
}
