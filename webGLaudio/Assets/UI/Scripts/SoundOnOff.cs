using UnityEngine;
using System.Collections;

public class SoundOnOff : MonoBehaviour
{

		public void toggleSound ()
		{
				if (AudioListener.pause)
						AudioListener.pause = false;
				else
						AudioListener.pause = true;
		}
}
