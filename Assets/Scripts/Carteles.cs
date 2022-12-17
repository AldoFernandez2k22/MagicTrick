using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Carteles : MonoBehaviour
{
    public TMP_Text text;
    public string texto;
    
    void Start()
    {
        
    }

    void Update()
    {
        SetText(texto);
    }

    public virtual void SetText(string a) {
       
        text.text = a;
    
    
    
    }



}
