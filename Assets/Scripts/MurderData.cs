using UnityEngine;
using System.Collections; 

namespace MurderData
{	
	public class Kitchen
	{
		//define the activities not associated with weapons
		public enum Kitchen_Generic_Activities
		{
			Washing_Dishes,
			Cooking,
			Chatting_On_Phone,
			Making_Tea
		}
		
		//define the weapons
		public readonly Knife knife;
		
		public Kitchen() {
			knife = new Knife(Knife.Knife_Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
		}
	}
		
	public struct Knife 
	{
		//define the possible activities with the weapon
		public enum Knife_Activities 
		{
			Cutting_Fruits,
			Pruning_Plants
		}
		public readonly Knife_Activities ka;	//store the activity associated to the room
		public Knife(Knife_Activities x) 
		{
			ka = x;	
		}
	}
	
}
