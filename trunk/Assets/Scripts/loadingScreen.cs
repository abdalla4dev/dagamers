using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {
	
	AsyncOperation async;
	float progress;
	
	// Use this for initialization
	void Start () {
		async = Application.LoadLevelAsync ("main");
    	yield async;
		Debug.Log("Loading complete");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
}
