using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour{
	AudioSource source;
	private Tempo tempo;

	public float delayTime;

	void Start(){
		tempo = GameObject.Find("Master Control").GetComponent<Tempo>();
		source = gameObject.GetComponent<AudioSource>();
		source.Play();
		Invoke("StartTempo", delayTime);
	}

	void StartTempo(){
		tempo.Play();
	}
}
