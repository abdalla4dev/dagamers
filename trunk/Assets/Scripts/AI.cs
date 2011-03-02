using UnityEngine;
using System.Collections;
using System.Xml;

/*
mother = 'm'
daughter = 'd'
son = 's'
housemaid = 'h'
*/

public class AI : MonoBehaviour{
	
	public TreeNode tree;
	
	//private XmlTextReader textReader;
	
	// Use this for initialization
	void  Start() {
		//textReader = new XmlTextReader("C:\\gameVars.xml");
		temporaryAI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void temporaryAI() {	
		//string tempo;
		//ScriptableObject.CreateInstance(typeof(TreeNode));
		tree = new TreeNode();
			
		QnNode temp = new QnNode();
		temp.setQn("Did you find the body?");
		temp.setAnswer("2.30pm");
		temp.setUnlockedNode(true);
		temp.setPerson('m');
		QnNode temp1 = new QnNode();
		temp1.setQn("Did you find the body?");
		temp1.setAnswer("2.30pm.");
		temp1.setUnlockedNode(true);
		temp1.setPerson('d');
		QnNode temp2 = new QnNode();
		temp2.setQn("Did you find the body?");
		temp2.setAnswer("No.");
		temp2.setUnlockedNode(true);
		temp2.setPerson('s');
		QnNode temp3 = new QnNode();
		temp3.setQn("Did you find the body?");
		temp3.setAnswer("No.");
		temp3.setUnlockedNode(true);
		temp3.setPerson('h');
		
		QnNode temp4 = new QnNode();
		temp4.setQn("What are you doing before 2.30pm?");
		temp4.setAnswer("I was watching TV.");
		temp4.setUnlockedNode(false);
		temp4.setPerson('m');
		QnNode temp5 = new QnNode();
		temp5.setQn("What are you doing before 2.30pm?");
		temp5.setAnswer("I was watching TV.");
		temp5.setUnlockedNode(false);
		temp5.setPerson('d');
		QnNode temp6 = new QnNode();
		temp6.setQn("What are you doing before 2.30pm?");
		temp6.setAnswer("I was outside.");
		temp6.setUnlockedNode(false);
		temp6.setPerson('s');
		QnNode temp7 = new QnNode();
		temp7.setQn("What are you doing before 2.30pm?");
		temp7.setAnswer("I was doing chorse around house.");
		temp7.setUnlockedNode(false);
		temp7.setPerson('h');
		
		QnNode temp8 = new QnNode();
		temp8.setQn("When did you last see Father?");
		temp8.setAnswer("12pm.");
		temp8.setUnlockedNode(false);
		temp8.setPerson('m');
		QnNode temp9 = new QnNode();
		temp9.setQn("When did you last see Father?");
		temp9.setAnswer("12pm.");
		temp9.setUnlockedNode(false);
		temp9.setPerson('d');
		QnNode temp10 = new QnNode();
		temp10.setQn("When did you last see Father?");
		temp10.setAnswer("12pm.");
		temp10.setUnlockedNode(false);
		temp10.setPerson('s');
		QnNode temp11 = new QnNode();
		temp11.setQn("When did you last see Father?");
		temp11.setAnswer("2pm.");
		temp11.setUnlockedNode(false);
		temp11.setPerson('h');
		
		temp.addNextNodes(temp4); //m 2
		temp.addNextNodes(temp8); // m 3
		temp1.addNextNodes(temp5); // d 2
		temp1.addNextNodes(temp9); // d 3
		temp2.addNextNodes(temp6); // s 2
		temp2.addNextNodes(temp10); // s 3
		temp3.addNextNodes(temp7); // h 2
		temp3.addNextNodes(temp11); // h 3
		
		tree.addStartNode(temp);
		tree.addStartNode(temp1);
		tree.addStartNode(temp2);
		tree.addStartNode(temp3);
		
		QnNode temp12 = new QnNode();
		temp12.setQn("For how long?");
		temp12.setAnswer("I was watching TV from 1pm to 2pm.");
		temp12.setUnlockedNode(false);
		temp12.setPerson('m');
		QnNode temp13 = new QnNode();
		temp13.setQn("Can anybody be your alibi?");
		temp13.setAnswer("Daughter.");
		temp13.setUnlockedNode(false);
		temp13.setPerson('m');
		QnNode temp14 = new QnNode();
		temp14.setQn("Where did you last see your Father?");
		temp14.setAnswer("At the living Room.");
		temp14.setUnlockedNode(false);
		temp14.setPerson('m');
		
		temp4.addNextNodes(temp12);
		temp12.addNextNodes(temp13);
		temp8.addNextNodes(temp14);
		
		QnNode temp15 = new QnNode();
		temp15.setQn("For how long?");
		temp15.setAnswer("I was watching TV from 1pm to 2pm.");
		temp15.setUnlockedNode(false);
		temp15.setPerson('d');
		QnNode temp16 = new QnNode();
		temp16.setQn("Can anybody be your alibi?");
		temp16.setAnswer("Mother.");
		temp16.setUnlockedNode(false);
		temp16.setPerson('d');
		QnNode temp17 = new QnNode();
		temp17.setQn("Where did you last see your Father?");
		temp17.setAnswer("At the living Room.");
		temp17.setUnlockedNode(false);
		temp17.setPerson('d');
		
		temp5.addNextNodes(temp15);
		temp15.addNextNodes(temp16);
		temp9.addNextNodes(temp17);
		
		QnNode temp18 = new QnNode();
		temp18.setQn("For how long?");
		temp18.setAnswer("I was outside from 1pm to 3pm.");
		temp18.setUnlockedNode(false);
		temp18.setPerson('s');
		QnNode temp19 = new QnNode();
		temp19.setQn("Can anybody be your alibi?");
		temp19.setAnswer("Maid.");
		temp19.setUnlockedNode(false);
		temp19.setPerson('s');
		QnNode temp20 = new QnNode();
		temp20.setQn("Where did you last see your Father?");
		temp20.setAnswer("At the living Room.");
		temp20.setUnlockedNode(false);
		temp20.setPerson('s');
		
		temp6.addNextNodes(temp18);
		temp18.addNextNodes(temp19);
		temp10.addNextNodes(temp20);
		
		QnNode temp21 = new QnNode();
		temp21.setQn("For how long?");
		temp21.setAnswer("I was doing chores around the house since 12pm.");
		temp21.setUnlockedNode(false);
		temp21.setPerson('h');
		QnNode temp22 = new QnNode();
		temp22.setQn("Can anybody be your alibi?");
		temp22.setAnswer("Nobody.");
		temp22.setUnlockedNode(false);
		temp22.setPerson('h');
		QnNode temp23 = new QnNode();
		temp23.setQn("Where did you last see your Father?");
		temp23.setAnswer("Bedroom.");
		temp23.setUnlockedNode(false);
		temp23.setPerson('h');
		
		temp7.addNextNodes(temp21);
		temp21.addNextNodes(temp22);
		temp11.addNextNodes(temp23);
		
		QnNode temp24 = new QnNode();
		temp24.setQn("Did you see Daughter watching TV at 2pm?");
		temp24.setAnswer("Yes, I was with her.");
		temp24.setUnlockedNode(false);
		temp24.setPerson('m');
		QnNode temp25 = new QnNode();
		temp25.setQn("Did you see Mother watching TV at 2pm?");
		temp25.setAnswer("Yes, I was with her.");
		temp25.setUnlockedNode(false);
		temp25.setPerson('d');
		QnNode temp26 = new QnNode();
		temp26.setQn("DId you see son going out at 1pm?");
		temp26.setAnswer("Yes but I only heard the car go out at 2.30pm.");
		temp26.setUnlockedNode(false);
		temp26.setPerson('h');
		
		temp13.addNextNodes(temp24);
		temp16.addNextNodes(temp25);
		temp19.addNextNodes(temp26);
		
		temp.setNextQn(temp4);
		temp4.setNextQn(temp8);
		temp8.setNextQn(temp12);
		temp12.setNextQn(temp13);
		temp13.setNextQn(temp14);
		temp14.setNextQn(temp24);
		
		temp1.setNextQn(temp5);
		temp5.setNextQn(temp9);
		temp9.setNextQn(temp15);
		temp15.setNextQn(temp16);
		temp16.setNextQn(temp17);
		temp17.setNextQn(temp25);
		
		temp2.setNextQn(temp6);
		temp6.setNextQn(temp9);
		temp9.setNextQn(temp18);
		temp18.setNextQn(temp19);
		temp19.setNextQn(temp20);
		
		
		temp3.setNextQn(temp7);
		temp7.setNextQn(temp10);
		temp10.setNextQn(temp21);
		temp21.setNextQn(temp22);
		temp22.setNextQn(temp23);
		temp23.setNextQn(temp26);
		
		tree.checker();

        //code for the weapon
        tree.addWeapon("Knife", "Why is the kitchen knifein the living room");
        tree.addWeapon("Shovel", "The shovel belongs to the garden");
        tree.addWeapon("Pipe", "The pipe seems to be at the right place");
        tree.addWeapon("ScrewDriver", "The screwdriver is supposed to be near the toolbox");
        tree.addWeapon("Belt", "This is father's favorite belt.");
        tree.addWeapon("Knife", "Why is the kitchen knifein the living room");
	}
}

