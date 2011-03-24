using UnityEngine;
using System.Collections;
using System;
using MurderData;
using System.Collections.Generic;

public class GenerateTimeline : MonoBehaviour
{
	//implement the CSP solver here
	/*
	 * 1) Generate a murderer x from Suspects
	 * 2) Access the data and give x a weapon related activity one hour before murder
	 * 3) Give x a false activity to cover up for (2)
	 * 4) Give a weapon related activity to another suspect. 
	 * 5) Give an alibi for (4)
	 * 6) Fill the rest of the timeline with Generic_Activities
	 * 
	 */
	
	/*
	created PERSON class to store the room and activity of a certain char for each time period (before, during and after murder).
	can store alibi too, but haven't gotten to that yet.
	
	edited MURDERDATA to include enums for rooms and weapons.
	
	genmethods are pretty much self explanatory..
	
	in START():
	murder weapon is chosen first.
	victim's activities then generated (so as to know which room he died for use in Person class)
	murderer's activities generated (currently he's doing weapon activity before and after murder.. of course this can be changed.)
	from murderer's true activities, a fake alibi is generated (currently everything except activity and room during murder is kept the same.)
	deathTime generated.
	time body was found generated (adding minutes (<60) to deathTime)
	
	current CONSOLE PRINTS:
	victim's room + activities till death. (1 name, 4 lines)
	murderer's room + activities (truth). (1 name, 6 lines)
	then for each list in Person, prints out the room and activity of fake alibi then truth. (12 lines)
	then room + activities for each remaining suspect in order (specified in MurderData) (1 name, 6 lines each. redHerring occurs only during murder, is shown explicitly.)
	next 6 lines prints out suspect name + activity at murder hr. for murderer prints out the activity from murderTruth, fakeAlibi, and what's in the timeline (which should be equal to fakeAlibi)
	last line printed shows deathTime and time body was found(double)	
	
	WHAT I HAVEN'T DONE
	nothing that checks for the alibis of the people.
	hopefully you understand my code and it's not too messy. >.< do ask if there's sth u don't get! esp in Person, that one is much less encapsulated. bleah. >:
	
	*/
	
	public const int earliestDeath = 8; //earliest possible time that murder is committed. (can change)
	public const int latestDeath = 20; //latest possible time that murder is committed. (can change)
	
	//easy is 6 contradictions, medium is 1 real and at most 2 fake contradictions, hard is same as medium but harder to uncover fact
	public static GameDiffEnum difficulty = GameDiffEnum.Easy;
	private static int numContradiction = 0;
	
	public static Person victim;
	public static Person murderer;
	public static WpnEnum murderWeap;
	public static RmEnum murderLoc;
	public static double deathTime; //24 hr clock. when murder was committed
	public static double bodyFound; //24 hr clock. when body was found.
	public static int befMurderTime;
	public static int aftMurderTime;

	public static bool someoneFoundBody = false;
	
	public static List<Person> timeline = new List<Person>(4);
	
	public Transform knife; 
	public Transform screwdriver;
	public Transform towel;
	public Transform scissors;
	public Transform spanner;
	
	public static RmEnum knifeLoc;
	public static RmEnum screwdriverLoc;
	public static RmEnum towelLoc;
	public static RmEnum scissorsLoc;
	public static RmEnum spannerLoc;
	
	System.Random rand = new System.Random();
	
	public static String startPara;
	
