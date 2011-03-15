using UnityEngine;
using System.Collections;
using System;
using MurderData;

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
		// This runs a loop that covers all the questions 
		// each question comes in the form of a 'if' loop
		for (int i=1; i<=8;i++) {
			if (i == 1) { // question 1
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you find the body?";
					answer = "Yes, I found it at " + Convert.ToString(GenerateTimeline.deathTime+1) + ".";
					if (GenerateTimeline.timeline[j].isFoundBody()) {
						temp.setQnNode(question,answer,j,true,true,'s',0);
						unlocker = j;
					}
					else {
						temp.setQnNode(question,"No, I did not.",j,true,false,'s',0);
					}
				}
				temp.moveToCurrNode();
				
			}
			else if (i == 2) { // question 2
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "When did you last see the victim?";
					answer = "I last saw him at " + Convert.ToString(GenerateTimeline.deathTime-1) + ".";
					temp.setQnNode(question,answer,j,false,false,'n',unlocker);
				}
			}
			else if (i == 3) { // question three
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where did you last see the victim?";
					answer = "I last saw him at " + GenerateTimeline.victim.getBefMurder(0) + ".";
					temp.setQnNode(question,answer,j,false,false,'n',unlocker);
				}
			}
			else if (i == 4) { // question four
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "What are you doing before " + GenerateTimeline.deathTime + "?";
					answer = "I was " + GenerateTimeline.timeline[j].getBefMurder(1) + ".";
					temp.setQnNode(question,answer,j,false,true,'n',unlocker);
				}
				temp.moveToCurrNode();
				
			}
			else if (i == 5) { // question 5
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where are you " + GenerateTimeline.timeline[j].getBefMurder(1) + " before " + GenerateTimeline.deathTime + "?";
					answer = "I was at " + GenerateTimeline.timeline[j].getBefMurder(0) + ".";
					temp.setQnNode(question,answer,j,false,false,'n',j);
				}
			}
			if (i == 6) { // question 6
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "For how long?";
					answer = "1 hr.";
					temp.setQnNode(question,answer,j,false,false,'n',j);
				}
			}
			if (i == 7) { // question 7
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Can anybody be your alibi?";
					answer = "Yes, " + GenerateTimeline.timeline[j].getBefMurder(2) + ".";
					temp.setQnNode(question,answer,j,false,true,'n',j);
				}
				temp.moveToCurrNode();
				temp.BFS();
				temp.DFS();
			}
			/*if (i == 8) { //question 8
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you see " +  GenerateTimeline.timeline[j].getBefMurder(2) + " at " + GenerateTimeline.deathTime + "?";
					answer = "Yes";
					temp.setQnNode(question,answer,j,false,true,'n',0);
				}
			}*/
		}
		return temp;
	}
	
	public static ArrayList HumanTriggered(int suspect) {
		return tree.retreiveQn(suspect);
	}
	
	
	public static string ClickingTriggered(int suspect,string qn) {
		return tree.retreiveAnswer(suspect, qn);
	}
}

