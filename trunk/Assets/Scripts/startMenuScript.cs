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
	public static bool lvlToggleEasy, lvlToggleMed, lvlToggleHard, easyClick, medClick, hardClick;
	
	// Use this for initialization
	void Start () {
		diffToggle = false;
		isClicked = false;
		easyClick = false;
		medClick = false;
		hardClick = false;
		
		//to store the original material
		originalColor = gameObj.renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver () {	
		gameObj.renderer.material = mouseOverMat;
	}
	
	void OnMouseExit () {
		//if the button is not selected, upon mouse exit, the material should change back to normal
		if(!isClicked && gameObj.name == "diffButton"){
			gameObj.renderer.material = originalColor;
		}
		
		if(!easyClick && gameObj.name == "EasyButton"){
			gameObj.renderer.material = originalColor;
		}
		
		if(!medClick && gameObj.name == "mediumButton"){
			gameObj.renderer.material = originalColor;
		}
		
		if(!hardClick && gameObj.name == "HardButton"){
			gameObj.renderer.material = originalColor;
		}
		
		if(gameObj.name == "startButton" || gameObj.name == "quitButton"){
			gameObj.renderer.material = originalColor;
		}
		
	}
	
	void OnMouseDown () {
		if(buttonNumber == 1){ // Start
			Application.LoadLevel("LoadingScreen"); 
		}
		else if (buttonNumber == 2){ // Difficulty
			isClicked = !isClicked;
			//reveals the buttons for difficulty level
			if(isClicked){
				gameObj.renderer.material = mouseOverMat;
				iTween.MoveBy(easyButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(mediumButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(hardButton, iTween.Hash("x", -5.3, "time", 0.5));
			}
			//moves the level buttons away from the screen space
			else{
				gameObj.renderer.material = originalColor;
				iTween.MoveBy(easyButton, iTween.Hash("x", 5.3, "time", 0.5));
				iTween.MoveBy(mediumButton, iTween.Hash("x", 5.3, "time", 0.5));
				iTween.MoveBy(hardButton, iTween.Hash("x", 5.3, "time", 0.5));
				easyClick = false;
				medClick = false;
				hardClick = false;
			}
		}
		//Easy Button
		else if(buttonNumber == 4){
			easyClick = !easyClick;
			
			if(easyClick){
				gameObj.renderer.material = mouseOverMat;
				mediumButton.renderer.material = originalColor;
				hardButton.renderer.material = originalColor;
				medClick = false;
				hardClick = false;
			}
			else{
				gameObj.renderer.material = originalColor;
			}
			
			diffLevel = MurderData.GameDiffEnum.Easy;

		}
		//Medium Button
		else if(buttonNumber == 5){
			medClick = !medClick;
			
			if(medClick){
				gameObj.renderer.material = mouseOverMat;
				easyButton.renderer.material = originalColor;
				hardButton.renderer.material = originalColor;
				easyClick = false;
				hardClick = false;
			}
			else{
				gameObj.renderer.material = originalColor;
			}
			
			diffLevel = MurderData.GameDiffEnum.Medium;

		}
		//Hard Button
		else if(buttonNumber == 6){
			hardClick = !hardClick;
			
			if(hardClick){
				gameObj.renderer.material = mouseOverMat;
				mediumButton.renderer.material = originalColor;
				easyButton.renderer.material = originalColor;
				easyClick = false;
				medClick = false;
			}
			else{
				gameObj.renderer.material = originalColor;
			}
			
			diffLevel = MurderData.GameDiffEnum.Hard;

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
