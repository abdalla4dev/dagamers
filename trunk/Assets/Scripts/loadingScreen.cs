using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {
	
	AsyncOperation async;
	float progress;
	
	// Use this for initialization
	IEnumerable Start () {
		async = Application.LoadLevelAsync ("main");
    	yield return async;
		Debug.Log("Loading complete");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
}
