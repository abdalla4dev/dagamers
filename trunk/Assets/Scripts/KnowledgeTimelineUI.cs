using UnityEngine;
using System.Collections;

public class KnowledgeTimelineUI : MonoBehaviour {

	private Rect windowRect = new Rect (20, 60, 512, 256);

	private bool showWindow = false; //to determine whether the player wants to see the window or not 
	
	private Vector2 scrollPos = new Vector2(0.5F, 0.5F);
	
	public GUIStyle timelineStyle;
	public GUIContent timelineContent;
	
	public GUIStyle boxStyle;
	public GUIStyle labelStyle;
		
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI () {
		
		GUILayout.Space(25);
		if (GUILayout.Button("Knowledge Timeline", GUILayout.Height(25))) showWindow = !showWindow;
		
		if (showWindow) GUILayout.Window(1, windowRect, TimelineFunction, timelineContent, timelineStyle);

	}
	
	void TimelineFunction (int windowID) {
		// Draw any Controls inside the window here
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Suspects", labelStyle, GUILayout.Height(50));
			GUILayout.Label("1200 hrs", labelStyle, GUILayout.Width(120));
			GUILayout.Label("1300 hrs", labelStyle);
			GUILayout.Label("1400 hrs", labelStyle);
			GUILayout.Label("1500 hrs", labelStyle);
			GUILayout.Label("1600 hrs", labelStyle);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Wife", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			GUI.backgroundColor = Color.red;
			GUILayout.Box("info related to wife at 1200", boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			GUILayout.Space(480);	
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Son", labelStyle, GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Space(240);
			GUI.backgroundColor = Color.yellow;
			GUILayout.Box("info related to son at 1400", boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			GUILayout.Space(240);
		GUILayout.EndHorizontal();
		
		GUILayout.Label("Daughter", labelStyle, GUILayout.Height(50));
		GUILayout.Label("Maid", labelStyle, GUILayout.Height(50));
		
		GUILayout.EndScrollView();
	}

}
