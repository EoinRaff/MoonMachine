using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	const float G = 0.6674f;

	public Rigidbody rb;
	public bool active = true;
	public bool decays;
	public float massDecay;
	float maxSpeed = 100;

	Quaternion rotation;

	float i;
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
			if (decays)
			{
				rb.mass -= massDecay;
				if (rb.mass <= 0)
				{
					active = false;
					i = 0;
				}
			}	
		}

	}

	void Attract(Attractor objToAttract)
	{
		Rigidbody rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		float forceMagnitude = G * (rb.mass * rbToAttract.mass)/Mathf.Pow(distance, 2);
		float clampSpeed = Mathf.Clamp(forceMagnitude, 0, maxSpeed);		   // Makes sure we do not accelerate beyond the speed of light
		//Vector3 force = direction.normalized * forceMagnitude;
		Vector3 force = direction.normalized * clampSpeed;



		if (gameObject.name == "Player")
		{

				rotation = Quaternion.LookRotation(objToAttract.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, i * distance);

			//this.gameObject.transform.RotateAround(objToAttract.transform.position, Vector3.up + Vector3.right, Mathf.Sin(1));
			i++;
		}


		rbToAttract.AddForce(force);

	}

	public void setMass(float n){

		rb.mass = n;

	}
}
