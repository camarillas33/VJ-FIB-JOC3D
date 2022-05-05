using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public float fuerzaPutiaso = 10;
    public Animator animator;
    private bool aplastado = false;
    private float colisionTime;
    private bool play;
    public Camera MainCamera;
    public bool freeMovement = false;
    private Vector3 initialPosition, initialScale, initialCameraPosition;
    private Quaternion initialRotation, initialCameraRotation;
    private float x, y;
    private bool collisioned = false;
    private bool finished = false;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        MainCamera = GetComponentInChildren<Camera>();
        initialCameraPosition = MainCamera.transform.localPosition;
        initialCameraRotation = MainCamera.transform.localRotation;
        colisionTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Obté cap a on es mou el personatge
        if(play) {
            if(freeMovement) {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
                // Moviment personatge
                transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge            
                transform.Translate(0, 0, y * runSpeed * Time.deltaTime); // Mou el personatge
            }
            else {
                if(Input.GetKey(KeyCode.A)) y = 1;
                else y = 0;
                if(Input.GetKeyUp(KeyCode.A)) x = 0;
                // Moviment personatge
                transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge            
                transform.Translate(0, 0, y * runSpeed * Time.deltaTime); // Mou el personatge
            }
            
            

        }

        if (aplastado)
        {

            colisionTime += Time.deltaTime;
            Debug.Log(colisionTime);
            if (colisionTime > 2.0f)
            {
                aplastado = false;
                colisionTime = 0;
                MainCamera.transform.localPosition = initialCameraPosition;
                transform.localScale = initialScale;
                respawn();
            }

        }

        animator.SetFloat("speedX", x);
            animator.SetFloat("speedY", y);
            animator.SetBool("col", collisioned); 
            animator.SetBool("end", finished);

    } 

    public void setPlaying(bool b) {
        play = b;
    }  

    public void changePlaying() {
        play = !play;
    }

    private void respawn()
    {
        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        x = 0;
        collisioned = false;
        play = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn") {
            respawn();
        }
        else if(other.tag == "TurnRight") {
            Debug.Log("Triggered by Turn");
            x = 1;
        }
        else if(other.tag == "StopTurn") {
            Debug.Log("Triggered by StopTurn");
            x = 0;
        }
        else if(other.tag == "Aplastador")
        {

            transform.localScale = new Vector3(initialScale.x, 5, initialScale.z * 1.5f);
            MainCamera.transform.localPosition = new Vector3(initialCameraPosition.x, initialCameraPosition.y + 0.5f, initialCameraPosition.z);
            aplastado = true;
            colisionTime = 0;
            play = false;

        }
        else if(other.tag == "Sierra")
        {
            aplastado = true;
            colisionTime = 0;
            play = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        { 
            finished = true;
            play = false;
            
        
            MainCamera.transform.rotation = Quaternion.Euler(0,180,0);
            MainCamera.transform.localPosition = new Vector3(0.0f,0.08f,0.3f);
        }
    }

        void OnCollisionEnter(Collision collision) {
        Collider c = collision.collider;
        Debug.Log(c.tag);
        if (c.tag == "Putiaso")
        {
            collisioned = true;
            Debug.Log("Tremendo putiaso me has dado");
            Vector3 height = new Vector3(0, 4, 0);
            rb.AddForce(fuerzaPutiaso * collision.contacts[0].normal + height, ForceMode.Impulse);
            x = 0;
            play = false;
            aplastado = true;
        }
    }

}
