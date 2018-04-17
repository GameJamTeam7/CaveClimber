using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class ButtonHandler : MonoBehaviour
{

    private GameManager GM;
    private Camera cam;


    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        if (GM.CurrentGameState == GameManager.GameState.GameState)
        {
            transform.position = transform.position - new Vector3(0, Time.deltaTime * GM.GameSpeed, 0);
            if (transform.position.y < (cam.transform.position.y - (0.5 * cam.orthographicSize + 2)))
            {
                if (gameObject.tag == "Button")
                {
                    GM.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
    }
}