using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour
{
    public enum Mode {Menu, Options, Game};
    public Mode mode;

    public GameObject player01;
    public GameObject player02;
    //SceneManager.LoadScene("MainMenu"); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1)) {
            SceneManager.LoadScene("Level01");
            mode = Mode.Game;
        }
        if(Input.GetKey(KeyCode.Alpha2)) {
            SceneManager.LoadScene("Level02");
            mode = Mode.Game;
        }
        if(Input.GetKey(KeyCode.Alpha3)) {
            SceneManager.LoadScene("Level03");
            mode = Mode.Game;
        }
        if(Input.GetKey(KeyCode.Alpha4)) {
            SceneManager.LoadScene("Level04");
            mode = Mode.Game;
        }
        if(Input.GetKey(KeyCode.Alpha5)) {
            SceneManager.LoadScene("Level05");
            mode = Mode.Game;
        }
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
            mode = Mode.Menu;
        }
        if(mode != Mode.Game) {
            player01.GetComponent<PlayerController>().setPlaying(false);
            player02.GetComponent<PlayerController>().setPlaying(false);
        } else if(mode == Mode.Game) {
            player01.GetComponent<PlayerController>().setPlaying(true);
            player02.GetComponent<PlayerController>().setPlaying(true);
        }
    }
}
