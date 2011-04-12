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
	public Texture2D rank1Tex;
	public Texture2D rank2Tex;
	public Texture2D rank3Tex;
	
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
	public static bool solved = false;
	public static int solveAttempts = 0;
	private bool solvedAttempted = false;
	private Rect solvePopup;
	
	// Use this for initialization
	void Start () {
		// for timeline
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		startTime = (int) GenerateTimeline.deathTime;
		
		// for solve
		for (int i=0; i< Globals.numSuspects; i++) {
			suspectAnswers.Add(Enum.GetName(typeof(SuspectEnum), i));
		}
		for (int i=0; i<Globals.numRooms; i++) {
			roomAnswers.Add(Enum.GetName(typeof(RmEnum),i));
		}
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
			GUI.Window(31, new Rect(0, 0, 500, 768), mapWindow, mapContentBg);
			mapButtonX = 500;
			GUI.BringWindowToFront(31);
		}
		else {
			mapButtonX = 0;	
		}

		// TIMELINE WINDOW 32
		if(showTimeline == true){
			GUI.Window(32, new Rect(0, 0, 500, 768), timelineWindow, timelineContentBg);
			timelineButtonX = 500;
			GUI.BringWindowToFront(32);
		}
		else {
			timelineButtonX = 0;	
		}
		
		// SOLVE WINDOW 33
		if(showSolve == true){
			GUI.Window(33, new Rect(0, 0, 500, 768), solveWindow, solveContentBg);
			solveButtonX = 500;
			GUI.BringWindowToFront(33);
		}
		else {
			solveButtonX = 0;	
		}
		
		if (solved) {
			GUI.Window(99, new Rect(Screen.width/3, Screen.height/3, 300, 300), doSolvePopup, "", solvedStyle);
			GUI.BringWindowToFront(99);
		}
		
		if (solvedAttempted) {
			if(GUI.Button(new Rect(Screen.width/3, Screen.height/3, 300,100), "Oops, that was a wrong guess! Time penalty of 5 minutes added. \n Click here to resume game.", solvedStyle))
			{
				Debug.Log(solvedAttempted);
				solvedAttempted = false;
				showSolve = false;
			}
			showSolve = false;
		}
	}
	
	void doSolvePopup(int Window) {
		int score = GenerateTimeline.scoreSystem();
		Texture showRank = rank1Tex;
		switch(score){
			case 1:
				showRank = rank1Tex;
				break;
			case 2:
				showRank = rank2Tex;
				break;
			case 3:
				showRank = rank3Tex;
				break;
		}
		
		GUILayout.Label("You have solved the mystery! Your rank is ");
		GUILayout.Box(showRank);
		if(GUILayout.Button("Return to Main Menu")){
			Application.LoadLevel("startmenu");
		}
		if(GUILayout.Button("Quit Game")){
			Application.Quit();
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
		
		string preMurderTime = GenerateTimeline.befMurderTime +  "00 hrs";
		string murderTime = GenerateTimeline.deathTime+  "00 hrs";
		string postMurderTime = GenerateTimeline.aftMurderTime+  "00 hrs";
				
		GUILayout.BeginHorizontal();
			GUILayout.Label("Suspects", timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Label(preMurderTime, timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Label(murderTime, timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Label(postMurderTime, timelineLabelStyle, GUILayout.Height(50), GUILayout.Width(120));
			//GUILayout.Label("1500 hrs", timelineLabelStyle);
			//GUILayout.Label("1600 hrs", timelineLabelStyle);
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		string wifePreMurder = "At " + GenerateTimeline.getPersonDetails(0,0,0) +", "+ GenerateTimeline.getPersonDetails(0,0,1) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,0, Person.alibi );
		string wifeMurder = "At " + GenerateTimeline.getPersonDetails(1,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,0,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,0, Person.alibi);
		string wifePostMurder = "At " + GenerateTimeline.getPersonDetails(2,0,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,0,Person.activity)
			+", seen by "+ GenerateTimeline.getPersonDetails(2,0, Person.alibi);
		
		wifePreMurder = wifePreMurder.Replace('_', ' ');
		wifeMurder = wifeMurder.Replace('_', ' ');
		wifePostMurder = wifePostMurder.Replace('_', ' ');
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Mrs. Darcy (Wife of victim)", timelineLabelStyle, GUILayout.Height(160), GUILayout.Width(120));
			//GUI.backgroundColor = Color.magenta;
			if (GenerateTimeline.timeline[0].getBefUnlocked()) {
				GUILayout.Box(wifePreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getDuringUnlocked()) {
				GUILayout.Box(wifeMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[0].getAftUnlocked()) {
				GUILayout.Box(wifePostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			//GUILayout.Space(480);	
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		
		string sonPreMurder = "At " + GenerateTimeline.getPersonDetails(0,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,1, Person.alibi);
		string sonMurder = "At " + GenerateTimeline.getPersonDetails(1,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,1, Person.alibi);
		string sonPostMurder = "At " + GenerateTimeline.getPersonDetails(2,1,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,1,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,1, Person.alibi);
		
		sonPreMurder = sonPreMurder.Replace('_', ' ');
		sonMurder = sonMurder.Replace('_', ' ');
		sonPostMurder = sonPostMurder.Replace('_', ' ');
				
		GUILayout.BeginHorizontal();
			GUILayout.Label("Wayne (Son of victim)", timelineLabelStyle, GUILayout.Height(160), GUILayout.Width(120));
			//GUILayout.Space(240);
			//GUI.backgroundColor = Color.yellow;
			if (GenerateTimeline.timeline[1].getBefUnlocked()) {
				GUILayout.Box(sonPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getDuringUnlocked()) {
				GUILayout.Box(sonMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[1].getAftUnlocked()) {
				GUILayout.Box(sonPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			//GUILayout.Space(240);
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		
		string daughterPreMurder = "At " + GenerateTimeline.getPersonDetails(0,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,2, Person.alibi);
		string daughterMurder = "At " + GenerateTimeline.getPersonDetails(1,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,2, Person.alibi);
		string daughterPostMurder = "At " + GenerateTimeline.getPersonDetails(2,2,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,2,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,2, Person.alibi);
		
		daughterPreMurder = daughterPreMurder.Replace('_', ' ');
		daughterMurder = daughterMurder.Replace('_', ' ');
		daughterPostMurder = daughterPostMurder.Replace('_', ' ');
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Jane (Daughter of victim)", timelineLabelStyle, GUILayout.Height(160), GUILayout.Width(120));
			//GUI.backgroundColor = Color.green;
			if (GenerateTimeline.timeline[2].getBefUnlocked()) {
				GUILayout.Box(daughterPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getDuringUnlocked()) {
				GUILayout.Box(daughterMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[2].getAftUnlocked()) {
				GUILayout.Box(daughterPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120); 
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		
		string maidPreMurder = "At " + GenerateTimeline.getPersonDetails(0,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(0,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(0,3, Person.alibi);
		string maidMurder = "At " + GenerateTimeline.getPersonDetails(1,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(1,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(1,3, Person.alibi);
		string maidPostMurder = "At " + GenerateTimeline.getPersonDetails(2,3,Person.place) +", "+ GenerateTimeline.getPersonDetails(2,3,Person.activity) 
			+", seen by "+ GenerateTimeline.getPersonDetails(2,3, Person.alibi);
		
		maidPreMurder = maidPreMurder.Replace('_', ' ');
		maidMurder = maidMurder.Replace('_', ' ');
		maidPostMurder = maidPostMurder.Replace('_', ' ');
				
		GUILayout.BeginHorizontal();
			GUILayout.Label("Maria the Maid", timelineLabelStyle, GUILayout.Height(160), GUILayout.Width(120));
			//GUI.backgroundColor = Color.cyan;
			if (GenerateTimeline.timeline[3].getBefUnlocked()) {
				GUILayout.Box(maidPreMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getDuringUnlocked()) {
				GUILayout.Box(maidMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
			if (GenerateTimeline.timeline[3].getAftUnlocked()) {
				GUILayout.Box(maidPostMurder, timelineBoxStyle, GUILayout.Width(120), GUILayout.Height(160));
			} else GUILayout.Space(120);
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
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
		GUILayout.Label("in ...");
		roomSelection = SelectList(roomAnswers ,roomSelection, OnCheckboxItemGUI);
		if (GUILayout.Button("Solve!") ) {
			if (GenerateTimeline.murderWeap.ToString() == weaponSelection && 
				GenerateTimeline.murderer.name.ToString() == suspectSelection &&
				GenerateTimeline.murderer.getDuringMurderRoom().ToString() == roomSelection) {
				//check if game solved
				solved = true;
				
			} else {
				solveAttempts++;
				solvedAttempted = true;
				GenerateTimeline.minCount+=5;
				Debug.Log("Answer is " + GenerateTimeline.murderer.getDuringMurderRoom().ToString() + " " + GenerateTimeline.murderWeap.ToString() + " " + GenerateTimeline.murderer.name.ToString());
			}
		}
		GUILayout.Label("Note: a 5 minute penalty will be imposed if you make a wrong/wild guess.");
	}
}