	void Start() {
		initializeContradictionNum();
		SuspectEnum murdererEnum = genMurderer();
		murderWeap = genWeap();
	
		deathTime = rand.Next(earliestDeath, latestDeath);
		bodyFound = deathTime+1;//genBodyFoundTime();
		befMurderTime = deathTime-1;
		aftMurderTime = deathTime+1;
		
		victim = new Person(SuspectEnum.Father, true, false);
		murderer = new Person(murdererEnum, false, true);
		for (int i=0; i<Globals.numSuspects; i++) {
			if (i == (int)murdererEnum) timeline.Insert(i, murderer);
			else timeline.Insert(i, new Person((SuspectEnum)i, false, false));
		}

		int victimBefMurderRoom = rand.Next(0, Globals.numRooms);
		victim.setBeforeMurder(befMurderTime, (RmEnum)victimBefMurderRoom, Globals.room[victimBefMurderRoom].randomGA(), WpnEnum.None);
		int victimDurMurderRoom = rand.Next(0, Globals.numRooms);
		victim.setDuringMurder(deathTime, victimDurMurderRoom, "dead", WpnEnum.None);
		timeline[(int)murdererEnum].setDuringMurder(deathTime, victimDurMurderRoom, "murder", murderWeap); //truth: murderer killed victim
		RmEnum murdererDurMurLieRm = Globals.randRoom((RmEnum)victimDurMurderRoom);
		timeline[(int)murdererEnum].setFakeDuringMurder(deathTime, 
			murdererDurMurLieRm, 
			Globals.room[(int)murdererDurMurLie].WeaponList[(int)murderWeap].activity[0],
			murderWeap); //murderer lies about his DurMurder activity
		//then need to place a person into this room, which the murderer lied about being in to create the contradiction
		
		//***generate the suspects+activities in pairs***truth+fake pairs
		
		for(int i=0; i<Globals.numSuspects; i++)
		{
			if(i==murderer)
			{
				fAlibi = genFakeAlibi();
				timeline.Add(fAlibi);
			}
			else
			{
				//Debug.Log(Enum.GetName(typeof(Suspects),i));
				timeline.Add(new Person(!guilty, !murdered));
			}
		}
		
		
		int redHerringIndex;
		redHerringIndex = -1;
		
		List<int> innocents = new List<int>();
		
		for(int i=0; i<timeline.Count; i++)
		{
			/*if(timeline[i].isFoundBody())
				Debug.Log("FOUND B " + Enum.GetName(typeof(Suspects),i));*/
			if(timeline[i].isRedHerring())
				redHerringIndex = i;
			else if(timeline[i].isMurderer())
				continue;
			else
				innocents.Add(i);
		}
		/*Debug.Log("redherring " + timeline[redHerringIndex].isMurderer() + timeline[redHerringIndex].isRedHerring() + Enum.GetName(typeof(Suspects),redHerringIndex) + " " + redHerringIndex);
		Debug.Log("suspect1 "  + timeline[innocents[0]].isMurderer() + timeline[innocents[0]].isRedHerring() + Enum.GetName(typeof(Suspects),innocents[0]) + " " + innocents[0]);
		Debug.Log("suspect2 " + timeline[innocents[1]].isMurderer() + timeline[innocents[1]].isRedHerring() + Enum.GetName(typeof(Suspects),innocents[1]) + " " + innocents[1]);
		Debug.Log("murderer " + timeline[murderer].isMurderer() + timeline[murderer].isRedHerring() + Enum.GetName(typeof(Suspects),murderer) + " " + murderer);
		*/
		
		int m = rand.Next(2);
		int n;
		if(m==0)
			n = 1;
		else
			n = 0;
		
		fAlibi.createBefMurderWitness(timeline[innocents[m]], innocents[m], murderer);
		timeline[redHerringIndex].createBefMurderWitness(timeline[innocents[n]], innocents[n], redHerringIndex);
		
		m = rand.Next(2);
		if(m==0)
			n = 1;
		else
			n = 0;
		
		fAlibi.createDurMurderWitness(timeline[innocents[m]], innocents[m], murderer);
		timeline[redHerringIndex].createDurMurderWitness(timeline[innocents[n]], innocents[n], redHerringIndex);
		
		for(int i=0; i<timeline.Count; i++)
		{
			if(timeline[i].getAftMurder(Person.alibi)=="null")
				for(int j=0; j<timeline.Count; j++)
				{
					if(j!=i && timeline[i].getAftMurder(Person.place)==timeline[j].getAftMurder(Person.place))
					{
						timeline[i].setAftMurder(Enum.GetName(typeof(Suspects),j), Person.alibi);
						timeline[j].setAftMurder(Enum.GetName(typeof(Suspects),i), Person.alibi);
					}						
				}
		}
		
		startPara = createStartPara(victim.getMurder(Person.place));
		
		
		//debugging purposes.
/* 		for(int i=0; i<Globals.numSuspects; i++)
 * 		{
 * 			if(i==murderer)
 * 			{
 * 				Debug.Log("i=" + i +" "+ Enum.GetName(typeof(Suspects), i) + " truth " + murderTruth.getMurder(Person.activity));
 * 				Debug.Log("i=" + i +" "+"fake " + fAlibi.getMurder(Person.activity));
 * 				Debug.Log("i=" + i +" "+"time " + timeline[i].getMurder(Person.activity));
 * 			}
 * 			else
 * 				Debug.Log("i=" + i +" "+Enum.GetName(typeof(Suspects), i) + " " + timeline[i].getMurder(Person.activity)+ " herring="+ timeline[i].isRedHerring());
 * 		}
 */
		
		Debug.Log(deathTime + " " + bodyFound);
		
		//placeWeapons(redHerringIndex, murderer);
				
		PrintMethod();
		AI.tree = AI.qnGenerator();
	}
	
