
using UnityEngine;
using System.Collections;
using System;



public class Cannon : MonoBehaviour
{
	public Transform shootBallPoint;
	public GameObject bulletPrefab;

	//Mira
	public bool showAim;

	public Transform aim;

    private void Start()
    {
		aim = GetComponentInChildren<Canvas>().transform;
		//aim.gameObject.SetActive(false);
	}
    
    


    public void Shoot() {
		
		GameObject clone = Instantiate (bulletPrefab,shootBallPoint.position,shootBallPoint.rotation);
		
		clone.GetComponent<Bala>().Shoot();
	
	}

	public void ShowAim() {

		aim.gameObject.SetActive(showAim = !showAim); 
	
	}

	public void DrawAim(Transform camera)
    {
		if (!showAim) {
			ShowAim();
		
		}
		RaycastHit hit;

		if (Physics.Raycast(camera.position, camera.forward, out hit)) {
			shootBallPoint.LookAt(hit.point);
		
		}

	   else
        {
			Vector3 nuevoVector = camera.forward * 20f;
			Vector3 end = camera.position + nuevoVector;
			shootBallPoint.LookAt(end);
			Debug.DrawRay(camera.position, end, Color.blue);

        }

    }
}