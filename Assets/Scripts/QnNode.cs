using UnityEngine;
using System.Collections; 

public class QnNode {
	
	int numOfQn;
	
	private string qn; // the questions and sub questions
	private string answer; // answer to the questions
	private char person; // for which person
	private bool unlockedNode; // is this unlocked?
	
	private ArrayList nextNodesList = new ArrayList(); // link to the next nodes that this qn can unlock
	private QnNode nextQn = new QnNode();
	
	void Start() {
	}
	
	public string getQn() {
		return qn;
	}
	
	public string getAnswer() {
		return answer;
	}
	
	public char getPerson() {
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
	
	public void setQn(string temp) {
		qn = temp;
	}

	public void setAnswer(string temp) {
		answer= temp;
	}
	
	public void setPerson(char temp) {
		person = temp;
	}
	
	public void setUnlockedNode(bool temp) {
		unlockedNode= temp; 
	}
	
	public void addNextNodes(QnNode temp) {
		nextNodesList.Add(temp);
	}
	
	public void setNextQn(QnNode temp) {
		nextQn = temp;
	}
}
