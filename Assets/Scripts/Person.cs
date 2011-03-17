using UnityEngine;
using System.Collections.Generic;
using System;
using MurderData;

public class Person {
	
	/*
	made up of 3 lists, befMurder, duringMurder and aftMurder (self explanatory).
	lists are made up of STRINGs, not INTs. basically because by the time i figured out how to access the activity i wanted from sth like kitchen.knife.activity i'd alr changed all to string. x.x 
	i can change it to int if u want. >.< 
	
	for all lists:
	position 0 is place (or room)
	position 1 is activity
	position 2 is alibi (not filled) [possible to extend to >2 in case got more than one witness.]
	
	genBefMurder(),genMurder(),genAftMurder() generates the respective lists. (murder refers to duringMurder)
	for each gen method:
	1. randomise out a room.
	2. according to room, find a weapon activity if murderer. else find generic activity.
		->for genMurder
			in GenerateTimeline, murdered's room generated first. murderer follows.
			if redHerring, person gets a weap activity instead. [redHerring status now only set in genMurder, but can be used in the other as well, just uncomment the code.
	
	CONSTRUCTORS
	Person(Person a) -> to facilitate building of fake alibi for murderer. all other chars probably would not use.
	Person(bool a, bool b) -> according to bools, specify if that person is murderer/murdered.
	
	ACCESSORS AND MUTATORS.
	all bools have accessors only.
	there are overloaded accessors and mutators for each list.
	accessor with no parameters returns the whole list.
	accessors with paramters changes the string in the specified position.
	
	method getWeapActivity basically takes in the room and weapon and outputs the specific activity for it. v long method cos for each num it goes through all the weaps to check if that's the weap i'm looking for.
	is there a simpler method?
	*/
	
	public const int place = 0;
	public const int activity = 1;
	public const int alibi = 2;
	
	List<String> befMurder = new List<String>();
	List<String> duringMurder = new List<String>();
	List<String> aftMurder = new List<String>();

	bool murderer, murdered;
	bool foundBody = false;
	bool redHerring = false; 
	bool fakeAlibi = false;
	//to menghui: please refer to this part for your gui coding
	bool[] befUnlocked = {false,false};
	bool[] duringUnlocked = {false,false};
	bool[] aftUnlocked = {false,false};
	
	Weapons rhWeap;
	
	System.Random rand;
	
	public Person(Person toCopy)
	{
		foreach(String s in toCopy.getBefMurder())
		befMurder.Add(s);
		foreach(String s in toCopy.getMurder())
		duringMurder.Add(s);
		foreach(String s in toCopy.getAftMurder())
		aftMurder.Add(s);
		fakeAlibi = true;
		murderer = true;
		foundBody = toCopy.foundBody;
		rhWeap = toCopy.rhWeap;
		/*befMurder = toCopy.getBefMurder();
		duringMurder = toCopy.getMurder();
		aftMurder = toCopy.getAftMurder();*/
	}
	
	public Person(bool guilty, bool dead)
	{
		murderer = guilty;
		murdered = dead;
		rand = new System.Random();
		genBefMurder();
		genMurder();
		genAftMurder();
	}
	
	/* accessors for bools*/
	public bool isFakeAlibi()
	{
		return fakeAlibi;
	}
	
	public bool isMurderer()
	{
		return murderer;
	}
	
	public bool isMurdered()
	{
		return murdered;
	}
	
	public bool isFoundBody()
	{
		return foundBody;
	}
	
	public void setFoundBody(bool fb)
	{
		foundBody = fb;
	}
	
	public bool isRedHerring()
	{
		return redHerring;
	}
	
	public void setRedHerring(bool rh)
	{
		redHerring = rh;
	}
	
	public Weapons getRHWeap()
	{
		return rhWeap;
	}
	
	public void setRHWeap(Weapons w)
	{
		rhWeap = w;
	}
	
	/*accessors and mutators for befMurder*/
	public List<String> getBefMurder() //returns whole list.
	{
		return befMurder;
	}
	
	public String getBefMurder(int pos) //returns String at specified position
	{
		return befMurder[pos];
	}
	
	public void setBefMurder(String s, int pos)
	{
		befMurder[pos] = s;
	}
	
