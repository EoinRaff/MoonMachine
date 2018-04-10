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

			this.gameObject.transform.RotateAround(objToAttract.transform.position, Vector3.up + Vector3.right, Mathf.Sin(1));
		}


		rbToAttract.AddForce(force);

	}

	public void setMass(float n){

		rb.mass = n;

	}
}
