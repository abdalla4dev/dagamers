using UnityEngine;
using System.Collections;

public class loadingScreen : MonoBehaviour {
	
	private AsyncOperation async;

	void Start () {
		async = Application.LoadLevelAsync ("main");
	}
	
	void Update () {

	}
	void OnGUI() {
		if (async != null){
			GUI.Box(new Rect(0, Screen.height - 40, async.progress * Screen.width, 40), "");	
		}
	}
}
