using UnityEngine;
using System.Collections;

public class WeaponNode {
	
	private string weapon;
	private string reply;
	
	public WeaponNode() {
	
	}
	
	public string getWeapon() {
		return weapon;
	}
	
	public string getReply() {
		return reply;
	}
	
	public void setWeapon(string temp) {
		weapon = temp;
	}
	
	public void setReply(string temp) {
		reply = temp;
	}
}