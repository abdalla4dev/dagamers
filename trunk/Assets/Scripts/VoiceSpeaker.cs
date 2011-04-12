// Voice Speaker  (c) ZJP //
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class VoiceSpeaker : MonoBehaviour 
{
	[DllImport ("Voice_speaker", EntryPoint="VoiceAvailable")] private static extern int    VoiceAvailable();
	[DllImport ("Voice_speaker", EntryPoint="InitVoice")]      private static extern void   InitVoice();
	[DllImport ("Voice_speaker", EntryPoint="WaitUntilDone")]  private static extern int    WaitUntilDone(int millisec);
	[DllImport ("Voice_speaker", EntryPoint="FreeVoice")]      private static extern void   FreeVoice();
	[DllImport ("Voice_speaker", EntryPoint="GetVoiceCount")]  private static extern int    GetVoiceCount();
	[DllImport ("Voice_speaker", EntryPoint="GetVoiceName")]   private static extern string GetVoiceName(int index);
	[DllImport ("Voice_speaker", EntryPoint="SetVoice")]       private static extern void   SetVoice(int index);
	[DllImport ("Voice_speaker", EntryPoint="Say")]            private static extern void   Say(string ttospeak);
	[DllImport ("Voice_speaker", EntryPoint="SayAndWait")]     private static extern void   SayAndWait(string ttospeak);
	[DllImport ("Voice_speaker", EntryPoint="SpeakToFile")]    private static extern int    SpeakToFile(string filename, string ttospeak);
	[DllImport ("Voice_speaker", EntryPoint="GetVoiceState")]  private static extern int    GetVoiceState();
	[DllImport ("Voice_speaker", EntryPoint="GetVoiceVolume")] private static extern int    GetVoiceVolume();
	[DllImport ("Voice_speaker", EntryPoint="SetVoiceVolume")] private static extern void   SetVoiceVolume(int volume);
	[DllImport ("Voice_speaker", EntryPoint="GetVoiceRate")]   private static extern int    GetVoiceRate();
	[DllImport ("Voice_speaker", EntryPoint="SetVoiceRate")]   private static extern void   SetVoiceRate(int rate);
	[DllImport ("Voice_speaker", EntryPoint="PauseVoice")]     private static extern void   PauseVoice();
	[DllImport ("Voice_speaker", EntryPoint="ResumeVoice")]    private static extern void   ResumeVoice();

	public int voice_nb = 1; 

	void Start ()
	{
        if( VoiceAvailable()>0 )
        {
            InitVoice(); // init the engine
			Debug.Log ("Number of voice : "+GetVoiceCount()); // Number of voice
			
			SetVoice(voice_nb); // 0 to voiceCount - 1
			
			Debug.Log ("Voice name : "+GetVoiceName(voice_nb));
			//Say("Hello. I can speak now. My name is "+GetVoiceName(voice_nb)+". Welcome to Unity");
        }
	}
	
	void Talk(String talkStr){
		Say(talkStr);	
	}
	
    void OnDisable()
	{ 
        if( VoiceAvailable()>0 )
        {
            FreeVoice();
        }
    } 
}