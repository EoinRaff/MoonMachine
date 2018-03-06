using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour {

    public GameObject[] wayPoints;
    public GameObject obstacle;
    public int currentWaypoint;
    public float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
        currentWaypoint = 0;
        obstacle = GameObject.Find("Obstacle1");
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 direction = (wayPoints[currentWaypoint].transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().velocity = direction * moveSpeed;
        if (Vector3.Distance(transform.position, wayPoints[currentWaypoint].transform.position) < 2)
        {
            currentWaypoint = (currentWaypoint + 1) % wayPoints.Length;
        }
    }
}
