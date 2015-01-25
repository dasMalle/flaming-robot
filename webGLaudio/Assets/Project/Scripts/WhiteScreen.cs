using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhiteScreen : MonoBehaviour {

	public static WhiteScreen instance;
	private Image image;
	public float flashDuration = 1;
	public AnimationCurve flashCurve;

	// Use this for initialization
	void Awake () {
		instance = this;
		image = GetComponent<Image> ();
	}

	// delete this
	public bool flashButton = false;
	void Update() {
		if (flashButton) {
			flashButton = false;
			Flash ();
		}
	}
	
	public void Flash () {
		StopCoroutine ("FlashCoroutine");
		StartCoroutine ("FlashCoroutine");
	}

	private IEnumerator FlashCoroutine() {
		float endTime = Time.time + flashDuration;
		while (endTime > Time.time) {
			float t = 1 - (endTime - Time.time);
			image.color = new Color(image.color.r, image.color.g, image.color.b, flashCurve.Evaluate(t*flashDuration));
			yield return 0;
		}
	}
}
