using UnityEngine;
using System.Collections;
using MurderData;

public class Bloodstain : MonoBehaviour {
	
	public GameObject stainKitchen;
	public GameObject stainLivingRoom;
	public GameObject stainMasterBedroom;
	public GameObject stainGarden;
	public GameObject stainMasterToilet;
	
	public GameObject handprintKitchen;
	public GameObject handprintLivingRoom;
	public GameObject handprintMasterBedroom;
	public GameObject handprintGarden;
	public GameObject handprintMasterToilet;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static putStains(RmEnum room){
		showStain(RmEnum room);
	}
	
	void showStain(RmEnum room)
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
