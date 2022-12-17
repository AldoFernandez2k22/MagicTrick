using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    public GameObject interfaz;
    public bool check = false;
    bool cursorSwitch;

    
    void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) )
        {

            if (!interfaz.activeInHierarchy) { interfaz.SetActive(true); }

            else { interfaz.SetActive(false); }

        }

           

        if (interfaz.activeInHierarchy)
        {
            cursorSwitch = true;
            Time.timeScale = 0;
        }

           else { cursorSwitch = false;
            Time.timeScale = 1; }

        Cursor.lockState = (cursorSwitch) ? CursorLockMode.None : CursorLockMode.Locked;
    }




    public void StartGame() {

        interfaz.SetActive(false);
        
    
    
    }


    public void QuitGame() {

        Application.Quit();
    
    }

   
}
