using UnityEngine;
using System.Collections;

public class KnowledgeTimelineUI : MonoBehaviour {
	
	private float hsbValue = 5.0F;
	private Rect windowRect = new Rect (20, 20, 600, 300);

	private bool showWindow = false; //to determine whether the player wants to see the window or not 
	
	public GUIStyle windowStyle;
	public GUIContent windowContent;
	
	public GUIStyle boxStyle;
		
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI () {
		
		//how to apply GUI style???
		//hsbValue = GUI.HorizontalScrollbar(new Rect(100, 100, 400, 400), hsbValue, 1.0F, 0.0F, 10.0F);
		//windowRect = GUI.Window (0, windowRect, WindowFunction, windowContent);
		
		GUILayout.Space(25);
		if (GUILayout.Button("Knowledge Timeline", GUILayout.Height(25))) showWindow = !showWindow;
		
		if (showWindow) GUILayout.Window(0, windowRect, WindowFunction, "Timeline");

	}
	
	void WindowFunction (int windowID) {
		// Draw any Controls inside the window here
		
		//GUILayout.BeginArea (new Rect(40,40,500,250));

		GUILayout.BeginHorizontal();
			GUILayout.Label("Suspects", GUILayout.Height(50));
			GUILayout.Label("1200 hrs", GUILayout.Width(120));
			GUILayout.Label("1300 hrs");
			GUILayout.Label("1400 hrs");
			GUILayout.Label("1500 hrs");
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Wife", GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Box("info related to wife at 1200", boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			GUILayout.Space(360);	
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			GUILayout.Label("Son", GUILayout.Height(50), GUILayout.Width(120));
			GUILayout.Space(240);
			GUILayout.Box("info related to son at 1400", boxStyle, GUILayout.Width(120), GUILayout.Height(50));
			GUILayout.Space(120);
		GUILayout.EndHorizontal();
		
		GUILayout.Label("Daughter", GUILayout.Height(50));
		GUILayout.Label("Maid", GUILayout.Height(50));
		
		
		//GUILayout.Label("Test");
		
		//hsbValue = GUI.HorizontalScrollbar(new Rect(20, 20, 600, 300), hsbValue, 1.0F, 0.0F, 24.0F);
		//GUILayout.EndArea();
	}

}
