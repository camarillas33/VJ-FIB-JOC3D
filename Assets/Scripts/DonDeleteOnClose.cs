using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DonDeleteOnClose : MonoBehaviour
{
   private void Awake()
    {
        string name = SceneManager.GetActiveScene().name;
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
        if (name == "MainMenu" || name == "InstructionsMenu" || name == "Credits")
        {
            GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
            if (musicObj.Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);
        
    }
}
