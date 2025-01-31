using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour
{
    public enum Mode {Menu, Options, Game};
    public Mode mode;
    public int scene;
    public GameObject player01;
    public GameObject player02;
    
    // Start is called before the first frame update
    void Start()
    {
        if (mode != Mode.Game)
        {
            player01.GetComponent<PlayerController>().setPlaying(false);
            player02.GetComponent<PlayerController>().setPlaying(false);
        }
        else if (mode == Mode.Game)
        {
            player01.GetComponent<PlayerController>().setPlaying(true);
            player02.GetComponent<PlayerController>().setPlaying(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        keyPress();
        
    }

    private void keyPress() {
        GameObject gameObjectM = GameObject.Find("BackgroundMusic");
        
        if (Input.GetKey(KeyCode.Alpha1)) {
            if (gameObjectM != null) Destroy(gameObjectM);
            SceneManager.LoadScene("Level01");
            mode = Mode.Game;
        }
        else if(Input.GetKey(KeyCode.Alpha2)) {
            if (gameObjectM != null) Destroy(gameObjectM);
            SceneManager.LoadScene("Level02");
            mode = Mode.Game;
        }
        else if(Input.GetKey(KeyCode.Alpha3)) {
            if (gameObjectM != null) Destroy(gameObjectM);
            SceneManager.LoadScene("Level03");
            mode = Mode.Game;
        }
        else if(Input.GetKey(KeyCode.Alpha4)) {
            if (gameObjectM != null) Destroy(gameObjectM);
            SceneManager.LoadScene("Level04");
            mode = Mode.Game;
        }
        else if(Input.GetKey(KeyCode.Alpha5)) {
            if (gameObjectM != null) Destroy(gameObjectM);
            SceneManager.LoadScene("Level05");
            mode = Mode.Game;
        }
        else if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
            mode = Mode.Menu;
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow) && mode == Mode.Menu) {
            GetComponent<MenuController>().changeModeUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && mode == Mode.Menu) {
            GetComponent<MenuController>().changeModeDown();
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            if (mode == Mode.Menu)
            {
                int button = GetComponent<MenuController>().getButton();
                if (button == 0)
                {
                    if (gameObjectM != null) Destroy(gameObjectM);
                    SceneManager.LoadScene("Level01");
                }
                else if (button == 1) SceneManager.LoadScene("InstructionsMenu");
                else SceneManager.LoadScene("Credits");
            }
            else if (mode == Mode.Game)
            {
                if (player01.GetComponent<PlayerController>().hasFinished() ||
                   player02.GetComponent<PlayerController>().hasFinished())
                {
                    if (scene == 1) SceneManager.LoadScene("Level02");
                    if (scene == 2) SceneManager.LoadScene("Level03");
                    if (scene == 3) SceneManager.LoadScene("Level04");
                    if (scene == 4) SceneManager.LoadScene("Level05");
                    if (scene == 5) SceneManager.LoadScene("Credits");
                }
            }
        }
    }
}
