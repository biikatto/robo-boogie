using UnityEngine;
using System.Collections;

public class Botney : MonoBehaviour{
	Texture2D characterSpriteSheet;
	Texture2D effectsSpriteSheet;

	private Texture2D[] final;
	private Texture2D[] laser;
	private Texture2D[] melee;
	private Texture2D[] projectile;
	private Texture2D[] win;

	private AudioSource meleeSound;
	private AudioSource projectileSound;
	private AudioSource laserSound;
	private AudioSource winSound;

	public enum State{Idle, Melee, Projectile, Laser, Stun, Victory, Defeat};
	private State currentState = State.Idle;

	private Vector2 position = new Vector2(75, 83);
	private Rect rect;
	private Rect effectsRect;

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

	bool finishingMoveCompleted = false;

	Tempo tempo;

	void Start(){

		meleeSound = GameObject.Find("Botney Sounds/Melee").GetComponent<AudioSource>();
		projectileSound = GameObject.Find("Botney Sounds/Projectile").GetComponent<AudioSource>();
		laserSound = GameObject.Find("Botney Sounds/Laser").GetComponent<AudioSource>();
		winSound = GameObject.Find("Botney Sounds/Win").GetComponent<AudioSource>();


		characterSpriteSheet = Resources.Load<Texture2D>("Textures/characters/botney/character_sprite_sheet");
		effectsRect = new Rect(0,0,Screen.width, Screen.height);

		final = new Texture2D[2];
		final[0] = Resources.Load<Texture2D>("Textures/characters/botney/effects/final1");
		final[1] = Resources.Load<Texture2D>("Textures/characters/botney/effects/final2");

    	laser = new Texture2D[2];
		laser[0] = Resources.Load<Texture2D>("Textures/characters/botney/effects/laser1");
		laser[1] = Resources.Load<Texture2D>("Textures/characters/botney/effects/laser2");

    	melee = new Texture2D[2];
		melee[0] = Resources.Load<Texture2D>("Textures/characters/botney/effects/melee1");
		melee[1] = Resources.Load<Texture2D>("Textures/characters/botney/effects/melee2");

    	projectile = new Texture2D[2];
		projectile[0] = Resources.Load<Texture2D>("Textures/characters/botney/effects/projectile1");
		projectile[1] = Resources.Load<Texture2D>("Textures/characters/botney/effects/projectile2");

    	win = new Texture2D[2];
		win[0] = Resources.Load<Texture2D>("Textures/characters/botney/effects/win1");
		win[1] = Resources.Load<Texture2D>("Textures/characters/botney/effects/win2");

		tempo = gameObject.GetComponent<Tempo>();
		Debug.Log(tempo);

		rect = new Rect(position.x, position.y, spriteSize * 0.7f, spriteSize * 0.7f);
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

	void Update(){
	}

	public void Draw(){
		switch(currentState){
			case State.Idle:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, idleSprites[1]);
				}
				break;
			case State.Melee:
				if(tempo.BeatProgress() < (0.25*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, meleeSprites[0]);
				}else if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTexture(effectsRect, melee[0]);
				}else{
					//GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, meleeSprites[1]);
					GUI.DrawTexture(effectsRect, melee[1]);
					if(!meleeSound.isPlaying){
						meleeSound.Play();
					}
				}
				break;
			case State.Projectile:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, projectileSprites[0]);
					GUI.DrawTexture(effectsRect, projectile[0]);
					if(!projectileSound.isPlaying){
						projectileSound.Play();
					}
				}else{
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, projectileSprites[1]);
					GUI.DrawTexture(effectsRect, projectile[1]);
				}
				break;
			case State.Laser:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, laserSprites[0]);
					GUI.DrawTexture(effectsRect, laser[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, laserSprites[1]);
					GUI.DrawTexture(effectsRect, laser[1]);
					if(!laserSound.isPlaying){
						laserSound.Play();
					}
				}
				break;
			case State.Stun:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, stunSprite);
				}
				break;
			case State.Victory:
				if(!finishingMoveCompleted){
					if(tempo.Beat() == 1){
						if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
							GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[0]);
						}else{
							GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, victorySprites[0]);
							GUI.DrawTexture(effectsRect, final[1]);
							if(!winSound.isPlaying){
								winSound.Play();
							}
							finishingMoveCompleted = true;
						}
					}
				}else{
					if(tempo.BeatProgress() < (0.333*tempo.BeatLength())){
						GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[0]);
					}else if(tempo.BeatProgress() < (0.666*tempo.BeatLength())){
						GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[0]);
						GUI.DrawTexture(effectsRect, win[1]);
					}else{
						GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, victorySprites[1]);
						GUI.DrawTexture(effectsRect, win[1]);
					}
				}
				break;
			case State.Defeat:
				GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, defeatSprite);
				break;
			default:
				if(tempo.BeatProgress() < (0.5*tempo.BeatLength())){
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, idleSprites[0]);
				}else{
					GUI.DrawTextureWithTexCoords(rect, characterSpriteSheet, idleSprites[1]);
				}
				break;
		}
	}
}
