using UnityEngine;
using System.Collections;
using MurderData;

public class startMenuScript : MonoBehaviour {
	
	public int buttonNumber;
	public GameObject gameObj;
	
	public GameObject easyButton;
	public GameObject mediumButton;
	public GameObject hardButton;
	
	Material originalColor;
	public Material mouseOverMat;
	
	public static GameDiffEnum diffLevel;
	bool diffToggle;
	bool isClicked;
	// Use this for initialization
	void Start () {
		diffToggle = false;
		isClicked = false;
		//if(gameObj.name == "easyButton" || gameObj.name == "mediumButton" || gameObj.name == "hardButton"){
			//originalColor = gameObj.renderer.material.color;
		//}
		originalColor = gameObj.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver () {
		
//		if(gameObj.name == "easyButton" || gameObj.name == "mediumButton" || gameObj.name == "hardButton"){
//			iTween.RotateTo(gameObj,iTween.Hash("z", 378, "time", 0.5));
//		}
//		else{
//			iTween.RotateTo(gameObj,iTween.Hash("z", 388, "time", 0.5));
//		}
		gameObj.renderer.material = mouseOverMat;
	}
	
	void OnMouseExit () {
		
//		if(gameObj.name == "easyButton" || gameObj.name == "mediumButton" || gameObj.name == "hardButton"){
//			iTween.RotateTo(gameObj,iTween.Hash("z", 348, "time", 0.5));
//		}
//		else{
//			iTween.RotateTo(gameObj,iTween.Hash("z", 348, "time", 0.5));
//		}
		if(!isClicked){
			gameObj.renderer.material = originalColor;
		}
	}
	
	void OnMouseDown () {
		if(buttonNumber == 1){ // Start
			Application.LoadLevel("LoadingScreen"); 
			//isClicked = !isClicked;
		}
		else if (buttonNumber == 2){ // Difficulty
			diffToggle = !diffToggle;
			isClicked = !isClicked;
			if(diffToggle){
				gameObj.renderer.material = mouseOverMat;
				iTween.MoveBy(easyButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(mediumButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(hardButton, iTween.Hash("x", -5.3, "time", 0.5));
			}
			else{
				gameObj.renderer.material = originalColor;
				iTween.MoveBy(easyButton, iTween.Hash("x", 5.3, "time", 0.5));
				iTween.MoveBy(mediumButton, iTween.Hash("x", 5.3, "time", 0.5));
				iTween.MoveBy(hardButton, iTween.Hash("x", 5.3, "time", 0.5));
			}
		}
		else if(buttonNumber == 4){
			diffLevel = MurderData.GameDiffEnum.Easy;
			Application.LoadLevel("LoadingScreen");
		}
		else if(buttonNumber == 5){
			diffLevel = MurderData.GameDiffEnum.Medium;
			Application.LoadLevel("LoadingScreen");
		}
		else if(buttonNumber == 6){
			diffLevel = MurderData.GameDiffEnum.Hard;
			Application.LoadLevel("LoadingScreen");
		}
		else { // 3 = quit
			var isWebPlayer = (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer);
			if (!isWebPlayer)
			{
				Application.Quit();
				Debug.Log("Quit");	
			}
		}
	}
	
}
