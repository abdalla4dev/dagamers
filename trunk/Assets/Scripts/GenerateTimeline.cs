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
	public static int deathTime; //24 hr clock. when murder was committed
	public static int bodyFound; //24 hr clock. when body was found.
	public static int befMurderTime;
	public static int aftMurderTime;

	public static bool someoneFoundBody = false;
	
	public static List<Person> timeline = new List<Person>(4);
	public static List<Fact> facts = new List<Fact>();
	public static List<Fact> wpnFacts = new List<Fact>(5);
	
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
	
	private static System.Random rand = new System.Random();
	
	public static String startPara;
	
	void Start() {
		initializeContradictionNum();
		initializeWeaponLocations();
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
	
//		RmEnum RHBefMurRoom;
//		RmEnum RHDurMurRoom;
//		RmEnum RHAftMurRoom;
		
		//victim was doing something
		int victimBefMurderRoom = rand.Next(0, Globals.numRooms);
		victim.setBeforeMurder(befMurderTime, (RmEnum)victimBefMurderRoom, Globals.room[victimBefMurderRoom].randomGA(), WpnEnum.None);
		//victim died
		int victimDurMurderRoom = rand.Next(0, Globals.numRooms);
		victim.setDuringMurder(deathTime, (RmEnum)victimDurMurderRoom, "dead", WpnEnum.None);
		//murderer's truth timeline
		timeline[(int)murdererEnum].setBeforeMurder(befMurderTime, Globals.randRoom((RmEnum)victimBefMurderRoom), "acquiring murder weapon", murderWeap);
		timeline[(int)murdererEnum].setDuringMurder(deathTime, (RmEnum)victimDurMurderRoom, "murder", murderWeap); //truth: murderer killed victim
		timeline[(int)murdererEnum].setAfterMurder(aftMurderTime, Globals.randRoom((RmEnum)victimDurMurderRoom), "disposing murder weapon", murderWeap);
		//murderer's lies
		timeline[(int)murdererEnum].setFakeBeforeMurder(befMurderTime, 
			timeline[(int)murdererEnum].getBeforeMurderRoom(), 
			Globals.room[(int)timeline[(int)murdererEnum].getBeforeMurderRoom()].WeaponList[(int)murderWeap].activity[0],
			murderWeap);//murderer lies about his befMurder activity
		RmEnum murdererDurMurLieRm = Globals.randRoom((RmEnum)victimDurMurderRoom);
		timeline[(int)murdererEnum].setFakeDuringMurder(deathTime, 
			murdererDurMurLieRm, 
			Globals.room[(int)murdererDurMurLieRm].WeaponList[(int)murderWeap].activity[0],
			murderWeap); //murderer lies about his DurMurder room and activity
		timeline[(int)murdererEnum].setFakeAfterMurder(aftMurderTime,
			timeline[(int)murdererEnum].getAfterMurderRoom(),
			Globals.room[(int)timeline[(int)murdererEnum].getAfterMurderRoom()].WeaponList[(int)murderWeap].activity[0],
			murderWeap); //murderer lies about his AftMurder activity
		timeline[(int)murdererEnum].setReturnLieBM();
		timeline[(int)murdererEnum].setReturnLieDM();
		timeline[(int)murdererEnum].setReturnLieAM();
		
		//find body
		int finder = rand.Next(0, Globals.numSuspects);
		timeline[finder].setFoundBody(true);
		
		//last seen
		int lastSaw = rand.Next(0, Globals.numSuspects);
		timeline[lastSaw].setLastSaw(true);
		
		switch(difficulty) {
			case GameDiffEnum.Easy: GenerateEasyGame(murdererEnum, victimBefMurderRoom, victimDurMurderRoom); break;
			case GameDiffEnum.Medium: GenerateMediumGame(murdererEnum, victimBefMurderRoom, victimDurMurderRoom); break;
			case GameDiffEnum.Hard: GenerateMediumGame(murdererEnum, victimBefMurderRoom, victimDurMurderRoom); break;
		}
		
		startPara = createStartPara(victim.getDuringMurderRoom().ToString());
		
		
		//*********** Not working anymore, cause no more redherring
		//placeWeaponsInWorld(redHerringIndex, murderer);		
		PrintMethod();
		
		AI.tree = AI.qnGenerator();
	}
	
	private static void initializeContradictionNum() {
		if (difficulty == GameDiffEnum.Easy) numContradiction = 6;
		else numContradiction = 3;
	}
	
	private static void initializeWeaponLocations() {
		knifeLoc = RmEnum.Kitchen;
		screwdriverLoc = RmEnum.Living_Room;
		towelLoc = RmEnum.Toilet_in_Master_Bedroom;
		scissorsLoc = RmEnum.Master_Bedroom;
		spannerLoc = RmEnum.Garden;
	}
	private static void placeWeapon(WpnEnum w, RmEnum r) {
		switch (w) {
			case WpnEnum.Knife: knifeLoc = r; break;
			case WpnEnum.Scissors: scissorsLoc = r; break;
			case WpnEnum.Screwdriver: screwdriverLoc = r; break;
			case WpnEnum.Spanner: spannerLoc = r; break;
			case WpnEnum.Towel: towelLoc = r; break;
		}
	}
	
	void Update()
	{
		/*Debug.Log(Time.time);
		Debug.Log(ToolBar.solveAttempts);
		counter++;
		if(counter==5)
			Time.timeScale = 0;
		if(counter==10)
			Time.timeScale = 1;*/
		if(MenuButton.gamePause)
			Time.timeScale = 0;
		else Time.timeScale = 1;
	}
	
	public static string scoreSystem()
	{
		String toReturn = "";
		double endTime = Time.time;
		endTime += ToolBar.solveAttempts*300; //every wrong attempt add 5 mins
		int bestTime = 0;
		switch(difficulty)
		{
		case GameDiffEnum.Easy:
				bestTime = 300; //5mins
			break;
		case GameDiffEnum.Medium:
				bestTime = 420; //7 mins
			break;
		case GameDiffEnum.Hard:
				bestTime = 600; //10 mins
			break;
		default:
				bestTime = 300;
			break;
		}
		if(endTime <= bestTime) 
		{
			toReturn += "We knew hiring you would be worth it!";
		}
		else if(endTime <= bestTime+120) //add 2 mins
		{
			toReturn += "But I'm sure you could do better than this.";
		}
		else
		{
			toReturn += "However we need to rethink your contract as a detective with us.";
		}
		
		return toReturn;
		
	}
	
	private static void GenerateEasyGame(SuspectEnum murdererEnum, int victimBefMurderRoom, int victimDurMurderRoom) {
		//***generate the suspects+activities in pairs***truth+fake pairs
		//need to place a person into these fake rooms, which the murderer lied about being in to create the contradiction
		int[] BMpairing,DMpairing,AMpairing; //{lie, truth, lie, truth}. index 0 is always the murderer
		//index 1 will be able to confirm that murderer was not doing what he said he was
		// what murderer was doing (murderer truthtimeline) is placed in facts in the scene
		BMpairing = genNumSequence(murdererEnum);
		DMpairing = genNumSequence(murdererEnum);
		AMpairing = genNumSequence(murdererEnum);
		timeline[BMpairing[1]].setBeforeMurder(befMurderTime, 
			timeline[BMpairing[0]].getFakeBeforeMurderRoom(), 
			Globals.room[(int)timeline[BMpairing[0]].getFakeBeforeMurderRoom()].randomGA(), 
			WpnEnum.None); //saw the murderer "acquiring murder weapon"
		timeline[BMpairing[0]].setBMFakeAlibi(timeline[BMpairing[1]].name);
		timeline[BMpairing[1]].setBMAlibi(timeline[BMpairing[0]].name);
		
		timeline[DMpairing[1]].setDuringMurder(deathTime, 
			timeline[DMpairing[0]].getFakeDuringMurderRoom(), 
			Globals.room[(int)timeline[DMpairing[0]].getFakeDuringMurderRoom()].randomGA(),
			WpnEnum.None); //did not see the murderer in where he claimed to be, and doing what he claimed to be doing
		//no alibi
		
		timeline[AMpairing[1]].setAfterMurder(deathTime, 
			timeline[AMpairing[0]].getFakeAfterMurderRoom(), 
			Globals.room[(int)timeline[AMpairing[0]].getFakeAfterMurderRoom()].randomGA(),
			WpnEnum.None); // saw the murderer "disposing murder weapon"
		timeline[AMpairing[0]].setAMFakeAlibi(timeline[AMpairing[1]].name);
		timeline[AMpairing[1]].setAMAlibi(timeline[AMpairing[0]].name);
		
		//record what index 0 was doing into facts
		//assuming all facts are accessed from CCTV in master bedrrom for now
		facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[1]].getBeforeMurderFact(), timeline[BMpairing[1]].name));
		facts.Add(new Fact(RmEnum.Kitchen, timeline[DMpairing[1]].getDuringMurderFact(), timeline[DMpairing[1]].name));
		facts.Add(new Fact(RmEnum.Kitchen, timeline[AMpairing[1]].getAfterMurderFact(), timeline[AMpairing[1]].name));
		
		//do the same for index 2 and 3 now.
		//index 2 is doing weapon activity and lies
		//index 3 is able to confirm index 2 is not doing so, and index2's truth timeline is placed into facts in the scene
		WpnEnum RHWpn = genWeap(murderWeap);
		RmEnum RHBefMurRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getBeforeMurderRoom());
		//the truth of what index 2 is doing
		timeline[BMpairing[2]].setBeforeMurder(befMurderTime, 
			RHBefMurRoom, 
			Globals.room[(int)RHBefMurRoom].WeaponList[(int)RHWpn].activity[0],
			RHWpn);
		RmEnum RHDurMurRoom = Globals.randRoom((RmEnum)victimDurMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
		timeline[DMpairing[2]].setDuringMurder(deathTime,
			RHDurMurRoom,
			Globals.room[(int)RHDurMurRoom].WeaponList[(int)RHWpn].activity[0],
			RHWpn);
		RmEnum RHAftMurRoom = Globals.randRoom(timeline[(int)murdererEnum].getAfterMurderRoom());
		timeline[AMpairing[2]].setAfterMurder(aftMurderTime, 
			RHAftMurRoom,
			Globals.room[(int)RHAftMurRoom].WeaponList[(int)RHWpn].activity[0],
			RHWpn);
		//the lies of index 2
		timeline[BMpairing[2]].setFakeBeforeMurder(befMurderTime, 
			RHBefMurRoom,
			Globals.room[(int)RHBefMurRoom].randomGA(),
			WpnEnum.None);
		timeline[DMpairing[2]].setFakeDuringMurder(deathTime,
			RHDurMurRoom,
			Globals.room[(int)RHDurMurRoom].randomGA(),
			WpnEnum.None);
		timeline[AMpairing[2]].setFakeAfterMurder(aftMurderTime, 
			RHAftMurRoom,
			Globals.room[(int)RHAftMurRoom].randomGA(),
			WpnEnum.None);
		//place index 3 into index2's room so as to disprove index2's lies
		timeline[BMpairing[3]].setBeforeMurder(befMurderTime,
			RHBefMurRoom,
			Globals.room[(int)RHBefMurRoom].randomGA(),
			WpnEnum.None);
		timeline[DMpairing[3]].setDuringMurder(deathTime,
			RHDurMurRoom,
			Globals.room[(int)RHDurMurRoom].randomGA(),
			WpnEnum.None);
		timeline[AMpairing[3]].setAfterMurder(aftMurderTime, 
			RHAftMurRoom,
			Globals.room[(int)RHAftMurRoom].randomGA(),
			WpnEnum.None);
		timeline[BMpairing[2]].setReturnLieBM();
		timeline[DMpairing[2]].setReturnLieDM();
		timeline[AMpairing[2]].setReturnLieAM();
		
		timeline[BMpairing[2]].setBMFakeAlibi(timeline[BMpairing[3]].name);
		timeline[BMpairing[3]].setBMAlibi(timeline[BMpairing[2]].name);
		timeline[DMpairing[2]].setDMFakeAlibi(timeline[DMpairing[3]].name);
		timeline[DMpairing[3]].setDMAlibi(timeline[DMpairing[2]].name);
		timeline[AMpairing[2]].setAMFakeAlibi(timeline[AMpairing[3]].name);
		timeline[AMpairing[3]].setAMAlibi(timeline[AMpairing[2]].name);
		
		//record what index 2 was doing into facts
		facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getBeforeMurderFact(), timeline[BMpairing[2]].name));
		facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getDuringMurderFact(), timeline[BMpairing[2]].name));
		facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getAfterMurderFact(), timeline[BMpairing[2]].name));
		
		//place the weapons that were used
		placeWeapon(murderWeap, timeline[(int)murdererEnum].getAfterMurderRoom());
		placeWeapon(RHWpn, timeline[AMpairing[2]].getAfterMurderRoom());
		
		wpnFacts.Insert((int)WpnEnum.Knife, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Knife, WpnEnum.Knife==RHWpn||WpnEnum.Knife==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Screwdriver, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Screwdriver, WpnEnum.Screwdriver==RHWpn||WpnEnum.Screwdriver==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Towel, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Towel, WpnEnum.Towel==RHWpn||WpnEnum.Towel==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Scissors, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Scissors, WpnEnum.Scissors==RHWpn||WpnEnum.Scissors==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Spanner, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Spanner, WpnEnum.Spanner==RHWpn||WpnEnum.Spanner==murderWeap));
	}
	
	private static void GenerateMediumGame(SuspectEnum murdererEnum, int victimBefMurderRoom, int victimDurMurderRoom) {
		//only has 3 contradictions	
		//***generate the suspects+activities in pairs***truth+fake pairs
		//need to place a person into these fake rooms, which the murderer lied about being in to create the contradiction
		int[] BMpairing,DMpairing,AMpairing; //{lie, truth, lie, truth}. index 0 is always the murderer
		//index 1 will be able to confirm that murderer was not doing what he said he was
		// what murderer was doing (murderer truthtimeline) is placed in facts in the scene
		BMpairing = genNumSequence(murdererEnum);
		DMpairing = genNumSequence(murdererEnum);
		AMpairing = genNumSequence(murdererEnum);
		
		//choose only one slot for murderer's contradiction.
		//record what index 0 was doing into facts
		//assuming all facts are accessed from CCTV in master bedrrom for now
		int chosen = rand.Next(3);
		RmEnum randomRoom;
		switch(chosen)
		{
			case 0:
				timeline[BMpairing[1]].setBeforeMurder(befMurderTime, 
					timeline[BMpairing[0]].getFakeBeforeMurderRoom(), 
					Globals.room[(int)timeline[BMpairing[0]].getFakeBeforeMurderRoom()].randomGA(), 
					WpnEnum.None); //saw the murderer "acquiring murder weapon"
			
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[1]].getBeforeMurderFact(), timeline[BMpairing[1]].name));
		
				//normal, can be doing anything.
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
				timeline[DMpairing[1]].setDuringMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[DMpairing[0]].getFakeDuringMurderRoom()].randomGA(),
					WpnEnum.None);
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getAfterMurderRoom());
				timeline[AMpairing[1]].setAfterMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[AMpairing[0]].getFakeAfterMurderRoom()].randomGA(),
					WpnEnum.None);
				facts.Add(new Fact(RmEnum.Kitchen, timeline[DMpairing[1]].getDuringMurderFact(), (SuspectEnum)BMpairing[1]));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[AMpairing[1]].getAfterMurderFact(), (SuspectEnum)AMpairing[1]));
			break;
			case 1:
				timeline[DMpairing[1]].setDuringMurder(deathTime, 
					timeline[DMpairing[0]].getFakeDuringMurderRoom(), 
					Globals.room[(int)timeline[DMpairing[0]].getFakeDuringMurderRoom()].randomGA(),
					WpnEnum.None); //did not see the murderer in where he claimed to be, and doing what he claimed to be doing
				facts.Add(new Fact(RmEnum.Kitchen, timeline[DMpairing[1]].getDuringMurderFact(), timeline[DMpairing[1]].name));
			
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
				timeline[BMpairing[1]].setBeforeMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[BMpairing[0]].getFakeBeforeMurderRoom()].randomGA(),
					WpnEnum.None);
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getAfterMurderRoom());
				timeline[AMpairing[1]].setAfterMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[AMpairing[0]].getFakeAfterMurderRoom()].randomGA(),
					WpnEnum.None);
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[1]].getBeforeMurderFact(), (SuspectEnum)BMpairing[1]));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[AMpairing[1]].getAfterMurderFact(), (SuspectEnum)AMpairing[1]));
			break;
			case 2:
				timeline[AMpairing[1]].setAfterMurder(deathTime, 
					timeline[AMpairing[0]].getFakeAfterMurderRoom(), 
					Globals.room[(int)timeline[AMpairing[0]].getFakeAfterMurderRoom()].randomGA(),
					WpnEnum.None); // saw the murderer "disposing murder weapon"
				facts.Add(new Fact(RmEnum.Kitchen, timeline[AMpairing[1]].getAfterMurderFact(), timeline[AMpairing[1]].name));
			
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
				timeline[DMpairing[1]].setDuringMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[DMpairing[0]].getFakeDuringMurderRoom()].randomGA(),
					WpnEnum.None);
				randomRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getBeforeMurderRoom());
				timeline[BMpairing[1]].setBeforeMurder(deathTime, 
					randomRoom, 
					Globals.room[(int)timeline[BMpairing[0]].getFakeBeforeMurderRoom()].randomGA(),
					WpnEnum.None);
				facts.Add(new Fact(RmEnum.Kitchen, timeline[DMpairing[1]].getDuringMurderFact(), (SuspectEnum)DMpairing[1]));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[1]].getBeforeMurderFact(), (SuspectEnum)BMpairing[1]));
					
			break;			
		}
		
		//do the same for index 2 and 3 now for remaining unchosen slot only. (cos only left 2 contradictions)
		//index 2 is doing weapon activity and lies
		//index 3 is able to confirm index 2 is not doing so, and index2's truth timeline is placed into facts in the scene
		WpnEnum RHWpn = genWeap(murderWeap);
		RmEnum RHBefMurRoom;
		RmEnum RHDurMurRoom;
		RmEnum RHAftMurRoom;
		RmEnum innRm;
		switch(chosen)
		{
			case 0:
				//truth
				RHDurMurRoom = Globals.randRoom((RmEnum)victimDurMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
				timeline[DMpairing[2]].setDuringMurder(deathTime,
					RHDurMurRoom,
					Globals.room[(int)RHDurMurRoom].WeaponList[(int)RHWpn].activity[0],
					RHWpn);
				RHAftMurRoom = Globals.randRoom(timeline[(int)murdererEnum].getAfterMurderRoom());
				timeline[AMpairing[2]].setAfterMurder(aftMurderTime, 
					RHAftMurRoom,
					Globals.room[(int)RHAftMurRoom].WeaponList[(int)RHWpn].activity[0],
					RHWpn);
				//lies
				timeline[DMpairing[2]].setFakeDuringMurder(deathTime,
				RHDurMurRoom,
				Globals.room[(int)RHDurMurRoom].randomGA(),
				WpnEnum.None);
				timeline[AMpairing[2]].setFakeAfterMurder(aftMurderTime, 
				RHAftMurRoom,
				Globals.room[(int)RHAftMurRoom].randomGA(),
				WpnEnum.None);
				//alibi
				timeline[DMpairing[3]].setDuringMurder(deathTime,
				RHDurMurRoom,
				Globals.room[(int)RHDurMurRoom].randomGA(),
				WpnEnum.None);
				timeline[AMpairing[3]].setAfterMurder(aftMurderTime, 
				RHAftMurRoom,
				Globals.room[(int)RHAftMurRoom].randomGA(),
				WpnEnum.None);
				timeline[DMpairing[2]].setReturnLieDM();
				timeline[AMpairing[2]].setReturnLieAM();
			
				//add fact
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getDuringMurderFact(), timeline[BMpairing[2]].name));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getAfterMurderFact(), timeline[BMpairing[2]].name));
			
				innRm = Globals.randRoom((RmEnum) victimDurMurderRoom, timeline[(int)murdererEnum].getBeforeMurderRoom(), timeline[BMpairing[1]].getBeforeMurderRoom());
				//non contradiction
				timeline[BMpairing[2]].setBeforeMurder(befMurderTime,
				innRm,
				Globals.room[(int)innRm].WeaponList[(int)RHWpn].activity[0],
				RHWpn);
				timeline[BMpairing[3]].setBeforeMurder(befMurderTime,
				innRm,
				Globals.room[(int)innRm].randomGA(),
				WpnEnum.None);
				
				timeline[BMpairing[2]].setBMAlibi(timeline[BMpairing[3]].name);
				timeline[BMpairing[3]].setBMAlibi(timeline[BMpairing[2]].name);
			
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[3]].getBeforeMurderFact(), timeline[BMpairing[3]].name));
				break;
		case 1:
				//truth
				RHBefMurRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getBeforeMurderRoom());
				timeline[BMpairing[2]].setBeforeMurder(befMurderTime, 
				RHBefMurRoom, 
				Globals.room[(int)RHBefMurRoom].WeaponList[(int)RHWpn].activity[0],
				RHWpn);
				RHAftMurRoom = Globals.randRoom(timeline[(int)murdererEnum].getAfterMurderRoom());
				timeline[AMpairing[2]].setAfterMurder(aftMurderTime, 
					RHAftMurRoom,
					Globals.room[(int)RHAftMurRoom].WeaponList[(int)RHWpn].activity[0],
					RHWpn);
				//lies
				timeline[BMpairing[2]].setFakeBeforeMurder(befMurderTime, 
					RHBefMurRoom,
					Globals.room[(int)RHBefMurRoom].randomGA(),
					WpnEnum.None);
				timeline[AMpairing[2]].setFakeAfterMurder(aftMurderTime, 
					RHAftMurRoom,
					Globals.room[(int)RHAftMurRoom].randomGA(),
					WpnEnum.None);
				//alibis
				timeline[BMpairing[3]].setBeforeMurder(befMurderTime,
					RHBefMurRoom,
					Globals.room[(int)RHBefMurRoom].randomGA(),
					WpnEnum.None);
				timeline[AMpairing[3]].setAfterMurder(aftMurderTime, 
					RHAftMurRoom,
					Globals.room[(int)RHAftMurRoom].randomGA(),
					WpnEnum.None);
				timeline[BMpairing[2]].setReturnLieBM();
				timeline[AMpairing[2]].setReturnLieAM();
			
				//add fact
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getBeforeMurderFact(), timeline[BMpairing[2]].name));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getAfterMurderFact(), timeline[BMpairing[2]].name));
				
				innRm = Globals.randRoom((RmEnum) victimDurMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom(), timeline[DMpairing[1]].getDuringMurderRoom());
				//non contradiction
				timeline[BMpairing[2]].setDuringMurder(deathTime,
				innRm,
				Globals.room[(int)innRm].WeaponList[(int)RHWpn].activity[0],
				RHWpn);
				timeline[BMpairing[3]].setDuringMurder(deathTime,
				innRm,
				Globals.room[(int)innRm].randomGA(),
				WpnEnum.None);
				
				timeline[DMpairing[2]].setDMAlibi(timeline[DMpairing[3]].name);
				timeline[DMpairing[3]].setDMAlibi(timeline[DMpairing[2]].name);
			
				facts.Add(new Fact(RmEnum.Kitchen, timeline[DMpairing[3]].getDuringMurderFact(), timeline[DMpairing[3]].name));
				break;
		case 2:
				//truth
				RHBefMurRoom = Globals.randRoom((RmEnum)victimBefMurderRoom, timeline[(int)murdererEnum].getBeforeMurderRoom());
				timeline[BMpairing[2]].setBeforeMurder(befMurderTime, 
					RHBefMurRoom, 
					Globals.room[(int)RHBefMurRoom].WeaponList[(int)RHWpn].activity[0],
					RHWpn);
				RHDurMurRoom = Globals.randRoom((RmEnum)victimDurMurderRoom, timeline[(int)murdererEnum].getDuringMurderRoom());
				timeline[DMpairing[2]].setDuringMurder(deathTime,
					RHDurMurRoom,
					Globals.room[(int)RHDurMurRoom].WeaponList[(int)RHWpn].activity[0],
					RHWpn);
				//lies
				timeline[BMpairing[2]].setFakeBeforeMurder(befMurderTime, 
					RHBefMurRoom,
					Globals.room[(int)RHBefMurRoom].randomGA(),
					WpnEnum.None);
				timeline[DMpairing[2]].setFakeDuringMurder(deathTime,
					RHDurMurRoom,
					Globals.room[(int)RHDurMurRoom].randomGA(),
					WpnEnum.None);
				//alibis
				timeline[BMpairing[3]].setBeforeMurder(befMurderTime,
					RHBefMurRoom,
					Globals.room[(int)RHBefMurRoom].randomGA(),
					WpnEnum.None);
				timeline[DMpairing[3]].setDuringMurder(deathTime,
					RHDurMurRoom,
					Globals.room[(int)RHDurMurRoom].randomGA(),
					WpnEnum.None);
				timeline[BMpairing[2]].setReturnLieBM();
				timeline[DMpairing[2]].setReturnLieDM();
				//facts
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getBeforeMurderFact(), timeline[BMpairing[2]].name));
				facts.Add(new Fact(RmEnum.Kitchen, timeline[BMpairing[2]].getDuringMurderFact(), timeline[BMpairing[2]].name));
				
			
				innRm = Globals.randRoom((RmEnum) victimDurMurderRoom, timeline[(int)murdererEnum].getAfterMurderRoom(), timeline[AMpairing[1]].getAfterMurderRoom());
				//non contradiction
				timeline[AMpairing[2]].setDuringMurder(aftMurderTime,
				innRm,
				Globals.room[(int)innRm].WeaponList[(int)RHWpn].activity[0],
				RHWpn);
				timeline[AMpairing[3]].setDuringMurder(aftMurderTime,
				innRm,
				Globals.room[(int)innRm].randomGA(),
				WpnEnum.None);
				
				timeline[AMpairing[2]].setAMAlibi(timeline[AMpairing[3]].name);
				timeline[AMpairing[3]].setAMAlibi(timeline[AMpairing[2]].name);
			
				facts.Add(new Fact(RmEnum.Kitchen, timeline[AMpairing[3]].getAfterMurderFact(), timeline[AMpairing[3]].name));
				break;
		}
		//place the weapons that were used
		placeWeapon(murderWeap, timeline[(int)murdererEnum].getAfterMurderRoom());
		placeWeapon(RHWpn, timeline[AMpairing[2]].getAfterMurderRoom());
		
		wpnFacts.Insert((int)WpnEnum.Knife, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Knife, WpnEnum.Knife==RHWpn||WpnEnum.Knife==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Screwdriver, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Screwdriver, WpnEnum.Screwdriver==RHWpn||WpnEnum.Screwdriver==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Towel, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Towel, WpnEnum.Towel==RHWpn||WpnEnum.Towel==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Scissors, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Scissors, WpnEnum.Scissors==RHWpn||WpnEnum.Scissors==murderWeap));
		wpnFacts.Insert((int)WpnEnum.Spanner, new Fact(timeline[AMpairing[2]].getAfterMurderFact(), timeline[AMpairing[2]].name, WpnEnum.Spanner, WpnEnum.Spanner==RHWpn||WpnEnum.Spanner==murderWeap));
	}
	private static void GenerateHardGame() {
		//only has 3 contradictions
		//need to uncover 2 facts in order to overcome a contradiction	
		
	}
	
	// Generate a murderer x from Suspects
	private SuspectEnum genMurderer() {
		return (SuspectEnum) rand.Next(0, Globals.numSuspects);
	}
	
	//generate murder weapon
	private WpnEnum genWeap() {
		return (WpnEnum) rand.Next(0, Globals.numWeapons);
	}
	
	// generate weapon different from that passed in
	private static WpnEnum genWeap(WpnEnum w) {
		WpnEnum temp;
		do {
			temp = (WpnEnum) rand.Next(0, Globals.numWeapons);
		} while(temp == w);
		return temp;
	}
	
	//generates a number sequence of 0-3. 
	// temp[0] lies, temp[1]'s truth can uncover temp[0]'s lie. 
	// temp[2] lies, temp[3]'s truth can uncover temp[2]'s lie. 
	private static int[] genNumSequence(SuspectEnum murderer) {
		int[] temp = new int[4];
		bool[] isUsed = {false, false, false, false};
		
		temp[0] = (int) murderer;
		do {
			temp[1] = rand.Next(0, Globals.numSuspects);
		} while (temp[0] == temp[1]);
		
		isUsed[temp[0]] = true;
		isUsed[temp[1]] = true;
		
		int j=2;
		for (int i=0; i<isUsed.Length; i++) {
			if (!isUsed[i]) {
				temp[j] = i;
				isUsed[i] = true;
				j++;
			}
		}
		Debug.Log("Rand seq " + temp[0] + " " +temp[1] + " " +temp[2] + " " +temp[3] + " end");
		return temp;
	}
	
	//generate time when body was found (based on deathTime)
	double genBodyFoundTime()
	{
		double minutes = rand.NextDouble() *60;
		double time = deathTime + (minutes/100);
		return time;		
	}
	
	/*void placeWeaponsInWorld(int RH, int M)
	{
		List<Vector3> positions = new List<Vector3>();
		List<RmEnum> place = new List<RmEnum>();
		List<int> usedPos = new List<int>();
		
		positions.Add(new Vector3(3.95f, 0.304f, 5.756f)); //change for knife
		place.Add(RmEnum.Living_Room);
		
		positions.Add(new Vector3(-6.397f, 0.307f, 5.028f)); //move up for sd
		place.Add(RmEnum.MBR_Toilet);
		
		positions.Add(new Vector3(1.689f, 1.636f, 0.398f));
		place.Add(RmEnum.Living_Room);
		
		positions.Add(new Vector3(-11.947f, 1.043f, -4.134f)); //move down for spanner
		place.Add(RmEnum.Garden);
		
		positions.Add(new Vector3(-2.738f, 0.935f, 4.734f));
		place.Add(RmEnum.Master_Bedroom);
		
		positions.Add(new Vector3(7.434f, 0.304f, -1.128f));//move up for scissors
		place.Add(RmEnum.Kitchen);
		
		positions.Add(new Vector3(3.822f, 1.13f, -4.796f));
		place.Add(RmEnum.Kitchen);
		
		positions.Add(new Vector3(-0.793f, 0.908f, -5.246f));
		place.Add(RmEnum.MBR_Toilet);
		
		positions.Add(new Vector3(-11.94f, 1.043f, 4.414f));
		place.Add(RmEnum.Garden);
		
		positions.Add(new Vector3(-4.98f, 0.973f, -3.933f));
		place.Add(RmEnum.Master_Bedroom);
		
		positions.Add(new Vector3(-3.521f, 0.484f, -5.913f)); //move up for towel
		place.Add(RmEnum.Master_Bedroom);
		
		positions.Add(new Vector3(-6.422f, 0.484f, 0.944f));
		place.Add(RmEnum.Master_Bedroom);
		
		String mRoom = timeline[M].getAftMurder(Person.place); //last room murderer was in
		String rhRoom= timeline[RH].getAftMurder(Person.place); //last room rh was in
		
		List<int> weapRooms = new List<int>();
		
		for(int i=0; i<place.Count; i++)
		{
			RmEnum r = place[i];
			if(mRoom==r.ToString()) //if murder room add to weapRooms
				weapRooms.Add(i);
		}
		
		int pos = weapRooms[rand.Next(0, weapRooms.Count)]; //random a position
		usedPos.Add(pos);
		Debug.Log(murderWeap + " MWEAP " + pos + " " + place[pos]);
		switch(murderWeap)
		{
			case WpnEnum.Knife:
				knife.transform.position = positions[pos];
				knifeLoc = place[pos];
				break;
			case WpnEnum.Screwdriver:
				screwdriver.transform.position = positions[pos];
				screwdriverLoc = place[pos];
				break;
			case WpnEnum.Towel:
				towel.transform.position = positions[pos];
				towelLoc = place[pos];
				break;
			case WpnEnum.Scissors:
				scissors.transform.position = positions[pos];
				scissorsLoc = place[pos];
				break;
			case WpnEnum.Spanner:
				spanner.transform.position = positions[pos];
				spannerLoc = place[pos];
				break;
			default:
				break;
		}
		
		weapRooms.Clear();
		
		for(int i=0; i<place.Count; i++)
		{
			RmEnum r = place[i];
			if(rhRoom==r.ToString())
				weapRooms.Add(i);
		}
		
		int pos2= weapRooms[rand.Next(0, weapRooms.Count)];
		usedPos.Add(pos2);
		//Debug.Log(timeline[RH].getRHWeap() + " RH " + pos2 + " " + place[pos2]);
		switch(timeline[RH].getRHWeap())
		{
			case WpnEnum.Knife:
				knife.transform.position = positions[pos2];
				knifeLoc = place[pos2];
				break;
			case WpnEnum.Screwdriver:
				screwdriver.transform.position = positions[pos2];
				screwdriverLoc = place[pos2];
				break;
			case WpnEnum.Towel:
				towel.transform.position = positions[pos2];
				towelLoc = place[pos2];
				break;
			case WpnEnum.Scissors:
				scissors.transform.position = positions[pos2];
				scissorsLoc = place[pos2];
				break;
			case WpnEnum.Spanner:
				spanner.transform.position = positions[pos2];
				spannerLoc = place[pos2];
				break;
			default:
				break;
		}
		
		foreach(WpnEnum w in Enum.GetValues(typeof(MurderData.WpnEnum)))
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
					case WpnEnum.Knife:
						knife.transform.position = positions[randPos];
						knifeLoc = place[randPos];
						Debug.Log("knife " + randPos + " " + place[randPos]);
						break;
					case WpnEnum.Screwdriver:
						screwdriver.transform.position = positions[randPos];
						screwdriverLoc = place[randPos];
					Debug.Log("sd " + randPos + " " + place[randPos]);
						break;
					case WpnEnum.Towel:
						towel.transform.position = positions[randPos];
						towelLoc = place[randPos];
					Debug.Log("t " + randPos + " " + place[randPos]);
						break;
					case WpnEnum.Scissors:
						scissors.transform.position = positions[randPos];
						scissorsLoc = place[randPos];
					Debug.Log("sc " + randPos + " " + place[randPos]);
						break;
					case WpnEnum.Spanner:
						spanner.transform.position = positions[randPos];
						spannerLoc = place[randPos];
					Debug.Log("sp " + randPos + " " + place[randPos]);
						break;
					default:
						break;
				}
			}
		}
		
	}*/
	
	private String createStartPara(String murderRoom)
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
	
	private void PrintMethod()
	{
		Debug.Log("PRINT");
		
		Debug.Log("BEFMURDER");
		for(int i=0; i<Globals.numSuspects; i++)
		{
			Debug.Log(  Enum.GetName(typeof(SuspectEnum), i) + " murderer=" + timeline[i].isMurderer + " " +timeline[i].getBefMurder(Person.place) + " " + timeline[i].getBefMurder(Person.activity) + " " + timeline[i].getBefMurder(Person.alibi));
		}
		Debug.Log("MURDER");
		for(int i=0; i<timeline.Count; i++)
		{
			Debug.Log(Enum.GetName(typeof(SuspectEnum), i)  + " murderer=" + timeline[i].isMurderer + " " + timeline[i].getMurder(Person.place) + " " + timeline[i].getMurder(Person.activity) + " " + timeline[i].getMurder(Person.alibi));
		}
		Debug.Log("AFTMURDER");
		for(int i=0; i<timeline.Count; i++)
		{
			Debug.Log(Enum.GetName(typeof(SuspectEnum), i) + " found body " + timeline[i].isFoundBody() + " murderer=" + timeline[i].isMurderer  + " " + timeline[i].getAftMurder(Person.place) + " " + timeline[i].getAftMurder(Person.activity) + " " + timeline[i].getAftMurder(Person.alibi));
		}
		
		Debug.Log("MURDER TRUTH");
		Debug.Log(  murderer.getBefMurder(Person.place) + " " + murderer.getBefMurder(Person.activity) + " " + murderer.getBefMurder(Person.alibi));
		Debug.Log( murderer.getMurder(Person.place) + " " + murderer.getMurder(Person.activity) + " " + murderer.getMurder(Person.alibi));
		Debug.Log( murderer.getAftMurder(Person.place) + " " + murderer.getAftMurder(Person.activity) + " " + murderer.getAftMurder(Person.alibi));

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
