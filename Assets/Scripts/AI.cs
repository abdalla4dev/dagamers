using UnityEngine;
using System.Collections;
using System;
using MurderData;
using System.Collections.Generic;

public class AI : MonoBehaviour{
	
	public static TreeNode tree = new TreeNode();
	
	// Use this for initialization
	void  Start() {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// this function generate questions based on  the timeline generated
	public static TreeNode qnGenerator() {
		
		TreeNode temp = new TreeNode();
		string question;
		string answer;
		int unlocker = 0;
		//string reply;
		//string weapon;
		//List<string> potentialWeapon = new List<string>();
		//bool isWeapon = false;
		// This runs a loop that covers all the questions 
		// each question comes in the form of a 'if' loop
		for (int i=1; i<=13;i++) {
			if (i == 1) { // question 1
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you find the body?";
					answer = "Yes, I found it at " + GenerateTimeline.bodyFound + ".";
					if (GenerateTimeline.timeline[j].isFoundBody()) {
						temp.setQnNode(1,question,answer,j,true,true,'s',0);
						unlocker = j;
					}
					else {
						temp.setQnNode(1,question,"No, I did not.",j,true,false,'s',0);
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 2) { // question 2
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "when did you last see the victim?";
					answer = "I last saw him at " + Convert.ToString(GenerateTimeline.deathTime-1) + ".";
					temp.setQnNode(2,question,answer,j,false,true,'n',10+unlocker);
				}
			}
			else if (i == 3) { // question 3
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where did you last see the victim?";
					answer = "I last saw him at " + GenerateTimeline.victim.getBefMurder(0) + ".";
					temp.setQnNode(3,question,answer,j,false,false,'n',10+unlocker);
				}
			}
			else if (i == 4) { // question 4
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getRHWeap() != null) {
						string thing = GenerateTimeline.timeline[j].getRHWeap().ToString();
						question = "What were you doing before " + GenerateTimeline.bodyFound + "?";
						answer = "I was " + GenerateTimeline.getPersonDetails(1,j,1) + " using " + thing + ".";
						temp.setQnNode(4,question,answer,j,false,false,'n',10+unlocker);
					}
					else {
						question = "What were you doing before " + GenerateTimeline.bodyFound + "?";
						answer = "I was " + GenerateTimeline.getPersonDetails(1,j,1) + ".";
						temp.setQnNode(4,question,answer,j,false,false,'n',10+unlocker);
					}
				}	
			}
			else if (i == 5) { // question 5
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.getPersonDetails(1,j,1) + " before " + GenerateTimeline.bodyFound + "?";
					answer = "I was at " + GenerateTimeline.getPersonDetails(1,j,0) + ".";
					temp.setQnNode(5,question,answer,j,false,false,'n',10+unlocker);
				}
			}
			else if (i == 6) { // question 6
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "How long were you " + GenerateTimeline.getPersonDetails(1,j,1) + " before " + GenerateTimeline.bodyFound +"?";
					answer = "1 hr.";
					temp.setQnNode(6,question,answer,j,false,true,'n',10+unlocker);
				}
				temp.moveToCurrNode();
			}
			else if (i == 7) { // question 7
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getRHWeap() != null) {
						string thing = GenerateTimeline.timeline[j].getRHWeap().ToString();
						question = "What were you doing after " + Convert.ToString(GenerateTimeline.deathTime-1) + "?";
						answer = "I was " + GenerateTimeline.getPersonDetails(0,j,1) + " using " + thing + ".";
						temp.setQnNode(7,question,answer,j,false,false,'n',24);
					}
					else {
						question = "What were you doing after " + Convert.ToString(GenerateTimeline.deathTime-1) + "?";
						answer = "I was " + GenerateTimeline.getPersonDetails(0,j,1) + ".";
						temp.setQnNode(7,question,answer,j,false,false,'n',24);
					}
				}
				
			}
			else if (i == 8) { // question 8
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.getPersonDetails(0,j,1) + " after " + Convert.ToString(GenerateTimeline.deathTime-1) + "?";
					answer = "I was at " + GenerateTimeline.getPersonDetails(0,j,0) + ".";
					temp.setQnNode(8,question,answer,j,false,false,'n',24);
				}
				
			}
			if (i == 9) { // question 9
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "How long were you " + GenerateTimeline.getPersonDetails(0,j,1) + " after " + Convert.ToString(GenerateTimeline.deathTime-1) +"?";
					answer = "1 hr.";
					temp.setQnNode(9,question,answer,j,false,true,'n',24);
				}
			}
			else if (i == 10) { // question 10
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi for the time period before " + GenerateTimeline.bodyFound + "?";
					answer = "Yes, " + GenerateTimeline.getPersonDetails(1,j,2) + ".";
					temp.setQnNode(10,question,answer,j,false,true,'n',60+j);
				}
				temp.moveToCurrNode();
			}
			else if (i == 11) { // question 11
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi for the time period after " + Convert.ToString(GenerateTimeline.deathTime-1) + "?";
					answer = "Yes, " + GenerateTimeline.getPersonDetails(0,j,2) + ".";
					temp.setQnNode(11,question,answer,j,false,true,'n',90+j);
				}
			}
			else if (i == 12) { // question 12
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can you vouch for " + GenerateTimeline.getPersonDetails(1,j,2) + " for the time period before " + GenerateTimeline.bodyFound + "?";
					for (int k=0;k<Globals.numSuspects;k++) {
						if (Enum.GetName(typeof(Suspects),k) == GenerateTimeline.getPersonDetails(1,j,2)) {
							answer = "Yes, he was " + GenerateTimeline.getPersonDetails(1,k,1) + " at the " + GenerateTimeline.getPersonDetails(1,k,0) + ".";
							temp.setQnNode(12,question,answer,j,false,false,'n',100+k);
							break;
						}
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 13) { // question 13
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can you vouch for " + GenerateTimeline.getPersonDetails(0,j,2) + " for the time period after " + Convert.ToString(GenerateTimeline.deathTime-1) + "?";
					for (int k=0;k<Globals.numSuspects;k++) {
						if (Enum.GetName(typeof(Suspects),k) == GenerateTimeline.getPersonDetails(0,j,2)) {
							answer = "Yes, he was " + GenerateTimeline.getPersonDetails(0,k,1) + " at the " + GenerateTimeline.getPersonDetails(0,k,0) + ".";
							temp.setQnNode(13,question,answer,j,false,false,'n',110+k);
							break;
						}
					}
				}
			}
			temp.BFS();
			temp.DFS();
		}
		
		/*for (int i=0;i<GenerateTimeline.timeline.Count;i++){ // add all potential murder weapons to the list
			if (GenerateTimeline.timeline[i].getRHWeap() != null) {
				potentialWeapon.Add(GenerateTimeline.timeline[i].getRHWeap().ToString());
			}
		}
		
		for (int i=0;i<Globals.numWeapons;i++) { // for each weapon 
			weapon = Enum.GetName(typeof(Weapons),i);
			for (int j=0;j<potentialWeapon.Count;j++) { // check with eacn weapon in the potentialWeapon list
				if (potentialWeapon[j] == weapon) {
					reply = weapon + " is not supposed to be in this room.";
					temp.setWeaponNode(weapon,reply);
					isWeapon = true;
				}
			}
			if (!isWeapon) {
				reply = weapon + " is in the correct room.";
				temp.setWeaponNode(weapon,reply);
			}
			else {
				isWeapon = false;
			}
		}*/
		
		return temp;
	}
	
	public static ArrayList HumanTriggered(int suspect) {
		return tree.retreiveQn(suspect);
	}
	
	
	public static string ClickingTriggered(int suspect,string qn) {
		return tree.retreiveAnswer(suspect, qn);
	}
	
	public static string weaponTriggered(string weapon) {
		return tree.retreiveWeapon(weapon);
	}
}

