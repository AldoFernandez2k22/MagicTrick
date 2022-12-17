using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelMusical : Carteles
{ 
    public AudioSource mainAs; 
    public AudioClip gameplaySound;
    void Start()
    {
        SetAudio(gameplaySound, mainAs);
    }

    // Update is called once per frame
    void Update()
    {
        SetText(texto);
       
    }

    public override void SetText(string a)
    {
        base.SetText(a);
       
         

        
    }


    void SetAudio(AudioClip clip, AudioSource source)
    {

        mainAs.clip = clip;
        mainAs.Play();

    }
}
