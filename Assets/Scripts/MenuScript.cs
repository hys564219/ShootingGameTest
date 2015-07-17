using UnityEngine;
using System.Collections;

/// Title screen script
public class MenuScript : MonoBehaviour {

	private GUISkin skin;

	void Start() {
		// Load a skin for the buttons
		skin = Resources.Load ("MyGUISkin") as GUISkin;
	}

	void OnGUI() {

		const int buttonWidth = 84;
		const int buttonHeight = 60;

		// Set the skin to use
		GUI.skin = skin;

		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect buttonRect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
		);

		// Draw a button to start the game
		if (GUI.Button (buttonRect, "Start!")) {
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("testScene");
		}
		highScore ();
	}

	private void highScore() {
		int highScore = 0;
		if (PlayerPrefs.HasKey ("highScore")) {
			highScore = PlayerPrefs.GetInt ("highScore");
		}
		GUI.Label(new Rect(10, 10, 200, 20), "highScore :" + highScore.ToString());
	}
}
