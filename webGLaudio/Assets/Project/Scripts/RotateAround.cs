using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	float rotateValue = 0.0f;
	float rotateSpeed = 30.0f;
	public bool isConstantlyRotating = false;
	public bool isRotatingDirectionClockwise = false;
	public RectTransform compassNeedle;
	
	public void RotateClockwise()
	{
		rotateValue += rotateSpeed * Time.deltaTime;
		transform.eulerAngles = Vector3.up * rotateValue;
		compassNeedle.eulerAngles = -Vector3.forward * (rotateValue - 90.0f);
	}

	public void RotateCounterClockwise()
	{
		rotateValue -= rotateSpeed * Time.deltaTime;
		transform.eulerAngles = Vector3.up * rotateValue;
		compassNeedle.eulerAngles = -Vector3.forward * (rotateValue - 90.0f);
	}

	public void ResetRotation()
	{
		rotateValue = 90.0f;
		transform.eulerAngles = Vector3.up * rotateValue;
		compassNeedle.eulerAngles = -Vector3.forward * (rotateValue - 90.0f);
	}

	void Update()
	{
		if(isConstantlyRotating)
		{
			if(isRotatingDirectionClockwise)
			{
				RotateClockwise();
			}
			else
			{
				RotateCounterClockwise();
			}
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			RotateCounterClockwise();
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			RotateClockwise();
		}

		if(Input.GetKey(KeyCode.Space))
		{
			ResetRotation();
		}
	}
}
