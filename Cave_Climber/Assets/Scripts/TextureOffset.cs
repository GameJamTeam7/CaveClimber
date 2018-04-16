using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour {

    public float ScrollSpeed = 0.0f;
    private float AcumulatedTime = 0.0f;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        AcumulatedTime += Time.deltaTime * ScrollSpeed;
        float offset = AcumulatedTime;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);

    }
}
