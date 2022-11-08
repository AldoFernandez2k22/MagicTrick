using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    //Character
    Transform tran;
    Rigidbody rb;

    public bool shooting = false;
    public bool interacting;

    public float walkSpeed = 200;

    public float power;


    //Character Anim

    Animator anim;

    private Vector2 animSpeed;


    //Camera

    public Transform cameraShoulder; //Eje de la Camara

    public Transform cameraHolder; //Posicion y rotacion de la camara respecto al personaje

    private Transform cam;

    private float rotY = 0f;
    public float rotationSpeed = 200f;
    public float minAngle = -45;
    public float maxAngle = 45;
    public float camSpeed = 200;


    //items 
    public Cannon cannon;

    //Win Conditions
    public float numberOfTry;
    public Image barraDeFuerza;

    public float fuerzaActual;

    public float fuerzaMax;
    public bool isPlaying;
    public bool isCurrentPlaying;



    void Start()
    {
        tran = this.transform;

        cam = Camera.main.transform;

        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();

        numberOfTry = 10;

    }


    void Update()
    {

        MoveController();
        CamControl();
        AnimControl();
        ActionControl();
        cannon = GetComponentInChildren<Cannon>();
        ItemControl();

        Debug.Log(numberOfTry);
    }

    void ActionControl()
    {
        power = Math.Clamp(power, 0, 1500);
      


        float deltT = Time.deltaTime;
        //Disparar

        if (Input.GetKey(KeyCode.Mouse0) && numberOfTry <10)
        {

            power += 500 * deltT;

        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && numberOfTry < 10)
        {

            shooting = true;


        }

        else
        {
            shooting = false;
        }

        fuerzaActual = power;
        fuerzaMax = 1500;
        barraDeFuerza.fillAmount = fuerzaActual / fuerzaMax;
        //Interactuar

        interacting = Input.GetKeyDown(KeyCode.E);
    }
    void ItemControl()
    {



        if (cannon != null)
        {


            cannon.DrawAim(cam);

            if (shooting)
            {
                cannon.Shoot();

            }
        }
        Cursor.lockState = (Input.GetKeyDown("q")) ? CursorLockMode.None : CursorLockMode.Locked;
    }
    private void MoveController()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        float deltaT = Time.deltaTime;

        animSpeed = new Vector2(deltaX, deltaZ);

        Vector3 side = walkSpeed * deltaX * deltaT * tran.right;
        Vector3 forward = walkSpeed * deltaZ * deltaT * tran.forward;
        Vector3 direction = side + forward;

        rb.velocity = direction;


    }

    void CamControl()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaT = Time.deltaTime;

        rotY += mouseY * rotationSpeed * deltaT;

        float rotX = mouseX * rotationSpeed * deltaT;

        tran.Rotate(0, rotX, 0, Space.Self);

        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        Quaternion localLocation = Quaternion.Euler(-rotY, 0, 0);

        cameraShoulder.localRotation = localLocation;

        cam.position = Vector3.Lerp(cam.position, cameraHolder.position, camSpeed * deltaT);
        cam.rotation = Quaternion.Lerp(cam.rotation, cameraHolder.rotation, camSpeed * deltaT);

    }

    void AnimControl()
    {
        anim.SetFloat("X", animSpeed.x);
        anim.SetFloat("Y", animSpeed.y);


    }
   
}