	/*accessors and mutators for duringMurder*/
	public List<String> getMurder()	
	{
		return duringMurder; 
	}
	
	public String getMurder(int pos)
	{
		return duringMurder[pos];
	}
	
	public void setMurder(String s, int pos)
	{
		duringMurder[pos] = s;
	}
	
	/*accessors and mutators for aftMurder*/
	public List<String> getAftMurder()
	{
		return aftMurder;
	}
	
	public String getAftMurder(int pos)
	{
		return aftMurder[pos];
	}
	
	public void setAftMurder(String s, int pos)
	{
		aftMurder[pos] = s;
	}
	//menghui, the accessors start here
	public bool getBefUnlocked() {
		return befUnlocked[0] && befUnlocked[1];
	}
	
	public void setBefUnlocked(int i, bool val) {
		befUnlocked[i] = val;
	}
	
	public bool getDuringUnlocked() {
		return duringUnlocked[0] && duringUnlocked[1];
	}
	
	public void setDuringUnlocked(int i, bool val) {
		duringUnlocked[i] = val;
	}	
	
	public bool getAftUnlocked() {
		return aftUnlocked[0] && aftUnlocked[1];
	}
	
	public void setAftUnlocked(int i, bool val) {
		aftUnlocked[i] = val;
	}
	// end here
	public void createBefMurderWitness(Person witness, int wIdx, int ownIdx) {
		if (rand==null)
			rand = new System.Random();
		//shift witness to be in the same room as "this"
		witness.befMurder[place] = this.befMurder[place];
		Rooms room = (Rooms) Enum.Parse(typeof(Rooms), this.befMurder[place]);
		//give the witness a generic activity in that room
		//witness.befMurder[activity] = ((room.Generic_Activities) rand.Next(0, room.Num_Activities)).ToString();
		
		switch (room)
		{
			case Rooms.Kitchen:
				witness.setBefMurder(( (Kitchen.Generic_Activities) rand.Next(0, Kitchen.Num_Activities) ).ToString(), activity); 
				break;
			case Rooms.Living_Room:
				witness.befMurder[activity] = ( (Living_Room.Generic_Activities) rand.Next(0, Living_Room.Num_Activities) ).ToString(); 
				break;
			case Rooms.Bedroom:
				witness.befMurder[activity] = ( (Bedroom.Generic_Activities) rand.Next(0, Bedroom.Num_Activities) ).ToString(); 
				break;
			case Rooms.Garden:
				witness.befMurder[activity] = ( (Garden.Generic_Activities) rand.Next(0, Garden.Num_Activities) ).ToString(); 
				break;
			case Rooms.Toilet:		
				witness.befMurder[activity] = ( (Toilet.Generic_Activities) rand.Next(0, Toilet.Num_Activities) ).ToString(); 
				break;
			default: break;
		}
		
		//set witness.befMurder[alibi]=this.Suspect and this.befMurder[alibi]=witness.Suspect
		//but name is not stored in Person
		
		this.befMurder[alibi] = Enum.GetName(typeof(Suspects), wIdx);
		witness.befMurder[alibi] = Enum.GetName(typeof(Suspects), ownIdx);
		
/* 		Debug.Log("witness " + Enum.GetName(typeof(Suspects), otherIdx) + " " + this.befMurder[alibi]);
 * 		Debug.Log("this " + Enum.GetName(typeof(Suspects), ownIdx) + " " + witness.befMurder[alibi]);
 */
	}
	
