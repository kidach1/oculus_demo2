using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Marker : MonoBehaviour {

	Image marker;
	public Image markerImage;
	GameObject compass;

	GameObject target;

	// Use this for initialization
	void Start () {

		target = GameObject.Find ("PlayerTarget");

		// マーカーをレーダー（コンパス）上に表示する処理
		// コンパス取得
		compass = GameObject.Find("Compass");
		// マーカーをprefabから初期化
		marker = Instantiate (markerImage, compass.transform.position, Quaternion.identity) as Image;
		// マーカーをコンパスの子オブジェクトに。第二引数をfalseにすることで、子オブジェクトにした際もスケールを維持。
		marker.transform.SetParent (compass.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
		// マーカーをプレイヤーの相対位置に配置
		Vector3 position = transform.position - target.transform.position;
		marker.transform.localPosition = new Vector3 (position.x, position.z, 0);
	}
}
