using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class MyGUI : MonoBehaviour {
	public Texture Heart;
	private int highscore;
	void OnGUI(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		highscore = PlayerPrefs.GetInt ("highscore");
		//makes sure player exists
		if (player != null) {
			//displays high score
			player.GetComponent<PlayerActions> ().highscoretext.text = "High Score: " + highscore;
			//adjusts high score if score is higher than current hs
			if (player.GetComponent<PlayerActions> ().score > highscore) {
				highscore = player.GetComponent<PlayerActions> ().score;
				PlayerPrefs.SetInt ("highscore", highscore);
				player.GetComponent<PlayerActions> ().highscoretext.text = "High Score: " + highscore;
			}
			//displays health in form of number of hearts
			for (int c = 0; c < player.GetComponent<PlayerActions> ().health; c++) {
				GUI.DrawTexture (new Rect (110 * c, 0, 100, 100), Heart);
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
