using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    private GameManager GM;


    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        transform.position = transform.position - new Vector3(0, Time.deltaTime * GM.GameSpeed, 0);       
    }
}