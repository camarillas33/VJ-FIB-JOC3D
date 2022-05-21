using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ObstacleController : MonoBehaviour
{
    
    public float speed = 25;
    public float fuerzaRebote = 40;
    private float rotation = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation += 1;
        if(gameObject.tag == "Barril")
        {
            if (transform.position.y < 0) Destroy(this.gameObject);
        }
        else if (gameObject.tag == "Fuegardo")
        {
            Debug.Log("Reboto");
            float dist = Mathf.Sin( Time.time);
            if (dist < 0) dist = 0;
            transform.localScale = new Vector3(dist * speed * Time.deltaTime, dist * speed * Time.deltaTime, dist * speed * Time.deltaTime);
        }
        else if(gameObject.tag == "Toro")
        {
            transform.rotation = Quaternion.Euler(0f, rotation, 0f); 

        }
        else
        {
            float dist = Mathf.Sin(2 * Time.time);
            transform.Translate(0.0f, 0.0f, dist * speed * Time.deltaTime);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider c = collision.collider;
        Debug.Log(c.tag);
        
            Debug.Log("Reboto");
            Vector3 v = new Vector3(0, fuerzaRebote, -fuerzaRebote);
            rb.AddForce(v, ForceMode.Impulse);
        
        
    }
}
