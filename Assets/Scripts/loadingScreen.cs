using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {
	
	private AsyncOperation async;

	// Use this for initialization
	void Start () {
		async = Application.LoadLevelAsync ("main");
    	//yield return async;
		//Debug.Log("Loading complete");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnGUI() {
		if (async != null){
			GUI.Box (new Rect(0, Screen.height - 40, async.progress * Screen.width, 40), "");	
		}
	}
}
