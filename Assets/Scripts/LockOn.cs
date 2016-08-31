using UnityEngine;
using System.Collections;

public class LockOn : MonoBehaviour {

	GameObject target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Lock")) {
			if (target != null) {
				target = null;
			} else {
				// ターゲットを取得する
//				target = GameObject.FindWithTag("Enemy");
				target = FindClosestEnemy();
			}

			if (target != null) {
				Debug.Log ("Remove Lock!");
				// 距離が離れたらロックを解除する
				if (Vector3.Distance (target.transform.position, transform.position) > 100) {
					target = null;
				}
			}
		}

		if (target != null) {
			// ターゲットの方向を向く
			transform.LookAt(target.transform);

			// スムーズにターゲットの方向を向く
			// Quaternion.LookRotation(A - B); B位置からA位置を向いた状態の向きを算出
			Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
			// Quaternion.Slerp(現在の向き, 目標の向き, 歩合); 自身がターゲットに徐々に向くように、3つ目の引数（歩合）＝回転のスピード。
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);

			// LookAtの際、XやZ軸もtargetの方向を向いてしまうのを補正
			transform.rotation = new Quaternion (0, transform.rotation.y, 0, transform.rotation.w);

			// カメラをターゲットに向ける
			Transform cameraParent = Camera.main.transform.parent;
			Quaternion targetRotation2 = Quaternion.LookRotation (target.transform.position - cameraParent.position);
			cameraParent.localRotation = Quaternion.Slerp (cameraParent.localRotation, targetRotation2, Time.deltaTime * 10);
			cameraParent.localRotation = new Quaternion (cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);
		}
	}

	// 一番近い敵を探して取得
	GameObject FindClosestEnemy() {
		GameObject[] gos;
		// Enemyタグのついたシーン内のすべてのオブジェクトをリスト化
		gos = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closest = null;

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in gos) {
			// Enemy一体(go)と自分の距離
			Vector3 diff = go.transform.position - position;
			// .sqrMagnitudeでベクトルの大きさ（＝長さ＝距離）を取得
			float curDistance = diff.sqrMagnitude;

			// curDistanceが、比較済みの他のgoとの距離より近ければ、最も近いと判定。
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}

		if (closest != null) {
			Debug.Log ("not need Lock!");

			// 一番近くの敵がロックオン範囲外ならロックしない
			if (Vector3.Distance (closest.transform.position, transform.position) > 100) {
				closest = null;
			}
		}

		return closest;
	}
}
