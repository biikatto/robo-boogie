using UnityEngine;
using System.Collections;

public class Tempo : MonoBehaviour{
	private int beat;           // Current beat in bar
	private int totalBeats;     // Total elapsed beats
	private float beatTime;     // Seconds per beat
	private float beatProgress;
	private bool playing = false;

	private PlayerInput player1;
	private PlayerInput player2;

	public bool debug = false;
	public int beatsPerBar = 4;
	public float bpm = 93f;

	void Start(){
		beat = 0;
		totalBeats = 0;
		beatProgress = 0f;
		beatTime = 60f / bpm;
		if(debug){
			Debug.Log(beat);
		}
		player1 = GameObject.Find("Player 1").GetComponent<PlayerInput>();
		player2 = GameObject.Find("Player 2").GetComponent<PlayerInput>();
	}

	void Update(){
		if(playing){
			// Add delta time to beat progress
			beatProgress += Time.deltaTime;
			// Check if a beat has passed and take action
			if(beatProgress >= beatTime){
				beat++;
				totalBeats++;
				beatProgress -= beatTime;
				// Constrain beat to number of beats in bar
				beat %= beatsPerBar;
				if(beat == 0){
					player1.AcceptInput(false);
					player2.AcceptInput(false);
				}
				if(debug){
					Debug.Log(beat);
				}
			}
		}
	}

	public bool Play(){
		playing = true;
		return playing;
	}

	public bool Playing(){
		return playing;
	}

	public bool Stop(){
		playing = false;
		return playing;
	}

	public int Beat(){
		return beat;
	}

	public float BeatProgress(){
		return beatProgress;
	}

	public float BeatLength(){
		return beatTime;
	}

	public int BeatTotal(){
		return totalBeats;
	}
}
