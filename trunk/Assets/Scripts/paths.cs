using UnityEngine;
using System.Collections;

public class paths : MonoBehaviour {
	
	public GameObject character;
	bool withinBoundary = false;
	bool isStopped = false;
	public string pathName;
	
	// Use this for initialization
	void Start () {
		walk(5);
	}
	
	// Update is called once per frame
	void Update () {
		// resume walking if stopped
		if(isStopped){
			if(!withinBoundary){
				walk(5);
				isStopped = false;
			}
		}
		// stop when you approach him
		if(withinBoundary){
			// for animation walking
			iTween.Pause(gameObject);
			character.animation.Stop();
			isStopped = true;
		}
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
	
	void walk(int num) {
		switch(num){
			case 5:	
				character.animation.CrossFade("walk");	
				iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", 10, "easetype", iTween.EaseType.linear,"onComplete", "walk", "onCompleteParams", 5, "onCompleteTarget", gameObject, "orienttopath", true));
				break;
		}
	}
	
	void OnMouseDown(){

	}
	
}
