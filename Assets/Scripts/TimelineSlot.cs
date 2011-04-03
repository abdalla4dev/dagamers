using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MurderData;
using System;

public class TimelineSlot  {

	private double startTime; //normalized. eg. 1.5 means 1.30am, 14.25 means 2.15pm, etc
	private double endTime;
	
	public readonly RmEnum place;
	public readonly string activity;
	public readonly WpnEnum weapon;
	public List<SuspectEnum> alibi = new List<SuspectEnum>();
	//public readonly SuspectEnum person; //probably not needed as its stored in person alr
	
	public TimelineSlot(double start, RmEnum p, string a, WpnEnum wpn/*, SuspectEnum s*/) {
		startTime = start;
		endTime = start + 1;
		place = p;
		activity = a;
		weapon = wpn;
	//	person = s;
	}
	
	public TimelineSlot() {}
	
	public string getStartTime() {
		int hour = (int) startTime;
		int minutes = (int) ((startTime - hour)*60);
		if (hour>12) {
			hour-=12;
			return ""+hour+":"+minutes+"pm";
		} else return ""+hour+":"+minutes+"am";
	}
	
	public string getDuration() {
		return "" + (endTime-startTime) + " hour";
	}
}
