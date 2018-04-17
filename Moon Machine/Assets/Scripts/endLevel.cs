using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel : MonoBehaviour
{

    public GameObject gameOverUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Debug.Log("end Game");
            // Time.timeScale = 0;
            // Cursor.visible = true;
            // Cursor.lockState = CursorLockMode.Confined;
            // gameOverUI.SetActive(true);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
