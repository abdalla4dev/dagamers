using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeNode {
	
	//num of suspects
	int numOfSus;
	
	//start node and current node of the QnNode tree
	private List<QnNode> startNode = new List<QnNode>();
	private List<QnNode> currNode = new List<QnNode>();
	private List<QnNode> currQn = new List<QnNode>(); //stores the currQn down the list of Qn
	private List<QnNode> nextCurrNode = new List<QnNode>();
	private List<List<string[]>> logList = new List<List<string[]>>();
	//private List<WeaponNode> weaponList = new List<WeaponNode>();
	
	//start node and current node of weaponNode tree
	
	public TreeNode() {
		setUpLogList();
	}
	
	public ArrayList retreiveQn(int suspect) {// function return correct qns to be printed out
		
		QnNode temp = new QnNode(null);
		ArrayList qnPrint = new ArrayList();
		
		for (int i=0;i<16;i++) {
			qnPrint.Add("temp");
		}
		
		for (int i=0;i<4;i++) {// search for the person first
			temp = startNode[i];
			if (temp.Sus == suspect) { // if got the correct person
				while (temp.NextQn != null) { 
					if (temp.QnNum == 6) {
						if (temp.Unlocked && temp.UnlockedTwo) {
							qnPrint = addAndArrange(qnPrint,temp);
						}
						temp = temp.NextQn;
					}
					else if (temp.Unlocked == true) { //check if question is unlocked
						qnPrint = addAndArrange(qnPrint,temp); // add unlocked qn to a Arraylist
						temp = temp.NextQn;
					}
					else {
						temp = temp.NextQn;
					}
				}
				if (temp.QnNum == 16) {
					if (temp.Unlocked == true) {
						qnPrint = addAndArrange(qnPrint,temp);
					}
				}
			}
		}
		return qnPrint; // return the Arraylist of qns to be printed
	}
	
	public string retreiveAnswer(int suspect, string qn) {//update boolean function when clicked on a question
		QnNode temp = new QnNode(null);

		for (int i=0;i<4;i++) {
			temp = startNode[i];
			if (temp.Sus == suspect) { // check for the person
				while (temp.NextQn != null) { 
					if (temp.Qn == qn) { // check for the correct qn
						temp.changeBooleanValues(); // unlocked the values
						temp.setGUIBool();
						sendToLog(temp,suspect); // send the qn and answer to log and lock the qn
						temp.Unlocked = false;
						temp.isLogged = true;
						return temp.Ans; // return the answer
					}
					else {
						temp = temp.NextQn;
					}
				}
				if (temp.QnNum == 16) {
					sendToLog(temp,suspect); // send the qn and answer to log and lock the qn
					temp.Unlocked = false;
					temp.isLogged = true;
					return temp.Ans;
				}
			}
		}
		
		return "0";
	}
	
	public ArrayList addAndArrange(ArrayList printQn, QnNode temp) {
		if (temp.QnNum == 1) {
			printQn.RemoveAt(0);
			printQn.Insert(0,temp.Qn);
		}
		else if (temp.QnNum == 2) {
			printQn.RemoveAt(1);
			printQn.Insert(1,temp.Qn);
		}
		else if (temp.QnNum == 3) {
			printQn.RemoveAt(2);
			printQn.Insert(2,temp.Qn);
		}
		else if (temp.QnNum == 5) {
			printQn.RemoveAt(3);
			printQn.Insert(3,temp.Qn);
		}
		else if (temp.QnNum == 8) {
			printQn.RemoveAt(4);
			printQn.Insert(4,temp.Qn);
		}
		else if (temp.QnNum == 4) {
			printQn.RemoveAt(5);
			printQn.Insert(5,temp.Qn);
		}
		else if (temp.QnNum == 7) {
			printQn.RemoveAt(6);
			printQn.Insert(6,temp.Qn);
		}
		else if (temp.QnNum == 10) {
			printQn.RemoveAt(7);
			printQn.Insert(7,temp.Qn);
		}
		else if (temp.QnNum == 6) {
			printQn.RemoveAt(8);
			printQn.Insert(8,temp.Qn);
		}
		else if (temp.QnNum == 9) {
			printQn.RemoveAt(9);
			printQn.Insert(9,temp.Qn);
		}
		else if (temp.QnNum == 12) {
			printQn.RemoveAt(10);
			printQn.Insert(10,temp.Qn);
		}
		else if (temp.QnNum == 11) {
			printQn.RemoveAt(11);
			printQn.Insert(11,temp.Qn);
		}
		else if (temp.QnNum == 13) {
			printQn.RemoveAt(12);
			printQn.Insert(12,temp.Qn);
		}
		else if (temp.QnNum == 14) {
			printQn.RemoveAt(13);
			printQn.Insert(13,temp.Qn);
		}
		else if (temp.QnNum == 15) {
			printQn.RemoveAt(14);
			printQn.Insert(14,temp.Qn);
		}
		else if (temp.QnNum == 16) {
			printQn.RemoveAt(15);
			printQn.Insert(15,temp.Qn);
		}
		return printQn;
	}
	
	public void setUpLogList() {
		List<string[]> zero = new List<string[]>();
		List<string[]> one = new List<string[]>();
		List<string[]> two = new List<string[]>();
		List<string[]> three = new List<string[]>();
		logList.Add(zero);
		logList.Add(one);
		logList.Add(two);
		logList.Add(three);
		
		string[] tempo = new string[2];
		tempo[0] = "temp";
		tempo[1] = "temp";	
		for (int i=0;i<4;i++) {
			for (int j=0;j<16;j++) {
				logList[i].Add(tempo);
			}
		}
	}
	
	public void sendToLog(QnNode temp, int sus) {
		
		string[] logNode = new string[2];
		logNode[0] = temp.Qn;
		logNode[1] = temp.Ans;
		
		if (temp.QnNum == 1) {
			logList[sus].RemoveAt(0);
			logList[sus].Insert(0,logNode);
		}
		else if (temp.QnNum == 2) {
			logList[sus].RemoveAt(1);
			logList[sus].Insert(1,logNode);
		}
		else if (temp.QnNum == 3) {
			logList[sus].RemoveAt(2);
			logList[sus].Insert(2,logNode);
		}
		else if (temp.QnNum == 5) {
			logList[sus].RemoveAt(3);
			logList[sus].Insert(3,logNode);
		}
		else if (temp.QnNum == 8) {
			logList[sus].RemoveAt(4);
			logList[sus].Insert(4,logNode);
		}
		else if (temp.QnNum == 4) {
			logList[sus].RemoveAt(5);
			logList[sus].Insert(5,logNode);
		}
		else if (temp.QnNum == 7) {
			logList[sus].RemoveAt(6);
			logList[sus].Insert(6,logNode);
		}
		else if (temp.QnNum == 10) {
			logList[sus].RemoveAt(7);
			logList[sus].Insert(7,logNode);
		}
		else if (temp.QnNum == 6) {
			logList[sus].RemoveAt(8);
			logList[sus].Insert(8,logNode);
		}
		else if (temp.QnNum == 9) {
			logList[sus].RemoveAt(9);
			logList[sus].Insert(9,logNode);
		}
		else if (temp.QnNum == 12) {
			logList[sus].RemoveAt(10);
			logList[sus].Insert(10,logNode);
		}
		else if (temp.QnNum == 11) {
			logList[sus].RemoveAt(11);
			logList[sus].Insert(11,logNode);
		}
		else if (temp.QnNum == 13) {
			logList[sus].RemoveAt(12);
			logList[sus].Insert(12,logNode);
		}
		else if (temp.QnNum == 14) {
			logList[sus].RemoveAt(13);
			logList[sus].Insert(13,logNode);
		}
		else if (temp.QnNum == 15) {
			logList[sus].RemoveAt(14);
			logList[sus].Insert(14,logNode);
		}
		else if (temp.QnNum == 16) {
			logList[sus].RemoveAt(15);
			logList[sus].Insert(15,logNode);
		}

		for (int i=0;i<logList.Count;i++) {
			for (int j=0;j<logList[i].Count;j++) {
				string[] temppo = logList[i][j];
				Debug.Log(temppo[0]);
				Debug.Log(temppo[1]);
			}
		}
	}
	
	public List<string[]> getLogList(int suspect) {
		return logList[suspect];
	}
	
    /*public string retreiveWeapon(string weapon)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponNode temp = new WeaponNode();
            temp = weaponList[i];
            if (weapon == temp.getWeapon())
            {
                return temp.getReply();
            }
        }
        return "";
    }

    public WeaponNode getWeapon(int num) {
        return (WeaponNode)weaponList[num];
    }
	
	public int getNumOfCurrNodes() {
		return currNode.Count;
	}
	
	public void removeOldNodes(int num) {
		for (int i=0;i<num;i++) {
			currNode.RemoveAt(0);
		}
	}*/
	// set a QnNode according to the information given 
	public void setQnNode(int tempNum, string tempQn, string tempAns, int sus, bool unlocked, bool unlocker, char node, int attachedPer) {
		
		QnNode newNode = new QnNode(null);
		newNode.QnNum = tempNum; // add the info into the newNode
		newNode.Qn = tempQn; 
		newNode.Ans = tempAns;
		newNode.Sus = sus;
		newNode.Unlocked = unlocked;
		if (node == 's') { // if newNode is the start of the TreeNode, add to startNode
			startNode.Add(newNode);
			currQn.Add(newNode);
		}
		else {// else, will convert the currQn to the one now
			for (int i=0; i<currQn.Count;i++) {
				if (sus == currQn[i].Sus) {
					currQn[i].NextQn = newNode;
					currQn.RemoveAt(i);
					currQn.Insert(i,newNode);
				}
			}
		}
		
		if (unlocker) { // if this question unlock anything, will add to the nextCurrNode
			nextCurrNode.Add(newNode);
		}
		for (int i=0;i<currNode.Count;i++) { // int person will tell us to which currNode we need to attach the newNode to  
			if (currNode[i].QnNum == attachedPer/10 && (currNode[i].Sus == attachedPer%10 || attachedPer%10 == 4)) {		
				currNode[i].addNextNode(newNode);
				//Debug.Log("in" + tempNum);
			}
		}
	}
	
	public void moveToCurrNode() {
		while (currNode.Count > 0) {
			currNode.RemoveAt(0);
		}
		while (nextCurrNode.Count > 0) {
			currNode.Add(nextCurrNode[0]);
			nextCurrNode.RemoveAt(0);
		}
		/*for (int i=0;i<nextCurrNode.Count;i++) {
			printNode(nextCurrNode[i]);
		}
		for (int i=0;i<currNode.Count;i++) {
			printNode(currNode[i]);
		}*/
	}
	
    /*public void setWeaponNode(string weapon, string reply)
    {
        WeaponNode temp = new WeaponNode(weapon,reply);
        weaponList.Add(temp);
		for (int i=0;i<weaponList.Count;i++){
			Debug.Log(weaponList[i].getWeapon());
			Debug.Log(weaponList[i].getReply());
		}
    }*/
	
	/*public void checker() {
		/*for (int i=0;i<1;i++) {
			QnNode curr = new QnNode();
			curr = (QnNode)startNode[i];
			Debug.Log(curr.getQn());
			Debug.Log(curr.getAnswer());
			Debug.Log(curr.getPerson());
			Debug.Log(curr.getNumOfNextNodes());
			if(curr.getNumOfNextNodes()>0)
				Debug.Log(curr.getNumOfNextNodes());
		}
		
		int unlockedCounter=0;
		for(int i=0; i<4; i++)
		{
			QnNode curr = new QnNode();
			curr = (QnNode)startNode[i];
		//unlockedCounter += BFS((QnNode)startNode[0]);
		unlockedCounter += DFS((QnNode)startNode[i]);
		}
		
		float currProgress = checkProgress(unlockedCounter);
	}
	
	float playTime, totalTime;
	float totalCounter;
	float checkProgress(int unlockedCounter) {
		float qnProgress = (unlockedCounter/totalCounter); 
		float timeProgress = (playTime/totalTime);
		float totalProgress = (qnProgress/timeProgress)*100;
		return totalProgress;
	}*/
	
	public void DFS() {
		
		QnNode temp = new QnNode(null);
		
		for(int i=0;i<startNode.Count;i++) {
			printNode(startNode[i]);
			temp = startNode[i];
			while (temp.NextQn != null) {
				temp = temp.NextQn;
				printNode(temp);
			}
		}
	}
	
	public void BFS()
	{
		List<QnNode> BFS = new List<QnNode>();
		for (int i=0; i<startNode.Count;i++) {
			BFS.Add(startNode[i]);
			//Debug.Log(startNode[i].Qn);
		}
		while (BFS.Count > 0) {
			if (BFS[0].getNumOfNextNodes() == 0) {
				printNode(BFS[0]);
				BFS.RemoveAt(0);
			}
			else if (BFS[0].getNumOfNextNodes() > 0) {
				for (int i=0;i<BFS[0].getNumOfNextNodes();i++) {
					BFS.Add(BFS[0].getNextNode(i));
				}
				printNode(BFS[0]);
				BFS.RemoveAt(0);
			}
		}
	}
	
	public void printNode(QnNode nodePrinted) {
		Debug.Log(nodePrinted.Qn + " " + nodePrinted.Ans + " " + nodePrinted.Sus + " " + nodePrinted.QnNum);
	}
}