	public void createDurMurderWitness(Person witness, int wIdx, int ownIdx) {
		if (rand==null)
			rand = new System.Random();
		//shift witness to be in the same room as "this"
		
		/*if(witness.isFoundBody() && this.duringMurder[place]!=GenerateTimeline.murderTruth.duringMurder[place])
		{
			witness.foundBody = false;
		}*/
		witness.duringMurder[place] = this.duringMurder[place];
		Rooms room = (Rooms) Enum.Parse(typeof(Rooms), this.duringMurder[place]);
		switch (room)
		{
			case Rooms.Kitchen:
				witness.duringMurder[activity] = ( (Kitchen.Generic_Activities) rand.Next(0, Kitchen.Num_Activities) ).ToString(); 
				break;
			case Rooms.Living_Room:
				witness.duringMurder[activity] = ( (Living_Room.Generic_Activities) rand.Next(0, Living_Room.Num_Activities) ).ToString(); 
				break;
			case Rooms.Bedroom:
				witness.duringMurder[activity] = ( (Bedroom.Generic_Activities) rand.Next(0, Bedroom.Num_Activities) ).ToString(); 
				break;
			case Rooms.Garden:
				witness.duringMurder[activity] = ( (Garden.Generic_Activities) rand.Next(0, Garden.Num_Activities) ).ToString(); 
				break;
			case Rooms.Toilet:
				witness.duringMurder[activity] = ( (Toilet.Generic_Activities) rand.Next(0, Toilet.Num_Activities) ).ToString(); 
				break;
			default: break;
		}
		
		//set witness.duringMurder[alibi]=this.Suspect and this.duringMurder[alibi]=witness.Suspect
		//but name is not stored in Person
		this.duringMurder[alibi] = Enum.GetName(typeof(Suspects), wIdx);
		if(this.isMurderer())
			return;
		witness.duringMurder[alibi] = Enum.GetName(typeof(Suspects), ownIdx);
	}
	
	public void makeFindBody(Person murderTruth)
	{
		this.foundBody = true;
		
		this.duringMurder[place] = murderTruth.duringMurder[place];
		
		Rooms room = (Rooms) Enum.Parse(typeof(Rooms), this.duringMurder[place]);
		
		switch (room)
		{
			case Rooms.Kitchen:
				this.duringMurder[activity] = ( (Kitchen.Generic_Activities) rand.Next(0, Kitchen.Num_Activities) ).ToString(); 
				break;
			case Rooms.Living_Room:
				this.duringMurder[activity] = ( (Living_Room.Generic_Activities) rand.Next(0, Living_Room.Num_Activities) ).ToString(); 
				break;
			case Rooms.Bedroom:
				this.duringMurder[activity] = ( (Bedroom.Generic_Activities) rand.Next(0, Bedroom.Num_Activities) ).ToString(); 
				break;
			case Rooms.Garden:
				this.duringMurder[activity] = ( (Garden.Generic_Activities) rand.Next(0, Garden.Num_Activities) ).ToString(); 
				break;
			case Rooms.Toilet:
				this.duringMurder[activity] = ( (Toilet.Generic_Activities) rand.Next(0, Toilet.Num_Activities) ).ToString(); 
				break;
			default: break;
		}
		
	}
	
	/*generates befMurder List*/
	void genBefMurder() 
	{
	 
		//randomise a room
		//int roomIndex = (int)(Rooms) rand.Next(0, GenerateTimeline.numRooms);
		//befMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));
		Rooms room = (Rooms) rand.Next(0, Globals.numRooms);
		befMurder.Add(room.ToString());
		 
		if(murderer) //if murderer get activity for murder weap in chosen room
		{
			rhWeap = GenerateTimeline.murderWeap;
			Debug.Log(rhWeap);
			befMurder.Add(getWeapActivity(room, GenerateTimeline.murderWeap));
			//befMurder.Add("null"); //alibi
			//return;
		}
	 	int test = rand.Next(2);
		//Debug.Log("TEST" + test + " " + " timeline count = " + GenerateTimeline.timeline.Count);
		if(!murdered && !murderer && !GenerateTimeline.redHerring && (test==0  || GenerateTimeline.timeline.Count==3))  //generate red herring. not sure where supposed to generate so comment out first.
	  	 {
			
			//Debug.Log("NO RH " + GenerateTimeline.timeline.Count);
			
				
				Weapons weapon;
		  		 do
		  		 {
		  			 weapon = (Weapons) rand.Next(0,Globals.numWeapons);
		  		 }while (weapon==GenerateTimeline.murderWeap);
				
				rhWeap = weapon;
		  		 
		  		 befMurder.Add(getWeapActivity(room,weapon));
		  		 GenerateTimeline.redHerring = true;
		  		 redHerring = true;
				/*befMurder.Add("null"); //alibi*/
				//Debug.Log("RH " + redHerring);
				//Debug.Log(befMurder[activity]);
			//return;*/
			
	  	}
	 
		else	//otherwise find generic activity for chosen room
		{
			//Debug.Log("NOT RH GIVE ACT");
			 switch(room)
			 {
				 case Rooms.Kitchen:
					 //befMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
					befMurder.Add( ( (Kitchen.Generic_Activities) rand.Next(0, Kitchen.Num_Activities) ).ToString() ); 
					break;
				 case Rooms.Living_Room:
					 befMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
					 break;
				 case Rooms.Bedroom:
					 befMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
					 break;
				 case Rooms.Garden:
					 befMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
					 break;
				 case Rooms.Toilet:
					 befMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
					 break;
				default:
					break;
			 }
		}
		
		
		befMurder.Add("null"); //alibi
		
		//debugging purposes.
		/*for(int i=0; i<befMurder.Count; i++)
			Debug.Log(i + " befMurder " + befMurder[i]);*/
	}
	
