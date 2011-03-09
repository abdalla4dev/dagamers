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
		if(Input.GetKeyDown(KeyCode.E)){
			if(withinBoundary && !open){
				iTween.RotateTo(door, iTween.Hash("y",doorRotation,"time",2.0));
				open = true;
			}
			else if(withinBoundary && open)
			{
				iTween.RotateTo(door, iTween.Hash("y",90,"time",2.0));
				open = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
}