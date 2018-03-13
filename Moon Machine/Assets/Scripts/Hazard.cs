using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	
	//TODO change behaviour for different types of hazard

	public string HazardType;

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			Debug.Log("Player was killed by " + HazardType);
		}
	}
}
