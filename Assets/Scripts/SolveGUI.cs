using UnityEngine;
using System.Collections;
using MurderData;
using System;
using System.Collections.Generic;

public class SolveGUI : MonoBehaviour {
	
	private bool showSolver = false;
	private Rect windowRect = new Rect(20, 60, 600, 600);
	private object selection;
	private List<string> answers = new List<string>();
	
	public static object SelectList( ICollection list, object selected, GUIStyle defaultStyle, GUIStyle selectedStyle )

        {         
            foreach( object item in list )
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
       
       
       
        public delegate bool OnListItemGUI( object item, bool selected, ICollection list );
       
       
       
        public static object SelectList( ICollection list, object selected, OnListItemGUI itemHandler )
        {
            ArrayList itemList;
           
            itemList = new ArrayList( list );
           
            foreach( object item in itemList )
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
	
	private bool OnCheckboxItemGUI( object item, bool selected, ICollection list )
    {
        return GUILayout.Toggle( selected, item.ToString() );
    }
	
	// Use this for initialization
	void Start () {
		for (int i=0; i< Globals.numSuspects; i++) {
			answers.Add(Enum.GetName(typeof(Suspects), i));
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
		if (GUILayout.Button("Solve", GUILayout.Height(25))) showSolver = !showSolver;
		if (showSolver) GUILayout.Window(4, windowRect, SolveFunction, "Solve!");
	}
	
	void SolveFunction(int windowID) {
		selection = SelectList(answers ,selection, OnCheckboxItemGUI);
	}
}
