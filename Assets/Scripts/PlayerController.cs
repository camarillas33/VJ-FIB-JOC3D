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
    private float x, y, rotate;
    private bool collisioned = false;
    private bool finished = false;
    private bool trepando = false;
    private float initialCameraFOV;
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
        initialCameraFOV = MainCamera.fieldOfView;
        colisionTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Obté cap a on es mou el personatge
        if(play) {
            if (trepando)
            {
                if (Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.B)) y = 1;
                else y = 0;
                transform.Translate(0, y * runSpeed * Time.deltaTime, 0); // Mou el personatge
            }
            else  if (freeMovement) {
                
                
                    x = Input.GetAxis("Horizontal");
                    y = Input.GetAxis("Vertical");
                    if (Input.GetKey(KeyCode.M)) y = 1;
                    // Moviment personatge
                    transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge            
                    transform.Translate(0, 0, y * runSpeed * Time.deltaTime); // Mou el personatge
                
            }
            else
            {
                if(gameObject.tag == "JuanCarlosI")
                {
                    if (Input.GetKey(KeyCode.B))
                    {
                        y = 1;
                        x = rotate;
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime); // Mou el personatge
                    }
                    else y = 0;
                    if (Input.GetKeyUp(KeyCode.B)) x = 0;
                    // Moviment personatge
                }
                else
                {
                    if (Input.GetKey(KeyCode.M))
                    {
                        y = 1;
                        x = rotate;
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime); // Mou el personatge
                    }
                    else y = 0;
                    if (Input.GetKeyUp(KeyCode.M)) x = 0;
                    // Moviment personatge
                }


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
        animator.SetBool("escalador", trepando);

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
        MainCamera.fieldOfView = initialCameraFOV;
        x = 0;
        collisioned = false;
        play = true;
        rotate = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn") {
            respawn();
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
        else if(other.tag == "Maria")
        {
            Debug.Log("pillo el petardo");
            MainCamera.fieldOfView = 80;
            trepando = true;
            rb.useGravity = false;
        }
        else if (other.tag == "TurnRight")
        {
            Debug.Log("Triggered by Turn");
            
            if (gameObject.tag == "JoseJuan") {
                rotationSpeed = 120;
            } else
            {
                rotationSpeed = 72;
            }
            rotate = 1;
        }
        else if (other.tag == "TurnLeft")
        {
            Debug.Log("Triggered by Turn");
            if (gameObject.tag == "JoseJuan")
            {
                rotationSpeed = 67;
            }
            else
            {
                rotationSpeed = 110;
            }
            rotate = -1;
        }
        else if (other.tag == "StopTurn0")
        {
            Debug.Log("Triggered by StopTurn");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotate = 0;
        }
        else if (other.tag == "StopTurn90")
        {
            Debug.Log("Triggered by StopTurn");
            transform.rotation = Quaternion.Euler(0, 90, 0);
            rotate = 0;
        }
        else if (other.tag == "StopTurn180")
        {
            Debug.Log("Triggered by StopTurn");
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rotate = 0;
        }
        else if (other.tag == "StopTurn270")
        {
            Debug.Log("Triggered by StopTurn");
            transform.rotation = Quaternion.Euler(0, 270, 0);
            rotate = 0;
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
        else if (other.tag == "Maria")
        {
            Debug.Log("suelto el porro"); 
            trepando = false;
            MainCamera.fieldOfView = initialCameraFOV;
            transform.Translate(0,0, 5 * runSpeed * Time.deltaTime); // Mou el personatge
            rb.useGravity = true;
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
