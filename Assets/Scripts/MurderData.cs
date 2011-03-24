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
		
		public static readonly List<Room> room;
		static Globals() {
			room.Add(new Room("Kitchen"));
			room.Add(new Room("Living Room"));
			room.Add(new Room("Master Bedroom"));
			room.Add(new Room("Garden"));
			room.Add(new Room("Master Bedroom Toilet"));
			/*room.Add(new Room("Bedroom"));
			room.Add(new Room("Toilet"));*/
		}
	}
	
	public enum RmEnum
	{
		Kitchen,
		Living_Room,
		Master_Bedroom,
		Garden,
		MBR_Toilet/*,
		Bedroom,
		Toilet*/
	}
	
	public enum WpnEnum
	{
		Knife,
		Screwdriver,
		Towel,
		Scissors,
		Spanner
	}
	
	public class Room {
		public readonly string name; // the room name
		public readonly List<string> Generic_Activities;
		public readonly List<Weapon> WeaponList;
		public Room(string s) {
			name = s;
			if (name == "Kitchen") {
				Generic_Activities.Add("washing the dishes");
				Generic_Activities.Add("cooking");
				Generic_Activities.Add("chatting on the phone");
				Generic_Activities.Add("making tea");
			} else if (name == "Living Room") {
				Generic_Activities.Add("watching TV");
				Generic_Activities.Add("doing my work");
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
	}
	
	public class Weapon {
		public readonly string name; // the weapon name
		public readonly List<string> activity;
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
					activity.Add("cutting my hair");
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
		Maid
	}
}
