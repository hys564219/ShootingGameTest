using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static GameState Instance;
	
	//private static int highScore;
	private int score;
	private int highScore;
	private bool gaming;

	private GUIStyle labelStyle;

	void Awake() {
		// Register the singleton
		if (Instance != null) {
			Debug.LogError("Multiple instances of GameState!");
		}
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		score = 0;
		highScore = 0;
		if (PlayerPrefs.HasKey ("highScore")) {
			highScore = PlayerPrefs.GetInt ("highScore");
		}

		gaming = true;
		//labelStyle
		labelStyle = new GUIStyle ();
		labelStyle.normal.background = null;
		labelStyle.normal.textColor = Color.blue;
		labelStyle.fontSize = 20;

	}
	
	// Update is called once per frame
	void Update () {
		if (gaming) {
			addScore(1);
			if(score/60 > highScore) {
				highScore = score/60;
			}
		}
	}

	public void gameOver() {
		gaming = false;
		PlayerPrefs.SetInt ("highScore", highScore);
	}

	public void addScore(int point) {
		score += point;
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), "score :" + (score/60).ToString(), labelStyle);
		GUI.Label(new Rect(10, 40, 200, 20), "highScore :" + highScore.ToString(), labelStyle);
	}
}
