using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class QnNode {
	
	protected int questionNumber;
	protected string question; // the questions and sub questions
	protected string answer; // answer to the questions
	protected int suspect; // for which person
	protected bool unlockedNode; // is this unlocked?
	
	protected List<QnNode> nextNodesList = new List<QnNode>(); // link to the next nodes that this qn can unlock
	protected QnNode nextQuestion; //link to the next question
	
	public QnNode(QnNode next) {
		nextQuestion = null;
	}
	
	public int QnNum {
		get {
			return questionNumber;
		}
		set {
			questionNumber = value;
		}
	}
	
	public string Qn {
		get {
			return question;
		}
		set {
			question = value;
		}
	}
	
	public string Ans {
		get {
			return answer;
		}
		set {
			answer = value;
		}
	}
	
	public int Sus {
		get {
			return suspect;
		}
		set {
			suspect= value;
		}
	}
	
	public bool Unlocked {
		get {
			return unlockedNode;
		}
		set {
			unlockedNode = value;
		}
	}
	
	public QnNode NextQn {
		get {
			return nextQuestion;
		}
		set {
			nextQuestion = value;
		}
	}
	
	public int getNumOfNextNodes() {
		return nextNodesList.Count;
	}
	
	public QnNode getNextNode(int i) {
		QnNode temp = new QnNode(null);
		temp = nextNodesList[i];
		return temp;
	}
	
	public void addNextNode(QnNode temp) {
		nextNodesList.Add(temp);
	}

	
	public void changeBooleanValues() {
		
		QnNode temp = new QnNode(null);

		for (int i=0;i<nextNodesList.Count; i++) {
			
			temp = nextNodesList[i];
			temp.Unlocked = true;
			nextNodesList.RemoveAt(i);
			nextNodesList.Insert(i,temp);
		}
	}
	
	public void setGUIBool() {
		GenerateTimeline.checkBool(questionNumber,suspect);
	}
}
