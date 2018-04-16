using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Rock_Spawner : MonoBehaviour {

    public GameObject SpawnObject;
    GameManager GM = null;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Instantiate(SpawnObject, transform.position, transform.rotation);

    }

    // Update is called once per frame
    void Update () {
	}
}
