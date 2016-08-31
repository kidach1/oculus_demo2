using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject target;
	public GameObject shot;
	public GameObject enemyMuzzle;

	float shotInterval = 0;
	float shotIntervalMax = 1.0f;

	public GameObject exprosion;

	public int armorPoint;
	public int armorPointMax = 1000;
	int damage = 100;

	// Use this for initialization
	void Start () {
		// ターゲットを取得
		target = GameObject.Find("PlayerTarget");

		armorPoint = armorPointMax;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (target.transform.position, transform.position) <= 200) {
			// ターゲットの方向を向く
//			transform.LookAt(target.transform);

			// スムーズにターゲットの方向を向く
			Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);

			shotInterval += Time.deltaTime;

			if (shotInterval > shotIntervalMax) {
				Instantiate (shot, transform.position, transform.rotation);
				shotInterval = 0;
			}
		}
	}

	private void OnCollisionEnter(Collision collider) {
		// プレイヤーの弾と衝突したら消滅
		if (collider.gameObject.tag == "Shot") {

			// ダメージをランダムで変える
			damage = collider.gameObject.GetComponent<ShotPlayer>().damage;

			armorPoint -= damage;
			if (armorPoint <= 0) {
				Destroy (gameObject);
				Instantiate (exprosion, transform.position, transform.rotation);
			}
		}
	}
}
