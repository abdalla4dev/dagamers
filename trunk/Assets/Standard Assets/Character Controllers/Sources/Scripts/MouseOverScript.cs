using UnityEngine;
using System.Collections;

public class MouseOverScript : MonoBehaviour {
	
	Color mouseOverColor = Color.yellow;
	bool isSelected = false;
	bool withinBoundary = false;
	//array to store the original material color
	Color[] originalColor;
	
	// Use this for initialization
	void Start () {
		originalColor = new Color[renderer.materials.Length];
		for(int i = 0; i < renderer.materials.Length; i++){
			originalColor[i] = renderer.materials[i].color;
		}
	}
	
	void OnMouseEnter(){
		//changing all the material to be yellow upon mouse hover
		for(int i = 0; i < renderer.materials.Length; i++){
			renderer.materials[i].color = mouseOverColor;
		}
	}
	
	void OnMouseDown(){
		//to toggle the selection mode
		if(isSelected){
				isSelected = false;
		}
		else{
			isSelected = true;
		}
	}
	
	void OnMouseExit(){
		//if user did not select the object
		if(isSelected == false){
			//changes back to the original material upon mouse exit
			for(int i = 0; i < renderer.materials.Length; i++){
				renderer.materials[i].color = originalColor[i];
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		withinBoundary = true;
	}
	
	void OnCollisionExit(Collision collision){
		withinBoundary = false;
	}
	
	void OnGUI() {
		if(isSelected == true && withinBoundary == true){
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a title");
		}
	}
		

	
	// Update is called once per frame
	void Update () {
	}
}
