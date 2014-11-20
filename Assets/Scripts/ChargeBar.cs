using UnityEngine;
using System.Collections;

public class ChargeBar : MonoBehaviour{
	public bool debug = false;

	private Texture2D empty;
	private Texture2D charge;

	private Texture2D botneyName;
	private Texture2D jacksonName;

	private Rect leftEmptyRect;
	private Rect leftChargeRect;
	private Rect leftNameRect;

	private Rect rightEmptyRect;
	private Rect rightChargeRect;
	private Rect rightNameRect;

	private int leftChargeLevel;
	private int rightChargeLevel;

	public int chargeLevels = 3;

	public int barSeparation = 40;
	public int barVerticalOffset = 80;
	public float barHorizontalScale = 1f;
	public float barVerticalScale = 1f;

	Font ethnocentric;
	public string player1name = "Botney Sparks";
	public string player2name = "Jackson Mk Five";

	private GUIStyle player1nameStyle;
	private GUIStyle player2nameStyle;

	private Tempo tempo;

	void Start(){
		// Tempo is for debugging purposes
		tempo = gameObject.GetComponent<Tempo>();//<

		ethnocentric = Resources.Load<Font>("Fonts/ethnocentric");

		player1nameStyle = new GUIStyle(GUIStyle.none);
//		player1nameStyle.alignment = TextAnchor.MiddleLeft;
		player1nameStyle.font = ethnocentric;
		player1nameStyle.fontSize = 22;

		player2nameStyle = new GUIStyle(player1nameStyle);
//		player2nameStyle.alignment = TextAnchor.MiddleRight;

		empty = Resources.Load<Texture2D>("Textures/charge_bar/empty");
		charge = Resources.Load<Texture2D>("Textures/charge_bar/full");
		jacksonName = Resources.Load<Texture2D>("Textures/charge_bar/names/jackson");
		botneyName = Resources.Load<Texture2D>("Textures/charge_bar/names/botney");

		leftEmptyRect = new Rect(
				(Screen.width/2) - barSeparation - (empty.width * barHorizontalScale),
				barVerticalOffset,
				empty.width * barHorizontalScale,
				empty.height * barVerticalScale);

		leftNameRect = new Rect(
				leftEmptyRect.x + 10,
				leftEmptyRect.y + leftEmptyRect.height * 0.25f,
				leftEmptyRect.width * 0.75f,
				leftEmptyRect.height * 0.6f);

		rightEmptyRect = new Rect(
				(Screen.width/2) + barSeparation + (empty.width * barHorizontalScale),
				barVerticalOffset,
				empty.width * -1 * barHorizontalScale,
				empty.height * barVerticalScale);

		rightNameRect = new Rect(
				rightEmptyRect.x - 370,
				rightEmptyRect.y + rightEmptyRect.height * 0.25f,
				rightEmptyRect.width * -0.75f,
				rightEmptyRect.height * 0.6f);

		leftChargeRect = new Rect(
				leftEmptyRect.x,
				leftEmptyRect.y,
				0,
				leftEmptyRect.height);

		rightChargeRect = new Rect(
				rightEmptyRect.x,
				rightEmptyRect.y,
				0,
				rightEmptyRect.height);
	}

	void Update(){
		if(debug){
			LeftChargeLevel(tempo.Beat());
			RightChargeLevel(tempo.Beat());
		}
	}

	public void Draw(){
		GUI.DrawTexture(leftEmptyRect, empty);
		GUI.DrawTexture(rightEmptyRect, empty);

		GUI.DrawTexture(leftChargeRect, charge);
		GUI.DrawTexture(rightChargeRect, charge);

		GUI.DrawTexture(leftNameRect, botneyName);
		GUI.DrawTexture(rightNameRect, jacksonName);
	}

	public int RightChargeLevel(){
		return leftChargeLevel;
	}

	public int RightChargeLevel(int newLevel){
		leftChargeLevel = newLevel;
		leftChargeRect.width = leftEmptyRect.width * ((float)leftChargeLevel/chargeLevels);
		return leftChargeLevel;
	}

	public int LeftChargeLevel(){
		return rightChargeLevel;
	}

	public int LeftChargeLevel(int newLevel){
		rightChargeLevel = newLevel;
		rightChargeRect.width = rightEmptyRect.width * ((float)rightChargeLevel/chargeLevels);
		return rightChargeLevel;
	}
}
