
using UnityEngine;
using System.Collections;
using System;



public class Cannon : MonoBehaviour
{



    public Transform shootBallPoint;
	public GameObject bulletPrefab;
	public GameObject ball;
	GameObject player;
	public Transform camera1;

	//Mira
	public bool showAim;

	public Transform aim;
	
    private void Start()
    {
		aim = GetComponentInChildren<Canvas>().transform;
		//aim.gameObject.SetActive(false);
		player = GameObject.FindGameObjectWithTag("Player");
		camera1 = Camera.main.transform;
	}


	private void Update()
	{
		


		DrawAim(camera1.transform);

	}
	

	public void Shoot() {

		/*GameObject clone = Instantiate (bulletPrefab,shootBallPoint.position,shootBallPoint.rotation);
		
		clone.GetComponent<Bala>().Shoot();*/

	
		//FindObjectOfType<CharacterController>().GetComponent<CharacterController>().items[0].GetComponent<Bala>().Shoot();



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
			Vector3 nuevoVector = camera.forward * 10000f;
			
			shootBallPoint.LookAt(nuevoVector);
		

        }

		Vector3 nuevoVectr = camera.forward * 20f;
		Vector3 end1 = camera.position + nuevoVectr;
		Debug.DrawRay(camera.position, nuevoVectr, Color.blue);
		if (FindObjectOfType<CharacterController>().items.Count > 0) { FindObjectOfType<CharacterController>().itemHolder.GetComponentInChildren<Bala>().transform.LookAt(nuevoVectr); }
	
	}
}