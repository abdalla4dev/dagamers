using UnityEngine; 
using System.Collections;

/*
mother = '0'
daughter = '1'
son = '2'
housemaid = '3'
*/

public class TreeNode : MonoBehaviour {
	
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
	//JESSICA THE TWO FUNCTIONS TT IS IMPORTANT IS THE TWO FUNCTION BELOW
	public ArrayList HumanTriggered(char suspect) {// function return correct qns to be printed out
		
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
	
	public string ClickingTriggered(char suspect, string qn) {//update boolean function when clicked on a question
		QnNode temp = new QnNode();
		
		for (int i=0;i<4;i++) {
			temp = (QnNode)startNode[i];
			if (temp.getPerson() == suspect) { // check for the person
				while (temp.getNextQn() != null) { 
					if (temp.getQn() == qn) { // check for the correct qn
						temp.changeBooleanValues(); // unlocked the values
						return temp.getAnswer();
					}
				}
				break;
			}
		}
		
		return "0";
	}

    public string weaponTriggered(string weapon)
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
	
	public void checker() {
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
	}
	
	public int BFS(QnNode start) {
		int unlocked=0;
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
			if(curr.getUnlockedNode())
				unlocked++;
		}
		return unlocked;		
	}
	
	public int DFS(QnNode start)
	{
		int unlocked=0;
		printIt(start);
		if(start.getUnlockedNode())
			unlocked++;
		if(start.getNumOfNextNodes()>0)
		{
			ArrayList next = new ArrayList();
			next = start.getNextNodeList();
			for(int i=0; i<next.Count; i++)
			{
				DFS((QnNode)next[i]);
			}
		}
			return unlocked;
	}
	
	public void printIt(QnNode node) {
		Debug.Log(node.getPerson() + " " + node.getQn() + " " + node.getAnswer());
	}
}
