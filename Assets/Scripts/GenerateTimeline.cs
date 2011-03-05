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
	
	public static int earliestDeath = 8; //earliest possible time that murder is committed. (can change)
	public static int latestDeath = 24; //latest possible time that murder is committed. (can change)
	
	public const int numWeapons = 5; //self explanatory, used it so only need to change once here.
	public const int numRooms = 5;
	public const int numSuspects = 4;
	
	const bool guilty = true; //murderer
	const bool murdered = true; //victim
	
	public static Person victim;
	public static Person murderTruth;
	public static int murderWeap;
	public static double deathTime; //24 hr clock. when murder was committed
	public static double bodyFound; //24 hr clock. when body was found.
	
	public static bool redHerring = false; //only 1 redHerring for now, so this is to keep track if there alr is one.
	
	List<Person> timeline = new List<Person>();
	
	System.Random rand;
	
	void Start() {
		rand= new System.Random();
		int murderer = genMurderer();
		murderWeap = genWeap();
		//Debug.Log(Enum.GetName(typeof(Suspects),murderer)); //murderer name.
		Debug.Log("victim");
		victim = new Person(!guilty, murdered);
		Debug.Log("murderer " + Enum.GetName(typeof(Suspects),murderer));
		murderTruth = new Person(guilty, !murdered);
		Person fAlibi = genFakeAlibi();
		deathTime = rand.Next(earliestDeath, latestDeath);
		bodyFound = genBodyFoundTime();
		
		for(int i=0; i<numSuspects; i++)
		{
			if(i==murderer)
			{
				timeline.Add(fAlibi);
			}
			else
			{
				Debug.Log(Enum.GetName(typeof(Suspects),i));
				timeline.Add(new Person(!guilty, !murdered));
			}
		}
		
		//debugging purposes.
		for(int i=0; i<numSuspects; i++)
		{
			if(i==murderer)
			{
				Debug.Log(Enum.GetName(typeof(Suspects), i) + " truth " + murderTruth.getMurder(Person.activity));
				Debug.Log("fake " + fAlibi.getMurder(Person.activity));
				Debug.Log("time " + timeline[i].getMurder(Person.activity));
			}
			else
				Debug.Log(Enum.GetName(typeof(Suspects), i) + " " + timeline[i].getMurder(Person.activity));
		}
		
		Debug.Log(deathTime + " " + bodyFound);
				
		
	}
	
	// Generate a murderer x from Suspects
	int genMurderer(){
		return (int)(Suspects) rand.Next(0, numSuspects);
	}
	
	//generate murder weapon
	int genWeap() {
		return (int) (Weapons) rand.Next(0, numWeapons);
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
			roomIndex = (int)(Rooms) rand.Next(0, numRooms);
 		} while (Enum.GetName(typeof(Rooms), roomIndex)==murderTruth.getMurder(Person.place)); //random a room that is not murder room.
		
		fake.setMurder(Enum.GetName(typeof(Rooms), roomIndex), Person.place); //set murder room to new room
		
		switch (roomIndex)	//find generic activity for new room
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
		}
		
		//debugging purposes.
		for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getBefMurder(i));
		Debug.Log(murderTruth.getBefMurder(i));}
		
		for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getMurder(i));
		Debug.Log(murderTruth.getMurder(i));}
		
		for(int i=0; i<2; i++)
		{Debug.Log(i + " fake " + fake.getAftMurder(i));
		Debug.Log(murderTruth.getAftMurder(i));}
		
		return fake;
	}
	
}
