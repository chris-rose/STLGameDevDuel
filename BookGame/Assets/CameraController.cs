using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera camera;
	public float cameraMoveSpeed;
	public GameObject cameraTarget;
	public GameObject player;
	public float borderLeft;
	public float borderRight;
	
	private bool moveLeftEnabled;
	private bool moveRightEnabled;
	private float screenWidth;
	private float screenHeight;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = camera.WorldToScreenPoint(player.transform.position);
		Vector3 tempPos;
		if (screenPos.x > screenWidth - borderRight){
			tempPos = transform.position;
			tempPos.x += cameraMoveSpeed * Time.deltaTime;
			transform.position = tempPos;
		}
		else if (screenPos.x < 0 + borderLeft){
			tempPos = transform.position;
			tempPos.x -= cameraMoveSpeed * Time.deltaTime;
			transform.position = tempPos;
		}
	}
	
}

