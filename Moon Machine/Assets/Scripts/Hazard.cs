using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	
	//TODO change behaviour for different types of hazard

	public string HazardType;
	
	public GameManager gameManager;

	private void OnTriggerEnter(Collider other) {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (other.tag == "Player")
		{
			Debug.Log("Player was killed by " + HazardType);
			gameManager.ResetLevel();
		}
	}
}
