using UnityEngine;
using System.Collections;

public class BeatBar : MonoBehaviour{
	// Fields/*{{{*/
	// Bar/*{{{*/
	private Texture2D bar;

	private Rect leftBarRect;
	private Rect rightBarRect;

	public int barSeparation;
	public int barVerticalOffset = 10;
	public float barVerticalScale = 1f;
	public float barHorizontalScale = 1f;/*}}}*/

	// Lights/*{{{*/
	private Texture2D blankLight;
	private Texture2D silverLight;
	private Texture2D redLight;
	private Texture2D yellowLight;
	private Texture2D greenLight;

	private Rect[] lightRect = new Rect[6];

	public float lightsScale = 1f;/*}}}*/

	// Disco ball/*{{{*/
	private Texture2D silverDiscoBall;
	private Texture2D redDiscoBall;
	private Texture2D yellowDiscoBall;
	private Texture2D greenDiscoBall;

	private Rect discoBallRect;
	private Rect discoBallBounceRect;
	private Rect discoBallSuperBounceRect;

	public int discoBallBounceAmount = 20;
	public int discoBallSuperBounceAmount = 35;
	/*}}}*/
	private Tempo tempo;/*}}}*/

	void Start(){/*{{{*/
		SetupBar();
		SetupLights();
		SetupDiscoBall();

		tempo = gameObject.GetComponent<Tempo>();
	}/*}}}*/

	void SetupBar(){/*{{{*/
		bar = Resources.Load<Texture2D>("Textures/beat_bar/bar");

		leftBarRect = new Rect(
				(Screen.width/2) - barSeparation,
				barVerticalOffset,
				bar.width * -1 * barHorizontalScale,
				bar.height * barVerticalScale);
		rightBarRect = new Rect(
				(Screen.width/2) + barSeparation,
				barVerticalOffset,
				bar.width * barHorizontalScale,
				bar.height * barVerticalScale);
	}/*}}}*/

	void SetupLights(){/*{{{*/
		blankLight = Resources.Load<Texture2D>("Textures/beat_bar/lights/blank");
		silverLight = Resources.Load<Texture2D>("Textures/beat_bar/lights/silver");
		redLight = Resources.Load<Texture2D>("Textures/beat_bar/lights/red");
		yellowLight = Resources.Load<Texture2D>("Textures/beat_bar/lights/yellow");
		greenLight = Resources.Load<Texture2D>("Textures/beat_bar/lights/green");

		int barWidth = (int)(rightBarRect.width);

		int lightOffset = 150;

		lightRect[0] = new Rect(
				Screen.width/2 - (2*barWidth/3) - lightOffset,
				barVerticalOffset,
				blankLight.width * lightsScale,
				blankLight.height * lightsScale);

		lightRect[1] = new Rect(
				Screen.width/2 - (barWidth/3) - lightOffset,
				barVerticalOffset,
				blankLight.width * lightsScale,
				blankLight.height * lightsScale);

		lightRect[2] = new Rect(
				Screen.width/2 - lightOffset,
				barVerticalOffset,
				blankLight.width * lightsScale,
				blankLight.height * lightsScale);

		lightRect[3] = CopyRect(lightRect[2]);
		lightRect[4] = CopyRect(lightRect[1]);
		lightRect[5] = CopyRect(lightRect[0]);

		for(int i=0;i<lightRect.Length;i++){
			lightRect[i].y += lightRect[i].height/4;
			if(i>2){
				lightRect[i].x = Screen.width - lightRect[i].x - lightRect[i].width;
			}
		}

		Debug.Log(lightRect[0]);
	}/*}}}*/

	Rect CopyRect(Rect copyMe){/*{{{*/
		return new Rect(copyMe.x, copyMe.y, copyMe.width, copyMe.height);
	}/*}}}*/

	void SetupDiscoBall(){/*{{{*/
		silverDiscoBall = Resources.Load<Texture2D>("Textures/beat_bar/disco_ball/silver");
		redDiscoBall = Resources.Load<Texture2D>("Textures/beat_bar/disco_ball/red");
		yellowDiscoBall = Resources.Load<Texture2D>("Textures/beat_bar/disco_ball/yellow");
		greenDiscoBall = Resources.Load<Texture2D>("Textures/beat_bar/disco_ball/green");

		discoBallRect = new Rect(
				(Screen.width/2),// - (discoBall.width/2),
				barVerticalOffset,
				silverDiscoBall.width/2,
				silverDiscoBall.height/2);

		discoBallBounceRect = new Rect(
				(Screen.width/2),// - (discoBall.width/2),
				barVerticalOffset,
				silverDiscoBall.width/2 + discoBallBounceAmount,
				silverDiscoBall.height/2 + discoBallBounceAmount);

		discoBallSuperBounceRect = new Rect(
				(Screen.width/2),// - (discoBall.width/2),
				barVerticalOffset,
				silverDiscoBall.width/2 + discoBallSuperBounceAmount,
				silverDiscoBall.height/2 + discoBallSuperBounceAmount);

		discoBallRect.x -= discoBallRect.width/2;
		discoBallRect.y -= discoBallRect.height/4;

		discoBallBounceRect.x -= discoBallBounceRect.width/2;
		discoBallBounceRect.y -= discoBallBounceRect.height/4;

		discoBallSuperBounceRect.x -= discoBallSuperBounceRect.width/2;
		discoBallSuperBounceRect.y -= discoBallSuperBounceRect.height/4;
	}/*}}}*/

