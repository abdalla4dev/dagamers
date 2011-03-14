using UnityEngine; 
using System.Collections;
using MurderData;
using System.Collections.Generic;

/*
mother = '0'
daughter = '1'
son = '2'
housemaid = '3'
*/

public class TreeNode {
	
	//num of suspects
	int numOfSus;
	
	//start node and current node of the tree
	private List<QnNode> startNode = new List<QnNode>();
	private List<QnNode> currNode = new List<QnNode>();
	private List<QnNode> currQn = new List<QnNode>(); //stores the currQn down the list of Qn
	//ArrayList weaponList = new ArrayList();
	
	public TreeNode() {
	}
	
	public ArrayList retreiveQn(int suspect) {// function return correct qns to be printed out
		
		QnNode temp = new QnNode();
		ArrayList qnPrint = new ArrayList();
		
		for (int i=0;i<4;i++) { // search for the person first
			temp = (QnNode)startNode[i];
			if (temp.getPerson() == suspect) { // if got the correct person
				while (temp.getNextQn() != null) { 
					if (temp.getUnlockedNode() == true) { //check if question is unlocked
						qnPrint.Add(temp.getQn()); // add unlocked qn to a Arraylist
						temp = temp.getNextQn();
					}
					else {
						temp = temp.getNextQn();
					}
				}
				break;
			}
		}
		return qnPrint; // return the Arraylist of qns to be printed
	}
	
	public string retreiveAnswer(int suspect, string qn) {//update boolean function when clicked on a question
		QnNode temp = new QnNode();

		for (int i=0;i<4;i++) {
			temp = (QnNode)startNode[i];
			if (temp.getPerson() == suspect) { // check for the person
				while (temp.getNextQn() != null) { 
					if (temp.getQn() == qn) { // check for the correct qn
						temp.changeBooleanValues(); // unlocked the values
						return temp.getAnswer(); // return the answer
					}
				}
				break;
			}
		}
		
		return "0";
	}

   /* public string weaponTriggered(string weapon)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponNode temp = new WeaponNode();
            temp = (WeaponNode)weaponList[i];
            if (weapon == temp.getWeapon())
            {
                return temp.getReply();
            }
        }
        return "";
    }*/
	
	/*public QnNode getStartNode(int num) {
		return (QnNode)startNode[num];
	}

    public WeaponNode getWeapon(int num) {
        return (WeaponNode)weaponList[num];
    }
	
	public void addStartNode(QnNode temp) {
		startNode.Add(temp);		
	}*/
	
	public int getNumOfCurrNodes() {
		return currNode.Count;
	}
	
	public void removeOldNodes(int num) {
		for (int i=0;i<num;i++) {
			currNode.RemoveAt(0);
		}
	}
	// set a QnNode according to the information given 
	public void setQnNode(string tempQn, string tempAns, int sus, bool unlocked, bool unlocker, char node, int person) {
		
		QnNode temp = new QnNode();
		/*newNode.setQn(tempQn); // add the info into the newNode
		newNode.setAnswer(tempAns);
		newNode.setPerson(sus);
		newNode.setUnlockedNode(unlocked);
		
		if (node == 's') { // if newNode is the start of the TreeNode, add to startNode
			startNode.Add(newNode);
			currQn.Add(newNode);
		}
		else {// else, will convert the currQn to the one now
			Debug.Log(currQn.Count);
			for (int i=0; i<currQn.Count;i++) {
				if (sus == currQn[i].getPerson()) {
					//currQn[i].setNextQn(newNode);
					//currQn.RemoveAt(i);
					//currQn.Insert(i,newNode);
				}
			}
		}
		
		if (unlocker) { // if this question unlock anything, will add to the currNode
			currNode.Add(newNode);
		}
		
		for (int i=0;i<currNode.Count;i++) { // int person will tell us to which currNode we need to attach the newNode to  
			if (currNode[i].getPerson() == person) {
				currNode[i].addNextNodes(newNode);
			}
		}*/
	}
	
    /*public void addWeapon(string weapon, string reply)
    {
        WeaponNode temp = new WeaponNode();
        temp.setWeapon(weapon);
        temp.setReply(reply);
        weaponList.Add(temp);
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
		
		QnNode temp = new QnNode();
		
		for(int i=0;i<startNode.Count;i++) {
			printNode(startNode[i]);
			while (startNode[i].getNextQn() != null) {
				temp = startNode[i].getNextQn();
				printNode(temp);
			}
		}
	}
	
	public void BFS()
	{
		List<QnNode> BFS = new List<QnNode>();
		int oldNodes = 0;
		for (int i=0; i<startNode.Count;i++) {
			BFS.Add(startNode[i]);
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
		Debug.Log(nodePrinted.getQn() + " " + nodePrinted.getAnswer() + " " + nodePrinted.getPerson());
	}
}
