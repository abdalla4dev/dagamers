using UnityEngine;
using System;
using System.Collections;
using MurderData;
using System.Collections.Generic;

public class InteractiveTrigger : MonoBehaviour {
	
	public Transform targetObject;
	public Texture2D questionTex;
	public Texture2D questionBoxTex;
	public Texture2D logTex;
	public Texture2D logBoxTex;
	public GUISkin customSkin;
	
	public GUIStyle questionStyle;
	//public AI AIlink;
	
	Texture2D windowTexture;
	
	Color mouseOverColor = Color.yellow;
	Color[] originalColor; //array to store the original material color
	
	bool displayText = false;
	bool withinBoundary = false;
	bool actionKey = false;
	bool overObject = false;
	bool questionToggle = true;
	bool callAns = false;
	bool isWeapon;
	
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	Vector3 charOffset = new Vector3(0,1.2f,0); 
	
	Vector2 scrollPosition = new Vector2(0, -5);
	
	string suspect, weapon, s, ans;
	
	int weaponEnum; 
	int numQn = 1;
	
	float qnButtonTop = 20;
	
	
	// Use this for initialization
	void Start () {
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			originalColor = new Color[targetObject.renderer.materials.Length];
			weapon = targetObject.name;
			isWeapon = true;
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
		}
		else{
			suspect = targetObject.name;
			isWeapon = false;
		}
		
		windowTexture = questionBoxTex;
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		if (displayText) {
			if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
				//GUILayout.Window(3, new Rect(screenPos.x, (Screen.height - screenPos.y),300,100), weaponWindow, "" + weapon);
				GUILayout.Window(3, new Rect(Screen.width - 270, 100, 262, 145), weaponWindow, "" + weapon, GUILayout.Width(262), GUILayout.Height(145));
			}
			else{
				
				if(GUI.Button(new Rect((Screen.width - 435), (Screen.height - 198), 210, 35), questionTex)){
					questionToggle = true;
				}
				else if(GUI.Button(new Rect((Screen.width - 190), (Screen.height - 198), 210, 35), logTex)){
					questionToggle = false;
				}
				
				//if(questionToggle){
					//question window
					GUILayout.Window(10, new Rect((Screen.width - 439), (Screen.height - 148), 439, 168), doWindow, windowTexture, GUILayout.Width(439), GUILayout.Height(168));
					//GUILayout.Window(10, new Rect((Screen.width - 439), (Screen.height - 167), 439, 167), doWindow, windowTexture, questionStyle, GUILayout(439), GUILayout(167));
				//}
				//else{
					//log window
					GUILayout.Window(11, new Rect((Screen.width - 439), (Screen.height - 148), 439, 168), logWindow, logBoxTex, GUILayout.Width(439), GUILayout.Height(168));
				//}
				if(callAns){
					GUI.Box(new Rect(screenPos.x, (Screen.height - screenPos.y)+150,300,100), ans);
				}
			}
		}

	}
	
	void weaponWindow(int windowID){
		if(isWeapon){
			weaponEnum = ((int)Enum.Parse(typeof(WpnEnum), weapon));
			GUILayout.Box(GenerateTimeline.wpnFacts[weaponEnum].revealInfo("weapon"), GUILayout.Width(260), GUILayout.Height(160));
		}
		else{
			weaponEnum = ((int)Enum.Parse(typeof(WpnEnum), suspect));
			GUILayout.Box(GenerateTimeline.wpnFacts[weaponEnum].revealInfo("CCTV"));
		}
	}
	
	void doWindow(int windowID){
		//if(GUI.Button(new Rect(30, 8, 256, 32), questionTex)){
			//questionToggle = true;
		if(questionToggle){
			windowTexture = questionBoxTex;
			ArrayList myList = AI.HumanTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(125), GUILayout.Width(380));
			//scrollPosition = GUI.BeginScrollView(new Rect((Screen.width - 409), (Screen.height - 188), 379, 160), scrollPosition, new Rect(0,0, 409, 300));
			foreach (string item in myList) {
				//if (GUI.Button(new Rect(30, (qnButtonTop * numQn), 450, 20), (item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower()))) {
				if (item != "temp") {
					if (GUILayout.Button(item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower())) {
						s = item;
						ans = AI.ClickingTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect), s);
						ans = ans.Replace('_', ' ');
						//ans = ans.Substring(0,1) + ans.Substring(1).ToLower();
						ans = ans.Replace(" i ", " I ");
						callAns = true;
					}
				}
			}
			GUILayout.EndScrollView();
		}
	}

	void logWindow(int windowID){
		if(!questionToggle){
			
			List<string[]> logText = AI.logTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			String text;
			
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(125), GUILayout.Width(380));
			for(int i = 0; i < logText.Count; i++){
				
				//the questions in log
				GUI.contentColor = new Color(1.0F, 0.6F, 0.0F);	
				String ansTemp = logText[i][0];
				ansTemp = ansTemp.Replace('_', ' ');
				ansTemp = ansTemp.Substring(0,1) + ansTemp.Substring(1).ToLower();
				ansTemp = ansTemp.Replace(" i ", " I ");
				GUILayout.Label("Q: " + ansTemp);
				
				//the answers in log
				GUI.contentColor = Color.white;
				String questionTemp = logText[i][1];
				questionTemp = questionTemp.Replace('_', ' ');
				questionTemp = questionTemp.Substring(0,1) + questionTemp.Substring(1).ToLower();
				questionTemp = questionTemp.Replace(" i ", " I ");
				GUILayout.Label("A: " + questionTemp);
			}
			GUILayout.EndScrollView();
			callAns = false;
		}
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
	
	//upon movement, the gui text box will disappear
	void TriggerToggle(){
		if((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.D))){
			displayText = false;
			actionKey = false;
			callAns = false;
		}
	}
	
	void OnMouseEnter(){
		overObject = true;
		//changing all the material to be yellow upon mouse hover, once the player is close to the object
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			//changing all the material to be yellow upon mouse hover, once the player is close to the object
			if(withinBoundary){
				for(int i = 0; i < targetObject.renderer.materials.Length; i++){
					targetObject.renderer.materials[i].color = mouseOverColor;
				}
			}
		}
	}
	
	void OnMouseExit(){
		overObject = false;
		//if user did not select the object
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			//if user did not select the object
				//changes back to the original material upon mouse exit
				for(int i = 0; i < targetObject.renderer.materials.Length; i++){
					targetObject.renderer.materials[i].color = originalColor[i];
				}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			screenPos = Camera.main.WorldToScreenPoint(targetObject.position + offset);
		}
		else{
			screenPos = Camera.main.WorldToScreenPoint(targetObject.position + charOffset);
		}
	
		TriggerToggle();
		
		if(Input.GetMouseButtonDown(0)){
			if(withinBoundary == true && actionKey == false && overObject == true){
				actionKey = true;
				displayText = true;
			}
		}
		
		if(!(withinBoundary)){
			actionKey = false;
		}
	}
}
