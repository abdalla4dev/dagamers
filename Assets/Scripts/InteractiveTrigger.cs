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
	
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	Vector3 charOffset = new Vector3(0,2,0);
	
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
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
		}
		else{
			suspect = targetObject.name;
		}
		
		windowTexture = questionBoxTex;
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		if (displayText) {
			if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
				GUILayout.Window(3, new Rect(screenPos.x, (Screen.height - screenPos.y),300,100), weaponWindow, "" + weapon);
			}
			else{
				
				if(GUI.Button(new Rect((Screen.width - 405), (Screen.height - 225), 256, 32), questionTex)){
					questionToggle = true;
				}
				else if(GUI.Button(new Rect((Screen.width - 205), (Screen.height - 225), 256, 32), logTex)){
					questionToggle = false;
				}
				
				//if(questionToggle){
					//question window
					GUILayout.Window(10, new Rect((Screen.width - 439), (Screen.height - 198), 439, 198), doWindow, windowTexture, GUILayout.Width(439), GUILayout.Height(198));
				//}
				//else{
					//log window
					GUILayout.Window(11, new Rect((Screen.width - 439), (Screen.height - 198), 439, 198), logWindow, logBoxTex);
				//}
				if(callAns){
					GUI.Box(new Rect(screenPos.x, (Screen.height - screenPos.y)+150,300,100), ans);
				}
			}
		}

	}
	
	void weaponWindow(int windowID){
		weaponEnum = ((int)Enum.Parse(typeof(WpnEnum), weapon));
		GUILayout.Box(GenerateTimeline.wpnFacts[weaponEnum].revealInfo("weapon"));
	}
	
	void doWindow(int windowID){
		//if(GUI.Button(new Rect(30, 8, 256, 32), questionTex)){
			//questionToggle = true;
		if(questionToggle){
			windowTexture = questionBoxTex;
			ArrayList myList = AI.HumanTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(120), GUILayout.Width(365));
			//scrollPosition = GUI.BeginScrollView(new Rect((Screen.width - 409), (Screen.height - 188), 379, 160), scrollPosition, new Rect(0,0, 409, 300));
			foreach (string item in myList) {
				//if (GUI.Button(new Rect(30, (qnButtonTop * numQn), 450, 20), (item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower()))) {
				if (GUILayout.Button(item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower())) {
					s = item;
					ans = AI.ClickingTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect), s);
					ans = ans.Replace('_', ' ');
					ans = ans.Substring(0,1) + ans.Substring(1).ToLower();
					ans = ans.Replace(" i ", " I ");
					callAns = true;
				}
			}
			//GUI.EndScrollView();
			GUILayout.EndScrollView();
		}
		//}
		//else if(GUI.Button(new Rect(265, 8, 256, 32), logTex)){
			//questionToggle = true;
//		else{
//			windowTexture = logBoxTex;
//		}
		//}
	}
	
	void dialogueWindow(int windowID){
		ArrayList myList = AI.HumanTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
		if(GUI.Button(new Rect(30, 8, 256, 20), questionTex)){
			print ("Got a click in window " + windowID);
			//GUI.BringWindowToFront(5);
			//ArrayList myList = AI.HumanTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			
			if(GUI.Button(new Rect(30, 40, 450, 40), "I am a button")){}
//			foreach (string item in myList) {
//				numQn++;
//				if (GUI.Button(new Rect(30, (qnButtonTop * numQn), 450, 40), (item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower()))) {
//					s = item;
//					ans = AI.ClickingTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect), s);
//					ans = ans.Replace('_', ' ');
//					ans = ans.Substring(0,1) + ans.Substring(1).ToLower();
//					ans = ans.Replace(" i ", " I ");
//					numQn--;
//					GUI.Box(new Rect(screenPos.x, (Screen.height - screenPos.y)+150, 300, 100), ans);
//				}
//			}
			questionToggle = true;
		}
		else if(GUI.Button(new Rect(265, 8, 256, 32), logTex)){
			print ("Got a click in window " + windowID);
			questionToggle = false;
		}
	}
	
	void logWindow(int windowID){
		if(!questionToggle){
			
			List<string[]> logText = AI.logTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			String text;
			
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(120), GUILayout.Width(365));
			//GUILayout.Label("Sorry, still under construction!");
			for(int i = 0; i < logText.Count; i++){
				GUI.contentColor = new Color(1.0F, 0.6F, 0.0F);	
				String ansTemp = logText[i][0];
				ansTemp = ansTemp.Replace('_', ' ');
				ansTemp = ansTemp.Substring(0,1) + ansTemp.Substring(1).ToLower();
				ansTemp = ansTemp.Replace(" i ", " I ");
				GUILayout.Label("Q: " + ansTemp);
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
//			called = true;
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
