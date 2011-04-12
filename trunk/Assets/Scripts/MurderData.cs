using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

namespace MurderData
{	
	
	/*
	 * Rooms are defined here
	 */ 
	
	public static class Globals 
	{
		public const int numWeapons = 5; //self explanatory, used it so only need to change once here.
		public const int numRooms = 5;
		public const int numSuspects = 4;
		public const int numNegativePersonality = 4;
		
		public static readonly List<string> reasonHateFather = new List<string>();
		public static readonly List<string> reasonMaidHateFather = new List<string>();
		
		public static readonly List<Room> room = new List<Room>(5);
		static Globals() {
			room.Add(new Room("Kitchen"));
			room.Add(new Room("Living Room"));
			room.Add(new Room("Master Bedroom"));
			room.Add(new Room("Garden"));
			room.Add(new Room("Master Bedroom Toilet"));
			/*room.Add(new Room("Bedroom"));
			room.Add(new Room("Toilet"));*/
			
			reasonHateFather.Add("because he has been a spendthrift of late.");
			reasonHateFather.Add("because he has been thinking of donating all his wealth away.");
			reasonHateFather.Add("because he seems to have had a mistress lately.");
			
			reasonMaidHateFather.Add("because he has been giving her alot of ridiculous tasks lately.");
			reasonMaidHateFather.Add("because he caught her stealing money recently.");
			reasonMaidHateFather.Add("because he has been very critical of her work these days.");
		}
		
		public static RmEnum randRoom(RmEnum rm) {
			//return a room different from rm
			System.Random r = new System.Random();
			RmEnum toReturn;
			do {
				toReturn = (RmEnum)r.Next(0, numRooms);
			} while (toReturn == rm);
			return toReturn;
		}
		public static RmEnum randRoom(RmEnum a, RmEnum b) {
			//return a room different from a and b
			System.Random r = new System.Random();
			RmEnum toReturn;
			do {
				toReturn = (RmEnum) r.Next(0, Globals.numRooms);
			} while( toReturn==a || toReturn==b);
			return toReturn;
		}
		public static RmEnum randRoom(RmEnum a, RmEnum b, RmEnum c) {
			//return a room different from a, b and c
			System.Random r = new System.Random();
			RmEnum toReturn;
			do {
				toReturn = (RmEnum) r.Next(0, Globals.numRooms);
			} while( toReturn==a || toReturn==b || toReturn == c);
			return toReturn;
		}
	}
	
	public enum RmEnum
	{
		Kitchen,
		Living_Room,
		Master_Bedroom,
		Garden,
		Toilet_in_Master_Bedroom/*,
		Bedroom,
		Toilet*/
	}
	
	public enum WpnEnum
	{
		Knife,
		Screwdriver,
		Towel,
		Scissors,
		Spanner,
		None
	}
	
	public class Room {
		public readonly string name; // the room name
		public readonly List<string> Generic_Activities = new List<string>();
		public readonly List<Weapon> WeaponList = new List<Weapon>();
		
		System.Random r = new System.Random();
	
		public Room(string s) {
			name = s;
			if (name == "Kitchen") {
				Generic_Activities.Add("washing the dishes");
				Generic_Activities.Add("cooking");
				Generic_Activities.Add("chatting on the phone");
				Generic_Activities.Add("making tea");
			} else if (name == "Living Room") {
				Generic_Activities.Add("watching TV");
				Generic_Activities.Add("doing work");
				Generic_Activities.Add("relaxing on the couch");
			} else if (name == "Master Bedroom") {
				Generic_Activities.Add("sleeping");
				Generic_Activities.Add("chatting on the phone");
				Generic_Activities.Add("relaxing");
				Generic_Activities.Add("reading a book");
			} else if (name == "Garden") {
				Generic_Activities.Add("gardening");
				Generic_Activities.Add("enjoying the view");
				Generic_Activities.Add("watering the plants");
			} else if (name == "Master Bedroom Toilet") {
				Generic_Activities.Add("cleaning");
				Generic_Activities.Add("doing business");
			} /*else if (name == "Bedroom") {
				
			} else if (name == "Toilet") {
				
			}*/
			
			//name is already initialized to the room name
			WeaponList.Add(new Weapon("Knife", name));
			WeaponList.Add(new Weapon("Screwdriver", name));
			WeaponList.Add(new Weapon("Towel", name));
			WeaponList.Add(new Weapon("Scissors", name));
			WeaponList.Add(new Weapon("Spanner", name));
		}
		public string randomGA() {
			if(r==null)
				r = new System.Random();
			return Generic_Activities[r.Next(0, Generic_Activities.Count)];
		}
	}
	
