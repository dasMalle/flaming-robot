using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	float rotateValue = 0.0f;
	float rotateSpeed = 80.0f;
	float targetRotateValue;
	public bool isConstantlyRotating = false;
	public bool isRotatingDirectionClockwise = false;
	public RectTransform compassNeedle;

	void Start()
	{
		ResetRotation ();
	}

	public void RotateClockwise()
	{
		targetRotateValue += rotateSpeed * Time.deltaTime;
	}

	public void RotateCounterClockwise()
	{
		targetRotateValue -= rotateSpeed * Time.deltaTime;
	}

	public void ResetRotation()
	{
		rotateValue = 90.0f;
		targetRotateValue = rotateValue;
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

		rotateValue += (targetRotateValue - rotateValue) * 0.1f;
		compassNeedle.eulerAngles = -Vector3.forward * (rotateValue - 90.0f);
		transform.eulerAngles = Vector3.up * rotateValue;
	}
}
