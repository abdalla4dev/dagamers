using UnityEngine;
using System.Collections;

public class characterPaths : MonoBehaviour {
	
	public GameObject character;
	public string characterName;
	bool withinBoundary = false;
	bool isStopped = false;
	GameObject player;
	
	// Use this for initialization
	void Start () {
		//character.animation.CrossFade("walk");
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// resume walking if stopped
		if(isStopped){
			if(!withinBoundary){
				iTween.Resume(gameObject,"MoveTo");
				character.animation.CrossFade("walk");
				isStopped = false;
			}
		}
		// stop when you approach him
		if(withinBoundary){
			// for animation walking
			iTween.Pause(gameObject,"MoveTo");
			iTween.LookTo(gameObject, iTween.Hash("looktarget", player.transform, "time", 1));
			if (characterName == "theDaughter"){
				character.animation.CrossFade("idle");
			}
			else {
				character.animation.Stop();
			}
			isStopped = true;
		}
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
}
