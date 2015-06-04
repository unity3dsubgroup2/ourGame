using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mnuMain : MonoBehaviour
{
	public Canvas canvas;
	public bool isShow;
	public Button btnNew;
	public Button btnDiff;
	public Button btnMusic;
	public Canvas statusbar;
	public Text txtHealth;
	public Image imgHealth;
	public Text txtArmor;
	public Text txtScore;
	public Canvas msgDialog;
	public bool isDlgShow = false;
	public Text txtInfo;
	public Image imgInfo;
	float health = 0;
	float armor = 0;
	float score = 0;

	void Start ()
	{
		ShowMenu (true);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (PlayerHealth.playerHealth.gameStarted)
				isShow = !isShow;
			if (isShow && !canvas.enabled) {
				ShowMenu (true);
			}
			if (!isShow && canvas.enabled) {
				ShowMenu (false);
			}
		}
		if (health != PlayerHealth.playerHealth.health) {
			health = PlayerHealth.playerHealth.health;
			imgHealth.fillAmount = health / 100f;
			imgHealth.color = Color.Lerp (Color.red, Color.green, imgHealth.fillAmount - 0.25f);
			txtHealth.text = string.Format ("{0:0}", health);
		}
		if (armor != PlayerHealth.playerHealth.armor) {
			armor = PlayerHealth.playerHealth.armor;
			txtArmor.text = armor.ToString ();
		}
		if (score != PlayerHealth.playerHealth.experience) {
			score = PlayerHealth.playerHealth.experience;
			txtScore.text = score.ToString ();
		}
	}

	public void OnExitClick ()
	{
		ShowMenu (false);
		Application.Quit ();
	}

	public void OnNewGameClick ()
	{
		PlayerHealth.playerHealth.gameStarted = true;
		Text textObj = btnNew.transform.Find ("Text").GetComponent<Text> ();
		textObj.text = "Resume";
		btnDiff.interactable = false;
		btnDiff.transform.Find ("Text").GetComponent<Text> ().color = btnDiff.colors.disabledColor;
		ShowMenu (false);
	}

	public void OnDiffClick ()
	{
		Text textObj = btnDiff.transform.Find ("Text").GetComponent<Text> ();
		if (textObj.text == "Difficulty: Easy") {
			textObj.text = "Difficulty: Normal";
			PlayerHealth.playerHealth.difficulty = 1f;
		} else if (textObj.text == "Difficulty: Normal") {
			textObj.text = "Difficulty: Hard";
			PlayerHealth.playerHealth.difficulty = 2f;
		} else {
			textObj.text = "Difficulty: Easy";
			PlayerHealth.playerHealth.difficulty = 0.5f;
		}
	}

	public void OnMusicClick ()
	{
		Text textObj = btnMusic.transform.Find ("Text").GetComponent<Text> ();
		if (textObj.text == "Music: off") {
			textObj.text = "Music: on";
		} else {
			textObj.text = "Music: off";
		}
	}

	void ShowMenu (bool show)
	{
		if (show) {
			canvas.enabled = true;
			statusbar.enabled = false;
			msgDialog.enabled = false;
			isShow = true;
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
			canvas.enabled = false;
			statusbar.enabled = true;
			if (isDlgShow) {
				Time.timeScale = 0;
				msgDialog.enabled = true;
			}
			isShow = false;
		}
	}

	public void ShowMsgDialog (string txt, Sprite img)
	{
		txtInfo.text = txt;
		imgInfo.sprite = img;
		Time.timeScale = 0;
		msgDialog.enabled = true;
		isDlgShow = true;
	}

	public void CloseMsgDialog ()
	{
		msgDialog.enabled = false;
		isDlgShow = false;
		Time.timeScale = 1;
	}
}