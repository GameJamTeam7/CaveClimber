using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class Rock : MonoBehaviour {

    GameManager GM = null;

	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(-Vector3.up * GM.gameSpeed, Space.World);
	}
}
