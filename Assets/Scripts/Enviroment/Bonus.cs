using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
	public enum Bonuses
	{
		Weapon1,
		Weapon2,
		MissileSystem
	}

	public Bonuses bonus;
	public Sprite imgBonus;
	public GameObject mainMenu;
	string txt = "";

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			if (bonus == Bonuses.Weapon1 && !other.GetComponent<PlayerControl> ().isWeapon1Active) {
				other.GetComponent<PlayerControl> ().isWeapon1Active = true;
				txt = "Left Weapon activated";
			}
			if (bonus == Bonuses.Weapon2 && !other.GetComponent<PlayerControl> ().isWeapon2Active) {
				other.GetComponent<PlayerControl> ().isWeapon2Active = true;
				txt = "Right Weapon activated";
			}
			if (bonus == Bonuses.MissileSystem && !other.GetComponent<PlayerControl> ().isMissileActive) {
				other.GetComponent<PlayerControl> ().isMissileActive = true;
				txt = "Missile System activated";
			}
			if (txt != "") {
				mainMenu.GetComponent<mnuMain> ().ShowMsgDialog (txt, imgBonus);
				txt = "";
				other.GetComponent<PlayerControl> ().ReactivateWeapons ();
			}
		}
	}
}