	private static void initializeContradictionNum() {
		if (difficulty == GameDiffEnum.Easy) numContradiction = 6;
		else numContradiction = 3;
	}
	
	// Generate a murderer x from Suspects
	private SuspectEnum genMurderer() {
		return (SuspectEnum) rand.Next(0, Globals.numSuspects);
	}
	
	//generate murder weapon
	private WpnEnum genWeap() {
		return (WpnEnum) rand.Next(0, Globals.numWeapons);
	}
	
	//generate time when body was found (based on deathTime)
	double genBodyFoundTime()
	{
		double minutes = rand.NextDouble() *60;
		double time = deathTime + (minutes/100);
		return time;		
	}
	
	//generate murderer's fake alibi.
	Person genFakeAlibi(){
		
		Person fake = new Person(murderTruth);
		int roomIndex;
		do
 		{
			roomIndex = (int)(Rooms) rand.Next(0, Globals.numRooms);
 		} while (Enum.GetName(typeof(Rooms), roomIndex)==murderTruth.getMurder(Person.place)); //random a room that is not murder room.
		
		fake.setMurder(Enum.GetName(typeof(Rooms), roomIndex), Person.place); //set murder room to new room
		
		fake.setMurder(fake.getWeapActivity((Rooms)roomIndex,murderWeap), Person.activity);
		
		/*switch (roomIndex)	//find generic activity for new room
		{
			case 0:
				 fake.setMurder(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)),Person.activity);
				 break;
			 case 1:
				 fake.setMurder(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)),Person.activity);
				 break;
			 case 2:
				 fake.setMurder(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)),Person.activity);
				 break;
			 case 3:
				 fake.setMurder(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)),Person.activity);
				 break;
			 case 4:
				 fake.setMurder(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)),Person.activity);
				 break;
			default:
				break;
		}*/
		
		//Debug.Log("FAKE timeline count " + timeline.Count + " " + !someoneFoundBody);
		if(!someoneFoundBody && timeline.Count==3)
		{
			//Debug.Log("GOT IN");
			Rooms room = (Rooms) Enum.Parse(typeof(Rooms), victim.getMurder(Person.place));
			fake.setAftMurder(room.ToString(), Person.place);
			murderTruth.setAftMurder(room.ToString(), Person.place);
			
			fake.setAftMurder(fake.getWeapActivity(room, murderWeap), Person.activity);
			murderTruth.setAftMurder(murderTruth.getWeapActivity(room, murderWeap), Person.activity);
			
			fake.setFoundBody(true);
			murderTruth.setFoundBody(true);
			
			someoneFoundBody = true;
		}
		
		if(!redHerring && timeline.Count==3)
		{
			//Debug.Log("GOT IN HERE");
			Person p = timeline[rand.Next(3)];
			
			Rooms r1 = (Rooms) Enum.Parse(typeof(Rooms),p.getBefMurder(Person.place));
			Rooms r2 = (Rooms) Enum.Parse(typeof(Rooms),p.getMurder(Person.place));
			
			Weapons rhWeap;
			do
	  		 {
	  			 rhWeap = (Weapons) rand.Next(0,Globals.numWeapons);
	  		 }while (rhWeap==murderWeap);
			
			p.setBefMurder(p.getWeapActivity(r1, rhWeap), Person.activity);
			p.setMurder(p.getWeapActivity(r2, rhWeap), Person.activity);
			
			p.setRedHerring(true);
			redHerring = true;
		}
		
		//debugging purposes.
		/*for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getBefMurder(i));
		Debug.Log(murderTruth.getBefMurder(i));}
		
		for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getMurder(i));
		Debug.Log(murderTruth.getMurder(i));}
		
		for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getAftMurder(i));
		Debug.Log(murderTruth.getAftMurder(i));}*/
		
		return fake;
	}
	
	
	
