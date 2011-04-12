using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public Texture2D menuTex;
	public GUISkin menuSkin;
	
	public GUIStyle MenuButtonStyle;
	
	public static bool gamePause;
	
	public MouseLook mouseControl;
	//public Controller mouseControl2;
	
	//public static bool mouseControl;
	
	bool toggle = false;
	// Use this for initialization
	void Start () {
		mouseControl = gameObject.GetComponent<MouseLook>();
		//mouseControl2 = gameObject.GetComponent<Controller>();
	}
	
	void OnGUI(){
		GUI.skin = menuSkin;
		
		if(GUI.Button(new Rect((Screen.width - 137),0, 157, 50), menuTex, MenuButtonStyle)){
			toggle = true;
		}
		
		if(toggle){
			gamePause = true;
			GUILayout.Window(21, new Rect((Screen.width/2) - 75, (Screen.height/2) - 100, 150, 200), menuWindow, "");
		//	mouseControl2.SendMessage("toggle");
			mouseControl.SendMessage("ControlToggle");
		}
	}
	
	void menuWindow(int windowID){
		if(GUILayout.Button("Resume")){
			toggle = false;
			gamePause = false;
			mouseControl.SendMessage("ControlToggle");
		//	mouseControl2.SendMessage("toggle");
		}
		
		if(GUILayout.Button("Return to Main Menu")){
			Application.LoadLevel("startmenu");	
		}
		
		if(GUILayout.Button("Quit")){
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
