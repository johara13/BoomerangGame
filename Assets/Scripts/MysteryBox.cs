using UnityEngine;
using System.Collections;

public class MysteryBox : MonoBehaviour {

	public void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag.Equals("Player")){
			collider.gameObject.GetComponent<PlayerActions>().MysteryBox();
			Destroy(gameObject);
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
