using UnityEngine;
using System.Collections;

public class Jackson : MonoBehaviour{
	private Texture2D spriteSheet;

	private Texture2D[] final;
	private Texture2D[] laser;
	private Texture2D[] melee;
	private Texture2D[] projectile;
	private Texture2D[] win;

	private AudioSource meleeSound;
	private AudioSource projectileSound;
	private AudioSource laserSound;
	private AudioSource winSound;

	private bool finishingMoveCompleted = false;

	public enum State{Idle, Melee, Projectile, Laser, Stun, Victory, Defeat};
	private State currentState = State.Idle;

	private float scaleFactor = (float)Screen.width/1920f;

	private Vector2 position = new Vector2(Screen.width-630, 75);
	private Rect rect;
	private Rect reverseRect;

	int spriteSize = 768;
	float xSize = 0.33333f;
	float ySize = 0.25f;
	private Rect[] idleSprites;
	private Rect[] meleeSprites;
	private Rect[] projectileSprites;
	private Rect[] laserSprites;
	private Rect[] victorySprites;

	private Rect stunSprite;
	private Rect defeatSprite;

	private Rect effectsRect;

	Tempo tempo;

	void Start(){
		meleeSound = GameObject.Find("Jackson Sounds/Melee").GetComponent<AudioSource>();
		projectileSound = GameObject.Find("Jackson Sounds/Projectile").GetComponent<AudioSource>();
		laserSound = GameObject.Find("Jackson Sounds/Laser").GetComponent<AudioSource>();
		winSound = GameObject.Find("Jackson Sounds/Win").GetComponent<AudioSource>();

		final = new Texture2D[2];
		final[0] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/final1");
		final[1] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/final2");

    	laser = new Texture2D[2];
		laser[0] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/laser1");
		laser[1] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/laser2");

    	melee = new Texture2D[2];
		melee[0] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/melee1");
		melee[1] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/melee2");

    	projectile = new Texture2D[2];
		projectile[0] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/projectile1");
		projectile[1] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/projectile2");

    	win = new Texture2D[4];
		win[0] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/win1");
		win[1] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/win2");
		win[2] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/win3");
		win[3] = Resources.Load<Texture2D>("Textures/characters/jackson/effects/win4");

		for(int i=0;i<4;i++){
			if(i<2){
				Debug.Log(final[i]);
				Debug.Log(laser[i]);
				Debug.Log(melee[i]);
				Debug.Log(projectile[i]);
			}
			Debug.Log(win[i]);
		}

		spriteSheet = Resources.Load<Texture2D>("Textures/characters/jackson/character_sprite_sheet");

		tempo = gameObject.GetComponent<Tempo>();
		Debug.Log(tempo);

		rect = new Rect(position.x + (spriteSize*0.7f), position.y, spriteSize * -0.7f, spriteSize * 0.7f);
		effectsRect = new Rect(0,0,Screen.width, Screen.height);
		SetupSprites();

		currentState = State.Idle;
	}

	void SetupSprites(){
		idleSprites = new Rect[2];
		idleSprites[0] = new Rect(xSize, ySize*3, xSize, ySize);
		idleSprites[1] = new Rect(0, ySize*3, xSize, ySize);

		meleeSprites = new Rect[2];
		meleeSprites[0] = new Rect(0, ySize*2, xSize, ySize);
		meleeSprites[1] = new Rect(xSize, ySize*2, xSize, ySize);

		projectileSprites = new Rect[2];
		projectileSprites[0] = new Rect(0, ySize, xSize, ySize);
		projectileSprites[1] = new Rect(xSize, ySize, xSize, ySize);

		laserSprites = new Rect[2];
		laserSprites[0] = new Rect(0, 0, xSize, ySize);
		laserSprites[1] = new Rect(xSize, 0, xSize, ySize);

		victorySprites = new Rect[2];
		victorySprites[0] = new Rect(xSize*2, ySize*3, xSize, ySize);
		victorySprites[1] = new Rect(xSize*2, ySize*2, xSize, ySize);

		stunSprite = new Rect(xSize*2, 0, xSize, ySize);
		defeatSprite = new Rect(xSize*2, ySize, xSize, ySize);
	}

	public void SetState(State state){
		currentState = state;
	}

	public State CurrentState(){
		return currentState;
	}

	public void Draw(){
		switch(currentState){
			case State.Idle:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, idleSprites[1]);
				}
				break;
			case State.Melee:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, meleeSprites[1]);
					GUI.DrawTexture(effectsRect, melee[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, meleeSprites[1]);
					GUI.DrawTexture(effectsRect, melee[1]);
					if(!meleeSound.isPlaying){
						meleeSound.Play();
					}
				}
				break;
			case State.Projectile:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, projectileSprites[0]);
					GUI.DrawTexture(effectsRect, projectile[0]);
					if(!projectileSound.isPlaying){
						projectileSound.Play();
					}
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, projectileSprites[1]);
					GUI.DrawTexture(effectsRect, projectile[1]);
				}
				break;
			case State.Laser:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, laserSprites[0]);
					GUI.DrawTexture(effectsRect, laser[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, laserSprites[1]);
					GUI.DrawTexture(effectsRect, laser[1]);
					if(!laserSound.isPlaying){
						laserSound.Play();
					}
				}
				break;
			case State.Stun:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, stunSprite);
				}
				break;
			case State.Victory:
				if(!finishingMoveCompleted){
					if(tempo.Beat() == 1){
						if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
							GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[0]);
						}else{
							GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[1]);
							finishingMoveCompleted = true;
						}
					}/*else if(tempo.Beat() == 2){
						if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
							GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[2]);
						}else{
							GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[3]);
							finishingMoveCompleted = true;
						}
					}*/
				}else{
					if(tempo.BeatProgress() < (0.333*tempo.BeatLength())){
						GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[0]);
						GUI.DrawTexture(effectsRect, win[1]);
						if(Random.value > 0.5){
							winSound.Play();
						}
					}else if(tempo.BeatProgress() < (0.666*tempo.BeatLength())){
						GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[1]);
						GUI.DrawTexture(effectsRect, win[2]);
					}else{
						GUI.DrawTextureWithTexCoords(rect, spriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[2]);
						GUI.DrawTexture(effectsRect, win[3]);
					}
				}
				break;
			case State.Defeat:
				GUI.DrawTextureWithTexCoords(rect, spriteSheet, defeatSprite);
				break;
			default:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, spriteSheet, idleSprites[1]);
				}
				break;
		}
	}
}
