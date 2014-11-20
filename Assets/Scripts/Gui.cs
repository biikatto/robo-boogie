using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour{
	private GUIStyle player1controlsStyle;
	private GUIStyle player2controlsStyle;

	private Font ethnocentric;

	private Texture2D controlBar;

	private Texture2D xControlL;
	private Texture2D sqrControlL;
	private Texture2D triControlL;

	private Texture2D xControlR;
	private Texture2D sqrControlR;
	private Texture2D triControlR;

	public Vector2 player1controlsLocation;
	public Vector2 player2controlsLocation;

	public int controlsHorizontalOffset = 20;
	public int controlsVerticalOffset = 35;
	public float controlsScale = 0.6f;

	private Rect[] player1controlsRect;
	private Rect[] player1controlsLabelsRect;
	private Rect[] player1controlsButtonsRect;


	private Rect[] player2controlsRect;
	private Rect[] player2controlsLabelsRect;
	private Rect[] player2controlsButtonsRect;

	private ChargeBar chargeBar;
	private BeatBar beatBar;
	private Background background;
	private Botney botney;
	private Jackson jackson;

	void Start(){

		chargeBar = gameObject.GetComponent<ChargeBar>();
		beatBar = gameObject.GetComponent<BeatBar>();
		background = gameObject.GetComponent<Background>();
		botney = gameObject.GetComponent<Botney>();
		jackson = gameObject.GetComponent<Jackson>();

		ethnocentric = Resources.Load("Fonts/Ethnocentric") as Font;

		player1controlsStyle = new GUIStyle(GUIStyle.none);
		player1controlsStyle.alignment = TextAnchor.MiddleRight;
		player1controlsStyle.font = ethnocentric;
		player1controlsStyle.fontSize = 22;
		player1controlsStyle.alignment = TextAnchor.MiddleRight;
		player1controlsStyle.contentOffset = new Vector2(-5, 0);

		player2controlsStyle = new GUIStyle(GUIStyle.none);
		player2controlsStyle.alignment = TextAnchor.MiddleRight;
		player2controlsStyle.font = ethnocentric;
		player2controlsStyle.fontSize = 22;
		player2controlsStyle.alignment = TextAnchor.MiddleRight;
		player2controlsStyle.contentOffset = new Vector2(-5, 0);

		controlBar = Resources.Load<Texture2D>("Textures/controls/bar");

		xControlL = Resources.Load<Texture2D>("Textures/controls/left/x");
		sqrControlL = Resources.Load<Texture2D>("Textures/controls/left/sqr");
		triControlL = Resources.Load<Texture2D>("Textures/controls/left/tri");

		xControlR = Resources.Load<Texture2D>("Textures/controls/right/x");
		sqrControlR = Resources.Load<Texture2D>("Textures/controls/right/sqr");
		triControlR = Resources.Load<Texture2D>("Textures/controls/right/tri");


		if((player1controlsLocation.x == 0) && (player1controlsLocation.y == 0)){
 			player1controlsLocation = new Vector2(15, Screen.height - 100);
		}
		if((player2controlsLocation.x == 0) && (player2controlsLocation.y == 0)){
 			player2controlsLocation = new Vector2(Screen.width-321, Screen.height - 118);
		}

		// Initialize rects
		player1controlsRect = new Rect[3];
		player1controlsLabelsRect = new Rect[3];
		player1controlsButtonsRect = new Rect[3];

		player1controlsRect[0] = new Rect(
				player1controlsLocation.x,
				player1controlsLocation.y,
				xControlL.width, xControlL.height);
		player1controlsRect[1] = new Rect(
				player1controlsLocation.x + controlsHorizontalOffset,
				player1controlsLocation.y + controlsVerticalOffset,
				xControlL.width, xControlL.height);
		player1controlsRect[2] = new Rect(
				player1controlsLocation.x + (controlsHorizontalOffset * 2),
				player1controlsLocation.y + (controlsVerticalOffset * 2),
				xControlL.width, xControlL.height);

		player2controlsRect = new Rect[3];

		player2controlsRect[0] = new Rect();
		for(int i=0;i<player1controlsRect.Length;i++){
			player2controlsRect[i].xMin = Screen.width - player1controlsRect[i].xMax;
			player2controlsRect[i].xMax = Screen.width - player1controlsRect[i].xMin;
			player2controlsRect[i].yMin = player1controlsRect[i].yMin;
			player2controlsRect[i].yMax = player1controlsRect[i].yMax;
			player2controlsRect[i].x += player1controlsRect[i].width/2;
		}

		// Initialize rects
		//player2controlsRect = new Rect[3];
		//player2controlsLabelsRect = new Rect[3];
		//player2controlsButtonsRect = new Rect[3];

		//player2controlsRect[0] = new Rect(
		//		player2controlsLocation.x, player2controlsLocation.y,
		//		512, 64);
		//player2controlsRect[1] = new Rect(
		//		player2controlsLocation.x - controlsHorizontalOffset,
		//		player2controlsLocation.y + controlsVerticalOffset,
		//		512, 64);
		//player2controlsRect[2] = new Rect(
		//		player2controlsLocation.x - (controlsHorizontalOffset * 2),
		//		player2controlsLocation.y + (controlsVerticalOffset * 2),
		//		512, 64);

		// Set up dependent rects
		for(int i=0;i<player1controlsRect.Length;i++){
			// Scale original rects
			player1controlsRect[i].width *= controlsScale;
			player1controlsRect[i].height *= controlsScale;

			// Set up label rects
		//	player1controlsLabelsRect[i] = new Rect(
		//			player1controlsRect[i].x - 330,
		//			player1controlsRect[i].y,
		//			player1controlsRect[i].width * -1f,
		//			player1controlsRect[i].height);

		//	// Set up button rects
		//	player1controlsButtonsRect[i] = new Rect(
		//			player1controlsRect[i].x - 308,
		//			player1controlsRect[i].y,
		//			xButton.width * controlsScale,
		//			xButton.height * controlsScale);
		}

		// Set up dependent rects
		for(int i=0;i<player2controlsRect.Length;i++){
			// Scale original rects
			player2controlsRect[i].width *= controlsScale;
			player2controlsRect[i].height *= controlsScale;

		//	// Set up label rects
		//	player2controlsLabelsRect[i] = new Rect(
		//			player2controlsRect[i].x - 150,
		//			player2controlsRect[i].y,
		//			player2controlsRect[i].width,
		//			player2controlsRect[i].height);

		//	// Set up button rects
		//	player2controlsButtonsRect[i] = new Rect(
		//			player2controlsRect[i].x + 268,
		//			player2controlsRect[i].y,
		//			xButton.width * controlsScale,
		//			xButton.height * controlsScale);
		}

	}

	void OnGUI(){
		background.Draw();
		chargeBar.Draw();
		beatBar.Draw();
		botney.Draw();
		jackson.Draw();
		DrawPlayer1controls();
		DrawPlayer2controls();
	}

	void DrawPlayer1controls(){
		// Bars
		GUI.DrawTexture(player1controlsRect[0], triControlL);
		GUI.DrawTexture(player1controlsRect[1], sqrControlL);
		GUI.DrawTexture(player1controlsRect[2], xControlL);

		//// Text
		//GUI.Label(player1controlsLabelsRect[0], "Move 1", player1controlsStyle);
		//GUI.Label(player1controlsLabelsRect[1], "Move 2", player1controlsStyle);
		//GUI.Label(player1controlsLabelsRect[2], "Move 3", player1controlsStyle);

		//// Buttons
		//GUI.DrawTexture(player1controlsButtonsRect[0], xButton);
		//GUI.DrawTexture(player1controlsButtonsRect[1], xButton);
		//GUI.DrawTexture(player1controlsButtonsRect[2], xButton);
	}

	void DrawPlayer2controls(){
		GUI.DrawTexture(player2controlsRect[0], triControlR);
		GUI.DrawTexture(player2controlsRect[1], sqrControlR);
		GUI.DrawTexture(player2controlsRect[2], xControlR);

		//// Text
		//GUI.Label(player2controlsLabelsRect[0], "Move 1", player2controlsStyle);
		//GUI.Label(player2controlsLabelsRect[1], "Move 2", player2controlsStyle);
		//GUI.Label(player2controlsLabelsRect[2], "Move 3", player2controlsStyle);

		//// Buttons
		//GUI.DrawTexture(player2controlsButtonsRect[0], xButton);
		//GUI.DrawTexture(player2controlsButtonsRect[1], xButton);
		//GUI.DrawTexture(player2controlsButtonsRect[2], xButton);
	}
}
