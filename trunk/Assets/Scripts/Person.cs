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
	
	//New code here 
	private List<TimelineSlot> truthTimeline = new List<TimelineSlot>(3); //truthTimeline[0] is befMurder, 1 is durMurder and 2 is aftMurder
	private List<TimelineSlot> falseTimeline = new List<TimelineSlot>(3);
	public readonly SuspectEnum name;
	public readonly bool isVictim;
	public readonly bool isMurderer;
	public bool foundBody;
	private bool[] shouldReturnLies = {false, false, false};
	//End new code, the rest can delete if not useful
	
	//for backward compatibility with GUI
	public const int place = 0;
	public const int activity = 1;
	public const int alibi = 2;

	//to menghui: please refer to this part for your gui coding
	bool[] befUnlocked = {false,false};
	bool[] duringUnlocked = {false,false};
	bool[] aftUnlocked = {false,false};
	
	System.Random rand = new System.Random();
	
	public Person(SuspectEnum s, bool victim, bool murderer) {
		name = s;
		isVictim = victim;
		isMurderer = murderer;
	}
	
	public void setBeforeMurder(double start, RmEnum p, string activity, WpnEnum w) {
		truthTimeline.Insert(0, new TimelineSlot(start, p, activity, w));
	}
	public void setDuringMurder(double start, RmEnum p, string activity, WpnEnum w) {
		truthTimeline.Insert(1, new TimelineSlot(start, p, activity, w));
	}
	public void setAfterMurder(double start, RmEnum p, string activity, WpnEnum w) {
		truthTimeline.Insert(2, new TimelineSlot(start, p, activity, w));
	}
	public void setFakeBeforeMurder(double start, RmEnum p, string activity, WpnEnum w) {
		falseTimeline.Insert(0, new TimelineSlot(start, p, activity, w));
	}
	public void setFakeDuringMurder(double start, RmEnum p, string activity, WpnEnum w) {
		falseTimeline.Insert(1, new TimelineSlot(start, p, activity, w));
	}
	public void setFakeAfterMurder(double start, RmEnum p, string activity, WpnEnum w) {
		falseTimeline.Insert(2, new TimelineSlot(start, p, activity, w));
	}
	
	public RmEnum getBeforeMurderRoom() {
		return truthTimeline[0].place;
	}
	public RmEnum getDuringMurderRoom() {
		return truthTimeline[1].place;
	}
	public RmEnum getAfterMurderRoom() {
		return truthTimeline[2].place;
	}
	public RmEnum getFakeBeforeMurderRoom() {
		return falseTimeline[0].place;
	}
	public RmEnum getFakeDuringMurderRoom() {
		return fakeTimeline[1].place;
	}
	public RmEnum getFakeAfterMurderRoom() {
		return falseTimeline[2].place;
	}
	public TimelineSlot getBeforeMurderFact() {
		return truthTimeline[0];
	}
	public TimelineSlot getDuringMurderFact() {
		return truthTimeline[1];
	}
	public TimelineSlot getAfterMurderFact() {
		return truthTimeline[2];
	}
	public void setReturnLieBM() {
		shouldReturnLies[0] = true;
	}
	public void setReturnLieDM() {
		shouldReturnLies[1] = true;
	}
	public void setReturnLieAM() {
		shouldReturnLies[2] = true;
	}
	
	//accessors for bools
	public bool isFoundBody()
	{
		return foundBody;
	}
	public void setFoundBody(bool fb)
	{
		foundBody = fb;
	}
	
	public WpnEnum getBMWpn() {
		if (shouldReturnLies[0]) {	
			return falseTimeline[0].weapon;
		}
		else {
			return truthTimeline[0].weapon;
		}
	}
	
	public WpnEnum getDMWpn() {
		if (shouldReturnLies[1]) {	
			return falseTimeline[1].weapon;
		}
		else {
			return truthTimeline[1].weapon;
		}
	}
	
	public WpnEnum getAMWpn() {
		if (shouldReturnLies[2]) {	
			return falseTimeline[2].weapon;
		}
		else {
			return truthTimeline[2].weapon;
		}
	}
	
	public RmEnum getBMPlace() {
		if (shouldReturnLies[0]) {	
			return falseTimeline[0].place;
		}
		else {
			return truthTimeline[0].place;
		}
	}
	
	public RmEnum getAMPlace() {
		if (shouldReturnLies[1]) {	
			return falseTimeline[1].place;
		}
		else {
			return truthTimeline[1].place;
		}
	}
	
	public RmEnum getDMPlace() {
		if (shouldReturnLies[2]) {	
			return falseTimeline[2].place;
		}
		else {
			return truthTimeline[2].place;
		}
	}
	
	public RmEnum getBMActivity() {
		if (shouldReturnLies[0]) {	
			return falseTimeline[0].activity;
		}
		else {
			return truthTimeline[0].activity;
		}
	}
	
	public RmEnum getDMActivity() {
		if (shouldReturnLies[1]) {	
			return falseTimeline[1].activity;
		}
		else {
			return truthTimeline[1].activity;
		}
	}
	
	public RmEnum getAMActivity() {
		if (shouldReturnLies[2]) {	
			return falseTimeline[2].activity;
		}
		else {
			return truthTimeline[2].activity;
		}
	}
	
	public List<SuspectEnum> getBMAlibi() {
		if (shouldReturnLies[0]) {
			return falseTimeline[0].alibi;
		}
		else {
			return truthTimeline[0].alibi;
		}
	}
	
	public List<SuspectEnum> getDMAlibi() {
		if (shouldReturnLies[1]) {
			return falseTimeline[1].alibi;
		}
		else {
			return truthTimeline[1].alibi;
		}
	}
	
	public List<SuspectEnum> getAMAlibi() {
		if (shouldReturnLies[2]) {
			return falseTimeline[2].alibi;
		}
		else {
			return truthTimeline[2].alibi;
		}
	}
	
	//accessor for befMurder
	public String getBefMurder(int pos) //returns String at specified position
	{
		if (shouldReturnLies[0]) {//0 is before murder
			switch(pos) {
				case (Person.activity):
					return falseTimeline[0].activity;
					break;
				case (Person.alibi):
					return falseTimeline[0].alibi[0];
					break;
				case (Person.place):
					return falseTimeline[0].place;
					break;
			}
		} else {
			switch(pos) {
				case (Person.activity):
					return truthTimeline[0].activity;
					break;
				case (Person.alibi):
					return truthTimeline[0].alibi[0];
					break;
				case (Person.place):
					return truthTimeline[0].place;
					break;
			}
		}
	}
	
	//accessors and mutators for duringMurder	
	public String getMurder(int pos)
	{
		if (shouldReturnLies[1]) {//1 is during murder
			switch(pos) {
				case (Person.activity):
					return falseTimeline[1].activity;
					break;
				case (Person.alibi):
					return falseTimeline[1].alibi[0];
					break;
				case (Person.place):
					return falseTimeline[1].place;
					break;
			}
		} else {
			switch(pos) {
				case (Person.activity):
					return truthTimeline[1].activity;
					break;
				case (Person.alibi):
					return truthTimeline[1].alibi[0];
					break;
				case (Person.place):
					return truthTimeline[1].place;
					break;
			}
		}
	}

	//accessor for aftMurder
	public String getAftMurder(int pos)
	{
		if (shouldReturnLies[2]) {//2 is after murder
			switch(pos) {
				case (Person.activity):
					return falseTimeline[2].activity;
					break;
				case (Person.alibi):
					return falseTimeline[2].alibi[0];
					break;
				case (Person.place):
					return falseTimeline[2].place;
					break;
			}
		} else {
			switch(pos) {
				case (Person.activity):
					return truthTimeline[2].activity;
					break;
				case (Person.alibi):
					return truthTimeline[2].alibi[0];
					break;
				case (Person.place):
					return truthTimeline[2].place;
					break;
			}
		}
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
}
