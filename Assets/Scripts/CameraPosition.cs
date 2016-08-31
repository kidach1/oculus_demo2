using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    private bool flg = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Jump1"))
        {
            if(flg)
            {
                transform.Translate(0, -3, 20);
                flg = false;
            } else
            {
                transform.Translate(0, 3, -20);
                flg = true;
            }
        }
	}
}
