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
	public Image imgGun1;
	public Image imgGun2;
	public Image imgGun3;
	public Canvas msgDialog;
	public bool isDlgShow = false;
	public Canvas helpDialog;
	public bool isHelpShow = false;
	public Text txtInfo;
	public Image imgInfo;

	public Canvas gameOverDialog;
	public bool isGameOverShow = false;

	
	float health = 0;
	float armor = 0;
	float score = 0;

	void Start ()
	{
		ShowMenu (true);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && !isGameOverShow) {
			if (PlayerHealth.playerHealth.gameStarted)
				isShow = !isShow;
			if (isShow && !canvas.enabled) {
				ShowMenu (true);
			}
			if (!isShow && canvas.enabled) {
				ShowMenu (false);
			}
		}
		if (health != PlayerHealth.playerHealth.Health) {
			health = PlayerHealth.playerHealth.Health;
			imgHealth.fillAmount = health / 100f;
			imgHealth.color = Color.Lerp (Color.red, Color.green, imgHealth.fillAmount - 0.25f);
			txtHealth.text = string.Format ("{0:0}", health);
		}
		if (armor != PlayerHealth.playerHealth.Armor) {
			armor = PlayerHealth.playerHealth.Armor;
			txtArmor.text = string.Format ("{0:0}", armor);
		}
		if (score != PlayerHealth.playerHealth.experience) {
			score = PlayerHealth.playerHealth.experience;
			txtScore.text = string.Format ("{0:0}", score);
		}
	}

	public void OnExitClick ()
	{
		ShowMenu (false);
		Application.Quit ();
	}

	public void OnNewGameClick ()
	{
		ShowMenu (false);
		if (!PlayerHealth.playerHealth.gameStarted) {
			ShowHelpDialog ();
			Text textObj = btnNew.transform.Find ("Text").GetComponent<Text> ();
			textObj.text = "Resume";
			btnDiff.interactable = false;
			btnDiff.transform.Find ("Text").GetComponent<Text> ().color = btnDiff.colors.disabledColor;
			PlayerHealth.playerHealth.gameStarted = true;
		}
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
			transform.root.Find ("MusicBG").GetComponent<AudioSource> ().Play ();
		} else {
			textObj.text = "Music: off";
			transform.root.Find ("MusicBG").GetComponent<AudioSource> ().Pause ();
		}
	}

	void ShowMenu (bool show)
	{
		if (show) {
			if (isActiveAndEnabled) { // Show menu only if MainMenu object is enabled
				canvas.enabled = true;
				statusbar.enabled = false;
				msgDialog.enabled = false;
				helpDialog.enabled = false;
				gameOverDialog.enabled = false;
				isShow = true;
				Time.timeScale = 0;
			}
		} else {
			Time.timeScale = 1;
			canvas.enabled = false;
			statusbar.enabled = true;
			if (isDlgShow) {
				Time.timeScale = 0;
				msgDialog.enabled = true;
			}
			if (isHelpShow) {
				Time.timeScale = 0;
				helpDialog.enabled = true;
			}
			isShow = false;
		}
	}

	public void ShowMsgDialog (string txt, Sprite img)
	{
		if (isActiveAndEnabled) { // only if the MainMenu object is enabled - show the MessageDialog
			txtInfo.text = txt;
			imgInfo.sprite = img;
			Time.timeScale = 0;
			msgDialog.enabled = true;
			isDlgShow = true;
		}
	}

	public void CloseMsgDialog ()
	{
		msgDialog.enabled = false;
		isDlgShow = false;
		Time.timeScale = 1;
	}

	public void ShowHelpDialog ()
	{
		if (isActiveAndEnabled) { // only if the MainMenu object is enabled - show the MessageDialog
			Time.timeScale = 0;
			helpDialog.enabled = true;
			isHelpShow = true;
		}
	}

	public void CloseHelpDialog ()
	{
		helpDialog.enabled = false;
		isHelpShow = false;
		Time.timeScale = 1;
	}

	public void GameOver (bool win)
	{
		transform.root.Find ("/player").GetComponent<AudioListener> ().enabled = false;
		gameOverDialog.enabled = true;
		isGameOverShow = true;
		Time.timeScale = 0;
		if (win) {
			gameOverDialog.transform.Find ("Win").GetComponent<Image> ().enabled = true;
			gameOverDialog.transform.Find ("Lost").GetComponent<Image> ().enabled = false;
		} else {
			gameOverDialog.transform.Find ("Win").GetComponent<Image> ().enabled = false;
			gameOverDialog.transform.Find ("Lost").GetComponent<Image> ().enabled = true;
		}
	}

	public void CloseGameOver ()
	{
		transform.root.Find ("/player").GetComponent<AudioListener> ().enabled = true;
		PlayerHealth.playerHealth.RestartLevel ();
	}
}