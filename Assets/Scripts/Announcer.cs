using UnityEngine;
using System.Collections;

public class Announcer : MonoBehaviour{
	AudioSource jacksonWins;
	AudioSource botneyWins;
	void Start(){
		jacksonWins = GameObject.Find("Jackson wins").GetComponent<AudioSource>();
		jacksonWins = GameObject.Find("Botney wins").GetComponent<AudioSource>();
	}
}
