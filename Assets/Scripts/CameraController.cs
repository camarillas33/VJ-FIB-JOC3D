using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera1;
    private Camera camera2;

    public GameObject player01;
    public GameObject player02;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = player01.GetComponentInChildren<Camera>();
        camera2 = player02.GetComponentInChildren<Camera>();

        camera1.enabled = true;
        camera2.enabled = true;

        player01.GetComponent<PlayerController>().setPlaying(true);
        player02.GetComponent<PlayerController>().setPlaying(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*(Input.GetKeyDown(KeyCode.C)) {
            camera1.enabled = !camera1.enabled;
            camera2.enabled = !camera2.enabled;
            
            player01.GetComponent<PlayerController>().changePlaying();
            player02.GetComponent<PlayerController>().changePlaying();
        }*/
    }
}
