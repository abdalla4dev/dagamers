using UnityEngine;
using System;
using System.Collections;
using MurderData;

public class MouseOverScript : MonoBehaviour {
	
	public Transform targetObject;
	public TreeNode tree;
	public GUISkin customSkin;

	
	Color mouseOverColor = Color.yellow;
	bool displayText = false;
	bool withinBoundary = false;
	bool actionKey = false;
	bool overObject = false;
	
	Color[] originalColor; //array to store the original material color
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	Vector3 charOffset = new Vector3(0,2,0);
	
	public AI AIlink;
	bool called = true;
	string s;
	string ans;
	string suspect, weapon;
				
	// Use this for initialization
	void Start () {
		//AIlink = gameObject.GetComponent("AI") as AI;
		suspect = targetObject.name;
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			originalColor = new Color[targetObject.renderer.materials.Length];
			weapon = targetObject.name;
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
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
			called = true;
		}
		
	}
	
	void OnGUI() {
		GUI.skin = customSkin;

		if(displayText){
			if(called){
			GUILayout.Window(7, new Rect(screenPos.x, (Screen.height - screenPos.y), 300,100), QuestionWindow, "Detective"); //window ID is 2 coz Timeline and map are using window IDs too
			}
			
			if(called == false){
				if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
					GUILayout.Window(6, new Rect(screenPos.x, (Screen.height - screenPos.y),300,100), AnswerWindow, "" + weapon);
					//print("weapon name = " + weapon);
				}
				else{
					GUILayout.Window(6, new Rect(screenPos.x, (Screen.height - screenPos.y)+150,300,100), AnswerWindow, "" + suspect);
				}
			}
		}
	}

	void QuestionWindow(int windowID) {
		
		//ArrayList weaponList = AI.weaponTriggered((int)Enum.Parse(typeof(Weapons), weapon));
		
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			called = false;
		}
		
		else{
			ArrayList myList = AI.HumanTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect));
			
			foreach (string item in myList) {
				if (GUILayout.Button(item.Substring(0,1) + (item.Replace('_', ' ')).Substring(1).ToLower())) {
					//print("clicked");
					s = item;
					ans = AI.ClickingTriggered((int)Enum.Parse(typeof(SuspectEnum), suspect), s);
					ans = ans.Replace('_', ' ');
					ans = ans.Substring(0,1) + ans.Substring(1).ToLower();
					ans = ans.Replace(" i ", " I ");
					called = false;
				}
			}
		}
	}		

	void AnswerWindow(int windowID) {
		if(called == false){
			if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
				GUILayout.Box(ans);
				called = true;
			}
			//print("answering");
			else{
				if (GUILayout.Button(ans)) {
					called = true;	
				}
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
