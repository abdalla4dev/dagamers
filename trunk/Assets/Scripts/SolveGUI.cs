using UnityEngine;
using System.Collections;
using MurderData;
using System;
using System.Collections.Generic;

public class SolveGUI : MonoBehaviour {
	
	public Texture buttonTexture; 
	public GUIStyle buttonStyle;
	
	private bool showSolver = false;
	private Rect windowRect = new Rect(60, 60, 400, 400);
	
	private string suspectSelection;
	private List<string> suspectAnswers = new List<string>();
	private string roomSelection;
	private List<string> roomAnswers = new List<string>();
	private string weaponSelection;
	private List<string> weaponAnswers = new List<string>();
	
	private bool solved = false;
	
	public static string SelectList( IEnumerable<string> list, string selected, GUIStyle defaultStyle, GUIStyle selectedStyle )

        {         
			foreach( string item in list )
            {
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
       
       
       
        public static string SelectList( IEnumerable<string> list, string selected, OnListItemGUI itemHandler )
        {
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
	
	private bool OnCheckboxItemGUI( string item, bool selected, IEnumerable<string> list )
    {
        return GUILayout.Toggle( selected, item.ToString() );
    }
	
	// Use this for initialization
	void Start () {
		for (int i=0; i< Globals.numSuspects; i++) {
			suspectAnswers.Add(Enum.GetName(typeof(Suspects), i));
		}
		/*for (int i=0; i<Globals.numRooms; i++) {
			roomAnswers.Add(Enum.GetName(typeof(Rooms),i));
		}*/
		for (int i=0; i<Globals.numWeapons; i++) {
			weaponAnswers.Add(Enum.GetName(typeof(Weapons),i));
		}
		//Debug.Log(answers[0] + " " +answers[1] + " " +answers[2] + " " +answers[3]);
		/*answers.Add("Wife");
		answers.Add("Son");
		answers.Add("Daughter");
		answers.Add("Maid");*/
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUILayout.Space(50);
		if (GUILayout.Button(buttonTexture, buttonStyle, GUILayout.Height(25))) showSolver = !showSolver;
		if (showSolver) GUILayout.Window(4, windowRect, SolveFunction, "Solve!");
		
		if (solved) GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100,100), "You Won!");
	}
	
	void SolveFunction(int windowID) {
		GUILayout.Label("The murderer was ...");
		suspectSelection = SelectList(suspectAnswers ,suspectSelection, OnCheckboxItemGUI);
		GUILayout.Label("using ...");
		weaponSelection = SelectList(weaponAnswers ,weaponSelection, OnCheckboxItemGUI);
		//GUILayout.Label("in ...");
		//roomSelection = SelectList(roomAnswers ,roomSelection, OnCheckboxItemGUI);
		if (GUILayout.Button("Solve!") ) {
			List<string> answer = GenerateTimeline.murderTruth.getMurder();
			if (GenerateTimeline.murderWeap.ToString() == weaponSelection && 
				Enum.GetName(typeof(Suspects), GenerateTimeline.murderer) == suspectSelection) {
				//check if game solved
				Debug.Log("Solved");
				solved = true;
			} else {
				Debug.Log("Answer is " + answer[Person.place] + " " + GenerateTimeline.murderWeap.ToString() + " " +Enum.GetName(typeof(Suspects), GenerateTimeline.murderer));
			}
		}
	}
}
