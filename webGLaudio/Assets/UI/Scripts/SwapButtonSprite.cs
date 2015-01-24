using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapButtonSprite : MonoBehaviour
{

		public Sprite one;
		public Sprite two;

		public void toggleSprite ()
		{
				if (this.GetComponent<Image> ().sprite == one) {
						this.GetComponent<Image> ().sprite = two;
				} else {
						this.GetComponent<Image> ().sprite = one;
				}
		}
}