	public void Draw(){/*{{{*/
		DrawBar();
		DrawLights();
		DrawDiscoBall();
	}/*}}}*/

	void DrawBar(){/*{{{*/
		GUI.DrawTexture(leftBarRect, bar);
		GUI.DrawTexture(rightBarRect, bar);
	}/*}}}*/
	
	void DrawLights(){/*{{{*/
		if(tempo.BeatProgress() > (0.5*tempo.BeatLength())){
			if(tempo.Beat() == 0){
				GUI.DrawTexture(lightRect[0], silverLight);
				GUI.DrawTexture(lightRect[1], silverLight);
				GUI.DrawTexture(lightRect[2], silverLight);

				GUI.DrawTexture(lightRect[3], silverLight);
				GUI.DrawTexture(lightRect[4], silverLight);
				GUI.DrawTexture(lightRect[5], silverLight);
			}else{
				GUI.DrawTexture(lightRect[0], blankLight);
				GUI.DrawTexture(lightRect[1], blankLight);
				GUI.DrawTexture(lightRect[2], blankLight);

				GUI.DrawTexture(lightRect[3], blankLight);
				GUI.DrawTexture(lightRect[4], blankLight);
				GUI.DrawTexture(lightRect[5], blankLight);
			}
		}else{
			if(tempo.Beat() == 0){
				GUI.DrawTexture(lightRect[0], silverLight);
				GUI.DrawTexture(lightRect[1], silverLight);
				GUI.DrawTexture(lightRect[2], silverLight);

				GUI.DrawTexture(lightRect[3], silverLight);
				GUI.DrawTexture(lightRect[4], silverLight);
				GUI.DrawTexture(lightRect[5], silverLight);
			}else if(tempo.Beat() == 1){
				GUI.DrawTexture(lightRect[0], redLight);
				GUI.DrawTexture(lightRect[1], blankLight);
				GUI.DrawTexture(lightRect[2], blankLight);

				GUI.DrawTexture(lightRect[3], blankLight);
				GUI.DrawTexture(lightRect[4], blankLight);
				GUI.DrawTexture(lightRect[5], redLight);
			}else if(tempo.Beat() == 2){
				GUI.DrawTexture(lightRect[0], blankLight);
				GUI.DrawTexture(lightRect[1], yellowLight);
				GUI.DrawTexture(lightRect[2], blankLight);

				GUI.DrawTexture(lightRect[3], blankLight);
				GUI.DrawTexture(lightRect[4], yellowLight);
				GUI.DrawTexture(lightRect[5], blankLight);
			}else if(tempo.Beat() == 3){
				GUI.DrawTexture(lightRect[0], blankLight);
				GUI.DrawTexture(lightRect[1], blankLight);
				GUI.DrawTexture(lightRect[2], greenLight);

				GUI.DrawTexture(lightRect[3], greenLight);
				GUI.DrawTexture(lightRect[4], blankLight);
				GUI.DrawTexture(lightRect[5], blankLight);
			}
		}
	}/*}}}*/

	void DrawDiscoBall(){/*{{{*/
		Rect rect;
		if(tempo.BeatProgress() > (0.5*tempo.BeatLength())){
			rect = discoBallRect;
		}else{
			if(tempo.Beat() == 0){
				rect = discoBallSuperBounceRect;
			}else{
				rect = discoBallBounceRect;
			}
		}
		if(tempo.Beat() == 0){
			GUI.DrawTexture(rect, silverDiscoBall);
		}else if(tempo.Beat() == 1){
			GUI.DrawTexture(rect, redDiscoBall);
		}else if(tempo.Beat() == 2){
			GUI.DrawTexture(rect, yellowDiscoBall);
		}else if(tempo.Beat() == 3){
			GUI.DrawTexture(rect, greenDiscoBall);
		}
	}/*}}}*/
}
