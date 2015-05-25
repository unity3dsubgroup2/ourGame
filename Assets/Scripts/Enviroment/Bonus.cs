using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{
	public enum Bonuses
	{
		Weapon1,
		Weapon2,
		MissileSystem
	}

	public Bonuses bonus;
	bool showMessage = false;
	string txt = "";

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			if (bonus == Bonuses.Weapon1) {
				other.GetComponent<PlayerControl> ().isWeapon1Active = true;
				txt = "Left Weapon activated.";
			}
			if (bonus == Bonuses.Weapon2) {
				other.GetComponent<PlayerControl> ().isWeapon2Active = true;
				txt = "Right Weapon activated.";
			}
			if (bonus == Bonuses.MissileSystem) {
				other.GetComponent<PlayerControl> ().isMissileActive = true;
				txt = "Missile System activated.";
			}
			other.GetComponent<PlayerControl> ().ReactivateWeapons ();
			showMessage = true;
			Time.timeScale = 0;
		}
	}

	void OnGUI ()
	{
		if (showMessage) {
			GUI.Window (0, new Rect ((Screen.width - 200) / 2,
			                         (Screen.height - 100) / 2,
			                         200, 100), GUIMainMenu, "Incoming message");
		}
	}

	void GUIMainMenu (int id)
	{
		GUI.Label (new Rect (30, 30, 200, 30), txt);
		showMessage = !GUI.Button (new Rect (130, 60, 60, 30), "Ok");
		if (showMessage == false) {
			Time.timeScale = 1;
		}
	}
}
