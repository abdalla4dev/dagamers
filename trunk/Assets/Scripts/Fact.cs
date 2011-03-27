using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MurderData;

public class Fact {

	public readonly RmEnum placeRevealed;
	public readonly TimelineSlot info;
	public readonly SuspectEnum person;
	
	public Fact(RmEnum rm, TimelineSlot ts, SuspectEnum personn) {
		placeRevealed = rm;
		info = ts;
		person = personn;
	}
	
	public string revealInfo() {
		return "The CCTV recorded that " + person + 
			" was doing " + info.activity + 
				" in " + info.place + 
				" with " + info.weapon + 
				" from " + info.getStartTime() + 
				" for " + info.getDuration() + 
				".";
	}
}
