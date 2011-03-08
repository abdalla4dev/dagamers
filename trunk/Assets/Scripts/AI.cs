using UnityEngine;
using System.Collections;
using System;
using MurderData;

/*
mother = 'm'
daughter = 'd'
son = 's'
housemaid = 'h'
*/

public class AI : MonoBehaviour{
	
	public TreeNode tree;
	public GenerateTimeline GTlink;
	//private XmlTextReader textReader;
	
	// Use this for initialization
	void  Start() {
		tree = qnGenerator();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// this function generate questions based on  the timeline generated
	public TreeNode qnGenerator() {
		
		TreeNode temp = new TreeNode();
		string question;
		string answer;
		int unlocker = 0;
		int oldQn = 0;
		// This runs a loop that covers all the questions 
		// each question comes in the form of a 'if' loop
		for (int i=1; i<=11;i++) {
			if (i == 1) {
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Did you find the body?";
					answer = "Yes, I found it at " + Convert.ToString(GenerateTimeline.deathTime) + ".";
					if (GenerateTimeline.timeline[i].isFoundBody()) {
						temp.setQnNode(question,answer,j,true,true,'s',0);
						unlocker = j;
					}
					else {
						temp.setQnNode(question,"No, I did not.",j,true,false,'s',0);
					}
				}
				oldQn = temp.getNumOfCurrNodes();
			}
			if (i == 2) {
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "When did you last see the victim?";
					answer = "I last saw him at" + Convert.ToString(GenerateTimeline.deathTime-1) + ".";
					temp.setQnNode(question,answer,j,false,false,'n',unlocker);
				}
			}
			if (i == 3) {
				for (int j=0;j<Globals.numSuspects;j++) {
					question = "Where did you last see the victim?";
					answer = "I last saw him at" + GenerateTimeline.victim.getBefMurder(0) + ".";
					temp.setQnNode(question,answer,j,false,false,'n',unlocker);
				}
			}
			if (i == 4) {
				for (int j=0;j<4;j++) {
					question = "What are you doing before" + GenerateTimeline.deathTime + "?";
					answer = "I was" + GenerateTimeline.timeline[i].getBefMurder(1) + ".";
					temp.setQnNode(question,answer,j,false,true,'n',unlocker);
				}
				temp.removeOldNodes(oldQn);
				oldQn = temp.getNumOfCurrNodes();
			}
			if (i == 5) {
				for (int j=0;j<4;j++) {
					question = "Where are you" + GenerateTimeline.timeline[i].getBefMurder(1) + "before" + GenerateTimeline.deathTime + "?";
					answer = "I was at" + GenerateTimeline.timeline[i].getBefMurder(0) + ".";
					tree.setQnNode(question,answer,j,false,false,'n',i);
				}
			}
			if (i == 6) {
				for (int j=0;j<4;j++) {
					question = "For how long?";
					answer = "1 hr.";
					tree.setQnNode(question,answer,j,false,false,'n',i);
				}
			}
			if (i == 7) {
				for (int j=0;j<4;j++) {
					question = "Can anybody be your alibi?";
					answer = "Yes, " + GenerateTimeline.timeline[i].getBefMurder(2) + ".";
					tree.setQnNode(question,answer,j,false,true,'n',i);
				}
				temp.removeOldNodes(oldQn);
				oldQn = temp.getNumOfCurrNodes();
			}
			if (i == 8) {
				for (int j=0;j<4;j++) {
					question = "Did you see " +  GenerateTimeline.timeline[i].getBefMurder(2) + " at " + GenerateTimeline.deathTime + "?";
					answer = "Yes";
					tree.setQnNode(question,answer,j,false,true,'n',0);
				}
			}
		}
		return temp;
	}
	
}

