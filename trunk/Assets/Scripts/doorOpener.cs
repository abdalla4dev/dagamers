using UnityEngine;
using System.Collections;

public class doorOpener : MonoBehaviour {

	public GameObject door;
	bool withinBoundary = false;
	// Use this for initialization
	void Start () {
		//door= new GameObject();
		//door = GameObject.Find("InnerDoor0");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
	
	void OnMouseEnter(){
		//changing all the material to be yellow upon mouse hover, once the player is close to the object
		if(withinBoundary){
				iTween.RotateTo(door, new Vector3(0,180,0), 2);
		}			
		
	}
}
