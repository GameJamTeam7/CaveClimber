using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Rock_Spawner : MonoBehaviour
{

    public GameObject SpawnObject;
    GameManager GM = null;
    public float SpawnTimer = 0;
    public float SpawnRate = 0.10f;

    // Use this for initialization
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.CurrentGameState == GameManager.GameState.GameState)
        {
            SpawnTimer += Time.deltaTime * SpawnRate;
            if (SpawnTimer >= 1)
            {
                Instantiate(SpawnObject, transform.position, Quaternion.identity);
                SpawnTimer = 0;
            }
        }
    }
}
