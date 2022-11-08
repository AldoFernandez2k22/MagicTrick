using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bala : MonoBehaviour
{
    private float power;
    public float lifeTime = 5f;
    public float numberOfTry;

    float deltaT = 0f;

    public Rigidbody bulletRb;
   

    private void Start()
    {
      

        
    }

    private void FixedUpdate()
    {
        
        deltaT += Time.deltaTime;

        if (deltaT >= lifeTime) {
            Destroy(this.gameObject);
        }
        
    }
    public void Shoot() {
        
       
        
        bulletRb = GetComponent<Rigidbody>();
        
        
        transform.position = FindObjectOfType<Cannon>().shootBallPoint.position;
        power = FindObjectOfType<CharacterController>().power;


        bulletRb.AddForce(transform.forward * power);

        FindObjectOfType<CharacterController>().power = 0f;
        FindObjectOfType<CharacterController>().numberOfTry = FindObjectOfType<CharacterController>().numberOfTry + 1 ;



    }


}
