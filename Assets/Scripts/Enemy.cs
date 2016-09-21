using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health;
	public int contactDmg;
	public float speed;
	public Transform player;
	public int difLevel;
	//private Transform player = GameObject.FindGameObjectWithTag("Player").transform;

	public void changeHealth(int dmg){
		health -= dmg;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
		
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = Vector3.MoveTowards (transform.position, player.position, 1/speed);
		}
	}
}
