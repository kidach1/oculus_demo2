using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	CharacterController controller;
	public float speed = 15.0f;
	public float jumpSpeed = 2.0f;
	public float gravity = 50.0f;
	private Vector3 moveDirection = Vector3.zero;
	private Animator animator;

	// Use this for initialization
	void Start () {
		// プレイヤーを移動させる
		controller = GetComponent<CharacterController>();

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		// モーションを切り替える
		if (Input.GetAxis ("Horizontal") > 0) {
			animator.SetInteger ("Horizontal", 1);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			animator.SetInteger ("Horizontal", -1);
		} else {
			animator.SetInteger ("Horizontal", 0);
		}
		if (Input.GetAxis ("Vertical") > 0) {
			animator.SetInteger ("Vertical", 1);
		} else if (Input.GetAxis ("Vertical") < 0) {
			animator.SetInteger ("Vertical", -1);
		} else {
			animator.SetInteger ("Vertical", 0);
		}

//		// ジャンプモーション
		animator.SetBool("Jump", Input.GetButton("Jump"));

		if (controller.isGrounded) {

			// 前後左右に移動
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			// ワールド座標系に変換
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;

			// 回転
			transform.Rotate(0, Input.GetAxis("Horizontal2"), 0);

			// カメラを回転させる
//			Camera.main.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);
			GameObject CameraParent = Camera.main.transform.parent.gameObject;
			CameraParent.transform.Rotate(Input.GetAxis("Vertical2"), 0, 0);

			// ジャンプ
			if (Input.GetButton ("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}
