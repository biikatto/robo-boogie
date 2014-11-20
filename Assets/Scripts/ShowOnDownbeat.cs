using UnityEngine;
using System.Collections;

public class ShowOnDownbeat : MonoBehaviour{
    private PlayerInput player1;
    private PlayerInput player2;
    private Tempo tempo;
    private Score score;
    private Jackson jackson;
    private Botney botney;
    private ChargeBar chargeBar;
    private bool stillPlaying = true;

    int lastBeat = -1;

    void Start(){
        player1 = GameObject.Find("/Player 1").GetComponent<PlayerInput>();
        player2 = GameObject.Find("/Player 2").GetComponent<PlayerInput>();
        tempo = gameObject.GetComponent<Tempo>();
        score = gameObject.GetComponent<Score>();

        jackson = gameObject.GetComponent<Jackson>();
        botney = gameObject.GetComponent<Botney>();
        chargeBar = gameObject.GetComponent<ChargeBar>();
    }

    public bool StillPlaying(){
    	return stillPlaying;
    }

    public bool StillPlaying(bool playing){
    	stillPlaying = playing;
    	return stillPlaying;
    }

    void Reset(){
    	stillPlaying = true;
    	jackson.SetState(Jackson.State.Idle);
    	botney.SetState(Botney.State.Idle);
    	chargeBar.LeftChargeLevel(0);
    	chargeBar.RightChargeLevel(0);
    	score.Reset();
    }

    void Update(){
    	if(Input.GetButtonDown("Reset")){
    		Reset();
    	}
    	if(stillPlaying){
        	if(tempo.Beat() != lastBeat){
            	lastBeat = tempo.Beat();
            	if(tempo.Beat() == 0){
                	string report = player1.LastInput() + " vs. " + player2.LastInput() + ":";
                	Debug.Log(report);
                	int result =WhoWins(
                        	player1.LastInput(),
                        	player2.LastInput());
                	if(result == 0){
                    	Debug.Log("Tie");
                	}else if(result == 1){
                    	Debug.Log("Player 1 gets the point!");
                    	score.IncPlayer1Score();
                	}else if(result == 2){
                    	Debug.Log("Player 2 gets the point!");
                    	score.IncPlayer2Score();
                	}
                	string scoreReport = "Player 1: " + score.Player1Score();
                	scoreReport += ", Player 2: " + score.Player2Score();
                	Debug.Log(scoreReport);
            	}else{
            		if(jackson.CurrentState() != Jackson.State.Victory){
            			if(jackson.CurrentState() != Jackson.State.Defeat){
            				jackson.SetState(Jackson.State.Idle);
            			}
            		}
            		if(botney.CurrentState() != Botney.State.Victory){
            			if(botney.CurrentState() != Botney.State.Defeat){
            				botney.SetState(Botney.State.Idle);
            			}
            		}
            	}
        	}
    	}
    }

    int WhoWins(PlayerInput.Choice p1choice, PlayerInput.Choice p2choice){
        int tie = 0;
        int p1 = 1;
        int p2 = 2;
        if(p1choice == PlayerInput.Choice.Rock){
            if(p2choice == PlayerInput.Choice.Rock){
                return tie;
            }else if(p2choice == PlayerInput.Choice.Paper){
            	jackson.SetState(Jackson.State.Stun);
            	botney.SetState(Botney.State.Projectile);
            	chargeBar.RightChargeLevel(chargeBar.RightChargeLevel() + 1);
                return p2;
            }else{ // vs. scissors
            	jackson.SetState(Jackson.State.Melee);
            	botney.SetState(Botney.State.Stun);
            	chargeBar.LeftChargeLevel(chargeBar.LeftChargeLevel() + 1);
                return p1;
            }
        }else if(p1choice == PlayerInput.Choice.Paper){
            if(p2choice == PlayerInput.Choice.Rock){
            	jackson.SetState(Jackson.State.Projectile);
            	botney.SetState(Botney.State.Stun);
            	chargeBar.LeftChargeLevel(chargeBar.LeftChargeLevel() + 1);
                return p1;
            }else if(p2choice == PlayerInput.Choice.Paper){
                return tie;
            }else{ // vs. scissors
            	jackson.SetState(Jackson.State.Stun);
            	botney.SetState(Botney.State.Laser);
            	chargeBar.RightChargeLevel(chargeBar.RightChargeLevel() + 1);
                return p2;
            }
        }else if(p1choice == PlayerInput.Choice.Scissors){
            if(p2choice == PlayerInput.Choice.Rock){
            	jackson.SetState(Jackson.State.Stun);
            	botney.SetState(Botney.State.Melee);
            	chargeBar.RightChargeLevel(chargeBar.RightChargeLevel() + 1);
                return p2;
            }else if(p2choice == PlayerInput.Choice.Paper){
            	jackson.SetState(Jackson.State.Laser);
            	botney.SetState(Botney.State.Stun);
            	chargeBar.LeftChargeLevel(chargeBar.LeftChargeLevel() + 1);
                return p1;
            }else{ // vs. scissors
                return tie;
            }
        }else return tie;
    }
}
