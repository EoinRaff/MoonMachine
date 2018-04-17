using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	// const float G = 667.4f;

	public Rigidbody rb;
	public bool active = true;
	public float minMagnitude = 25;
	public float maxMagnitude = 50;


	void FixedUpdate() {
		Attractor[] attractors = FindObjectsOfType<Attractor>();
		
		if (active)
		{
			foreach (Attractor a in attractors)
			{
				if (a != this)
				{
					Attract(a);
				}
			}
		}
	}

	void Attract(Attractor objToAttract)
	{
		
		Rigidbody rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		//float forceMagnitude = G * (rb.mass * rbToAttract.mass)/Mathf.Pow(distance, 2);
		//Debug.Log("mag = " + forceMagnitude);
		// forceMagnitude = Mathf.Clamp(forceMagnitude, minMagnitude, maxMagnitude);
		// forceMagnitude = Mathf.Clamp()
		// Debug.Log("Clamped mag = " + forceMagnitude);
		Vector3 force = direction.normalized * 25; //replace Grav. calculation with constant force power. Not stored as variable because it keeps changing to 0 for some reason...

		rbToAttract.AddForce(force);
	}

	public void setMass(float n){

		rb.mass = n;

	}
}
