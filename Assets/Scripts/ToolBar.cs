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
	
	float timelineWindowX, timelineWindowY, timelineWindowWidth, timelineWindowHeight, timelineButtonX, timelineButtonY, timelineInsideWinWidth, timelinePos;
	float solveWindowX, solveWindowY, solveWindowWidth, solveWindowHeight, solveButtonX, solveButtonY;
	//float startX = 0;
	
	bool clicked = false;
	
	// for timeline
	public GenerateTimeline theTimeline;
	private int startTime;
	public GUIStyle timelineStyle;
	public GUIContent timelineContent;
	public GUIStyle timelineBoxStyle;
	public GUIStyle timelineLabelStyle;
	private Vector2 scrollPos = new Vector2(0.5F, 0.5F);
	
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
		
		// for timeline
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		startTime = (int) GenerateTimeline.deathTime;
	}
	
	void OnGUI(){
		GUI.skin = mapSkin;
		
		//map window
		GUI.Window(0, new Rect(0, 0, MapWindowWidth, MapWindowHeight), DoMyWindow, "");
		
		//timeline window
		GUI.Window(1, new Rect(timelineWindowX, timelineWindowY, timelineWindowWidth, timelineWindowHeight), DoMyWindow, "");
		timelinePos = 0;
		GUI.Window(32, new Rect(timelinePos, timelinePos, timelineInsideWinWidth, 768), TimelineFunction, timelineContent, timelineStyle);
			
		//solve window
		GUI.Window(2, new Rect(solveWindowX, solveWindowY, solveWindowWidth, solveWindowHeight), DoMyWindow, "");
	}
	
	
	void DoMyWindow(int windowID) {
		
		// MAP
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
		
		// TIMELINE
		else if(windowID == 1){
			//if (GUILayout.Button(timelineTex, GUILayout.Width(30), GUILayout.Height(150))){
			if (GUI.Button(new Rect(timelineButtonX, timelineButtonY, 32, 256), timelineTex)){
				if(clicked == false){
					clicked = true;
				} else {
					clicked = false;
				}
			}
			if(clicked == true){
				//iTween.ValueTo(gameObject, iTween.Hash("from",(new Rect(0,256,35,256)), "to",(new Rect(0,0,400,768)),"onupdate","myButton","onupdateparams","Rect","time",1));
				timelineWindowY = 0;
				timelineWindowWidth = 400;
				timelineWindowHeight = Screen.height;
				timelineButtonX = 360;
				timelineButtonY = 256;
				timelineInsideWinWidth = 400;
				timelinePos = 0;
				GUI.BringWindowToFront(32);
			}
			else {
				//iTween.ValueTo(gameObject, iTween.Hash("from",(new Rect(0,0,400,768)), "to",(new Rect(0,256,35,256)),"onupdate","myButton","onupdateparams","Rect","time",1));
				timelineWindowY = 256;
				timelineWindowHeight = 200;
				timelineWindowWidth = 35;
				timelineButtonX = 0;
				timelineButtonY = 0;
				timelineInsideWinWidth = 0;
				timelinePos = -400;
				//GUI.Window(32, new Rect(0, 0, 0, 0), TimelineFunction, timelineContent, timelineStyle);
			}
			//GUI.Window(32, new Rect(timelinePos, timelinePos, timelineInsideWinWidth, 768), TimelineFunction, timelineContent, timelineStyle);
			/*
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
			if (timelineWindowWidth	== 400) {
				GUI.Window(32, new Rect(2, 2, 400, 768), TimelineFunction, timelineContent, timelineStyle);
				GUI.BringWindowToFront(32);
			}
			*/
		}
		
		// SOLVE
		else if(windowID == 2){
			// if (GUILayout.Button(timelineTex, GUILayout.Width(30), GUILayout.Height(150))){
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
		
	// imported from KnowledgeTimelineUI.cs
	void TimelineFunction (int windowID) {
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		
	/*	Debug.Log("scrollview death time: " + GenerateTimeline.deathTime);
		Debug.Log("scrollview wife pre place: " + GenerateTimeline.getPersonDetails(0,0,0));*/
		
		// Draw any Controls inside the window here
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		
		string preMurderTime = startTime +  "00 hrs";
		string murderTime = (startTime+1)+  "00 hrs";
		string postMurderTime = (startTime+2)+  "00 hrs";
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Suspects", timelineLabelStyle, GUILayout.Height(50));
			GUILayout.Label(preMurderTime, timelineLabelStyle, GUILayout.Width(120));
			GUILayout.Label(murderTime, timelineLabelStyle);
			GUILayout.Label(postMurderTime, timelineLabelStyle);
			//GUILayout.Label("1500 hrs", timelineLabelStyle);
			//GUILayout.Label("1600 hrs", timelineLabelStyle);
		GUILayout.EndHorizontal();
		
		string wifePreMurder = "At " + GenerateTimeline.getPersonDetails(0,0,0) +", "+ GenerateTimeline.getPersonDetails(0,0,1) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,0, Person.alibi );
		string wifeMurder = "At " + GenerateTimeline.getPersonDetails(1,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,0,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,0, Person.alibi);
		string wifePostMurder = "At " + GenerateTimeline.getPersonDetails(2,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,0,Person.activity)
			+", seen by "+ GenerateTimeline.getPersonDetails(2,0, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Wife", timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.magenta;
			if (GenerateTimeline.timeline[0].getBefUnlocked()) {
				GUILayout.Box(wifePreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getDuringUnlocked()) {
				GUILayout.Box(wifeMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getAftUnlocked()) {
				GUILayout.Box(wifePostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			//GUILayout.Space(480);	
		GUILayout.EndHorizontal();
		
		string sonPreMurder = "At " + GenerateTimeline.getPersonDetails(0,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,1, Person.alibi);
		string sonMurder = "At " + GenerateTimeline.getPersonDetails(1,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,1, Person.alibi);
		string sonPostMurder = "At " + GenerateTimeline.getPersonDetails(2,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,1, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Son", timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUILayout.Space(240);
			//GUI.backgroundColor = Color.yellow;
			if (GenerateTimeline.timeline[1].getBefUnlocked()) {
				GUILayout.Box(sonPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getDuringUnlocked()) {
				GUILayout.Box(sonMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getAftUnlocked()) {
				GUILayout.Box(sonPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			//GUILayout.Space(240);
		GUILayout.EndHorizontal();
		
		string daughterPreMurder = "At " + GenerateTimeline.getPersonDetails(0,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,2, Person.alibi);
		string daughterMurder = "At " + GenerateTimeline.getPersonDetails(1,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,2, Person.alibi);
		string daughterPostMurder = "At " + GenerateTimeline.getPersonDetails(2,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,2, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Daughter", timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.green;
			if (GenerateTimeline.timeline[2].getBefUnlocked()) {
				GUILayout.Box(daughterPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getDuringUnlocked()) {
				GUILayout.Box(daughterMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getAftUnlocked()) {
				GUILayout.Box(daughterPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120); 
		GUILayout.EndHorizontal();
		
		string maidPreMurder = "At " + GenerateTimeline.getPersonDetails(0,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,3, Person.alibi);
		string maidMurder = "At " + GenerateTimeline.getPersonDetails(1,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,3, Person.alibi);
		string maidPostMurder = "At " + GenerateTimeline.getPersonDetails(2,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,3, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Maid", timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.cyan;
			if (GenerateTimeline.timeline[3].getBefUnlocked()) {
				GUILayout.Box(maidPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getDuringUnlocked()) {
				GUILayout.Box(maidMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getAftUnlocked()) {
				GUILayout.Box(maidPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
		GUILayout.EndHorizontal();
		
		GUILayout.EndScrollView();
		
		//Debug.Log("In theTimeline Scrollview\n" + wifePreMurder + "\n"+ wifeMurder);
	}
}
