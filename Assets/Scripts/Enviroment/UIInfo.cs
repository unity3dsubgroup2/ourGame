using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class UIInfo : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Update ()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI ()
	{
		int w = Screen.width, h = Screen.height;
		Rect rect = new Rect (0, 0, w, h * 2 / 100);
		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label (rect, text, style);

		GUI.TextField (new Rect (10, Screen.height - 30, Screen.width / 2 - 20, 20), 
		               "Player Health: " + PlayerHealth.playerHealth.health);
		GUI.TextField (new Rect (Screen.width / 2 + 10, Screen.height - 30, Screen.width / 2 - 20, 20), 
		               "Player Experience: " + PlayerHealth.playerHealth.experience);
	}
}
