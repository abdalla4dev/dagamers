using UnityEngine;
using System.Collections;
using MurderData;

public class KnowledgeTimelineUI : MonoBehaviour {
	
	public Texture buttonTexture; 
	public GUIStyle buttonStyle;

	private Rect windowRect = new Rect (20, 60, 512, 256);
	private bool showWindow = false; //to determine whether the player wants to see the window or not 
	
	private Vector2 scrollPos = new Vector2(0.5F, 0.5F);
			
	public GUIStyle timelineStyle;
	public GUIContent timelineContent;
	
	public GUIStyle boxStyle;
	public GUIStyle labelStyle;
	
	public GenerateTimeline theTimeline;
	
	private int startTime;
		
	// Use this for initialization
	void Start () {
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		startTime = (int) GenerateTimeline.deathTime;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI () {
		
		GUILayout.Space(25);
		if (GUILayout.Button(buttonTexture, buttonStyle, GUILayout.Width(200))) showWindow = !showWindow;
		
		if (showWindow) GUILayout.Window(1, windowRect, TimelineFunction, timelineContent, timelineStyle);

	}
	
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
			GUILayout.Label("Suspects", labelStyle, GUILayout.Height(50));
			GUILayout.Label(preMurderTime, labelStyle, GUILayout.Width(120));
			GUILayout.Label(murderTime, labelStyle);
			GUILayout.Label(postMurderTime, labelStyle);
			//GUILayout.Label("1500 hrs", labelStyle);
			//GUILayout.Label("1600 hrs", labelStyle);
		GUILayout.EndHorizontal();
		
		string wifePreMurder = "At " + GenerateTimeline.getPersonDetails(0,0,0) +", "+ GenerateTimeline.getPersonDetails(0,0,1) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,0, Person.alibi );
		string wifeMurder = "At " + GenerateTimeline.getPersonDetails(1,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,0,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,0, Person.alibi);
		string wifePostMurder = "At " + GenerateTimeline.getPersonDetails(2,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,0,Person.activity)
			+", seen by "+ GenerateTimeline.getPersonDetails(2,0, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Wife", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.magenta;
			if (GenerateTimeline.timeline[0].getBefUnlocked()) {
				GUILayout.Box(wifePreMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getDuringUnlocked()) {
				GUILayout.Box(wifeMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getAftUnlocked()) {
				GUILayout.Box(wifePostMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
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
			GUILayout.Label("Son", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUILayout.Space(240);
			//GUI.backgroundColor = Color.yellow;
			if (GenerateTimeline.timeline[1].getBefUnlocked()) {
				GUILayout.Box(sonPreMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getDuringUnlocked()) {
				GUILayout.Box(sonMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getAftUnlocked()) {
				GUILayout.Box(sonPostMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
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
			GUILayout.Label("Daughter", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.green;
			if (GenerateTimeline.timeline[2].getBefUnlocked()) {
				GUILayout.Box(daughterPreMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getDuringUnlocked()) {
				GUILayout.Box(daughterMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getAftUnlocked()) {
				GUILayout.Box(daughterPostMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120); 
		GUILayout.EndHorizontal();
		
		string maidPreMurder = "At " + GenerateTimeline.getPersonDetails(0,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,3, Person.alibi);
		string maidMurder = "At " + GenerateTimeline.getPersonDetails(1,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,3, Person.alibi);
		string maidPostMurder = "At " + GenerateTimeline.getPersonDetails(2,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,3, Person.alibi);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Maid", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUI.backgroundColor = Color.cyan;
			if (GenerateTimeline.timeline[3].getBefUnlocked()) {
				GUILayout.Box(maidPreMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getDuringUnlocked()) {
				GUILayout.Box(maidMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getAftUnlocked()) {
				GUILayout.Box(maidPostMurder, boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			} else GUILayout.Space(120);
		GUILayout.EndHorizontal();
		
		GUILayout.EndScrollView();
		
		//Debug.Log("In theTimeline Scrollview\n" + wifePreMurder + "\n"+ wifeMurder);
	}

}
