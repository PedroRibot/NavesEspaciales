using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
	public float speed = 100;
	public float actualSpeed;
	public float minSpeed = 25;
	public float maxSpeedBoost = 150;
	public float efectoGravedad = 25.0f;
	public float tiempoRecuperarRotacion = 5;
	

	bool boost = false;

	public GameObject Cabina;
	public ParticleSystem BoostPS;

	void Start()
	{
		actualSpeed = speed;
	}

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(Input.GetAxis("Vertical") * 0.4f, 0.0f, -Input.GetAxis("Horizontal") * 0.6f);
		


		Cabina.transform.Rotate(-Input.GetAxis("RightVertical") , 0.0f, -Input.GetAxis("RightHorizontal") );

		Vector3 directionMovement = transform.forward; 

		if (Input.GetAxis("RightVertical") == 0 && -Input.GetAxis("RightHorizontal") == 0)
		{
			Quaternion rotation = transform.rotation;
			Cabina.transform.rotation = Quaternion.Lerp(Cabina.transform.rotation, rotation, Time.deltaTime * tiempoRecuperarRotacion);
		}

		transform.position += transform.forward * Time.deltaTime * actualSpeed;



		//actualSpeed -= transform.forward.y * Time.deltaTime * efectoGravedad; //aqui hay que cambiarlo en valor al angulo con el planeta que esta tirando la gravedad

		//if (actualSpeed > )

		if (actualSpeed < 25)
		{
			actualSpeed = 25;
		}

		Boost();

		if (actualSpeed > speed && boost == false)
		{
			actualSpeed = Mathf.Lerp(actualSpeed, speed, Time.deltaTime);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90, Time.deltaTime);
		}

		RaycastHit hitInfo;
		
		if (Physics.Raycast(this.transform.position, this.transform.up * -1, out hitInfo , 50))
		{
			float angle = Vector3.Angle(this.transform.forward, hitInfo.transform.up);

			if (angle > 87 && angle < 120)
			{
				print(hitInfo.transform.eulerAngles);

				//transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, hitInfo.transform.eulerAngles, Time.deltaTime );

				//Quaternion rotation = hitInfo.transform.rotation;
				//transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * tiempoRecuperarRotacion);
			}

			
		}

		Debug.DrawRay(transform.position, this.transform.up * -1 * hitInfo.distance, Color.yellow);

		


	}

	void Boost()
	{
		
		if (Input.GetAxis("RightTrigger") > 0)
		{
			if (!BoostPS.isPlaying)
			{
				BoostPS.Play();
			}

				
			


			if (transform.rotation == Cabina.transform.rotation)
			{
				actualSpeed = Mathf.Lerp(actualSpeed, maxSpeedBoost, Time.deltaTime *4);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 130, Time.deltaTime * 4);
				boost = true;
			}
			else
			{
				Quaternion rotation = Cabina.transform.rotation;
				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 7);
				actualSpeed = Mathf.Lerp(actualSpeed, maxSpeedBoost, Time.deltaTime * 4);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 130, Time.deltaTime * 4);
				boost = true;
			}
		}
		else
		{
			boost = false;
			if (BoostPS.isPlaying)
			{
				BoostPS.Stop();
			}
		}
		
	}
}
