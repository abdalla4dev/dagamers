using UnityEngine;
using System.Collections.Generic;
using System;
using MurderData;

public class Person{
	
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
	
	public bool isRedHerring()
	{
		return redHerring;
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
	
	/*generates befMurder List*/
	void genBefMurder() {
	 
	//randomise a room
	 int roomIndex = (int)(Rooms) rand.Next(0, GenerateTimeline.numRooms);
	 befMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));
	 
	 if(murderer) //if murderer get activity for murder weap in chosen room
		befMurder.Add(getWeapActivity(roomIndex, GenerateTimeline.murderWeap));
/* 	 else if((rand.Next(2)/100)==0 && !GenerateTimeline.redHerring)  //generate red herring. not sure where supposed to generate so comment out first.
 * 	 {
 * 		 int weapon;
 * 		 do
 * 		 {
 * 			 weapon = rand.Next(GenerateTimeline.numWeapons);
 * 		 }while (weapon==GenerateTimeline.murderWeap);
 * 		 
 * 		 befMurder.Add(getWeapActivity(roomIndex,weapon));
 * 		 GenerateTimeline.redHerring = true;
 * 		 redHerring = true;
 * 	 }
 */
	else	//otherwise find generic activity for chosen room
	{
		 switch(roomIndex)
		 {
			 case 0:
				 befMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
				 break;
			 case 1:
				 befMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
				 break;
			 case 2:
				 befMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
				 break;
			 case 3:
				 befMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
				 break;
			 case 4:
				 befMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
				 break;
			default:
				break;
		 }
	}
	
