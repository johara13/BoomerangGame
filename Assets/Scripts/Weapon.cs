using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public int shotspeed;
	public int firerate;
	public bool piercing;
	public bool isRang;
	public int dmg;
	public bool back = false;
	public Sprite image;
	public bool isSword;
	// Use this for initialization
	public void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Enemy")) {
			collision.gameObject.GetComponent<Enemy> ().changeHealth (dmg);
			//if (!piercing)
				Destroy (gameObject);
			//else {
				Physics.IgnoreCollision (GetComponent<Collider> (), collision.collider);
			//}
		} else
			Destroy (gameObject);
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals ("Enemy")) {
			other.gameObject.GetComponent<Enemy> ().changeHealth (dmg);
		} else if (other.gameObject.tag.Equals ("Player")) {
			if (back) {
				Destroy (gameObject);
			}
		} else if (back && !other.gameObject.tag.Equals ("Weapon")) {
			Destroy (gameObject);
		} else if (other.gameObject.tag.Equals ("Untagged") && !isRang && !isSword) {
			Destroy (gameObject);
		}
	}

	void Start () {
		
	}

	public void destroySelf(){
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			if (isRang && Vector3.Distance (transform.position, player.transform.position) > 10 && !back) {
				GetComponent<Rigidbody> ().velocity = -GetComponent<Rigidbody> ().velocity;
				back = true;
				gameObject.layer = LayerMask.NameToLayer ("Default");
			} else if (isSword) {
				transform.position = player.GetComponent<PlayerActions> ().curdir.position;
				transform.rotation = player.GetComponent<PlayerActions> ().curdir.rotation;
				transform.Rotate (0, -90, 0);
			}
		}

	}
}
