using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class UIInfo : MonoBehaviour
{

	void OnGUI ()
	{
		GUI.TextField (new Rect (10, Screen.height - 30, Screen.width / 2 - 20, 20), 
		               "Player Health: " + PlayerHealth.playerHealth.health);
		GUI.TextField (new Rect (Screen.width / 2 + 10, Screen.height - 30, Screen.width / 2 - 20, 20), 
		               "Player Experience: " + PlayerHealth.playerHealth.experience);
	}
}
