using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour {
	public float speed;
	public Rigidbody bullet;
	public Rigidbody boomerang;
	public Rigidbody sword;
	public Rigidbody arrow;
	public Text highscoretext;
	public Transform ushot;
	public Transform dshot;
	public Transform lshot;
	public Transform rshot;
	public Transform curdir;
	public Rigidbody curWeapon;
	public Material newMat;
	public Material mat;
	public int fireRate;
	public float iTime;
	private bool shooting = false;
	public int health;
	public Text scorenum;
	public Image weaponImage;
	public Text newItem;
	public Texture Heart;
	public VirtualJoystick mJoy;
	public VirtualJoystick sJoy;
	public int score;
	// Use this for initialization

	public void OnCollisionEnter(Collision collision){
		//Debug.Log ("Enter registered");
		if(collision.collider.tag.Equals("Enemy")){
			Debug.Log("Registered hit Enemy");
			changeHealth (collision.gameObject.GetComponent<Enemy>().contactDmg);
			/*Vector3 explosionpos = transform.position;
			Collider[] hits = Physics.OverlapSphere (explosionpos, 5);
			foreach (Collider col in hits) {
				Rigidbody body = col.GetComponent<Rigidbody> ();
				if (body != null && !body.tag.Equals("Player")) {
					body.AddExplosionForce (500, explosionpos, 5);
				}
			}*/
			invincibilityFrames ();
		}
	}

	public void invincibilityFrames(){
		gameObject.layer = LayerMask.NameToLayer ("iFrames");
		MeshRenderer myrend = GetComponent<MeshRenderer>();
		myrend.material = newMat;
		int c = transform.childCount;
		for (int i = 0; i < c; i++) {
			transform.GetChild (i).gameObject.layer = LayerMask.NameToLayer ("iFrames");
		}
		Invoke ("resetLayer", iTime);
	}

	public void resetLayer(){
		gameObject.layer = LayerMask.NameToLayer ("player");
		int c = transform.childCount;
		for (int i = 0; i < c; i++) {
			transform.GetChild (i).gameObject.layer = LayerMask.NameToLayer ("player");
		}
		GetComponent<MeshRenderer>().material = mat;
	}

	public void changeHealth(int damage){
		//a negative value for damage means the player is healed
		health -= damage;
		if (health <= 0) {
			score = 0;
			Destroy (gameObject);
			bullet.GetComponent<Weapon> ().dmg = 1;
			boomerang.GetComponent<Weapon> ().dmg = 2;
			bullet.GetComponent<Weapon> ().firerate = 10;
			boomerang.GetComponent<Weapon> ().firerate = 2;
			sword.transform.localScale = new Vector3 (.25f, .02f, .1f);
			arrow.GetComponent<Weapon> ().dmg = 3;
			arrow.GetComponent<Weapon> ().firerate = 2;
			SceneManager.LoadScene ("Main Menu");
		}
		//healthnum.text = "Health: ";
	}

	public void Shoot() {
		if (!shooting)
			return;
		int shotspeed = curWeapon.GetComponent<Weapon> ().shotspeed;
		Rigidbody shot = Instantiate (curWeapon, curdir.position, curdir.rotation * curWeapon.rotation) as Rigidbody;
		shot.transform.Rotate (new Vector3 (0, 45+180, 0));
		shot.AddForce (curdir.forward * shotspeed);
		if(curWeapon.GetComponent<Weapon> ().isRang)
		shot.AddTorque (new Vector3(0, 1000, 0));
	}

	public void changeWeapon(){
		int weap = Random.Range (1, 5);
		if (weap == 1) {
			curWeapon = bullet;
		} else if (weap == 2) {
			curWeapon = boomerang;
		} else if (weap == 3) {
			curWeapon = sword;
		} else if (weap == 4) {
			curWeapon = arrow;
		}
		weaponImage.sprite = curWeapon.GetComponent<Weapon> ().image;
		CancelInvoke ();
		if (curWeapon != sword)
			InvokeRepeating ("Shoot", (float)0.0, (float)1.0 / curWeapon.GetComponent<Weapon> ().firerate);
		else {
			Rigidbody shot = Instantiate (curWeapon, ushot.position, ushot.rotation*curWeapon.rotation) as Rigidbody;
		}
	}

	public void MysteryBox(){
		int r = Random.Range (1, 10);
		if (r == 1) {
			changeHealth (-1);
			newItem.text = "Health Up";
			Invoke ("resetText", 1);
		} else if (r == 2) {
			boomerang.GetComponent<Weapon> ().dmg *= 2;
			newItem.text = "Double Boomerang Damage";
			Invoke ("resetText", 1);
		} else if (r == 3) {
			bullet.GetComponent<Weapon> ().dmg *= 2;
			newItem.text = "Double Bullet Damage";
			Invoke ("resetText", 1);
		} else if (r == 4) {
			bullet.GetComponent<Weapon> ().firerate *= 2;
			newItem.text = "Double Bullet Firerate";
			Invoke ("resetText", 1);
		} else if (r == 5) {
			boomerang.GetComponent<Weapon> ().firerate *= 2;
			newItem.text = "Double Boomerang Firerate";
			Invoke ("resetText", 1);
		} else if (r == 6) {
			sword.transform.localScale += new Vector3 (.05f, 0, 0);
			newItem.text = "Double sword length";
			Invoke ("resetText", 1);
		} else if (r == 7) {
			speed += 2;
			newItem.text = "Increased Movement Speed";
			Invoke ("resetText", 1);
		} else if (r == 8) {
			arrow.GetComponent<Weapon> ().firerate *= 2;
			newItem.text = "Double Arrow Firerate";
			Invoke ("resetText", 1);
		} else if (r == 9) {
			arrow.GetComponent<Weapon> ().dmg *= 2;
			newItem.text = "Double Arrow Damage";
			Invoke ("resetText", 1);
		}
	}

	public void resetText(){
		newItem.text = "";
	}


		
	void Start () {
		//healthnum.text = "Health: ";
		changeWeapon ();
		score = 0;
		scorenum.text = "Score: " + score;
		//InvokeRepeating ("Shoot", (float)0.0, (float)1.0 / curWeapon.GetComponent<Weapon>().firerate);
	}
	
	// Update is called once per frame
	void Update () {
		shooting = false;
		float h = 0;
		float v = 0;
		//h = Input.GetAxis ("Horizontal");
		//v = Input.GetAxis ("Vertical");
		h = mJoy.Horizontal();
		v = mJoy.Vertical ();
		transform.Translate (h * speed* Time.deltaTime, 0, v * speed*Time.deltaTime);
		//float tstart = Time.time;
		float ph = 0;
		float pv = 0;
		//pv = Input.GetAxis ("Fire1");
		//ph = Input.GetAxis ("Fire2");
		pv = sJoy.Vertical();
		ph = sJoy.Horizontal ();
		//sets curdir to the appropriate transform
		if (ph != 0 || pv != 0) {
			if (pv == 1) {
				curdir = ushot;
			} else if (pv == -1) {
				curdir = dshot;
			} else if (ph == 1) {
				curdir = rshot;
			} else if (ph == -1) {
				curdir = lshot;
			}
			shooting = true;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			changeHealth (1000);
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			openDoor ();
		}
	}

	public void openDoor(){
		GameObject[] doors = GameObject.FindGameObjectsWithTag ("Door");
		bool close = false;
		if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
			foreach (GameObject d in doors) {
				Transform t = d.transform;
				if(Vector3.Distance(t.position,transform.position) <= 3){
					close = true;
					break;
				}
			}
			if (close) {
				//Debug.Log ("Entered Door");
				score++;
				scorenum.text = "Score: " + score;
				transform.position = new Vector3 (-transform.position.x, transform.position.y, -transform.position.z);
				GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);
				gameObject.layer = LayerMask.NameToLayer ("player");
				resetText ();
				GameObject.FindGameObjectWithTag ("Room").GetComponent<RoomGeneration> ().reloadRoom ();
				changeWeapon ();
			}
		}
	}
}
