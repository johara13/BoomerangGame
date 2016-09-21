using UnityEngine;
using System.Collections;

public class RoomGeneration : MonoBehaviour {
	public GameObject Chaser;
	public GameObject fastChaser;
	public GameObject MBox;
	public GameObject Tank;
	public int difficulty;
	public int itemfreq;
	private int counter;
	private int ecounter = 0;
	private int ogChaserHealth =3, ogChaserDifficulty =1, ogChaserSpeed=50;
	private int ogFastHealth=3, ogFastDifficulty=25, ogFastSpeed=20;
	private int ogTankHealth=15, ogTankDifficulty=50, ogTankSpeed=60;
	// Use this for initialization
	void Start () {
		counter = 0;
		for (int i = 0; i < difficulty; i++) {
			int horizontal = Random.Range (-9, 9);
			int vertical = Random.Range (-4, 4);
			while (Vector3.Distance (new Vector3 (horizontal, 1, vertical), GameObject.FindGameObjectWithTag ("Player").transform.position) <5) {
				horizontal = Random.Range (-9, 9);
				vertical = Random.Range (-4, 4);
			}
			Instantiate (Chaser, new Vector3 (horizontal, 1, vertical), Chaser.transform.rotation);
		}
	}

	public void reloadRoom(){
		if (counter < itemfreq) {
			difficulty++;
			int i = 0;
			while (i < Mathf.Pow (2, difficulty)) {
				int r = Random.Range (1, 101);
				if (r <= 60) {
					int horizontal = Random.Range (-9, 9);
					int vertical = Random.Range (-4, 4);
					while (Vector3.Distance (new Vector3 (horizontal, 1, vertical), GameObject.FindGameObjectWithTag ("Player").transform.position) < 5) {
						horizontal = Random.Range (-9, 9);
						vertical = Random.Range (-4, 4);
					}
					Instantiate (Chaser, new Vector3 (horizontal, 1, vertical), Chaser.transform.rotation);
					i += Chaser.GetComponent<Enemy> ().difLevel;
				} else if (r > 60 && r<=80 && Mathf.Pow(2, difficulty) - i >= fastChaser.GetComponent<Enemy> ().difLevel) {
					int horizontal = Random.Range (-9, 9);
					int vertical = Random.Range (-4, 4);
					while (Vector3.Distance (new Vector3 (horizontal, 1, vertical), GameObject.FindGameObjectWithTag ("Player").transform.position) < 5) {
						horizontal = Random.Range (-9, 9);
						vertical = Random.Range (-4, 4);
					}
					Instantiate (fastChaser, new Vector3 (horizontal, 1, vertical), Chaser.transform.rotation);
					i += fastChaser.GetComponent<Enemy> ().difLevel;
				} else if (r >=80 && r<=100 && Mathf.Pow(2, difficulty) - i >= Tank.GetComponent<Enemy> ().difLevel) {
					int horizontal = Random.Range (-9, 9);
					int vertical = Random.Range (-4, 4);
					while (Vector3.Distance (new Vector3 (horizontal, 1, vertical), GameObject.FindGameObjectWithTag ("Player").transform.position) < 5) {
						horizontal = Random.Range (-9, 9);
						vertical = Random.Range (-4, 4);
					}
					Instantiate (Tank, new Vector3 (horizontal, GameObject.FindGameObjectWithTag ("Player").transform.position.y, vertical), Tank.transform.rotation);
					i += Tank.GetComponent<Enemy> ().difLevel;
				}
			}
			counter++;
			ecounter++;
			if (ecounter == 5) {
				Debug.Log ("Harder");
				ecounter = 0;
				Chaser.GetComponent<Enemy> ().health += 1;
				Chaser.GetComponent<Enemy> ().speed -= 5;
				Chaser.GetComponent<Enemy> ().difLevel *= 2;
				fastChaser.GetComponent<Enemy> ().health += 1;
				fastChaser.GetComponent<Enemy> ().speed -= 5;
				fastChaser.GetComponent<Enemy> ().difLevel *= 2;
				Tank.GetComponent<Enemy> ().health += 1;
				Tank.GetComponent<Enemy> ().speed -= 5;
				Tank.GetComponent<Enemy> ().difLevel *= 2;
			}
		} else if (counter == itemfreq) {
			counter = 0;
			Instantiate (MBox, new Vector3 (0, 0, 0), MBox.transform.rotation);
		}
		GameObject[] weapons = GameObject.FindGameObjectsWithTag ("Weapon");
		foreach (GameObject w in weapons) {
			Destroy (w);
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("Player") == null) {
			ecounter = 0;
			Chaser.GetComponent<Enemy> ().speed = ogChaserSpeed;
			Chaser.GetComponent<Enemy> ().difLevel = ogChaserDifficulty;
			Chaser.GetComponent<Enemy> ().health = ogChaserHealth;

			fastChaser.GetComponent<Enemy> ().speed = ogFastSpeed;
			fastChaser.GetComponent<Enemy> ().difLevel = ogFastDifficulty;
			fastChaser.GetComponent<Enemy> ().health = ogFastHealth;

			Tank.GetComponent<Enemy> ().speed = ogTankSpeed;
			Tank.GetComponent<Enemy> ().difLevel = ogTankSpeed;
			Tank.GetComponent<Enemy> ().health = ogTankSpeed;
		}
		/*if (Input.GetKeyDown (KeyCode.G)) {
			Instantiate (MBox, new Vector3 (0, 0, 0), MBox.transform.rotation);
		}*/
	}
}
