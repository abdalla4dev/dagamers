using UnityEngine;
using System.Collections;

public class doorOpener : MonoBehaviour {

	public GameObject door;
	bool withinBoundary = false;
	bool open = false;
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
	
	IEnumerator OnMouseEnter(){
		//changing all the material to be yellow upon mouse hover, once the player is close to the object
		if(withinBoundary && !open){
			Vector3 doorPos = transform.TransformPoint(door.transform.position);
			Vector3 doorPosWorld = doorPos + new Vector3(-2,0,0);
			iTween.MoveTo(door, doorPosWorld, 2);
				//iTween.MoveBy(door, new Vector3(0,0,-2), 2);
			open = true;
		}
			
		yield return new WaitForSeconds(5);
		if(open)
		{
		open = false;
			Vector3 doorPos = transform.TransformPoint(door.transform.position);
			Vector3 doorPosWorld = doorPos + new Vector3(2,0,0);
			iTween.MoveTo(door, doorPosWorld, 2);
		//iTween.MoveBy(door, new Vector3(0,0,2),2);
		}
		
	}
}
