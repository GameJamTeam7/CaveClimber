using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class Rock : MonoBehaviour {

    GameManager GM = null;
    private float SelfTimer;

	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        SelfTimer += Time.deltaTime;
        this.transform.Translate(-Vector3.up * 0.016f *GM.gameSpeed, Space.World);
        if (SelfTimer > 30)
        {
            Destroy(this.gameObject);
        }
	}
}
