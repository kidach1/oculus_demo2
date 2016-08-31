using UnityEngine;
using System.Collections;

public class ShotEnemy : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		// 出現後一定時間で自動的に消滅させる
		Destroy(gameObject, 2.0F);
	}
	
	// Update is called once per frame
	void Update () {
		// 弾を前進させる
		transform.position += transform.forward * Time.deltaTime * 300;
	}

	// 他のオブジェクトと衝突した時に呼ばれるメソッド
	private void OnCollisionEnter(Collision collider) {
//		Debug.Log ("collision!" + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player" || collider.gameObject.name == "Terrain") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}
}
