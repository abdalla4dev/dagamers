using UnityEngine;
using System.Collections;
using System;
using MurderData;
using System.Collections.Generic;

public class AI : MonoBehaviour{
	
	public static TreeNode tree = new TreeNode();
	public static System.Random rand = new System.Random();
	public static List<string> startingConvo = new List<string>();
	public static List<string> conseConvo = new List<string>();
	
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
		
		string firstTime = AI.convertTime(GenerateTimeline.befMurderTime);
		string secondTime = AI.convertTime(GenerateTimeline.deathTime);
		string thirdTime = AI.convertTime(GenerateTimeline.aftMurderTime);
		string fourthTime = AI.convertTime(GenerateTimeline.bodyFound);
		
		// This runs a loop that covers all the questions 
		// each question comes in the form of a 'if' loop
		for (int i=1; i<=50;i++) {
			if (i == 1) { // question 1
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you find the body?";
					answer = "Yes, I found it at " + fourthTime + ".";
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
					question = "When and where did you last see Mr. Darcy?";
					if (GenerateTimeline.timeline[j].isLastSaw()) {
						answer = "I last saw him at " + firstTime + " in the " + GenerateTimeline.victim.getBMPlace() + ".";
						temp.setQnNode(2,question,answer,j,false,true,'n',10+unlocker);
					}
					else {
						answer = "I last saw him way before the murder timing, so I do not think it will be of help.";
						temp.setQnNode(2,question,answer,j,false,true,'n',10+unlocker);
					}
				}
			}
			else if (i == 3) { // question 3
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getAMWpn() != WpnEnum.None) {
						question = "What were you doing around the time " + fourthTime + ", when the body is found?";
						answer = "I was " + GenerateTimeline.timeline[j].getAMActivity() + " using " + GenerateTimeline.timeline[j].getAMWpn() + " from " + 
						thirdTime + " to " + fourthTime + ".";
						temp.setQnNode(3,question,answer,j,false,true,'n',10+unlocker);
					}
					else {
						question = "What were you doing around the time " + fourthTime + ", when the body is found?";
						answer = "I was " + GenerateTimeline.timeline[j].getAMActivity() + " from " + thirdTime + " to " + fourthTime + ".";
						temp.setQnNode(3,question,answer,j,false,true,'n',10+unlocker);
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 4) { // question 4
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getBMWpn() != WpnEnum.None) {
						question = "What were you doing around the time " + firstTime + ", when the Mr.Darcy is last seen?";
						answer = "I was " + GenerateTimeline.timeline[j].getBMActivity() + " using " + GenerateTimeline.timeline[j].getBMWpn() + " from " + 
						firstTime + " to " + secondTime + ".";
						temp.setQnNode(4,question,answer,j,false,true,'n',24);
					}
					else {
						question = "What were you doing around the time " + firstTime + ", when the Mr.Darcy is last seen?";
						answer = "I was " + GenerateTimeline.timeline[j].getBMActivity() + " from " + firstTime + " to " + secondTime + ".";
						temp.setQnNode(4,question,answer,j,false,true,'n',24);
					}
				}
			}
			else if (i == 5) { // question 5
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "You were " + GenerateTimeline.timeline[j].getAMActivity() + " from " + thirdTime + " to " + fourthTime + " at...?";
					answer = "I was at " + GenerateTimeline.timeline[j].getAMPlace() + ".";
					temp.setQnNode(5,question,answer,j,false,true,'n',30+j);
				}
				temp.moveToCurrNode();
			}
			else if (i == 6) { // question 6
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getDMWpn() != WpnEnum.None) {
						question = "Then what about your activity between the unaccounted time of " + secondTime + " and " + thirdTime + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getDMActivity() + " using " + GenerateTimeline.timeline[j].getDMWpn() + ".";
						temp.setQnNode(6,question,answer,j,false,true,'n',40+j);
					}
					else {
						question = "Then what about your activity between the unaccounted time of " + secondTime + " and " + thirdTime + "?";
						answer = "I was " + GenerateTimeline.timeline[j].getDMActivity() + ".";
						temp.setQnNode(6,question,answer,j,false,true,'n',40+j);
					}
				}
			}
			else if (i == 7) { // question 7
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "You were " + GenerateTimeline.timeline[j].getBMActivity()+ " from " + firstTime + " to " + secondTime + " at...?";
					answer = "I was at " + GenerateTimeline.timeline[j].getBMPlace() + ".";
					temp.setQnNode(7,question,answer,j,false,true,'n',40+j);
				}
			}
			else if (i == 8) { // question 8
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Could anybody be your alibi for the time period from " + thirdTime + " to " + fourthTime + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getAMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(8,question,answer,j,false,false,'n',50+j);
					}
					else {
						answer = "Yes, I was with " + AI.returnName((int)tempAli[0]) + ".";
						temp.setQnNode(8,question,answer,j,false,true,'n',50+j);
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 9) { // question 9
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where were you " + GenerateTimeline.timeline[j].getDMActivity() + " between " + secondTime + " and " + thirdTime + "?";
					answer = "I was at " + GenerateTimeline.timeline[j].getDMPlace() + ".";
					temp.setQnNode(9,question,answer,j,false,true,'n',60+j);
				}
			}
			else if (i == 10) { // question 10
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Could anybody be your alibi for the time period from " + firstTime + " to " + secondTime + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getBMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(10,question,answer,j,false,false,'n',70+j);
					}
					else {
						answer = "Yes, I was with " + AI.returnName((int)tempAli[0]) + ".";
						temp.setQnNode(10,question,answer,j,false,true,'n',70+j);
					}
				}
			}
			else if (i == 11) { // question 11
				for (int j=0;j<Globals.numSuspects;j++) {
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getAMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<Globals.numSuspects;k++) { // check with everybody
							if (tempAli[0] == (SuspectEnum)k) {// if this suspect is the alibi
								question = "Did you see " + AI.returnName(j) + " " + GenerateTimeline.timeline[j].getAMActivity() + " from " + thirdTime + " to " + fourthTime + "?";
								List<SuspectEnum> checkAli = new List<SuspectEnum>();
								checkAli = GenerateTimeline.timeline[k].getAMAlibi();
								if (checkAli.Count > 0) {
									if (checkAli[0] == (SuspectEnum)j) {
										if (GenerateTimeline.timeline[j].isTrueAMActivity()) {
											answer = "Yes, " + AI.returnName(j) + " was " + GenerateTimeline.timeline[j].getAMActivity() + " and was with me.";
											temp.setQnNode(11,question,answer,k,false,false,'n',80+j);
										}
										else {
											answer = "Yes, " + AI.returnName(j) + " was with me but " + AI.returnName(j) + " was  not " + GenerateTimeline.timeline[j].getAMActivity() + ".";
											temp.setQnNode(11,question,answer,k,false,false,'n',80+j);
										}
									}
									else {
										answer = "No, " + AI.returnName(j) + " was not with me.";
										temp.setQnNode(11,question,answer,k,false,false,'n',80+j);
									}
								}
							}
						}
					}
				}
				temp.moveToCurrNode();
			}
			else if (i == 12) { // question 12
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Could anybody be your alibi for the time period from " + secondTime + " to " + thirdTime + "?";
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getDMAlibi();
					if (tempAli.Count == 0) {
						answer = "No, I was alone.";
						temp.setQnNode(12,question,answer,j,false,false,'n',90+j);
					}
					else {
						answer = "Yes, I was with " + AI.returnName((int)tempAli[0]) + ".";

						temp.setQnNode(12,question,answer,j,false,true,'n',90+j);
					}
				}
			}
			else if (i == 13) { // question 13
				for (int j=0;j<Globals.numSuspects;j++) {
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getBMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<Globals.numSuspects;k++) { // check with everybody
							if (tempAli[0] == (SuspectEnum)k) {// if this suspect is the alibi
								question = "Did you see " + AI.returnName(j) + " " + GenerateTimeline.timeline[j].getBMActivity() + " from " + firstTime + " to " + secondTime + "?";
								List<SuspectEnum> checkAli = new List<SuspectEnum>();
								checkAli = GenerateTimeline.timeline[k].getBMAlibi();
								if (checkAli.Count > 0) {
									if (checkAli[0] == (SuspectEnum)j) {
										if (GenerateTimeline.timeline[j].isTrueBMActivity()) {
											answer = "Yes, " + AI.returnName(j) + " was " + GenerateTimeline.timeline[j].getBMActivity() + " and was with me.";
											temp.setQnNode(13,question,answer,k,false,false,'n',100+j);
										}
										else {
											answer = "Yes, " + AI.returnName(j) + " was with me but " + AI.returnName(j) + " was not " + GenerateTimeline.timeline[j].getBMActivity() + ".";
											temp.setQnNode(13,question,answer,k,false,false,'n',100+j);
										}
									}
									else {
										answer = "No, " + AI.returnName(j) + " was not with me.";
										temp.setQnNode(13,question,answer,k,false,false,'n',100+j);
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
					List<SuspectEnum> tempAli = new List<SuspectEnum>();
					tempAli = GenerateTimeline.timeline[j].getBMAlibi();
					if (tempAli.Count != 0) { // if got alibi
						for (int k=0;k<Globals.numSuspects;k++) { // check with everybody
							if (tempAli[0] == (SuspectEnum)k) {// if this suspect is the alibi
								question = "Did you see " + AI.returnName(j) + " " + GenerateTimeline.timeline[j].getDMActivity() + " from " + secondTime + " to " + thirdTime + "?";
								List<SuspectEnum> checkAli = new List<SuspectEnum>();
								checkAli = GenerateTimeline.timeline[k].getDMAlibi();
								if (checkAli.Count > 0) {
									if (checkAli[0] == (SuspectEnum)j) {
										if (GenerateTimeline.timeline[j].isTrueDMActivity()) {
											answer = "Yes, " + AI.returnName(j) + " was " + GenerateTimeline.timeline[j].getDMActivity() + " and was with me.";
											temp.setQnNode(14,question,answer,k,false,false,'n',120+j);
										}
										else {
											answer = "Yes, " + AI.returnName(j) + " was with me but " + AI.returnName(j) + " was not " + GenerateTimeline.timeline[j].getDMActivity() + ".";
											temp.setQnNode(14,question,answer,k,false,false,'n',120+j);
										}
									}
									else {
										answer = "No, " + AI.returnName(j) + " was not with me.";
										temp.setQnNode(14,question,answer,k,false,false,'n',120+j);
									}
								}
							}
						}
					}
				}
			}
			else if (i == 15) {
			
				question = "who in the family would do this thing to your Father?";
				int num = rand.Next(4);
				for (int j=0;j<Globals.numSuspects;j++) {
					if (j == num) {
						num++;
					}
					answer = "I think " + AI.returnName(num) + " really hated him, " + GenerateTimeline.timeline[num].getHateFather();
					temp.setQnNode(15,question,answer,j,true,false,'n',0);
					num++;
					if (num == 4) {
						num = 0;
					}
				}
			}
			else if (i == 16) {
				question = "who are you close to?";
				for (int j=0;j<Globals.numSuspects;j++) {
					if (GenerateTimeline.timeline[j].getLikeWho() == SuspectEnum.None) {
						answer = "Nobody, really.";
						temp.setQnNode(16,question,answer,j,true,false,'n',0);
					}
					else {
						answer = "I like to hang out with " + AI.returnName((int)GenerateTimeline.timeline[j].getLikeWho()) + " recently.";
						temp.setQnNode(16,question,answer,j,true,false,'n',0);
					}
				}
			}
		}
		
		for (int i=0;i<Globals.numSuspects;i++) {
			string start = "";
			string conse = "";
			if (GenerateTimeline.timeline[i].getPersonality() == NegativePersonalityEnum.angry) {
				start = "I'm " + AI.returnName(i) + ". This better not be long.";
				conse = "What? Why are you bothering me again?";
			}
			else if (GenerateTimeline.timeline[i].getPersonality() == NegativePersonalityEnum.shy) {
				start = "...H...Hi... I'm " + AI.returnName(i) + "... What can I help you... with...?";
				conse = "... Oh... it's you again....";
			}
			else if (GenerateTimeline.timeline[i].getPersonality() == NegativePersonalityEnum.antisocial) {
				start = "... I'm " + AI.returnName(i) + "...";
				conse = "...";
			}
			else if (GenerateTimeline.timeline[i].getPersonality() == NegativePersonalityEnum.unhappy) {
				start = "(sob).... Yes? I'm" + AI.returnName(i) + ".";
				conse = "Please, leave me alone! I have nothing more to say! (sob)";
			}
			else {
				start = "Hello I'm " + AI.returnName(i) + ". Please to meet you.";
				conse = "Is there anything else you need to ask me?";
			}
			startingConvo.Add(start);
			conseConvo.Add(conse);
		}
		temp.BFS();
		temp.DFS();
		return temp;
	}
	
	public static string convertTime(int time) {
		string convertedTime;
		if (time > 12) {
			convertedTime = Convert.ToString(time-12) + ".00 pm";
		}
		else if (time == 12) {
			convertedTime = "12.00 pm";
		}
		else {
			convertedTime = Convert.ToString(time) + ".00 am";
		}
		
		return convertedTime;
	}
	
	public static string returnName(int sus) {
		if (sus == -1) {
			return "Mr. Darcy";
		}
		else if (sus == 0) {
			return "Mrs. Darcy";
		}
		else if (sus == 1) {
			return "Wayne";
		}
		else if (sus == 2) {
			return "Jane";
		}
		else if (sus == 3) {
			return "Maria";
		}
		return null;
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

