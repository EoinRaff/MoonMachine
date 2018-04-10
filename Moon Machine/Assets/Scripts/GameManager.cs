using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameIsPaused = false;

    public static GameManager instance = null;
    public GameObject inputManagerPrefab;
    public GameObject pauseMenuUIPrefab;

    private InputManager inputManager;
	private GameObject pauseMenuUI;

    private Scene scene;
    private int currentLevel = 0;
    private string currentState = "paused";

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        inputManager = Instantiate(inputManagerPrefab).GetComponent<InputManager>();
        inputManager.transform.parent = this.transform;

		if (SceneManager.GetActiveScene().buildIndex > 0)
		{
			Canvas canvas = GameObject.FindObjectOfType<Canvas>();
			pauseMenuUI = Instantiate(pauseMenuUIPrefab);
			pauseMenuUI.transform.parent = canvas.transform;
		}
    }
    private void Start()
    {
        try
        {
            //pauseMenuUI = GameObject.FindGameObjectWithTag("Pause");

        }
        catch (System.Exception)
        {
            Debug.LogError("Can't find pause screen");
            throw;
        }
    }


    void Update()
    {
        CheckInput();
		if (pauseMenuUI == null)
		{
			Debug.Log("Look for UI Element");
			pauseMenuUI = GameObject.FindGameObjectWithTag("Pause");
		}
    }

    public void CheckInput()
    {
        inputManager.checkInput();
    }

    public void ResetLevel()
    {
        //TODO: Implement a "soft reset" within a level
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
    public void Resume()
    {
        //Destroy(pauseMenuUI);
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
