using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Conditions : MonoBehaviour
{
	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player")
		{
			Scene scene = SceneManager.GetActiveScene(); // We get the current level that the player is playing
			if (this.name == "Floor")
			{
				SceneManager.LoadScene(scene.name); // We use this because application.loadLevel is obsolete
			}

			if (this.name == "FinishPlatform")
			{
				SceneManager.LoadScene(scene.buildIndex + 1);    // We check what levels are in the build, and take the one after this.
			}
		}
	}
}
