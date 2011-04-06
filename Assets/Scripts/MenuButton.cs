using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public Texture2D menuTex;
	public GUISkin menuSkin;
	
	bool toggle = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		GUI.skin = menuSkin;
		
		if(GUI.Button(new Rect((Screen.width - 137),0, 157, 50), menuTex)){
			toggle = true;
		}
		
		if(toggle){
			GUILayout.Window(21, new Rect((Screen.width/2) - 100, (Screen.height/2) - 100, 200, 200), menuWindow, "PAUSE");
		}
	}
	
	void menuWindow(int windowID){
		if(GUILayout.Button("Resume")){
			toggle = false;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
