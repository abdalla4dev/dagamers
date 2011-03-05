using UnityEngine;
using System.Collections;
using System;
using MurderData;

public class AccessingMurderData : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Debug.Log(Kitchen.knife.activity); //access the only possible activity of the knife in the kitchen
		foreach (string s in Enum.GetNames(typeof(Knife.Activities))) { //access all possible knife activities, you should not need to use this
			Debug.Log(s + " " + (int)Enum.Parse(typeof(Knife.Activities), s));
		}
		foreach (string s in Enum.GetNames(typeof(Kitchen.Generic_Activities))) { //access all generic kitchen activities
			Debug.Log(s + " " + (int)Enum.Parse(typeof(Kitchen.Generic_Activities), s))	;
		}
		Debug.Log(Kitchen.Generic_Activities.Cooking); //access a specific kitchen activity
		Debug.Log(Enum.GetName(typeof(Kitchen.Generic_Activities), 1));
		
		System.Random rand = new System.Random();
		Debug.Log((Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)); //how you may wish to randomize the activities
		Debug.Log((Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities));
		Debug.Log((Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities));
		Debug.Log((Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities));
		
		foreach (string s in Enum.GetNames(typeof(Living_Room.Generic_Activities))) {//access all generic living room activities
			Debug.Log(s + " " + (int)Enum.Parse(typeof(Living_Room.Generic_Activities), s))	;
		}
		Debug.Log(Living_Room.screwdriver.activity); //access activity of screwdriver in living room
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
