using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class setPaths : MonoBehaviour {
	public GameObject theSon;
	public GameObject theDaughter;
	public GameObject theWife;
	public GameObject theMaid;
	public string theSonPath;
	public string theDaughterPath;
	public string theWifePath;
	public string theMaidPath;
	private List<int> mylist = new List<int>();
		
	// Use this for initialization
	void Start () {
		int randPath = -1;
		int temp = randPath;
		System.Random rand = new System.Random();
		
		// SON
		randPath = rand.Next(5);
		mylist.Add(randPath);

		theSonPath = selectPath(randPath);
		Debug.Log(theSonPath);
		
		// DAUGHTER
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theDaughterPath = selectPath(randPath);
		Debug.Log(theDaughterPath);
		
		// THE WIFE
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theWifePath = selectPath(randPath);
		Debug.Log(theWifePath);
		
		// MAID
		do {
			randPath = rand.Next(5);
		} while (mylist.Contains(randPath));
		mylist.Add(randPath);
		
		theMaidPath = selectPath(randPath);
		Debug.Log(theMaidPath);;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	string selectPath(int pathSelector){
		string randomPath = "";
		switch(pathSelector){
			case 1:
					randomPath = "bedroom2";	
					break;
			case 2:
					randomPath = "masterbedroom";
					break;
			case 3:
					randomPath = "kitchen";
					break;
			case 4:
					randomPath = "livingroom";
					break;
			case 5:
					randomPath = "corridor";
					break;
			case 6:
					randomPath = "garden";
					break;
		}
		return randomPath;
	}
}
