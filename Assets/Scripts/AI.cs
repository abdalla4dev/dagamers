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
		for (int i=1; i<=20;i++) {
			if (i == 1) { // question 1
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you find the body?";
					answer = "Yes, I found it at " + GenerateTimeline.aftMurderTime + ".";
					if (GenerateTimeline.timeline[j].isFoundBody()) {
						temp.setQnNode(1,question,answer,j,true,true,'s',0);
						unlocker = j;
					}
					else {
						temp.setQnNode(1,question,"No, I did not.",j,true,false,'s',0);
					}
				}
				temp.moveToCurrNode();
			}//generate the victim to be with a person
			else if (i == 2) { // question 2
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "When did you last see the victim?";
					answer = "I last saw him at " + Convert.ToString(GenerateTimeline.bodyFound-3) + ".";
					temp.setQnNode(2,question,answer,j,false,true,'n',10+unlocker);
				}
			}
			else if (i == 3) { // question 3
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where did you last see the victim?";
					answer = "I last saw him at " + GenerateTimeline.victim.getBMPlace() + ".";
					temp.setQnNode(3,question,answer,j,false,false,'n',10+unlocker);
				}
			}//
			else if (i == 4) { // question 4
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getDMWpn() != WpnEnum.None) {
						question = "What were you doing around the time " + GenerateTimeline.bodyFound + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getAMActivity() + " using " + GenerateTimeline.timeline[j].getAMWpn() + " from " + 
						Convert.ToString(GenerateTimeline.bodyFound-1) + " to " + GenerateTimeline.bodyFound;
						temp.setQnNode(4,question,answer,j,false,true,'n',10+unlocker);
					}
					else {
						question = "What were you doing around the time " + GenerateTimeline.bodyFound + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getAMActivity() + " from " + Convert.ToString(GenerateTimeline.bodyFound-1) + " to " + GenerateTimeline.bodyFound;
						temp.setQnNode(4,question,answer,j,false,true,'n',10+unlocker);
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 5) { // question 5
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getDMWpn() != WpnEnum.None) {
						question = "What were you doing around the time " + Convert.ToString(GenerateTimeline.bodyFound-2) + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getBMActivity() + " using " + GenerateTimeline.timeline[j].getBMWpn() + " from " + 
						Convert.ToString(GenerateTimeline.bodyFound-3) + " to " + Convert.ToString(GenerateTimeline.bodyFound-2);
						temp.setQnNode(5,question,answer,j,false,true,'n',24);
					}
					else {
						question = "What were you doing around the time " + Convert.ToString(GenerateTimeline.bodyFound-2) + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getAMActivity() + " from " + Convert.ToString(GenerateTimeline.bodyFound-3) + " to " + Convert.ToString(GenerateTimeline.bodyFound-2);
						temp.setQnNode(5,question,answer,j,false,true,'n',24);
					}
				}
			}
			else if (i == 6) { // question 6
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getDMWpn() != WpnEnum.None) {
						question = "What were you doing between " + Convert.ToString(GenerateTimeline.bodyFound-2) + " to " + Convert.ToString(GenerateTimeline.bodyFound-1) +"?";
						answer = "I was " + GenerateTimeline.timeline[j].getDMActivity() + " using " + GenerateTimeline.timeline[j].getDMWpn() + ".";
						temp.setQnNode(5,question,answer,j,false,true,'n',24);
					}
					else {
						question = "What were you doing between " + Convert.ToString(GenerateTimeline.bodyFound-2) + " to " + Convert.ToString(GenerateTimeline.bodyFound-1) +"?";
						answer = "I was " + GenerateTimeline.timeline[j].getDMActivity() + ".";
						temp.setQnNode(6,question,answer,j,false,true,'n',24);
					}
				}
			}
			else if (i == 7) { // question 7
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.timeline[j].getAMActivity() + " around the time " + GenerateTimeline.bodyFound + "?";
					answer = "I was at " + GenerateTimeline.timeline[j].getAMPlace() + ".";
					temp.setQnNode(7,question,answer,j,false,false,'n',40+j);
				}
				temp.moveToCurrNode();
			}
			else if (i == 8) { // question 8
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.timeline[j].getBMActivity() + " around the time " + Convert.ToString(GenerateTimeline.bodyFound-3) + "?";
					answer = "I was at " + GenerateTimeline.timeline[j].getBMPlace() + ".";
					temp.setQnNode(8,question,answer,j,false,false,'n',50+j);
				}
			}
			else if (i == 9) { // question 9
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.timeline[j].getDMActivity() + " between " + Convert.ToString(GenerateTimeline.bodyFound-2) + " and " + Convert.ToString(GenerateTimeline.bodyFound-1);
					answer = "I was at " + GenerateTimeline.timeline[j].getDMPlace() + ".";
					temp.setQnNode(9,question,answer,j,false,false,'n',60+j);
				}
			}
			else if (i == 10) { // question 10
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi for the time period around " + GenerateTimeline.bodyFound + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getAMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(10,question,answer,j,false,false,'n',70+j);
					}
					else {
						answer = "Yes, I was with ";
						for (int k=0;k<tempAli.Count;k++) {
							answer += " and " + tempAli[k]; 
						}
						answer += ".";
						temp.setQnNode(10,question,answer,j,false,false,'n',70+j);
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 11) { // question 11
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi for the time period around " + Convert.ToString(GenerateTimeline.bodyFound-3) + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getBMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(11,question,answer,j,false,false,'n',80+j);
					}
					else {
						answer = "Yes, I was with ";
						for (int k=0;k<tempAli.Count;k++) {
							answer += " and " + tempAli[k]; 
						}
						answer += ".";
						temp.setQnNode(11,question,answer,j,false,false,'n',80+j);
					}
				}
			}
			else if (i == 12) { // question 12
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi for the time period from " + Convert.ToString(GenerateTimeline.bodyFound-2) +
					" to " + Convert.ToString(GenerateTimeline.bodyFound-1) + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getDMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(12,question,answer,j,false,false,'n',90+j);
					}
					else {
						answer = "Yes, I was with " + Enum.GetName(typeof(SuspectEnum),tempAli[0]);
						for (int k=1;k<tempAli.Count;k++) {
							answer += " and " + Enum.GetName(typeof(SuspectEnum),tempAli[k]); 
						}
						answer += ".";
						temp.setQnNode(12,question,answer,j,false,false,'n',90+j);
					}
				}
			}
			else if (i == 13) { // question 13
				for (int j=0;j<Globals.numSuspects;j++) {
					bool isAlibi = false;
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getAMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<tempAli.Count;k++) { // for each alibi
							question = "Can you vouch for " + Enum.GetName(typeof(SuspectEnum),j) + " for the time period around " + GenerateTimeline.bodyFound + "?";
							for (int m=0;m<Globals.numSuspects;m++) { 
								if (tempAli[k] == (SuspectEnum)m) { // if this suspect is the alibi
									List<SuspectEnum> checkAli = new List<SuspectEnum>();
									checkAli = GenerateTimeline.timeline[m].getAMAlibi();
									for (int n=0;n<checkAli.Count;n++) {
										if (checkAli[n] == (SuspectEnum)m) { //if this suspect alibi is the same
											answer = "Yes, he was with me.";
											isAlibi = true;
											temp.setQnNode(13,question,answer,m,false,false,'n',100+j);
										}
									}
									if (!isAlibi) {
										answer = "No, he was not with me.";
										temp.setQnNode(13,question,answer,m,false,false,'n',100+j);
									}
									else {
										isAlibi = false;
									}
								}
							}
						}
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 14) { // question 14
				for (int j=0;j<Globals.numSuspects;j++) {
					bool isAlibi = false;
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getBMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<tempAli.Count;k++) { // for each alibi
							question = "Can you vouch for " + Enum.GetName(typeof(SuspectEnum),j) + " for the time period around " + Convert.ToString(GenerateTimeline.bodyFound-3) + "?";
							for (int m=0;m<Globals.numSuspects;m++) { 
								if (tempAli[k] == (SuspectEnum)m) { // if this suspect is the alibi
									List<SuspectEnum> checkAli = new List<SuspectEnum>();
									checkAli = GenerateTimeline.timeline[m].getBMAlibi();
									for (int n=0;n<checkAli.Count;n++) {
										if (checkAli[n] == (SuspectEnum)m) { //if this suspect alibi is the same
											answer = "Yes, he was with me.";
											isAlibi = true;
											temp.setQnNode(13,question,answer,m,false,false,'n',110+j);
										}
									}
									if (!isAlibi) {
										answer = "No, he was not with me.";
										temp.setQnNode(13,question,answer,m,false,false,'n',110+j);
									}
									else {
										isAlibi = false;
									}
								}
							}
						}
					}
				}
			}
			else if (i == 15) { // question 15
				for (int j=0;j<Globals.numSuspects;j++) {
					bool isAlibi = false;
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getDMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<tempAli.Count;k++) { // for each alibi
							question = "Can you vouch for " + Enum.GetName(typeof(SuspectEnum),j) + " for the time period from " + Convert.ToString(GenerateTimeline.bodyFound-2) + " to " + Convert.ToString(GenerateTimeline.bodyFound-2) + "?";
							for (int m=0;m<Globals.numSuspects;m++) { 
								if (tempAli[k] == (SuspectEnum)m) { // if this suspect is the alibi
									List<SuspectEnum> checkAli = new List<SuspectEnum>();
									checkAli = GenerateTimeline.timeline[m].getDMAlibi();
									for (int n=0;n<checkAli.Count;n++) {
										if (checkAli[n] == (SuspectEnum)m) { //if this suspect alibi is the same
											answer = "Yes, he was with me.";
											isAlibi = true;
											temp.setQnNode(13,question,answer,m,false,false,'n',120+j);
										}
									}
									if (!isAlibi) {
										answer = "No, he was not with me.";
										temp.setQnNode(13,question,answer,m,false,false,'n',120+j);
									}
									else {
										isAlibi = false;
									}
								}
							}
						}
					}
				}
			}
		}
		
		temp.BFS();
		temp.DFS();
		return temp;
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
	}
	
	public static ArrayList HumanTriggered(int suspect) {
		return tree.retreiveQn(suspect);
	}
	
	
	public static string ClickingTriggered(int suspect,string qn) {
		return tree.retreiveAnswer(suspect, qn);
	}
	
	public static List<string[]> logTriggered(int suspect) {
		return tree.getLogList(suspect);
	}
	
	/*public static string weaponTriggered(string weapon) {
		return tree.retreiveWeapon(weapon);
	}*/
}

