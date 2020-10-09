using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject Nave;
	// Update is called once per frame
	void Update()
    {
		Vector3 moveCamTo = Nave.transform.position - Nave.transform.forward * 25 + Vector3.up * 15;
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo *(1.0f - bias) ;
		Camera.main.transform.LookAt(Nave.transform.position + Nave.transform.forward * 30);
    }
}
