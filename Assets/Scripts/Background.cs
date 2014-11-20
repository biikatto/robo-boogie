using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour{
	private Texture2D bg1;
	private Texture2D bg2;

	private Tempo tempo;

	bool swapFrames = false;

	Rect screen;

	void Start(){
		if(swapFrames){
			bg1 = Resources.Load<Texture2D>("Textures/bg/upbeat");
			bg2 = Resources.Load<Texture2D>("Textures/bg/downbeat");
		}else{
			bg1 = Resources.Load<Texture2D>("Textures/bg/downbeat");
			bg2 = Resources.Load<Texture2D>("Textures/bg/upbeat");
		}

		tempo = gameObject.GetComponent<Tempo>();

		screen = new Rect(0, 0, Screen.width, Screen.height);
	}

	public void Draw(){
		if(tempo.BeatProgress() <= (0.5*tempo.BeatLength())){
			GUI.DrawTexture(screen, bg1);
		}else{
			GUI.DrawTexture(screen, bg2);
		}
	}
}
