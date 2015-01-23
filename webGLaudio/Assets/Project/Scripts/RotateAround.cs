using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	float rotateValue = 0.0f;
	float rotateSpeed = 30.0f;

	void Update ()
	{
		rotateValue += Input.GetAxis ("Mouse X") * rotateSpeed * Time.deltaTime;
		transform.eulerAngles = Vector3.up * rotateValue;
	}
}