	void placeWeapons(int RH, int M)
	{
		List<Vector3> positions = new List<Vector3>();
		List<Rooms> place = new List<Rooms>();
		List<int> usedPos = new List<int>();
		
		positions.Add(new Vector3(3.95f, 0.304f, 5.756f)); //change for knife
		place.Add(Rooms.Living_Room);
		
		positions.Add(new Vector3(-6.397f, 0.307f, 5.028f)); //move up for sd
		place.Add(Rooms.Toilet);
		
		positions.Add(new Vector3(1.689f, 1.636f, 0.398f));
		place.Add(Rooms.Living_Room);
		
		positions.Add(new Vector3(-11.947f, 1.043f, -4.134f)); //move down for spanner
		place.Add(Rooms.Garden);
		
		positions.Add(new Vector3(-2.738f, 0.935f, 4.734f));
		place.Add(Rooms.Bedroom);
		
		positions.Add(new Vector3(7.434f, 0.304f, -1.128f));//move up for scissors
		place.Add(Rooms.Kitchen);
		
		positions.Add(new Vector3(3.822f, 1.13f, -4.796f));
		place.Add(Rooms.Kitchen);
		
		positions.Add(new Vector3(-0.793f, 0.908f, -5.246f));
		place.Add(Rooms.Toilet);
		
		positions.Add(new Vector3(-11.94f, 1.043f, 4.414f));
		place.Add(Rooms.Garden);
		
		positions.Add(new Vector3(-4.98f, 0.973f, -3.933f));
		place.Add(Rooms.Bedroom);
		
		positions.Add(new Vector3(-3.521f, 0.484f, -5.913f)); //move up for towel
		place.Add(Rooms.Bedroom);
		
		positions.Add(new Vector3(-6.422f, 0.484f, 0.944f));
		place.Add(Rooms.Bedroom);
		
		String mRoom = timeline[M].getAftMurder(Person.place); //last room murderer was in
		String rhRoom= timeline[RH].getAftMurder(Person.place); //last room rh was in
		
		List<int> weapRooms = new List<int>();
		
		for(int i=0; i<place.Count; i++)
		{
			Rooms r = place[i];
			if(mRoom==r.ToString()) //if murder room add to weapRooms
				weapRooms.Add(i);
		}
		
		int pos = weapRooms[rand.Next(0, weapRooms.Count)]; //random a position
		usedPos.Add(pos);
		Debug.Log(murderWeap + " MWEAP " + pos + " " + place[pos]);
		switch(murderWeap)
		{
			case Weapons.Knife:
				knife.transform.position = positions[pos];
				knifeLoc = place[pos];
				break;
			case Weapons.Screwdriver:
				screwdriver.transform.position = positions[pos];
				screwdriverLoc = place[pos];
				break;
			case Weapons.Towel:
				towel.transform.position = positions[pos];
				towelLoc = place[pos];
				break;
			case Weapons.Scissors:
				scissors.transform.position = positions[pos];
				scissorsLoc = place[pos];
				break;
			case Weapons.Spanner:
				spanner.transform.position = positions[pos];
				spannerLoc = place[pos];
				break;
			default:
				break;
		}
		
		weapRooms.Clear();
		
		for(int i=0; i<place.Count; i++)
		{
			Rooms r = place[i];
			if(rhRoom==r.ToString())
				weapRooms.Add(i);
		}
		
		int pos2= weapRooms[rand.Next(0, weapRooms.Count)];
		usedPos.Add(pos2);
		Debug.Log(timeline[RH].getRHWeap() + " RH " + pos2 + " " + place[pos2]);
		switch(timeline[RH].getRHWeap())
		{
			case Weapons.Knife:
				knife.transform.position = positions[pos2];
				knifeLoc = place[pos2];
				break;
			case Weapons.Screwdriver:
				screwdriver.transform.position = positions[pos2];
				screwdriverLoc = place[pos2];
				break;
			case Weapons.Towel:
				towel.transform.position = positions[pos2];
				towelLoc = place[pos2];
				break;
			case Weapons.Scissors:
				scissors.transform.position = positions[pos2];
				scissorsLoc = place[pos2];
				break;
			case Weapons.Spanner:
				spanner.transform.position = positions[pos2];
				spannerLoc = place[pos2];
				break;
			default:
				break;
		}
		
		foreach(Weapons w in Enum.GetValues(typeof(MurderData.Weapons)))
		{
			if(w==timeline[RH].getRHWeap() || w==murderWeap)
				continue;
			else
			{
				int randPos;
				do
				{
					 randPos = rand.Next(0, positions.Count);
				}while(usedPos.Contains(randPos));
				
				usedPos.Add(randPos);
				
				switch(w)
				{
					case Weapons.Knife:
						knife.transform.position = positions[randPos];
						knifeLoc = place[randPos];
						Debug.Log("knife " + randPos + " " + place[randPos]);
						break;
					case Weapons.Screwdriver:
						screwdriver.transform.position = positions[randPos];
						screwdriverLoc = place[randPos];
					Debug.Log("sd " + randPos + " " + place[randPos]);
						break;
					case Weapons.Towel:
						towel.transform.position = positions[randPos];
						towelLoc = place[randPos];
					Debug.Log("t " + randPos + " " + place[randPos]);
						break;
					case Weapons.Scissors:
						scissors.transform.position = positions[randPos];
						scissorsLoc = place[randPos];
					Debug.Log("sc " + randPos + " " + place[randPos]);
						break;
					case Weapons.Spanner:
						spanner.transform.position = positions[randPos];
						spannerLoc = place[randPos];
					Debug.Log("sp " + randPos + " " + place[randPos]);
						break;
					default:
						break;
				}
			}
		}
		
	}
	
