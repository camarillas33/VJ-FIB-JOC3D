using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObstacleController : MonoBehaviour
{
    
    public float speed = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Sin(2*Time.time);
        transform.Translate(0.0f, 0.0f, dist*speed*Time.deltaTime);
    }
}
