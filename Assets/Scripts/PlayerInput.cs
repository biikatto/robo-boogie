using UnityEngine;
using System.Collections;

public class PlayerInput  : MonoBehaviour{
    public bool player2;
    [Tooltip("Prints incoming inputs as they happen.")]
    public bool debug;

    public enum Choice{None, Rock, Paper, Scissors};
    private Choice lastInput;
    private Hashtable inputList;
    private string playerPrefix;
    private bool acceptingInput = true;

    void Reset(){
    	lastInput = Choice.None;
    }

    void Start(){
        inputList = new Hashtable();
        inputList.Add("Rock", "Rock");
        inputList.Add("Paper", "Paper");
        inputList.Add("Scissors", "Scissors");
        playerPrefix = "P1 ";
        if(player2){
            playerPrefix = "P2 ";
        }
        foreach(string key in ((Hashtable)inputList.Clone()).Keys){
            inputList[key] = playerPrefix + inputList[key];
            if(debug){
            	Debug.Log(inputList[key]);
            }
        }
    }

    void Update(){
        if(Input.GetButtonDown((string)inputList["Rock"])){
            lastInput = Choice.Rock;
            if(debug){
                Debug.Log(playerPrefix + (string)inputList["Rock"]);
            }
        }else if(Input.GetButtonDown((string)inputList["Paper"])){
            lastInput = Choice.Paper;
            if(debug){
                Debug.Log(playerPrefix + (string)inputList["Paper"]);
            }
        }else if(Input.GetButtonDown((string)inputList["Scissors"])){
            lastInput = Choice.Scissors;
            if(debug){
                Debug.Log(playerPrefix + (string)inputList["Scissors"]);
            }
        }
    }

    public bool AcceptInput(){
    	return acceptingInput;
    }

    public bool AcceptInput(bool state){
    	acceptingInput = state;
    	return acceptingInput;
    }

    public Choice LastInput(){
        return lastInput;
    }
}
