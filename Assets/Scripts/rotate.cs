using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    public float speed = 10f;

	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
