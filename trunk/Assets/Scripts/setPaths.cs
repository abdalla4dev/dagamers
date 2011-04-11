using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class setPaths : MonoBehaviour {
	public GameObject theSon;
	public GameObject theDaughter;
	public GameObject theWife;
	public GameObject theMaid;
	public static string theSonPath;
	public static string theDaughterPath;
	public static string theWifePath;
	public static string theMaidPath;
	private List<int> mylist = new List<int>();
	int theTime;
		
	// Use this for initialization
	void Start () {
		int randPath = -1;
		int temp = randPath;
		System.Random rand = new System.Random();
		
		// SON
		randPath = rand.Next(5);
		mylist.Add(randPath);

		theSonPath = selectPath(randPath);
		walk(theSon,theSonPath);

		// DAUGHTER
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theDaughterPath = selectPath(randPath);
		walk(theDaughter,theDaughterPath);
		// THE WIFE
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theWifePath = selectPath(randPath);
		walk(theWife,theWifePath);
		// MAID
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theMaidPath = selectPath(randPath);
		walk(theMaid,theMaidPath);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	string selectPath(int pathSelector){
		string randomPath = "";
		switch(pathSelector){
			case 0:
					randomPath = "bedroom2";	
					break;
			case 1:
					randomPath = "masterbedroom";
					break;
			case 2:
					randomPath = "kitchen";
					break;
			case 3:
					randomPath = "livingroom";
					break;
			case 4:
					randomPath = "garden";
					break;
		}
		return randomPath;
	}
	
	void walk(GameObject target, string pathName) {
		iTween.PutOnPath(target, iTweenPath.GetPath(pathName), 0);
		if(pathName == "bedroom2"){
			theTime = 10;
		}
		else if(pathName == "garden"){ 
			theTime = 30; 
		}
		else {
			theTime = 20;
		}
		iTween.MoveTo(target, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", theTime, "easetype", iTween.EaseType.linear,"onComplete", "walk", "looptype", "loop", "orienttopath", true));
	}
}
