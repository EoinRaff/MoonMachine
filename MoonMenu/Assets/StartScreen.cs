using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

    private GUIStyle fontSize = new GUIStyle();
    private bool gameStart;
    private Rect[] rec;
    private int offset;
    private int visualizationWidth, visualizationHeight;
    ArrayList test = new ArrayList();
    ArrayList test2 = new ArrayList();
    private int textWidth, textHeight;
    private string[] equipment;

    private Camera cam;
    float camSpeed, x;
    private bool camDir;

    bool gamePaused = false;
    // Use this for initialization
    void Start()
    {

        camDir = false;
        cam = Camera.main;
        camSpeed = .0025f;

        visualizationWidth = 1000;
        visualizationHeight = 1000;
        textWidth = 10;
        textHeight = 5;

        gameStart = false;
        equipment = new string[5];
        rec = new Rect[equipment.Length];
        offset = Screen.height / 8;

        test.Add("CONTINUE");
        test.Add("NEW GAME");
        test.Add("LOAD GAME");
        test.Add("OPTIONS");
        test.Add("EXIT");

        test2.Add("CONTINUE");
        test2.Add("LAST SAVE");
        test2.Add("LOAD GAME");
        test2.Add("OPTIONS");
        test2.Add("EXIT");

    }

    void OnGUI() // optimize this 
    {
        fontSize.fontSize = 60;


        if (!gameStart)
        {
            for (int i = 0; i < rec.Length; i++)
            {
                rec[i] = new Rect(offset, i * offset + offset, 200, 100);
                GUI.Box(rec[i], (string)test[i], fontSize);

                if (rec[i].Contains(Event.current.mousePosition))
                {
                    GUI.Box(new Rect(offset - offset / 2, i * offset + offset - (fontSize.fontSize / 4), 600, 100), "");
                    if (Input.GetMouseButtonDown(0))
                    {
                        action((string)test[i]);

                    }
                }
            }
        }

        if (gameStart)
        {
            //            if (Input.GetKeyDown(KeyCode.Escape))
            if (gamePaused)
            {
                Debug.Log("Game paused");
                for (int i = 0; i < rec.Length; i++)
                {
                    rec[i] = new Rect(offset, i * offset + offset, 200, 100);
                    GUI.Box(rec[i], (string)test2[i], fontSize);

                    if (rec[i].Contains(Event.current.mousePosition))
                    {
                        GUI.Box(new Rect(offset - offset / 2, i * offset + offset - (fontSize.fontSize / 4), 600, 100), "");
                        if (Input.GetMouseButtonDown(0))
                        {
                            action((string)test2[i]);
                            gamePaused = false;
                        }
                    }
                }
            }
        }
    }

    void action(string act) { // Include the pausedMenu options too
        
        switch (act)
        {
            case "CONTINUE":
                Debug.Log("Game continued");
                gameStart = true;
                break;

            case "NEW GAME":
                Debug.Log("New game started");
                gameStart = true; 
                break;

            case "LOAD GAME":
                Debug.Log("Load game");
                break;

            case "OPTIONS":
                Debug.Log("Options selected");
                break;

            case "EXIT":
                Debug.Log("Exit selected");
                Application.Quit();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gamePaused = true;

        if (!gameStart)
        {
            if (!camDir)
            {
                if (x >= 10)
                    camDir = true;
                x += camSpeed;
            }

            else if (camDir)
            {
                if (x <= 0)
                    camDir = false;
                x -= camSpeed;
            }

            cam.transform.position = new Vector3(x, cam.transform.position.y, cam.transform.position.z);
        }
    }

// include if ESC
}