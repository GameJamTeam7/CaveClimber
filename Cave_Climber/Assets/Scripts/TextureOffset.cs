﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour {

    private float AcumulatedTime = 0.0f;
    public Renderer rend;
    GameManager GM = null;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        AcumulatedTime += Time.deltaTime * GM.GameSpeed;
        float offset = AcumulatedTime;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);

    }
}
