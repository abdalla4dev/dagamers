using UnityEngine;
using System.Collections;

public class startMenuScript : MonoBehaviour {
	
	public int playOrQuit;
	public GameObject gameObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver () {
		iTween.RotateTo(gameObject,iTween.Hash("z", 388, "time", 0.5));
	}
	
	void OnMouseExit () {
		iTween.RotateTo(gameObject,iTween.Hash("z", 348, "time", 0.5));
	}
	
	void OnMouseDown () {
		if(playOrQuit == 1){
			Application.LoadLevel("main"); 
		}
		else if (playOrQuit == 555){
			Application.LoadLevel("StartMenu"); 
		}
		else {
			var isWebPlayer = (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer);
			if (!isWebPlayer)
			{
				Application.Quit();
				Debug.Log("Quit");	
			}
		}
	}
	
}
