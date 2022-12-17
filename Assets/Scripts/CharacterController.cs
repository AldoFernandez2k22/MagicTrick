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
    public GameObject feet;

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
    public bool cursorSwitch = true;
    public bool activeMenu;

    //items 
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject itemHolder;
    public GameObject itemSpawner;
    
  

    public  List<GameObject> items = new List<GameObject>();

    float itemOnPlayer;
    int itemsCount = 0;

    bool holding;

   

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

        

        feet = GameObject.Find("Feet");
    }


    void Update()
    {
        

        MoveController();
        CamControl();
        AnimControl();
        ActionControl();
        ItemControl();
        Debug.Log(shooting);
        Debug.Log(numberOfTry);
    }

    void ActionControl()
    {
        power = Math.Clamp(power, 0, 3000);
        float i;

        bool control = true;

        float deltT = Time.deltaTime;
        //Disparar

        if (power >= 3000) {
            control = false;
        
        }

        if (power <= 150)
        {

            i = .3f;
        }
        else {
            i = 0f;
        }

        if (Input.GetKey(KeyCode.Mouse0) && items.Count > 0)
        {

           

            if (control) { 
            power += 1000 * deltT;
            }

            

            holding = true;
            Debug.Log(power);





    }
        //Coloca el objeto en la mano con delay para coincidir con la animacion 
        if (Input.GetKeyDown(KeyCode.Mouse0) && items.Count > 0)
        {
            StartCoroutine(Timer()); 
        }

        //Dispara
            if (Input.GetKeyUp(KeyCode.Mouse0) && items.Count > 0)
        {
            /* items[0].GetComponent<Rigidbody>().isKinematic = false;
             itemHolder.transform.DetachChildren();

             items[0].GetComponent<Bala>().Shoot();

             items.Remove(items[0]);

             holding = false;*/

            StartCoroutine(Shoot(i)); //Corrige errores en la animacion 

           
        }
        
       

        fuerzaActual = power;
        fuerzaMax = 3000;
        barraDeFuerza.fillAmount = fuerzaActual / fuerzaMax;

        
        
        
    }

   
    void ItemControl()
    {


        Debug.Log(items.Count);
        Debug.Log(itemOnPlayer);
     
        RaycastHit item;

        //busca objetos en la escena 
        if (Physics.Raycast(cam.position, cam.forward, out item, 100f) && Input.GetKeyDown(KeyCode.E))
        {

            if (item.transform.tag == "isItem" )
            {
                item.transform.GetComponent<Rigidbody>().isKinematic = true;
                items.Add(item.transform.gameObject); //agrega el objeto a la lista
              


            }
        }

        if (items.Count == 1 ){
           
            items[0].GetComponent<Rigidbody>().isKinematic = true;

              if (!holding) { 
              items[0].transform.parent = item1.transform;
              items[0].transform.position = item1.transform.position;
             }
           
          Debug.Log(items[0].name);
            
        }


        if (items.Count == 2)
        {
            items[1].GetComponent<Rigidbody>().isKinematic = true;

            if (!holding)
            {
                items[1].transform.parent = item2.transform;
                items[1].transform.position = item2.transform.position;
            }
            Debug.Log(items[1].name);

        }

        if (items.Count == 3)
        {
            items[2].GetComponent<Rigidbody>().isKinematic = true;

            if (!holding)
            {
                items[2].transform.parent = item3.transform;
                items[2].transform.position = item3.transform.position;
            }
            Debug.Log(items[2].name);

        }

        
        Debug.DrawRay(cam.transform.position, cam.forward * 10, Color.green);

    }
    private void MoveController()
    {


        float deltaX = Input.GetAxis("Horizontal"); //toma el valor horizonal
        float deltaZ = Input.GetAxis("Vertical"); //toma un valor vertical
        float deltaT = Time.deltaTime;

        animSpeed = new Vector2(deltaX, deltaZ);//almacena las direcciones para las animaciones

        Vector3 side = walkSpeed * deltaX * deltaT * tran.right; //almacena el movimiento de izquierda a derecha
        Vector3 forward = walkSpeed * deltaZ * deltaT * tran.forward; //almacena el movimiento de arriba a abajo
        Vector3 direction = side + forward; //establece la direccion

        rb.AddForce(direction * 5); //mueve al objeto

        Debug.DrawRay(feet.transform.position, feet.transform.forward, Color.red);

        RaycastHit floor;
        // Detecta si se esta sobre el suelo
        if (Physics.Raycast(feet.transform.position, feet.transform.forward * 2f, out floor, .2f))
        {

            if (rb.velocity.magnitude > 20)
            {

                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 20);

            }
            rb.mass = 0.2f;
            rb.drag = 7f;
        }
        else
        {
            Debug.Log("caigo");
            rb.mass = 5f;
            rb.drag = 0f;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5000);


        }


        Debug.DrawRay(feet.transform.position, feet.transform.forward, Color.red);




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
        anim.SetBool("Holding", holding);


    }

    IEnumerator Timer() {

        yield return new WaitForSeconds(.2f);

        if (items.Count > 0 )
        {
            items[0].transform.parent = itemHolder.transform;
            items[0].transform.transform.position = itemHolder.transform.position;
        }

    }

    IEnumerator Shoot(float i)
    {

        yield return new WaitForSeconds(i);

        items[0].GetComponent<Rigidbody>().isKinematic = false;
        itemHolder.transform.DetachChildren();

        items[0].GetComponent<Bala>().Shoot();

        items.Remove(items[0]);

        holding = false;



    }
}

