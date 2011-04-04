using UnityEngine;
using System.Collections;

public class InteractiveTrigger : MonoBehaviour {
	
	public Transform targetObject;
	public Texture2D questionTex;
	public Texture2D questionBoxTex;
	public Texture2D logTex;
	public Texture2D logBoxTex;
	public GUISkin customSkin;
	
	Color mouseOverColor = Color.yellow;
	Color[] originalColor; //array to store the original material color
	
	bool displayText = false;
	bool withinBoundary = false;
	bool actionKey = false;
	bool overObject = false;
	bool questionToggle = true;
	
	string suspect, weapon;
	
	// Use this for initialization
	void Start () {
		if(targetObject.name == "Knife" || targetObject.name == "Scissors" || targetObject.name == "Spanner" || targetObject.name == "Screwdriver" || targetObject.name == "Towel"){
			originalColor = new Color[targetObject.renderer.materials.Length];
			weapon = targetObject.name;
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
		}
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		if (displayText) {
			
			if(questionToggle){
				//question window
				GUI.Window(4, new Rect((Screen.width - 512), (Screen.height - 256), 512, 256), dialogueWindow, questionBoxTex);
			}
			else{
				//log window
				GUI.Window(5, new Rect((Screen.width - 512), (Screen.height - 256), 512, 256), dialogueWindow, logBoxTex);
			}
		}

	}
	
	void dialogueWindow(int windowID){
		if(GUI.Button(new Rect(30, 8, 256, 32), questionTex)){
			GUI.BringWindowToBack(5);
			questionToggle = true;
		}
		else if(GUI.Button(new Rect(265, 8, 256, 32), logTex)){
			GUI.BringWindowToBack(4);
			questionToggle = false;
		}
		
		if(questionToggle){
			GUI.Button(new Rect(40, 50, 450, 32), "Were you the one who found the body?");
			GUI.Button(new Rect(40, 90, 450, 32), "Where were you during the time of murder?");
			GUI.Button(new Rect(40, 130, 450, 32), "What were you doing before the time of murder?");
			
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
