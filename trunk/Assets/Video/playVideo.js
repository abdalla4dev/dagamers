var m : MovieTexture;

function Start() {
	yield WaitForSeconds(55);
	Application.LoadLevel("startmenu");
}

function Update () {
	m.Play();
	if(m.isPlaying == false || Input.GetKeyDown(KeyCode.Escape)){
		Application.LoadLevel("startmenu");
	}
}
function OnGUI (){
	GUI.DrawTexture (Rect (0,0.078125f*Screen.height,Screen.width,0.84f*Screen.height),m);
}