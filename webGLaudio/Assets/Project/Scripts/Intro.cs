using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour
{
	AudioSource source;
	public Transform audioListener;
	Transform parentOfListener;
	public Text helpBox;

	void Start ()
	{
		helpBox.text = "Press any key to SKIP the intro.";
		parentOfListener = audioListener.transform.parent;

		source = GetComponent<AudioSource> ();
		Invoke ("StartGame", source.clip.length);
	}

	void StartGame ()
	{
		helpBox.text = "Tweet with tag #TweetLovers\n+ Mr. or Mrs. Dinosaur + north, south, east, west, roar or meteor";

		source.Stop ();
		audioListener.transform.parent = parentOfListener;
		audioListener.transform.localPosition = Vector3.zero;
		audioListener.transform.localRotation = Quaternion.identity;
		PlayerManager.isGameRunning = true;
	}

	void Update()
	{
		if(source.isPlaying)
		{
			audioListener.transform.parent = transform;
			audioListener.transform.localPosition = Vector3.zero;
			audioListener.transform.localRotation = Quaternion.identity;

			if(Input.GetMouseButtonDown(0) || Input.anyKeyDown)
			{
				CancelInvoke("StartGame");
				StartGame();
			}
		}

	}

}
