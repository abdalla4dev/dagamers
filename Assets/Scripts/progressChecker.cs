using UnityEngine;
using System;
using System.Xml;
using System.Collections;

public class progressChecker : MonoBehaviour {
	/* got errors so colin commented out first
	float playTime, totalTime;
	float CheckProgress() {
		countUnlocked(startNode);
		float qnProgress = (unlockedCounter/totalCounter); //percentage
		float timeProgress = (playTime/totalTime);
		float totalProgress = (qnProgress/timeProgress);
		return totalProgress;
	}
	
	float unlockedCounter;
	float totalCounter; //initialise beforehand.
	void countUnlocked(startNode) {
		if (startNode.getUnlockedNode() && !startNode.getAccessed())
		{
			unlockedCounter++;
			startNode.setAccessed(true);
		}
		if(startNode.QnNode!=null)
		{
			for(int j=0; j<startNode.QnNode.size; j++)
			{
				startNode.QnNode[j].checkProgress();
			}
		}
		else return;
	}*/
}
