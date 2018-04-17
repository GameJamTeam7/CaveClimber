using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class TextureOffset : MonoBehaviour {


    [SerializeField]
    private float scrollSpeed;

    private float AcumulatedTime = 0.0f;
    public Renderer rend;
    GameManager GM = null;


	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GM.CurrentGameState == GameManager.GameState.GameState)
        {
            AcumulatedTime += Time.deltaTime * scrollSpeed;
            float offset = AcumulatedTime;

            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
        }
    }
}
