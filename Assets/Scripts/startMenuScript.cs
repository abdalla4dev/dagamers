using UnityEngine;
using System.Collections;
using MurderData;

public class startMenuScript : MonoBehaviour {
	
	public int buttonNumber;
	public GameObject gameObject;
	
	public GameObject easyButton;
	public GameObject mediumButton;
	public GameObject hardButton;
	
	public static GameDiffEnum diffLevel;
	bool diffToggle;
	// Use this for initialization
	void Start () {
		diffToggle = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver () {
		if(gameObject.name == "easyButton" || gameObject.name == "mediumButton" || gameObject.name == "hardButton"){
			iTween.RotateTo(gameObject,iTween.Hash("z", 378, "time", 0.5));
		}
		else{
			iTween.RotateTo(gameObject,iTween.Hash("z", 388, "time", 0.5));
		}
	}
	
	void OnMouseExit () {
		if(gameObject.name == "easyButton" || gameObject.name == "mediumButton" || gameObject.name == "hardButton"){
			iTween.RotateTo(gameObject,iTween.Hash("z", 348, "time", 0.5));
		}
		else{
			iTween.RotateTo(gameObject,iTween.Hash("z", 348, "time", 0.5));
		}
	}
	
	void OnMouseDown () {
		if(buttonNumber == 1){ // Start
			Application.LoadLevel("LoadingScreen"); 
		}
		else if (buttonNumber == 2){ // Difficulty
			diffToggle = !diffToggle;
			if(diffToggle){
				iTween.MoveBy(easyButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(mediumButton, iTween.Hash("x", -5.3, "time", 0.5));
				iTween.MoveBy(hardButton, iTween.Hash("x", -5.3, "time", 0.5));
			}
			else{
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
