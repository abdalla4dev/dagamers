using UnityEngine;
using System.Collections;
using System;
using MurderData;

public class KnowledgeMapUIv2 : MonoBehaviour {
	
	private bool showMap = false;
	private Rect windowRect = new Rect (20, 60, 600, 600);
	//enum the possible rooms and coordinates in the map
	
	//need custom texture for scissors, spanner, towel, knife, screwdriver
	private bool showMW = false;
	
	public Texture scissorsTexture;
	public Texture spannerTexture;
	public Texture towelTexture;
	public Texture knifeTexture;
	public Texture screwdriverTexture;
	
	//coords for kitchen, living room, bedroom, garden, toilet
	public GenerateTimeline theTimeline;
	private Texture kitchenTexture;
	private Texture LRTexture;
	private Texture bedroomTexture;
	private Texture gardenTexture;
	private Texture toiletTexture;
	
	//public Texture mapTexture;
	
	public GUIContent mapContent;
	public GUIStyle mapStyle;
	
	// Use this for initialization
	void Start () {
		theTimeline = gameObject.GetComponent<GenerateTimeline>();
		switch(GenerateTimeline.knifeLoc) {
			case Rooms.Bedroom:		bedroomTexture=knifeTexture; break;
			case Rooms.Garden:		gardenTexture=knifeTexture; break;
			case Rooms.Kitchen:		kitchenTexture=knifeTexture; break;
			case Rooms.Living_Room:	LRTexture=knifeTexture; break;
			case Rooms.Toilet:		toiletTexture=knifeTexture; break;
			default: break;
		}
		switch(GenerateTimeline.screwdriverLoc) {
			case Rooms.Bedroom:		bedroomTexture=screwdriverTexture; break;
			case Rooms.Garden:		gardenTexture=screwdriverTexture; break;
			case Rooms.Kitchen:		kitchenTexture=screwdriverTexture; break;
			case Rooms.Living_Room:	LRTexture=screwdriverTexture; break;
			case Rooms.Toilet:		toiletTexture=screwdriverTexture; break;
			default: break;
		}
		switch(GenerateTimeline.towelLoc) {
			case Rooms.Bedroom:		bedroomTexture=towelTexture; break;
			case Rooms.Garden:		gardenTexture=towelTexture; break;
			case Rooms.Kitchen:		kitchenTexture=towelTexture; break;
			case Rooms.Living_Room:	LRTexture=towelTexture; break;
			case Rooms.Toilet:		toiletTexture=towelTexture; break;
			default: break;
		}
		switch(GenerateTimeline.scissorsLoc) {
			case Rooms.Bedroom:		bedroomTexture=scissorsTexture; break;
			case Rooms.Garden:		gardenTexture=scissorsTexture; break;
			case Rooms.Kitchen:		kitchenTexture=scissorsTexture; break;
			case Rooms.Living_Room:	LRTexture=scissorsTexture; break;
			case Rooms.Toilet:		toiletTexture=scissorsTexture; break;
			default: break;
		}
		switch(GenerateTimeline.spannerLoc) {
			case Rooms.Bedroom:		bedroomTexture=spannerTexture; break;
			case Rooms.Garden:		gardenTexture=spannerTexture; break;
			case Rooms.Kitchen:		kitchenTexture=spannerTexture; break;
			case Rooms.Living_Room:	LRTexture=spannerTexture; break;
			case Rooms.Toilet:		toiletTexture=spannerTexture; break;
			default: break;
		}
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
		GUILayout.Space(150);
		
		GUILayout.BeginHorizontal();
			//living room, then kitchen
			GUILayout.Space(200);
			if (GUILayout.Button(LRTexture, GUILayout.Width(50), GUILayout.Height(50)) ) showMW = !showMW;
			GUILayout.Space(100);
			if (GUILayout.Button(kitchenTexture, GUILayout.Width(50), GUILayout.Height(50)) ) showMW = !showMW;
		GUILayout.EndHorizontal();
		
		GUILayout.Space(100);
		GUILayout.BeginHorizontal();
			//Bedroom
			GUILayout.Space(150);
			if (GUILayout.Button(bedroomTexture, GUILayout.Width(50), GUILayout.Height(50)) ) showMW = !showMW;
		GUILayout.EndHorizontal();
		
		GUILayout.Space(50);
		GUILayout.BeginHorizontal();
			//toilet
			GUILayout.Space(150);
			if (GUILayout.Button(toiletTexture, GUILayout.Width(50), GUILayout.Height(50)) ) showMW = !showMW;
		GUILayout.EndHorizontal();
		
		GUILayout.Space(50);
		GUILayout.BeginHorizontal();
			//garden
			GUILayout.Space(200);
			if (GUILayout.Button(gardenTexture, GUILayout.Width(50), GUILayout.Height(50)) ) showMW = !showMW;
		GUILayout.EndHorizontal();
		
		/*
		if(showMW) {
			GUILayout.Box("the belt was here");
		}*/
	}
}
