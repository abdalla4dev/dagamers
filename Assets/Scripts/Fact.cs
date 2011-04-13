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
			{	toReturn = "The CCTV recorded that " + returnName((int)person) + 
					" was " + info.activity + 
						" in " + info.place;
				if(info.weapon!=WpnEnum.None)
					toReturn += " with " + info.weapon;
				toReturn += " from " + info.getStartTime() + 
					" for " + info.getDuration() + 
					".";
			}
			else
				toReturn = "The CCTV recorded that " + returnName((int)person) +
				" was in " + info.place + 
				" from " + info.getStartTime() + 
				" for " + info.getDuration() + 
				".";
			
		}
		else if(type.Contains("weapon"))
		{
			
			if(used)
				toReturn = "Forensics has verified that the " + weapon.ToString() +
					" was used recently. It could be the murder weapon.";
			if(!used)
				toReturn = "Forensics has verified that this item was not used recently.";
		
		}
		return toReturn;
	}
	
	public string returnName(int sus) {
		if (sus == -1) {
			return "Mr. Darcy";
		}
		else if (sus == 0) {
			return "Mrs. Darcy";
		}
		else if (sus == 1) {
			return "Wayne";
		}
		else if (sus == 2) {
			return "Jane";
		}
		else if (sus == 3) {
			return "Maria";
		}
		return null;
	}
}
