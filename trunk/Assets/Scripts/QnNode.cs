using UnityEngine;
using System.Collections; 
using MurderData;

public class QnNode {
	
	int numOfQn;
	
	private string qn; // the questions and sub questions
	private string answer; // answer to the questions
	private int person; // for which person
	private bool unlockedNode; // is this unlocked?
	
	private ArrayList nextNodesList = new ArrayList(); // link to the next nodes that this qn can unlock
	private QnNode nextQn;
	
	void Start() {
	}
	
	public string getQn() {
		return qn;
	}
	
	public string getAnswer() {
		return answer;
	}
	
	public int getPerson() {
		return person;
	}
	
	public bool getUnlockedNode() {
		return unlockedNode;
	}
	
	public QnNode getNextQn() {
		return nextQn;
	}
	
	public int getNumOfNextNodes() {
		return nextNodesList.Count;
	}
	
	public QnNode getNextNode(int i) {
		QnNode temp = new QnNode();
		temp = (QnNode)nextNodesList[i];
		return temp;
	}
	
	public void setQn(string temp) {
		qn = temp;
	}

	public void setAnswer(string temp) {
		answer= temp;
	}
	
	public void setPerson(int temp) {
		person = temp;
	}
	
	public void setUnlockedNode(bool temp) {
		unlockedNode= temp; 
	}
	
	public void addNextNodes(QnNode temp) {
		nextNodesList.Add(temp);
	}
	
	public void setNextQn(QnNode temp) {
		nextQn = new QnNode();
		nextQn = temp;
	}
	
	public void changeBooleanValues() {
		
		QnNode temp = new QnNode();
		
		for (int i=0;i<nextNodesList.Count; i++) {
			temp = (QnNode)nextNodesList[i];
			temp.setUnlockedNode(true);
		}
	}
}
