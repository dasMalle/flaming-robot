using UnityEngine;
using System.Collections;

public class RotateSprite : MonoBehaviour
{

		public GameObject obj;
		RectTransform rect;
		// Use this for initialization
		void Start ()
		{
				rect = obj.GetComponent<RectTransform> ();
		}
		
		public void rotNorth ()
		{
				rect.rotation = new Quaternion (0, 0, 0, 0);

		}
		public void rotSouth ()
		{
				rect.rotation = new Quaternion (0, 0, 90, 0);
		}
		public void rotEast ()
		{
				rect.rotation = new Quaternion (0, 0, 180, 0);
		}
		public void rotWest ()
		{
				rect.rotation = new Quaternion (0, 0, 270, 0);
		}

}
