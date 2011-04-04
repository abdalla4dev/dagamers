using UnityEngine;
using System.Collections;

public class ToolBar : MonoBehaviour {
	
	public GUISkin mapSkin;
	
	public Texture2D timelineTex;
	public Texture2D timelineBgTex;
	public Texture2D mapTex;
	public Texture2D mapBgTex;
	public Texture2D solveTex;
	public Texture2D solveBgTex;
	
	float MapWindowWidth, MapWindowHeight;
	float mapButtonY = 0;
	float mapButtonX = 0;
	
	float timelineWindowX, timelineWindowY, timelineWindowWidth, timelineWindowHeight, timelineButtonX, timelineButtonY;
	float solveWindowX, solveWindowY, solveWindowWidth, solveWindowHeight, solveButtonX, solveButtonY;
	//float startX = 0;
	
	bool clicked = false;
	
	// Use this for initialization
	void Start () {
		MapWindowWidth = 35;
		MapWindowHeight = 256;
		
		timelineWindowWidth = 35;
		timelineWindowHeight = 256;
		timelineWindowX = 0;
		timelineWindowY = 256;
		timelineButtonX = 0;
		timelineButtonY = 0;
		
		solveWindowWidth = 35;
		solveWindowHeight = 256;
		solveWindowX = 0;
		solveWindowY = 512;
		solveButtonX = 0;
		solveButtonY = 0;
		
	}
	
	void OnGUI(){
		GUI.skin = mapSkin;
		
		//map window
		GUI.Window(0, new Rect(0, 0, MapWindowWidth, MapWindowHeight), DoMyWindow, "");
		
		//timeline window
		GUI.Window(1, new Rect(timelineWindowX, timelineWindowY, timelineWindowWidth, timelineWindowHeight), DoMyWindow, "");
		
		//solve window
		GUI.Window(2, new Rect(solveWindowX, solveWindowY, solveWindowWidth, solveWindowHeight), DoMyWindow, "");
	}
	
	
	void DoMyWindow(int windowID) {
		if (windowID == 0) {
			//if (GUILayout.Button(mapTex, GUILayout.Width(32), GUILayout.Height(256))){
			if (GUI.Button(new Rect(mapButtonX, mapButtonY, 32, 256), mapTex)){
				if(clicked == false){
					MapWindowWidth = 400;
					MapWindowHeight = Screen.height;
					mapButtonX = 360;
					//GUI.BringWindowToBack(0);
					clicked = true;
					print("Got a click");
				}
				else{
					MapWindowHeight = 256;
					MapWindowWidth = 35;
					mapButtonX = 0;
					clicked = false;
						
				}
			}
		}
		else if(windowID == 1){
			//if (GUILayout.Button(timelineTex, GUILayout.Width(30), GUILayout.Height(150))){
			if (GUI.Button(new Rect(timelineButtonX, timelineButtonY, 32, 256), timelineTex)){
				if(clicked == false){
					timelineWindowY = 0;
					timelineWindowWidth = 400;
					timelineWindowHeight = Screen.height;
					timelineButtonX = 360;
					timelineButtonY = 256;
					clicked = true;
					print("Got a click1");
				}
				else{
					timelineWindowY = 256;
					timelineWindowHeight = 256;
					timelineWindowWidth = 35;
					timelineButtonX = 0;
					timelineButtonY = 0;
					clicked = false;	
				}
			}
		}
		else if(windowID == 2){
			//if (GUILayout.Button(timelineTex, GUILayout.Width(30), GUILayout.Height(150))){
			if (GUI.Button(new Rect(solveButtonX, solveButtonY, 32, 256), solveTex)){
				if(clicked == false){
					solveWindowY = 0;
					solveWindowWidth = 400;
					solveWindowHeight = Screen.height;
					solveButtonX = 360;
					solveButtonY = 512;
					clicked = true;
					print("Got a click1");
				}
				else{
					solveWindowY = 512;
					solveWindowHeight = 256;
					solveWindowWidth = 35;
					solveButtonX = 0;
					solveButtonY = 0;
					clicked = false;	
				}
			}
		}
			
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
}
