using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MurderData;

public class Fact {

	public readonly RmEnum placeRevealed;
	public readonly TimelineSlot info;
	public readonly SuspectEnum person;
	public readonly WpnEnum weapon;
	public readonly bool used;
	
	public Fact(RmEnum rm, TimelineSlot ts, SuspectEnum personn) {
		placeRevealed = rm;
		info = ts;
		person = personn;
	}
	
	public Fact(TimelineSlot ts, SuspectEnum p, WpnEnum w, bool use)
	{
		info = ts;
		person = p;
		weapon = w;
		used = use;
	}
	
	public string revealInfo(string type) {
		string toReturn = "";
		if(type.Equals("CCTV"))
		{
			if(GenerateTimeline.difficulty!=GameDiffEnum.Hard)
			{	toReturn = "The CCTV recorded that " + person + 
					" was doing " + info.activity + 
						" in " + info.place;
				if(info.weapon!=WpnEnum.None)
					toReturn += " with " + info.weapon;
				toReturn += " from " + info.getStartTime() + 
					" for " + info.getDuration() + 
					".";
			}
			else
				toReturn = "The CCTV recorded that " + person +
				" was in " + info.place + 
				" from " + info.getStartTime() + 
				" for " + info.getDuration() + 
				".";
			
		}
		else if(type.Contains("weapon"))
		{
			
			if(used)
				toReturn = "Forensics has verified that someone has used the " + weapon.ToString() +
					" recently to " +
					info.activity + ". It could be the murder weapon.";
			else
				toReturn = "Forencis has verified that this item was not used recently.";
		
		}
		return toReturn;
	}
}
