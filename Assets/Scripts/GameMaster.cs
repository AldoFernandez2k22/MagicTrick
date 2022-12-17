using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public bool playing;
    public static bool firstCheck = false;
    public AudioSource pointSound;
    public GameObject fxSpawn;
    

    float play = 0;
    public static int points;
    Vector3 spawnPosition;

    public TMP_Text pointText;
    public TMP_Text countText;

    [SerializeField] private UnityEvent point;



    private void Start()
    {
        SetGravity1();

    }
    private void Update()
    {
        Debug.Log(points);

        if (this.name == "Check1") {
            SetText("Puntaje: " + points, FindObjectOfType<CharacterController>().GetComponent<CharacterController>().items.Count + "");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "Check1" && other.tag == "isItem")
        {

            firstCheck = true;
            Debug.Log("fCheck");

        }

        if (this.name == "Check2" && other.tag == "isItem")
        {

            if (firstCheck) {

                point.Invoke();

                firstCheck = false;

            }





        }


    }


    void SetGravity1() {

        Physics.gravity = new Vector3(0, -20, 0);


    }


    void SetText(string a, string b) {

        pointText.text = a;
        countText.text = b;

    }

    public void MorePoints() {

        points = points + 1;

    }

    public void ActiveThings( GameObject a){

        GameObject b;

      
        b = Instantiate(a, fxSpawn.transform.position,fxSpawn.transform.rotation);
        Destroy(b, .9f);
    
    }
    public void ActiveSoundEffect(AudioClip a) {

        pointSound.clip = a;
        pointSound.Play();
    
    }

    public void String1(string a)
    {

        Debug.Log(a );

    }

    public void String2(string a)
    {

        Debug.Log(a );

    }

    public void String3(string a)
    {

        Debug.Log(a );

    }

}