	public class Weapon {
		public readonly string name; // the weapon name
		public readonly List<string> activity = new List<string>();
		public Weapon(string wpn, string room) {
			name = wpn;
			if (name == "Knife") {
				if (room == "Kitchen") {
					activity.Add("cutting fruits");
				} else if (room == "Living Room") {
					activity.Add("cutting fruits");
				} else if (room == "Master Bedroom") {
					activity.Add("cutting fruits");
				} else if (room == "Garden") {
					activity.Add("cutting fruits");
				} else if (room == "Master Bedroom Toilet") {
					activity.Add("cutting fruits");
				}/* else if (room == "Bedroom") {
					
				} else if (room == "Toilet") {
					
				}*/
			} else if (name == "Screwdriver") {
				if (room == "Kitchen") {
					activity.Add("fixing the light");
				} else if (room == "Living Room") {
					activity.Add("fixing the light");
				} else if (room == "Master Bedroom") {
					activity.Add("fixing the light");
				} else if (room == "Garden") {
					activity.Add("fixing the light");
				} else if (room == "Master Bedroom Toilet") {
					activity.Add("fixing the light");
				}/* else if (room == "Bedroom") {
					
				} else if (room == "Toilet") {
					
				}*/
			} else if (name == "Towel") {
				if (room == "Kitchen") {
					activity.Add("cleaning the floor");
				} else if (room == "Living Room") {
					activity.Add("cleaning the floor");
				} else if (room == "Master Bedroom") {
					activity.Add("cleaning the floor");
				} else if (room == "Garden") {
					activity.Add("washing the car");
				} else if (room == "Master Bedroom Toilet") {
					activity.Add("bathing");
				}/* else if (room == "Bedroom") {
					
				} else if (room == "Toilet") {
					
				}*/
			} else if (name == "Scissors") {
				if (room == "Kitchen") {
					activity.Add("doing handicraft");
				} else if (room == "Living Room") {
					activity.Add("doing handicraft");
				} else if (room == "Master Bedroom") {
					activity.Add("doing handicraft");
				} else if (room == "Garden") {
					activity.Add("pruning the plants");
				} else if (room == "Master Bedroom Toilet") {
					activity.Add("cutting hair");
				}/* else if (room == "Bedroom") {
					
				} else if (room == "Toilet") {
					
				}*/
			} else if (name == "Spanner") {
				if (room == "Kitchen") {
					activity.Add("tightening the tap");
				} else if (room == "Living Room") {
					activity.Add("tightening the pipe");
				} else if (room == "Master Bedroom") {
					activity.Add("tightening the pipe");
				} else if (room == "Garden") {
					activity.Add("tightening the pipe");
				} else if (room == "Master Bedroom Toilet") {
					activity.Add("tightening the pipe");
				}/* else if (room == "Bedroom") {
					
				} else if (room == "Toilet") {
					
				}*/
			}
		}
	}
	
	public enum SuspectEnum
	{
		Wife,
		Son,
		Daughter,
		Maid,
		Father,
		None
	}
	
	public enum GameDiffEnum {
		Easy,
		Medium,
		Hard
	}
	
	public enum NegativePersonalityEnum {
		shy,
		unhappy,
		antisocial,
		angry,
		none
	}
}
