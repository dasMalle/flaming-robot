using UnityEngine;
using System.Collections;

public class AudioMute : MonoBehaviour
{
	void ToggleMute ()
	{
		AudioListener.pause = !AudioListener.pause;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.M))
		{
			ToggleMute();
		}
	}
}
