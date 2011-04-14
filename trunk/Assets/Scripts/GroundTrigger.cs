using UnityEngine;
using System.Collections;

public class GroundTrigger : MonoBehaviour {
	
	public Transform charObj;
	
	float offset = 1.42f;
	Vector3 charPosition;
	// Use this for initialization
	void Start () {
	 
	}
	
	void OnTriggerEnter(){
		charPosition = charObj.position;
		charPosition.y = offset;
		charObj.position = charPosition;
	}
	
	void OnTriggerExit(){
//		charPosition = charObj.position;
//		charPosition.y = offset;
//		charObj.position = charPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
