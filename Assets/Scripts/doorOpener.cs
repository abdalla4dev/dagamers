using UnityEngine;
using System.Collections;

public class doorOpener : MonoBehaviour {

	public GameObject door;
	bool withinBoundary = false;
	bool open = false;
	public int doorRotation = 175;
	public int doorRotateBack = 90;
	
	void Start () {
	
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
	
	IEnumerator OnMouseDown(){
		if(withinBoundary && !open){
			Vector3 doorPos = transform.TransformPoint(door.transform.position);
			Vector3 doorPosWorld = doorPos + new Vector3(-2,0,0);
			//iTween.MoveTo(door, doorPosWorld, 2);
			//iTween.MoveBy(door, new Vector3(0,0,-2), 2);
			iTween.RotateTo(door, iTween.Hash("y",doorRotation,"time",1.0));
			open = true;
		}
			
		yield return new WaitForSeconds(5);
		if(open)
		{
			Vector3 doorPos = transform.TransformPoint(door.transform.position);
			Vector3 doorPosWorld = doorPos + new Vector3(2,0,0);
			//iTween.MoveTo(door, doorPosWorld, 2);
			//iTween.MoveBy(door, new Vector3(0,0,2),2);
			iTween.RotateTo(door, iTween.Hash("y",90,"time",1.0));
			open = false;
		}
		
	}
}
