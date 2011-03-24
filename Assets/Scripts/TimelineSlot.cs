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
	public readonly SuspectEnum person;
	
	public TimelineSlot(double start, RmEnum p, string a, WpnEnum wpn, SuspectEnum s) {
		startTime = start;
		place = p;
		activity = a;
		weapon = wpn;
		person = s;
	}
	
	public string getStartTime() {
		int hour = (int) startTime;
		int minutes = (startTime - hour)*60;
		if (hour>12) {
			hour-=12;
			return ""+hour+":"+minutes+"pm";
		} else return ""+hour+":"+minutes+"am";
	}
}
