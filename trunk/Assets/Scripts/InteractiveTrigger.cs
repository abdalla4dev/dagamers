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
	
	public GUIStyle answerStyle;
	public GUIStyle askingStyle;
	public GUIStyle MouseLabelStyle;
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
	bool isFact;
	bool spoken = false;
	bool isCCTVcalled = false;
	static string weaponStr; // to store the string
	
	Vector3 mousePos;
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	Vector3 charOffset = new Vector3(0,1.2f,0); 
	
	Vector2 scrollPosition = new Vector2(0, -5);
	
	string suspect, weapon, s, ans, fact, suspectName;
	
	int weaponEnum, factEnum; 
	int numQn = 1;
	
	float qnButtonTop = 20;
	float mouseX, mouseY;
	
	
	
	// Use this for initialization
	void Start () {
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			originalColor = new Color[targetObject.renderer.materials.Length];
			weapon = targetObject.name;
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
		}
		else if(targetObject.name == "CCTV"){
			isFact = true;
			weapon = targetObject.name;
			fact = targetObject.name;
		}
		else{
			suspect = targetObject.name;
		}
		
		windowTexture = questionBoxTex;
	}
	
	void nameGenerator(){
		if(targetObject.name == "Daughter"){
			suspectName = "Jane";
		}
		else if(targetObject.name == "Wife"){
			suspectName = "Mrs Darcy";
		}
		else if(targetObject.name == "Son"){
			suspectName = "Wayne";
		}
		else if(targetObject.name == "Maid"){
			suspectName = "Maria";
		}
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		if(overObject){
			//GUI.Label(new Rect(mousePos.x, mouseY, 50, 30), targetObject.name, MouseLabelStyle); 
			if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "CCTV"){
				GUI.Label(new Rect(screenPos.x - 50, (Screen.height - screenPos.y) + 100,150,30), targetObject.name, MouseLabelStyle);
			}
			else if(targetObject.name == "Towel"){
				GUI.Label(new Rect(screenPos.x - 50, (Screen.height - screenPos.y + 300),150,30), targetObject.name, MouseLabelStyle);
			}
			else{
				nameGenerator();
				GUI.Label(new Rect((screenPos.x - 50), (Screen.height - screenPos.y)+ 100,150,30), suspectName, MouseLabelStyle);
			}
		}
		
		if (displayText) {
			// for weapons only and CCTV
			if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel" || targetObject.name == "CCTV"){
				//GUILayout.Window(3, new Rect(screenPos.x, (Screen.height - screenPos.y),300,100), weaponWindow, "" + weapon);
				GUILayout.Window(3, new Rect(Screen.width - 420, 0, 262, 120), weaponWindow, "asd " + weapon, GUILayout.Width(262));
			}
			else{
				
				if(GUI.Button(new Rect((Screen.width - 435), (Screen.height - 198), 210, 35), questionTex)){
					questionToggle = true;
				}
				else if(GUI.Button(new Rect((Screen.width - 190), (Screen.height - 198), 210, 35), logTex)){
					questionToggle = false;
				}
				
				//question window
				GUILayout.Window(10, new Rect((Screen.width - 439), (Screen.height - 148), 439, 168), doWindow, windowTexture, GUILayout.Width(439), GUILayout.Height(168));
				
				//log window
				GUILayout.Window(11, new Rect((Screen.width - 439), (Screen.height - 148), 439, 168), logWindow, logBoxTex, GUILayout.Width(439), GUILayout.Height(168));
				
				if(callAns){
					//GUI.Box(new Rect(screenPos.x, (Screen.height - screenPos.y)+150,300,100), ans);
					GUI.Box(new Rect(Screen.width - 420, 0, 262, 100), ans, answerStyle);
					GUI.Box(new Rect(Screen.width - 512, 450, 512, 64), s, askingStyle);
				}
			}
		}
		
		if(Input.GetMouseButtonDown(0)){
			isCCTVcalled = false;
			if(withinBoundary == true && actionKey == false && overObject == true){ // Colin commented out overObject == true to test
				actionKey = true;
				displayText = true;
				spoken = false;
			}	
		}
	}
	
	void weaponWindow(int windowID){
		System.Random rand = new System.Random();
		
		if(isFact){
			//factEnum = ((int)Enum.Parse(typeof(WpnEnum), suspect));
			if(!isCCTVcalled){
				weaponStr = GenerateTimeline.facts[rand.Next(0, GenerateTimeline.facts.Count)].revealInfo("CCTV");
				weaponStr = weaponStr.Replace('_', ' ');
				weaponStr += " (Click again for another fact)";
			}
			isCCTVcalled = true;
			GUILayout.Box(weaponStr, GUILayout.Width(260), GUILayout.Height(100));
			
		}
		else{
			weaponEnum = ((int)Enum.Parse(typeof(WpnEnum), weapon));
			weaponStr = GenerateTimeline.wpnFacts[weaponEnum].revealInfo("weapon");
			GUILayout.Box(weaponStr, GUILayout.Width(260), GUILayout.Height(100));
		}
		
		// VoiceSpeaker
		if(!spoken){
			VoiceSpeaker.Talk(weaponStr);
			spoken = true;	
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
				/*if (logText[i][0] != "temp") {*/
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
				//}
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
			VoiceSpeaker.stopTalk();
		}
	}
	
	void OnMouseEnter(){
		overObject = true;
		
		mousePos = Input.mousePosition;
		//mouseX = Math.Abs(mousePos.x - Screen.width);
		mouseY = Math.Abs(mousePos.y - Screen.height);
		
		 
		
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
	
	void OnMouseDown(){

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
	
		
		if(!(withinBoundary)){
			actionKey = false;
		}
	}
}