	/*generates duringMurder List*/
	void genMurder() {
		
		//randomise room
		Rooms room = (Rooms) rand.Next(0, Globals.numRooms);
		
		if(murdered) //if murdered, activity is murdered.
		{
			duringMurder.Add(room.ToString());
			duringMurder.Add("dead");
			/*for(int i=0; i<duringMurder.Count; i++)
				Debug.Log(i + " duringMurder " + duringMurder[i]);*/
			return;
		}
		
		if(murderer) //if murderer, set room to same as murdered, activity as murdering.
		{
			duringMurder.Add(GenerateTimeline.victim.duringMurder[place]);
			duringMurder.Add("murder");
			duringMurder.Add("null");//alibi
			/*for(int i=0; i<duringMurder.Count; i++)
				Debug.Log(i + " duringMurder " + duringMurder[i]);*/
			return;
		}
		
		/*if((rand.Next(2)/100)==0 && !GenerateTimeline.redHerring) //generate red herring.
		{
			duringMurder.Add(room.ToString());
			Weapons weapon;
			do
			{
				weapon = (Weapons) rand.Next(0, Globals.numWeapons);
			} while (weapon == GenerateTimeline.murderWeap);
			
			rhWeap = weapon;
			
			duringMurder.Add(getWeapActivity(room,weapon));
			GenerateTimeline.redHerring = true;
			redHerring = true;
			duringMurder.Add("null"); //alibi
			for(int i=0; i<duringMurder.Count; i++)
				Debug.Log(i + " duringMurder redHerring " + duringMurder[i]);
			return;
		} */
		
		if(redHerring)
		{
			duringMurder.Add(room.ToString());
			duringMurder.Add(getWeapActivity(room, rhWeap));
			duringMurder.Add("null"); //alibi.
			return;
		}
		else
		{
			duringMurder.Add(room.ToString());	//normal, if neither murderer nor murdered.
			/*if(duringMurder[place]==GenerateTimeline.victim.duringMurder[place])	//if was in the same room as dead body then found body.
			{
				foundBody = true;
				/*duringMurder.Add("Found_Body"); //if found body was an activity.
				return;
			}*/
			
			//find generic activity for chosen room
			 switch(room)
			 {
				 case Rooms.Kitchen:
					 duringMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
					 break;
				 case Rooms.Living_Room:
					 duringMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
					 break;
				 case Rooms.Bedroom:
					 duringMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
					 break;
				 case Rooms.Garden:
					 duringMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
					 break;
				 case Rooms.Toilet:
					 duringMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
					 break;
				default:
					break;
			 }
			 
			 duringMurder.Add("null"); //alibi
			
			 //debugging purposes.
			/*for(int i=0; i<duringMurder.Count; i++)
				Debug.Log(i + " duringMurder " + duringMurder[i]);*/
		 }
	} // end genMurder()
	
