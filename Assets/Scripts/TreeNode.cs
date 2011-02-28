using UnityEngine; 
using System.Collections;

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
	ArrayList startNode = new ArrayList();
	ArrayList currNode = new ArrayList();

	// Use this for initialization
	void  Start() {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void HumanTriggered(char suspect) {// function print out correct qn
		//first find which person is triggered
		char person;
		QnNode temp = new QnNode();
		
		for (int i=0;i<4;i++) {
			temp = (QnNode)startNode[i];
			
		}
	}
	
	public void ClickingTriggered(int suspect) {//update boolean function
		
	}
	
	public QnNode getStartNode(int num) {
		return (QnNode)startNode[num];
	}
	
	public QnNode getCurrNode(int num) {
		return (QnNode)currNode[num];
	}
	
	public void addStartNode(QnNode temp) {
		startNode.Add(temp);		
	}
	
	public void addCurrNode(QnNode temp) {
		currNode.Add(temp);
	}
	
	public void printMania() {
		/*for (int i=0;i<1;i++) {
			QnNode curr = new QnNode();
			curr = (QnNode)startNode[i];
			Debug.Log(curr.getQn());
			Debug.Log(curr.getAnswer());
			Debug.Log(curr.getPerson());
			Debug.Log(curr.getNumOfNextNodes());
			if(curr.getNumOfNextNodes()>0)
				Debug.Log(curr.getNumOfNextNodes());
		}*/
		//BFS((QnNode)startNode[0]);
		DFS((QnNode)startNode[0]);
	}
	
	public void BFS(QnNode start) {
		Queue questions = new Queue();
		QnNode curr = new QnNode();
		
		questions.Enqueue(start);
		while (questions.Count>0)
		{
			curr = (QnNode) questions.Dequeue();
			if(curr.getNumOfNextNodes()>0)
			{
				ArrayList next = new ArrayList();
				next = curr.getNextNodeList();
				for(int i=0; i<next.Count; i++)
				{
					questions.Enqueue((QnNode)next[i]);
				}
			}
			printIt(curr);
		}
			
	}
	
	public void DFS(QnNode start)
	{
		printIt(start);
		if(start.getNumOfNextNodes()>0)
		{
			ArrayList next = new ArrayList();
			next = start.getNextNodeList();
			for(int i=0; i<next.Count; i++)
			{
				DFS((QnNode)next[i]);
			}
		}
			return;
	}
	
	public void printIt(QnNode node) {
		Debug.Log(node.getQn() + "qn");
	}
}
