using UnityEngine;
using System.Collections;


public class MouseOverScript : MonoBehaviour {
	
	public Transform targetObject;
	public TreeNode tree;
	public char suspect = 'd'; // to integrate with treenode.cs

	
	Color mouseOverColor = Color.yellow;
	bool isSelected = false;
	bool withinBoundary = false;
	bool actionKey = false;
	Color[] originalColor; //array to store the original material color
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	public AI AIlink;
	bool called = true;
	string s;
	string ans;
				
	// Use this for initialization
	void Start () {
		//AIlink = gameObject.GetComponent("AI") as AI;
		if(targetObject.name == "knife1" || targetObject.name == "scissors" || targetObject.name == "spanner1" || targetObject.name == "screwdriver" || targetObject.name == "towel"){
			originalColor = new Color[targetObject.renderer.materials.Length];
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				originalColor[i] = targetObject.renderer.materials[i].color;
			}
		}
	}
	
	void OnMouseEnter(){
		//changing all the material to be yellow upon mouse hover, once the player is close to the object
		if(targetObject.name == "knife1" || targetObject.name == "scissors" || targetObject.name == "spanner1" || targetObject.name == "screwdriver" || targetObject.name == "towel"){
			//changing all the material to be yellow upon mouse hover, once the player is close to the object
			if(withinBoundary){
				for(int i = 0; i < targetObject.renderer.materials.Length; i++){
					targetObject.renderer.materials[i].color = mouseOverColor;
				}
			}
		}
	}
	
	void OnMouseExit(){
		//if user did not select the object
		if(targetObject.name == "knife1" || targetObject.name == "scissors" || targetObject.name == "spanner1" || targetObject.name == "screwdriver" || targetObject.name == "towel"){
			//if user did not select the object
			if(isSelected == false){
				//changes back to the original material upon mouse exit
				for(int i = 0; i < targetObject.renderer.materials.Length; i++){
					targetObject.renderer.materials[i].color = originalColor[i];
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		withinBoundary = true;
	}
	
	void OnTriggerExit(Collider other){
		withinBoundary = false;
	}
	
	void OnGUI() {
		
		//BUG is here
		//action key is toggled every Left click , so it cannot click anything in the windows
		if(withinBoundary == true && actionKey == true){
			
			
	//if (withinBoundary && Input.GetMouseButtonDown(0))	{	
			
			GUI.Window(2, new Rect(screenPos.x, (Screen.height - screenPos.y), 300,100), QuestionWindow, "Questions" ); //window ID is 2 coz Timeline and map are using window IDs too
	
		/*	if (GUI.Button(new Rect(screenPos.x, (Screen.height - screenPos.y), 300, 100), s) && called) {
				print("clicked");
				s = AIlink.tree.ClickingTriggered(suspect,s);
				called = false;
			}*/
			
			//GUILayout.Box(answer);

			//GUILayout.Box(s);
			GUI.Window(3, new Rect(screenPos.x, (Screen.height - screenPos.y)+150,300,100), AnswerWindow, "Answers");
			
		}
	}

	void QuestionWindow(int windowID) {

		ArrayList myList = AI.tree.HumanTriggered(suspect);

		
		ArrayList myList = AI.tree.HumanTriggered(suspect);
		if (called) {		
			foreach (string item in myList) {
				if (GUILayout.Button(item)) {
					print("clicked");
					ans = AI.tree.ClickingTriggered(suspect,s);
				//	called = false;

				}
			}
		}
	}		

	void AnswerWindow(int windowID) {
		if (GUILayout.Button(ans)) {
			called = true;	
		}	
	}
	
	// Update is called once per frame
	void Update () {
		screenPos = Camera.main.WorldToScreenPoint(targetObject.position + offset);
		
		if(Input.GetKeyDown(KeyCode.E)){
			
			if(withinBoundary == true){
				actionKey = !actionKey;
				
				//~ if(actionKey){
					//~ isSelected = true;
				//~ }
				//~ else{
					//~ isSelected = false;
				//~ }
			}
		}
		
		if(!(withinBoundary)){
			actionKey = false;
			//isSelected = false;
		}
	}
}