	/*generates aftMurder List*/
	void genAftMurder() {
		if(murdered) //cannot do anything
		{
			return;
		}
		
		//randomise room
		Rooms room = (Rooms) rand.Next(0, Globals.numRooms);
				
		if(room.ToString() == GenerateTimeline.victim.duringMurder[place])
		{
			foundBody = true;
			GenerateTimeline.someoneFoundBody = true;
		}
		
		//Debug.Log("FIND BODY " + foundBody + " " + GenerateTimeline.timeline.Count + " " +GenerateTimeline.someoneFoundBody);
		if(GenerateTimeline.timeline.Count==3 && !GenerateTimeline.someoneFoundBody)
		{
			room = (Rooms) Enum.Parse(typeof(Rooms),GenerateTimeline.victim.duringMurder[place]);
			foundBody = true;
			GenerateTimeline.someoneFoundBody = true;
		}
		
		aftMurder.Add(room.ToString());
		
		if(murderer) //murderer has activity in chosen room with murder weap. NOTE: can change cos i'm not sure if supposed to be this.
		{
			aftMurder.Add(getWeapActivity(room, GenerateTimeline.murderWeap));
			aftMurder.Add("null"); //alibi.
			return;
		}
		/*else if((rand.Next(2)/100)==0 && !GenerateTimeline.redHerring)  //generate red herring. not sure where supposed to generate so comment out first.
		 {
			 int weapon;
			 do
			 {
				 weapon = rand.Next(GenerateTimeline.numWeapons);
			 }while (weapon==GenerateTimeline.murderWeap);
			 
			 befMurder.Add(getWeapActivity(roomIndex,weapon));
			 GenerateTimeline.redHerring = true;
			 redHerring = true;
		 }*/
		if(redHerring)
		{
			aftMurder.Add(getWeapActivity(room, rhWeap));
			aftMurder.Add("null"); //alibi
			return;
		}
		else //find generic activity for chosen room
		{
			 switch(room)
			 {
				 case Rooms.Kitchen:
					 aftMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
					 break;
				 case Rooms.Living_Room:
					 aftMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
					 break;
				 case Rooms.Bedroom:
					 aftMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
					 break;
				 case Rooms.Garden:
					 aftMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
					 break;
				 case Rooms.Toilet:
					 aftMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
					 break;
				default:
					break;
			 }
		 }
		 
		 aftMurder.Add("null"); //alibi
		 
		 //debugging purposes.
		/* for(int i=0; i<aftMurder.Count; i++)
		Debug.Log(i + " aftMurder " + aftMurder[i]);*/
	}
	
