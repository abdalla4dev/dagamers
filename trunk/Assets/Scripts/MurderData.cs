using UnityEngine;
using System.Collections; 

namespace MurderData
{	
	/*
	 * Rooms are defined here
	 */ 
	
	public static class Kitchen
	{
		//define the activities not associated with weapons
		public enum Generic_Activities
		{
			Washing_Dishes, 	//0
			Cooking,			//1
			Chatting_On_Phone,	//2
			Making_Tea			//3
		}
		public static readonly int Num_Activities = 4;
		
		//define the weapons
		public static readonly Knife knife;
		public static readonly Screwdriver screwdriver;
		public static readonly Towel towel;
		public static readonly Scissors scissors;
		public static readonly Spanner spanner;
		static Kitchen() {
			knife = new Knife(Knife.Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
			screwdriver = new Screwdriver(Screwdriver.Activities.Fixing_Light);
			towel = new Towel(Towel.Activities.Cleaning_Floor);
			scissors = new Scissors(Scissors.Activities.Doing_Handicraft);
			spanner = new Spanner(Spanner.Activities.Tightening_Tap);
		}
	}
	
	public static class Living_Room 
	{
		public enum Generic_Activities
		{
			Watching_TV,
			Doing_Work,
			Relaxing_On_Couch
		}
		public static readonly int Num_Activities = 3;
		
		public static readonly Knife knife;
		public static readonly Screwdriver screwdriver;
		public static readonly Towel towel;
		public static readonly Scissors scissors;
		public static readonly Spanner spanner;
		static Living_Room() {
			knife = new Knife(Knife.Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
			screwdriver = new Screwdriver(Screwdriver.Activities.Fixing_Light);
			towel = new Towel(Towel.Activities.Cleaning_Floor);
			scissors = new Scissors(Scissors.Activities.Doing_Handicraft);
			spanner = new Spanner(Spanner.Activities.Tightening_Pipe);
		}
	}
	
	public static class Bedroom
	{
		public enum Generic_Activities
		{
			Sleeping,
			Chatting_On_Phone,
			Relaxing,
			Reading_Book
		}
		public static readonly int Num_Activities = 4;
		
		public static readonly Knife knife;
		public static readonly Screwdriver screwdriver;
		public static readonly Towel towel;
		public static readonly Scissors scissors;
		public static readonly Spanner spanner;
		static Bedroom() {
			knife = new Knife(Knife.Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
			screwdriver = new Screwdriver(Screwdriver.Activities.Fixing_Light);
			towel = new Towel(Towel.Activities.Cleaning_Floor);
			scissors = new Scissors(Scissors.Activities.Doing_Handicraft);
			spanner = new Spanner(Spanner.Activities.Tightening_Pipe);
		}
	}
	
	public static class Garden 
	{
		public enum Generic_Activities
		{
			Gardening,
			Enjoying_The_View,
			Watering_The_Plants
		}
		public static readonly int Num_Activities = 3;
		
		public static readonly Knife knife;
		public static readonly Screwdriver screwdriver;
		public static readonly Towel towel;
		public static readonly Scissors scissors;
		public static readonly Spanner spanner;
		static Garden() {
			knife = new Knife(Knife.Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
			screwdriver = new Screwdriver(Screwdriver.Activities.Fixing_Light);
			towel = new Towel(Towel.Activities.Washing_Car);
			scissors = new Scissors(Scissors.Activities.Pruning_Plants);
			spanner = new Spanner(Spanner.Activities.Tightening_Pipe);
		}
	}
	
	public static class Toilet
	{
		public enum Generic_Activities
		{
			Cleaning,
			Doing_Business
		}
		public static readonly int Num_Activities = 2;
		
		public static readonly Knife knife;
		public static readonly Screwdriver screwdriver;
		public static readonly Towel towel;
		public static readonly Scissors scissors;
		public static readonly Spanner spanner;
		static Toilet() {
			knife = new Knife(Knife.Activities.Cutting_Fruits);	//define the only possible activity in this room with this weapon
			screwdriver = new Screwdriver(Screwdriver.Activities.Fixing_Light);
			towel = new Towel(Towel.Activities.Bathing);
			scissors = new Scissors(Scissors.Activities.Cutting_Hair);
			spanner = new Spanner(Spanner.Activities.Tightening_Pipe);
		}
	}
	
	/*
	 * Weapons are defined here 
	 */
	
	public struct Knife 
	{
		//define the possible activities with the weapon
		public enum Activities 
		{
			Cutting_Fruits,
			Pruning_Plants
		}
		public readonly Activities activity;	//store the activity associated to the room
		public Knife(Activities x) { activity = x;	}
	}
	
	public struct Screwdriver
	{
		public enum Activities { Fixing_Light }
		public readonly Activities activity;
		public Screwdriver (Activities x) { activity = x; }
	}
	
	public struct Towel 
	{
		public enum Activities 
		{
			Cleaning_Floor,
			Washing_Car,
			Bathing
		}
		public readonly Activities activity;
		public Towel(Activities x) { activity = x; }
	}
	
	public struct Scissors 
	{
		public enum Activities 
		{
			Doing_Handicraft,
			Pruning_Plants,
			Cutting_Hair
		}
		public readonly Activities activity;
		public Scissors(Activities x) { activity = x; }
	}
	
	public struct Spanner
	{
		public enum Activities
		{
			Tightening_Tap,
			Tightening_Pipe
		}
		public readonly Activities activity;
		public Spanner(Activities x) { activity = x; }
	}
	
	/*
	 * Suspects are defined here
	 */ 
	
	public enum Suspects
	{
		Wife,
		Son,
		Daughter,
		Maid
	}
}
