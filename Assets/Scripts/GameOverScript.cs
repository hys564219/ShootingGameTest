using UnityEngine;
using System.Collections;

/// Start or quit the game
public class GameOverScript : MonoBehaviour {

	private GUISkin skin;

	void Start() {
		// Load a skin for the buttons
		skin = Resources.Load ("MyGUISkin") as GUISkin;
	}

	void OnGUI() {
		const int buttonWidth = 120;
		const int buttonHeight = 60;

		// Set the skin to use
		GUI.skin = skin;

		if (
			GUI.Button (
			// Center in X, 1/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(1 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Retry!"
			)
		) {
			// Reload the level
			Application.LoadLevel("testScene");
		}

		if (
			GUI.Button (
			// Center in X, 2/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Back to Menu"
			)
			) {
			// Back to menu
			Application.LoadLevel("menuScene");
		}
	}

}