	//debugging purposes.
	for(int i=0; i<befMurder.Count; i++)
		Debug.Log(i + " befMurder " + befMurder[i]);
	}
	
	/*generates duringMurder List*/
	void genMurder() {
		
		//randomise room
		int roomIndex = (int)(Rooms) rand.Next(0, 5);
		
		if(murdered) //if murdered, activity is murdered.
		{
			duringMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));
			duringMurder.Add("dead");
			for(int i=0; i<duringMurder.Count; i++)
			Debug.Log(i + " duringMurder " + duringMurder[i]);
			return;
		}
		
		if(murderer) //if murderer, set room to same as murdered, activity as murdering.
		{
			duringMurder.Add(GenerateTimeline.victim.duringMurder[0]);
			duringMurder.Add("murder");
			for(int i=0; i<duringMurder.Count; i++)
			Debug.Log(i + " duringMurder " + duringMurder[i]);
			return;
		}
		
		if((rand.Next(2)/100)==0 && !GenerateTimeline.redHerring) //generate red herring.
		 {
			 duringMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));
			 int weapon;
			 do
			 {
				 weapon = rand.Next(GenerateTimeline.numWeapons);
			 }while (weapon==GenerateTimeline.murderWeap);
			 
			 duringMurder.Add(getWeapActivity(roomIndex,weapon));
			 GenerateTimeline.redHerring = true;
			 redHerring = true;
			 for(int i=0; i<duringMurder.Count; i++)
			Debug.Log(i + " duringMurder redHerring " + duringMurder[i]);
			 return;
		 }
	
		else
		{
			duringMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));	//normal, if neither murderer nor murdered.
			if(duringMurder[0]==GenerateTimeline.victim.duringMurder[0])	//if was in the same room as dead body then found body.
			{
				foundBody = true;
				/*duringMurder.Add("Found_Body"); //if found body was an activity.
				return;*/
			}
			
			//find generic activity for chosen room
			 switch(roomIndex)
			 {
				 case 0:
					 duringMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
					 break;
				 case 1:
					 duringMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
					 break;
				 case 2:
					 duringMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
					 break;
				 case 3:
					 duringMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
					 break;
				 case 4:
					 duringMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
					 break;
				default:
					break;
			 }
			
			 //debugging purposes.
			for(int i=0; i<duringMurder.Count; i++)
			Debug.Log(i + " duringMurder " + duringMurder[i]);
		 }
	}
	
	/*generates aftMurder List*/
	void genAftMurder() {
		if(murdered) //cannot do anything
		{
			return;
		}
		
		//randomise room
		int roomIndex = (int)(Rooms) rand.Next(0, 5);
		aftMurder.Add(Enum.GetName(typeof(Rooms), roomIndex));
		
		if(murderer) //murderer has activity in chosen room with murder weap. NOTE: can change cos i'm not sure if supposed to be this.
		{
			aftMurder.Add(getWeapActivity(roomIndex, GenerateTimeline.murderWeap));
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
		else //find generic activity for chosen room
		{
			 switch(roomIndex)
			 {
				 case 0:
					 aftMurder.Add(Enum.GetName(typeof(Kitchen.Generic_Activities), (Kitchen.Generic_Activities) rand.Next(0,Kitchen.Num_Activities)));
					 break;
				 case 1:
					 aftMurder.Add(Enum.GetName(typeof(Living_Room.Generic_Activities), (Living_Room.Generic_Activities) rand.Next(0,Living_Room.Num_Activities)));
					 break;
				 case 2:
					 aftMurder.Add(Enum.GetName(typeof(Bedroom.Generic_Activities), (Bedroom.Generic_Activities) rand.Next(0,Bedroom.Num_Activities)));
					 break;
				 case 3:
					 aftMurder.Add(Enum.GetName(typeof(Garden.Generic_Activities), (Garden.Generic_Activities) rand.Next(0,Garden.Num_Activities)));
					 break;
				 case 4:
					 aftMurder.Add(Enum.GetName(typeof(Toilet.Generic_Activities), (Toilet.Generic_Activities) rand.Next(0,Toilet.Num_Activities)));
					 break;
				default:
					break;
			 }
		 }
		 
		 //debugging purposes.
		 for(int i=0; i<aftMurder.Count; i++)
		Debug.Log(i + " aftMurder " + aftMurder[i]);
	}
	
	/*method to get the activity that's linked to a certain weapon and room*/
	String getWeapActivity(int roomIndex, int weapon)
	{
		String s="";
		int act;
		
		if(roomIndex==0)
		{
			switch(weapon)
			{
				case 0:
					act = (int)(Kitchen.knife.activity);
					s = Enum.GetName(typeof(Knife.Activities), act);
				break;
				case 1:
					act = (int)(Kitchen.screwdriver.activity);
					s = Enum.GetName(typeof(Screwdriver.Activities), act);
				break;
				case 2:
					act = (int)(Kitchen.towel.activity);
					s = Enum.GetName(typeof(Towel.Activities), act);
				break;
				case 3:
					act = (int)(Kitchen.scissors.activity);
					s = Enum.GetName(typeof(Scissors.Activities), act);
				break;
				case 4:
					act = (int)(Kitchen.spanner.activity);
					s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		else if(roomIndex==1)
		{
			switch(weapon)
			{
				case 0:
					act = (int)(Living_Room.knife.activity);
					s = Enum.GetName(typeof(Knife.Activities), act);
				break;
				case 1:
					act = (int)(Living_Room.screwdriver.activity);
					s = Enum.GetName(typeof(Screwdriver.Activities), act);
				break;
				case 2:
					act = (int)(Living_Room.towel.activity);
					s = Enum.GetName(typeof(Towel.Activities), act);
				break;
				case 3:
					act = (int)(Living_Room.scissors.activity);
					s = Enum.GetName(typeof(Scissors.Activities), act);
				break;
				case 4:
					act = (int)(Living_Room.spanner.activity);
					s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		else if(roomIndex==2)
		{
			switch(weapon)
			{
				case 0:
					act = (int)(Bedroom.knife.activity);
					s = Enum.GetName(typeof(Knife.Activities), act);
				break;
				case 1:
					act = (int)(Bedroom.screwdriver.activity);
					s = Enum.GetName(typeof(Screwdriver.Activities), act);
				break;
				case 2:
					act = (int)(Bedroom.towel.activity);
					s = Enum.GetName(typeof(Towel.Activities), act);
				break;
				case 3:
					act = (int)(Bedroom.scissors.activity);
					s = Enum.GetName(typeof(Scissors.Activities), act);
				break;
				case 4:
					act = (int)(Bedroom.spanner.activity);
					s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		else if(roomIndex==3)
		{
			switch(weapon)
			{
				case 0:
					act = (int)(Garden.knife.activity);
					s = Enum.GetName(typeof(Knife.Activities), act);
				break;
				case 1:
					act = (int)(Garden.screwdriver.activity);
					s = Enum.GetName(typeof(Screwdriver.Activities), act);
				break;
				case 2:
					act = (int)(Garden.towel.activity);
					s = Enum.GetName(typeof(Towel.Activities), act);
				break;
				case 3:
					act = (int)(Garden.scissors.activity);
					s = Enum.GetName(typeof(Scissors.Activities), act);
				break;
				case 4:
					act = (int)(Garden.spanner.activity);
					s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		else if(roomIndex==4)
		{
			switch(weapon)
			{
				case 0:
					act = (int)(Toilet.knife.activity);
					s = Enum.GetName(typeof(Knife.Activities), act);
				break;
				case 1:
					act = (int)(Toilet.screwdriver.activity);
					s = Enum.GetName(typeof(Screwdriver.Activities), act);
				break;
				case 2:
					act = (int)(Toilet.towel.activity);
					s = Enum.GetName(typeof(Towel.Activities), act);
				break;
				case 3:
					act = (int)(Toilet.scissors.activity);
					s = Enum.GetName(typeof(Scissors.Activities), act);
				break;
				case 4:
					act = (int)(Toilet.spanner.activity);
					s = Enum.GetName(typeof(Spanner.Activities), act);
				break;
				default:
					s = "null";
					break;
			}
		}
		return s;
	}
	
	
}
