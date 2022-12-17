using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bala : MonoBehaviour
{
    private float power;
    public float lifeTime = 1000f;
    public float numberOfTry;
    Vector3 shotForward = new Vector3(0,23,40);

    float deltaT = 0f;

    public Rigidbody bulletRb;

    
    private void Start()
    {
      

        
    }

    private void FixedUpdate()
    {
        
        /*deltaT += Time.deltaTime;

        if ( this.GetComponentInParent<CharacterController>().tag != "isPlayer") {
            Destroy(this.gameObject);
        } */
        
    }
    public void Shoot() {


        Debug.Log("Orden de salida");
        bulletRb = GetComponent<Rigidbody>();
        
       
        Transform sShoot = FindObjectOfType<Cannon>().shootBallPoint.transform;
        power = FindObjectOfType<CharacterController>().power;
      

        bulletRb.AddForce(sShoot.forward * power );

        FindObjectOfType<CharacterController>().power = 0f;
        FindObjectOfType<CharacterController>().numberOfTry = FindObjectOfType<CharacterController>().numberOfTry + 1 ;



    }


}
