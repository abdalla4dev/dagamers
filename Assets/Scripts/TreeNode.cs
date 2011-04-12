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
		
		for (int i=0;i<4;i++) { // search for the person first
			temp = startNode[i];
			if (temp.Sus == suspect) { // if got the correct person
				while (temp.NextQn != null) { 
					if (temp.QnNum == 6) {
						if (temp.Unlocked && temp.UnlockedTwo) {
							qnPrint.Add(temp.Qn);
						}
						temp = temp.NextQn;
					}
					else if (temp.Unlocked == true) { //check if question is unlocked
						qnPrint.Add(temp.Qn); // add unlocked qn to a Arraylist
						temp = temp.NextQn;
					}
					else {
						temp = temp.NextQn;
					}
				}
				if (temp.QnNum == 16) {
					if (temp.Unlocked == true) {
						qnPrint.Add(temp.Qn);
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
						sendToLog(temp.Qn,temp.Ans,suspect); // send the qn and answer to log and lock the qn
						temp.Unlocked = false;
						temp.isLogged = true;
						return temp.Ans; // return the answer
					}
					else {
						temp = temp.NextQn;
					}
				}
				if (temp.QnNum == 16) {
					return temp.Ans;
				}
			}
		}
		
		return "0";
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
	}
	
	public void sendToLog(string qn, string ans, int sus) {
		string[] logNode = new string[2];
		logNode[0] = qn;
		logNode[1] = ans;
		logList[sus].Add(logNode);
		for (int i=0;i<logList.Count;i++) {
			for (int j=0;j<logList[i].Count;j++) {
				string[] temp = logList[i][j];
				//Debug.Log(temp[0]);
				//Debug.Log(temp[1]);
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