	public String createStartPara(String murderRoom)
	{
		String mRoom = murderRoom.Replace('_', ' ');
		String s = "Mr. XXX, a rich and obnoxious businessman, was found murdered in " + mRoom + " at a certain time with a certain weapon by one of his family members.";
		s += "Your goal is to interview all the family members and find out who is the murderer, what weapon did he/she murder with and when did he commit the murder.";
		return s;
	}
	
	/*METHODS TO GET RESULTS*/
	/*FOR INDIVIDUALS*/
	public static String getPersonDetails(int time, int person, int pos)	//asking a person for where, what and alibi.
	{
		if(time==0)
			return timeline[person].getBefMurder(pos);
		if(time==1)
			return timeline[person].getMurder(pos);
		if(time==2)
			return timeline[person].getAftMurder(pos);
		else return "invalid";
	}
	
	String befMurderWeap(int person,int weapon)	//asking if a person used a murder weap weapon before murder
	{
		String act = timeline[person].getBefMurder(Person.activity);
		switch(weapon)
		{
			case 0:
				foreach (string s in Enum.GetNames(typeof(Knife.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 1:
				foreach (string s in Enum.GetNames(typeof(Screwdriver.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 2:
				foreach (string s in Enum.GetNames(typeof(Towel.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 3:
				foreach (string s in Enum.GetNames(typeof(Scissors.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 4:
				foreach (string s in Enum.GetNames(typeof(Spanner.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			default:
				break;
		}
		return "null";
	}
	
	String durMurderWeap(int person,int weapon)	//asking if a person used a murder weap weapon during murder
	{
		String act = timeline[person].getMurder(Person.activity);
		switch(weapon)
		{
			case 0:
				foreach (string s in Enum.GetNames(typeof(Knife.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 1:
				foreach (string s in Enum.GetNames(typeof(Screwdriver.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 2:
				foreach (string s in Enum.GetNames(typeof(Towel.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 3:
				foreach (string s in Enum.GetNames(typeof(Scissors.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 4:
				foreach (string s in Enum.GetNames(typeof(Spanner.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			default:
				break;
		}		return "null";
	}
	
	String aftMurderWeap(int person, int weapon)	//asking if a person used a murder weap weapon after murder
	{
		String act = timeline[person].getAftMurder(Person.activity);
		switch(weapon)
		{
			case 0:
				foreach (string s in Enum.GetNames(typeof(Knife.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 1:
				foreach (string s in Enum.GetNames(typeof(Screwdriver.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 2:
				foreach (string s in Enum.GetNames(typeof(Towel.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 3:
				foreach (string s in Enum.GetNames(typeof(Scissors.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			case 4:
				foreach (string s in Enum.GetNames(typeof(Spanner.Activities))) { 
					if(s==act)
						return act;
				}
				break;
			default:
				break;
		}
		return "null";
	}
	
	/*FOR ASKING ABOUT OTHERS*/
	String getOtherPersonDetails(int time, int self, int other, int pos)	//ask someone about other person's activities.
	{
		if(time==0)
		{
			if(!timeline[other].isMurderer())
				return timeline[other].getBefMurder(pos);
			else
				return murderTruth.getBefMurder(pos);
		}
		if(time==1)
			return timeline[other].getBefMurder(pos);
		if(time==2)
		{
			if(!timeline[other].isMurderer())
				return timeline[other].getBefMurder(pos);
			else
				return murderTruth.getBefMurder(pos);
		}
		else return "invalid";
	}
	
	bool isAlibi(int time, int self, int other)	//returns if self/other can vouch for each other
	{
		if(time==0)
		{
			if(timeline[self].getBefMurder(Person.alibi) == timeline[other].getBefMurder(Person.alibi))
				return true;
			else return false;
		}
		if(time==1)
		{
			if(timeline[self].getMurder(Person.alibi) == timeline[other].getMurder(Person.alibi))
				return true;
			else return false;
		}
		if(time==2)
		{
			if(timeline[self].getAftMurder(Person.alibi) == timeline[other].getAftMurder(Person.alibi))
				return true;
			else return false;
		}
		else return false;
	}
	
	/*RETURN WEAPON LOCATION*/
	Rooms getWeapLoc(Weapons weap) 
	{
		Rooms room = Rooms.Kitchen;
		switch(weap)
		{
		case Weapons.Knife:
				room = knifeLoc;
				break;
			case Weapons.Screwdriver:
				room = screwdriverLoc;
				break;
			case Weapons.Towel:
				room = towelLoc;
				break;
			case Weapons.Scissors:
				room = scissorsLoc;
				break;
			case Weapons.Spanner:
				room = spannerLoc;
				break;
			default:
				break;
		}
		
		return room;
	}
	
	void PrintMethod()
	{
		Debug.Log("PRINT");
		
		Debug.Log("BEFMURDER");
		for(int i=0; i<Globals.numSuspects; i++)
		{
			Debug.Log(  Enum.GetName(typeof(Suspects), i) + " red herring=" + timeline[i].isRedHerring() + " murderer=" + timeline[i].isMurderer() + " " +timeline[i].getBefMurder(Person.place) + " " + timeline[i].getBefMurder(Person.activity) + " " + timeline[i].getBefMurder(Person.alibi));
		}
		Debug.Log("MURDER");
		for(int i=0; i<timeline.Count; i++)
		{
			Debug.Log(Enum.GetName(typeof(Suspects), i) +  " red herring=" + timeline[i].isRedHerring() + " murderer=" + timeline[i].isMurderer() + " " + timeline[i].getMurder(Person.place) + " " + timeline[i].getMurder(Person.activity) + " " + timeline[i].getMurder(Person.alibi));
		}
		Debug.Log("AFTMURDER");
		for(int i=0; i<timeline.Count; i++)
		{
			Debug.Log(Enum.GetName(typeof(Suspects), i) + " found body " + timeline[i].isFoundBody() + " red herring=" + timeline[i].isRedHerring() + " murderer=" + timeline[i].isMurderer()  + " " + timeline[i].getAftMurder(Person.place) + " " + timeline[i].getAftMurder(Person.activity) + " " + timeline[i].getAftMurder(Person.alibi));
		}
		
		Debug.Log("MURDER TRUTH");
		Debug.Log(  murderTruth.getBefMurder(Person.place) + " " + murderTruth.getBefMurder(Person.activity) + " " + murderTruth.getBefMurder(Person.alibi));
		Debug.Log( murderTruth.getMurder(Person.place) + " " + murderTruth.getMurder(Person.activity) + " " + murderTruth.getMurder(Person.alibi));
		Debug.Log( murderTruth.getAftMurder(Person.place) + " " + murderTruth.getAftMurder(Person.activity) + " " + murderTruth.getAftMurder(Person.alibi));

		Debug.Log("WEAPON LOCS");
		Debug.Log("knife" + knifeLoc.ToString());
		Debug.Log("screwdriver " + screwdriverLoc.ToString());
		Debug.Log("spanner " + spannerLoc.ToString());
		Debug.Log("towel " + towelLoc.ToString());
		Debug.Log("scissors " + scissorsLoc.ToString());
		
		for(int i=0; i<timeline.Count; i++)
		{
			if(timeline[i].isFoundBody())
				Debug.Log(i + " " + timeline[i].getMurder(Person.place));
			Debug.Log(timeline[i].isFoundBody() + " " + i);
		}
	}
	
	public static void checkBool(int qnNum, int sus) {
		for (int i=0;i<timeline.Count;i++) {
			if (i == sus) {
				if (qnNum == 4) {
					timeline[i].setBefUnlocked(0, true);
				}
				else if (qnNum == 6) {
					timeline[i].setBefUnlocked(1, true);
				}
				else if (qnNum == 5) {
					timeline[i].setDuringUnlocked(0, true);
				}
				else if (qnNum == 8) {
					timeline[i].setDuringUnlocked(1, true);
				}				
			}
		}
	}
	
	// Create window to tell the story
	private Rect windowRect = new Rect(200, 100, 400, 200);
	bool displayWindow = true;
	public GUIStyle windowStyle;
	public GUIStyle labelStyle;
	public GUIStyle buttonStyle;
	
	void OnGUI() {
		if (displayWindow == true)
			windowRect = GUILayout.Window(5, windowRect, DoMyWindow, "Welcome to DaDetective", windowStyle);
	}
	
	void DoMyWindow(int windowID) {
		GUILayout.Label(startPara,labelStyle);
        if (GUILayout.Button("Start Game!", buttonStyle)){
			displayWindow = !(displayWindow);
		}
    }
}