	/*method to get the activity that's linked to a certain weapon and room*/
	public String getWeapActivity(Rooms room, Weapons weapon)
	{
		String s="";
		
		if(room == Rooms.Kitchen)
		{
			switch(weapon)
			{
				case Weapons.Knife:
					s = Kitchen.knife.activity.ToString();
					//act = (int)(Kitchen.knife.activity);
					//s = Enum.GetName(typeof(Knife.Activities), act);
					break;
				case Weapons.Screwdriver:
					s=Kitchen.screwdriver.activity.ToString();
					//act = (int)(Kitchen.screwdriver.activity);
					//s = Enum.GetName(typeof(Screwdriver.Activities), act);
					break;
				case Weapons.Towel:
					s = Kitchen.towel.activity.ToString();
					//act = (int)(Kitchen.towel.activity);
					//s = Enum.GetName(typeof(Towel.Activities), act);
					break;
				case Weapons.Scissors:
					s = Kitchen.scissors.activity.ToString();
					//act = (int)(Kitchen.scissors.activity);
					//s = Enum.GetName(typeof(Scissors.Activities), act);
					break;
				case Weapons.Spanner:
					s = Kitchen.spanner.activity.ToString();
					//act = (int)(Kitchen.spanner.activity);
					//s = Enum.GetName(typeof(Spanner.Activities), act);
					break;
				default:
					s = "null";
					break;
			}
		}
		else if(room==Rooms.Living_Room)
		{
			switch(weapon)
			{
				case Weapons.Knife:
					s = Living_Room.knife.activity.ToString();
					//act = (int)(Kitchen.screwdriver.activity);
					//s = Enum.GetName(typeof(Knife.Activities), act);
					break;
				case Weapons.Screwdriver:
					s = Living_Room.screwdriver.activity.ToString();
					//act = (int)(Living_Room.screwdriver.activity);
					//s = Enum.GetName(typeof(Screwdriver.Activities), act);
					break;
				case Weapons.Towel:
					s = Living_Room.towel.activity.ToString();
					//act = (int)(Living_Room.towel.activity);
					//s = Enum.GetName(typeof(Towel.Activities), act);
					break;
				case Weapons.Scissors:
					s = Living_Room.scissors.activity.ToString();
					//act = (int)(Living_Room.scissors.activity);
					//s = Enum.GetName(typeof(Scissors.Activities), act);
					break;
				case Weapons.Spanner:
					s = Living_Room.spanner.activity.ToString();
					//act = (int)(Living_Room.spanner.activity);
					//s = Enum.GetName(typeof(Spanner.Activities), act);
					break;
				default:
					s = "null";
					break;
			}
		}
		else if(room==Rooms.Bedroom)
		{
			switch(weapon)
			{
				case Weapons.Knife:
					s= Bedroom.knife.activity.ToString();
					//act = (int)(Bedroom.knife.activity);
					//s = Enum.GetName(typeof(Knife.Activities), act);
					break;
				case Weapons.Screwdriver:
					s= Bedroom.screwdriver.activity.ToString();
					//act = (int)(Bedroom.screwdriver.activity);
					//s = Enum.GetName(typeof(Screwdriver.Activities), act);
					break;
				case Weapons.Towel:
					s= Bedroom.towel.activity.ToString();
					//act = (int)(Bedroom.towel.activity);
					//s = Enum.GetName(typeof(Towel.Activities), act);
					break;
				case Weapons.Scissors:
					s= Bedroom.scissors.activity.ToString();
					//act = (int)(Bedroom.scissors.activity);
					//s = Enum.GetName(typeof(Scissors.Activities), act);
					break;
				case Weapons.Spanner:
					s= Bedroom.spanner.activity.ToString();
					//act = (int)(Bedroom.spanner.activity);
					//s = Enum.GetName(typeof(Spanner.Activities), act);
					break;
				default:
					s = "null";
					break;
			}
		}
		else if(room==Rooms.Garden)
		{
			switch(weapon)
			{
				case Weapons.Knife:
					s = Garden.knife.activity.ToString();
					//act = (int)(Garden.knife.activity);
					//s = Enum.GetName(typeof(Knife.Activities), act);
					break;
				case Weapons.Screwdriver:
					s = Garden.screwdriver.activity.ToString();
					//act = (int)(Garden.screwdriver.activity);
					//s = Enum.GetName(typeof(Screwdriver.Activities), act);
					break;
				case Weapons.Towel:
					s = Garden.towel.activity.ToString();
					//act = (int)(Garden.towel.activity);
					//s = Enum.GetName(typeof(Towel.Activities), act);
					break;
				case Weapons.Scissors:
					s = Garden.scissors.activity.ToString();
					//act = (int)(Garden.scissors.activity);
					//s = Enum.GetName(typeof(Scissors.Activities), act);
					break;
				case Weapons.Spanner:
					s = Garden.spanner.activity.ToString();
					//act = (int)(Garden.spanner.activity);
					//s = Enum.GetName(typeof(Spanner.Activities), act);
					break;
				default:
					s = "null";
					break;
			}
		}
		else if(room==Rooms.Toilet)
		{
			switch(weapon)
			{
				case Weapons.Knife:
					s = Toilet.knife.activity.ToString();
					//act = (int)(Toilet.knife.activity);
					//s = Enum.GetName(typeof(Knife.Activities), act);
					break;
				case Weapons.Screwdriver:
					s = Toilet.screwdriver.activity.ToString();
					//act = (int)(Toilet.screwdriver.activity);
					//s = Enum.GetName(typeof(Screwdriver.Activities), act);
					break;
				case Weapons.Towel:
					s = Toilet.towel.activity.ToString();
					//act = (int)(Toilet.towel.activity);
					//s = Enum.GetName(typeof(Towel.Activities), act);
					break;
				case Weapons.Scissors:
					s = Toilet.scissors.activity.ToString();
					//act = (int)(Toilet.scissors.activity);
					//s = Enum.GetName(typeof(Scissors.Activities), act);
					break;
				case Weapons.Spanner:
					s = Toilet.spanner.activity.ToString();
					//act = (int)(Toilet.spanner.activity);
					//s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		return s;
	}
}
