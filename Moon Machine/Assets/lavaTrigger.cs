using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaTrigger : MonoBehaviour
{

    public GameObject lava;
	public float rate;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT");
            if (lava.GetComponent<risingLava>() != null)
            {
                lava.GetComponent<risingLava>().isRising = true;
                lava.GetComponent<risingLava>().rate = rate;


            }
        }
    }
}
