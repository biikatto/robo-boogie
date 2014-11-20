using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour{
	private AudioSource jacksonWins;
	private AudioSource botneyWins;
    private int p1score = 0;
    private int p2score = 0;
    private Tempo tempo;
    private bool stillPlaying = true;
    private Jackson jackson;
    private Botney botney;
    private ShowOnDownbeat showOnDownbeat;

    public int scoreLimit = 3;

    void Start(){
    	jacksonWins = GameObject.Find("Jackson wins").GetComponent<AudioSource>();
    	botneyWins = GameObject.Find("Botney wins").GetComponent<AudioSource>();
    	tempo = gameObject.GetComponent<Tempo>();
        jackson = gameObject.GetComponent<Jackson>();
        botney = gameObject.GetComponent<Botney>();
        showOnDownbeat = gameObject.GetComponent<ShowOnDownbeat>();
    }

    public void Reset(){
    	p1score = 0;
    	p2score = 0;
    	stillPlaying = true;
    }

    public int IncPlayer1Score(){
        p1score++;
        return p1score;
    }
    
    public int Player1Score(){
        return p1score;
    }

    public int IncPlayer2Score(){
        p2score++;
        return p2score;
    }

    public int Player2Score(){
        return p2score;
    }

    void Update(){
    	if(stillPlaying){
        	if(tempo.Beat() == 1){
        		if(p1score >= scoreLimit){
        			jacksonWins.Play();
            		Debug.Log("Jackson Mk Five wins!");
            		jackson.SetState(Jackson.State.Victory);
            		botney.SetState(Botney.State.Defeat);
        			//tempo.Stop();
        			stillPlaying = false;
        			showOnDownbeat.StillPlaying(false);
        		}else if(p2score >= scoreLimit){
        			botneyWins.Play();
            		Debug.Log("Botney Sparks wins!");
            		jackson.SetState(Jackson.State.Defeat);
            		botney.SetState(Botney.State.Victory);
        			//tempo.Stop();
        			stillPlaying = false;
        			showOnDownbeat.StillPlaying(false);
        		}
        	}
        }
    }
}
