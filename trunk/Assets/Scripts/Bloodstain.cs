using UnityEngine;
using System.Collections;
using MurderData;

public class Bloodstain : MonoBehaviour {
	
	public static GameObject stainKitchen;
	public static GameObject stainLivingRoom;
	public static GameObject stainMasterBedroom;
	public static GameObject stainGarden;
	public static GameObject stainMasterToilet;
	
	public static GameObject handprintKitchen;
	public static GameObject handprintLivingRoom;
	public static GameObject handprintMasterBedroom;
	public static GameObject handprintGarden;
	public static GameObject handprintMasterToilet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	static void showStain(RmEnum room)
	{
		switch(room)
		{
		case RmEnum.Kitchen:
				stainKitchen.renderer.enabled = true;
			handprintKitchen.renderer.enabled = true;
			break;
		case RmEnum.Living_Room:
				stainLivingRoom.renderer.enabled = true;
			handprintLivingRoom.renderer.enabled = true;
			break;
		case RmEnum.Master_Bedroom:
				stainMasterBedroom.renderer.enabled = true;
			handprintMasterBedroom.renderer.enabled = true;
			break;
		case RmEnum.Garden:
				stainGarden.renderer.enabled = true;
			handprintGarden.renderer.enabled = true;
			break;
		case RmEnum.Toilet_in_Master_Bedroom:
				stainMasterToilet.renderer.enabled = true;
			handprintMasterToilet.renderer.enabled = true;
			break;
		default:
				break;
		}
	}
}
