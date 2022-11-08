using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool playing;
    float play = 0;
    Vector3 originalPosition;


    private void Start()
    {
        originalPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
       
        other.GetComponent<CharacterController>().numberOfTry -=10;
     



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        other.GetComponent<CharacterController>().numberOfTry = 10;
    }



}
