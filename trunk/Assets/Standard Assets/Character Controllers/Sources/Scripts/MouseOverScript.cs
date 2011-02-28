using UnityEngine;
using System.Collections;


public class MouseOverScript : MonoBehaviour {
	
	public Transform targetObject;
	Color mouseOverColor = Color.yellow;
	bool isSelected = false;
	bool withinBoundary = false;
	bool actionKey = false;
	Color[] originalColor; //array to store the original material color
	Vector3 offset = Vector3.up;
	Vector3 screenPos;
	
	// Use this for initialization
	void Start () {
		originalColor = new Color[targetObject.renderer.materials.Length];
		for(int i = 0; i < targetObject.renderer.materials.Length; i++){
			originalColor[i] = targetObject.renderer.materials[i].color;
		}
	}
	
	void OnMouseEnter(){
		//changing all the material to be yellow upon mouse hover, once the player is close to the object
		if(withinBoundary){
			for(int i = 0; i < targetObject.renderer.materials.Length; i++){
				targetObject.renderer.materials[i].color = mouseOverColor;
			}
		}
	}
	
	void OnMouseExit(){
		//if user did not select the object
		if(isSelected == false){
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
	
	void OnGUI() {
		if(isSelected == true && withinBoundary == true && actionKey == true){
			GUI.Box(new Rect(screenPos.x, (Screen.height - screenPos.y), 100, 100), "Hello World!");
		}
	}
		

	
	// Update is called once per frame
	void Update () {
		screenPos = Camera.main.WorldToScreenPoint(targetObject.position + offset);
		
		if(Input.GetMouseButtonDown(0)){
			
			if(withinBoundary == true){
				actionKey = !actionKey;
				
				if(actionKey){
					isSelected = true;
				}
				else{
					isSelected = false;
				}
			}
		}
		
		if(!(isSelected == true && withinBoundary == true)){
			actionKey = false;
			isSelected = false;
		}
	}
}
