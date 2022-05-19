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
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
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
    private Vector3 moveDirection;
    public float playerHeight;
    private float factor = 0;

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
        //x = Input.GetAxis("Horizontal");
        // Obté cap a on es mou el personatge
        if (play)
        {
            if (trepando)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) y = 1;
                else y = 0;
                transform.Translate(0, y * runSpeed * Time.deltaTime, 0); // Mou el personatge
            }
            else if (freeMovement)
            {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
                if (Input.GetKey(KeyCode.UpArrow)) y = 1;
                // Moviment personatge
                transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge            
                transform.Translate(0, 0, y * runSpeed * Time.deltaTime); // Mou el personatge

            }
            else
            {
                if (gameObject.tag == "JuanCarlosI")
                {
                    //if (Input.GetKeyDown(KeyCode.W)) factor = 0;
                    if (Input.GetKey(KeyCode.W))
                    {
                        factor += 0.05f;
                        y = 1;
                        x = rotate;
                        if (factor > 1) factor = 1;
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime * factor); // Mou el personatge
                    }
                    else
                    {
                        y = 1;
                        factor -= 0.05f;
                        if (factor < 0) factor = 0;
                        x = rotate;
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime * factor); // Mou el personatge
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime * factor, 0); // Rota el personatge
                    }
                    if (Input.GetKeyUp(KeyCode.W))
                    {
                        x = 0;
                    }

                    // Moviment personatge
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow)) factor = 0;
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        y = 1;
                        factor += 0.05f;
                        x = rotate;
                        if (factor > 1) factor = 1;
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rota el personatge
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime * factor); // Mou el personatge
                    }
                    else
                    {
                        factor -= 0.05f;
                        if (factor < 0) factor = 0;
                        x = rotate;
                        transform.Translate(0, 0, 1 * runSpeed * Time.deltaTime * factor); // Mou el personatge
                        transform.Rotate(0, x * rotationSpeed * Time.deltaTime * factor, 0); // Rota el personatge
                        y = 1;
                    }
                    if (Input.GetKeyUp(KeyCode.UpArrow)) x = 0;
                    // Moviment personatge
                }
                if (OnSlope())
                {
                    rb.AddForce(GetSlopeMoveDirection() * runSpeed * 20f, ForceMode.Force);

                    if (rb.velocity.y > 0)
                        rb.AddForce(Vector3.down * 80f, ForceMode.Force);
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

        animator.SetFloat("speedX", x * factor);
        animator.SetFloat("speedY", y * factor);
        animator.SetBool("col", collisioned);
        animator.SetBool("end", finished);
        animator.SetBool("escalador", trepando);

    }

    public void setPlaying(bool b)
    {
        play = b;
    }

    public void changePlaying()
    {
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
        if (other.tag == "Respawn")
        {
            respawn();
        }
        else if (other.tag == "Aplastador")
        {
            play = false;
            transform.localScale = new Vector3(initialScale.x, 5, initialScale.z * 1.5f);
            MainCamera.transform.localPosition = new Vector3(initialCameraPosition.x, initialCameraPosition.y + 0.5f, initialCameraPosition.z);
            aplastado = true;
            colisionTime = 0;


        }
        else if (other.tag == "Sierra")
        {
            play = false;
            aplastado = true;
            colisionTime = 0;

        }
        else if (other.tag == "Maria")
        {
            Debug.Log("pillo el petardo");
            MainCamera.fieldOfView = 80;
            trepando = true;
            rb.useGravity = false;
        }
        else if (other.tag == "TurnRight")
        {
            Debug.Log("Triggered by Turn");

            if (gameObject.tag == "JoseJuan")
            {
                rotationSpeed = 120;
            }
            else
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
        else if (other.tag == "Checkpoint")
        {
            initialPosition = other.transform.position;
        }
        else if (other.tag == "SaltoFresco")
        {
            rb.AddForce(new Vector3(0, fuerzaPutiaso, fuerzaPutiaso / 2), ForceMode.Impulse);
        }
        else if (other.tag == "Fuegardo")
        {
            Debug.Log("me quemo el ojete");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Finish")
        {
            finished = true;
            play = false;

            if (gameObject.tag == "JuanCarlosI")
            {
                gameObject.GetComponent<WinController>().winPlayer01();
                gameObject.transform.parent.GetChild(1).GetComponent<PlayerController>().setPlaying(false);
            }
            if (gameObject.tag == "JoseJuan")
            {
                gameObject.GetComponent<WinController>().winPlayer02();
                gameObject.transform.parent.GetChild(1).GetComponent<PlayerController>().setPlaying(false);
            }

            MainCamera.transform.rotation = Quaternion.Euler(0, 180, 0);
            MainCamera.transform.localPosition = new Vector3(0.0f, 0.08f, 0.3f);
        }
        else if (other.tag == "Maria")
        {
            Debug.Log("suelto el porro");
            trepando = false;
            MainCamera.fieldOfView = initialCameraFOV;
            transform.Translate(0, 0, 5 * runSpeed * Time.deltaTime); // Mou el personatge
            rb.useGravity = true;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider c = collision.collider;
        Debug.Log(c.tag);
        if (c.tag == "Putiaso" || c.tag == "Barril")
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

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
