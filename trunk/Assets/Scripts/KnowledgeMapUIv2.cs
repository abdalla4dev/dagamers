using UnityEngine;
using System.Collections;

public class KnowledgeMapUIv2 : MonoBehaviour {
	
	private bool showMap = false;
	private Rect windowRect = new Rect (20, 60, 600, 600);
	//enum the possible rooms and coordinates in the map
	
	//need custom texture for belt, knife, pipe, shovel, tools
	private bool showMW = false;
	
	public Texture beltTexture;
	
	public Texture mapTexture;
	
	public GUIContent mapContent;
	public GUIStyle mapStyle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if (GUILayout.Button("Knowledge Map", GUILayout.Height(25))) showMap = !showMap;
		
		if (showMap) {
			//draw the floor plan texture
			GUILayout.Window(0, windowRect, MapFunction, mapContent, mapStyle);
		}
	}
	
	void MapFunction (int windowID) {
		//draw the murder weapons texture as buttons
		if (GUILayout.Button(beltTexture, GUILayout.Width(100), GUILayout.Height(100)) ) showMW = !showMW;
		
		if(showMW) {
			GUILayout.Box("the belt was here");
		}
	}
}
