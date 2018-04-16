using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Rock_Spawner : MonoBehaviour {

    public GameObject SpawnObject;
    GameManager GM = null;
    public float SpawnTimer = 0;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        SpawnTimer += Time.deltaTime * GM.gameSpeed;
        if (SpawnTimer >= 1)
        {
            Instantiate(SpawnObject, transform.position, transform.rotation);
            SpawnTimer = 0;
        }
	}
}
