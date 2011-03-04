using UnityEngine;
using System.Collections;
using System;
using MurderData;

public class AccessingMurderData : MonoBehaviour {
	
	private readonly Kitchen theKitchen = new Kitchen();
	
	// Use this for initialization
	void Start () {
		Debug.Log(theKitchen.knife.ka);
		foreach (string s in Enum.GetNames(typeof(Knife.Knife_Activities))) {
			Debug.Log(s + " " + (int)Enum.Parse(typeof(Knife.Knife_Activities), s));
		}
		foreach (string s in Enum.GetNames(typeof(Kitchen.Kitchen_Generic_Activities))) {
			Debug.Log(s + " " + (int)Enum.Parse(typeof(Kitchen.Kitchen_Generic_Activities), s))	;
		}
		Debug.Log(Kitchen.Kitchen_Generic_Activities.Cooking);
		Debug.Log(Enum.GetName(typeof(Kitchen.Kitchen_Generic_Activities), 1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
