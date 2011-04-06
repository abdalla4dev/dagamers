using UnityEngine;
using System.Collections;
using MurderData;
using System;
using System.Collections.Generic;

public class ToolBar : MonoBehaviour {
	
	public GUISkin tabSkin;
	
	public Texture2D timelineTex;
	public Texture2D mapTex;
	public Texture2D solveTex;
	
	float mapButtonX = 0;
	float timelineButtonX = 0;
	float solveButtonX = 0;
	bool showMap = false;
	bool showTimeline = false;
	bool showSolve = false;
	
	public GUIContent mapContentBg;
	public GUIContent timelineContentBg;
	public GUIContent solveContentBg;

	// for map
	public Texture2D floorplan;

	// for timeline
	public GenerateTimeline theTimeline;
	private int startTime;
	public GUIStyle timelineBoxStyle;
	public GUIStyle timelineLabelStyle;
	private Vector2 scrollPos = new Vector2(0.5F, 0.5F);
	
	// for solve
	private string suspectSelection;
	private List<string> suspectAnswers = new List<string>();
	private string roomSelection;
	private List<string> roomAnswers = new List<string>();
	private string weaponSelection;
	private List<string> weaponAnswers = new List<string>();
	public GUIStyle solvedStyle;
	private bool solved = false;
	
	// Use this for initialization
	void Start () {
		// for timeline
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		startTime = (int) GenerateTimeline.deathTime;
		
		// for solve
		for (int i=0; i< Globals.numSuspects; i++) {
			suspectAnswers.Add(Enum.GetName(typeof(SuspectEnum), i));
		}
		/*for (int i=0; i<Globals.numRooms; i++) {
			roomAnswers.Add(Enum.GetName(typeof(Rooms),i));
		}*/
		for (int i=0; i<Globals.numWeapons; i++) {
			weaponAnswers.Add(Enum.GetName(typeof(WpnEnum),i));
		}
		//Debug.Log(answers[0] + " " +answers[1] + " " +answers[2] + " " +answers[3]);
		/*answers.Add("Wife");
		answers.Add("Son");
		answers.Add("Daughter");
		answers.Add("Maid");*/
	}
	
	void OnGUI(){
		GUI.skin = tabSkin;
		
		// 3 TAB Buttons
		if (GUI.Button(new Rect(mapButtonX, 5, 38, 225), mapTex)){
			showMap = !showMap;
		}
		if (GUI.Button(new Rect(timelineButtonX, 230, 38, 225), timelineTex)){
			showTimeline = !showTimeline;
		}
		if (GUI.Button(new Rect(solveButtonX, 460, 38, 225), solveTex)){
			showSolve = !showSolve;
		}
		
		// MAP WINDOW 31
		if(showMap == true){
			GUI.Window(31, new Rect(0, 0, 500, Screen.height), mapWindow, mapContentBg);
			mapButtonX = 475;
			GUI.BringWindowToFront(31);
		}
		else {
			mapButtonX = 0;	
		}

		// TIMELINE WINDOW 32
		if(showTimeline == true){
			GUI.Window(32, new Rect(0, 0, 500, Screen.height), timelineWindow, timelineContentBg);
			timelineButtonX = 475;
			GUI.BringWindowToFront(32);
		}
		else {
			timelineButtonX = 0;	
		}
		
		// SOLVE WINDOW 33
		if(showSolve == true){
			GUI.Window(33, new Rect(0, 0, 500, Screen.height), solveWindow, solveContentBg);
			solveButtonX = 475;
			GUI.BringWindowToFront(33);
		}
		else {
			solveButtonX = 0;	
		}
		
		if (solved) {
			GUI.Label(new Rect(Screen.width/2, Screen.height/2, 300,100), "You have solved the puzzle and won!", solvedStyle);
			showSolve = false;
		}
	}
		
	// Update is called once per frame
	void Update () {
	
	}
	
	void mapWindow(int windowID) {
		GUI.Label(new Rect(0,0,450,768), floorplan);
	}
		
	// imported from KnowledgeTimelineUI.cs
	void timelineWindow (int windowID) {
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
	
	// imported from SolveGUI.cs
	public static string SelectList( IEnumerable<string> list, string selected, GUIStyle defaultStyle, GUIStyle selectedStyle ){         
		foreach( string item in list ){
            if( GUILayout.Button( item.ToString(), ( selected == item ) ? selectedStyle : defaultStyle ) )
            {
                if( selected == item )
                // Clicked an already selected item. Deselect.
                {
                    selected = null;
                }
                else
                {
                    selected = item;
                }
            }
        }
        return selected;
	}
	
    public delegate bool OnListItemGUI( string item, bool selected, IEnumerable<string> list );
	
	public static string SelectList( IEnumerable<string> list, string selected, OnListItemGUI itemHandler ){
	    List<string> itemList;
	   
	    itemList = new List<string>( list );
	   
	    foreach( string item in itemList )
	    {
	        if( itemHandler( item, item == selected, list ) )
	        {
	            selected = item;
	        }
	        else if( selected == item )
	        // If we *were* selected, but aren't any more then deselect
	        {
	            selected = null;
	        }
	    }
	
	    return selected;
    }
	
	private bool OnCheckboxItemGUI( string item, bool selected, IEnumerable<string> list ){
        return GUILayout.Toggle( selected, item.ToString() );
    }
	
	void solveWindow(int windowID) {
		GUILayout.Label("The murderer was ...");
		suspectSelection = SelectList(suspectAnswers ,suspectSelection, OnCheckboxItemGUI);
		GUILayout.Label("using ...");
		weaponSelection = SelectList(weaponAnswers ,weaponSelection, OnCheckboxItemGUI);
		//GUILayout.Label("in ...");
		//roomSelection = SelectList(roomAnswers ,roomSelection, OnCheckboxItemGUI);
		if (GUILayout.Button("Solve!") ) {
			string place_answer = GenerateTimeline.murderer.getDuringMurderRoom().ToString(); //not used for answering, coz there's no questions about place of murder
			if (GenerateTimeline.murderWeap.ToString() == weaponSelection && 
				GenerateTimeline.murderer.name.ToString() == suspectSelection) {
				//check if game solved
				Debug.Log("Solved");
				solved = true;
			} else {
				Debug.Log("Answer is " + place_answer + " " + GenerateTimeline.murderWeap.ToString() + " " + GenerateTimeline.murderer.name.ToString());
			}
		}
	}
}
