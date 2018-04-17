using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class ButtonManager : MonoBehaviour
{


    #region Exposed Variables

    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]
    private GameObject buttonPrefab;

    #endregion

    private GameManager gm;
    private float buttonTimer;
    private Camera cam;
    private RaycastHit hit;


    void Start()
    {
        buttonTimer = 0;
        gm = gameObject.GetComponent<GameManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        if (gm.CurrentGameState == GameManager.GameState.GameState)
        {
            buttonTimer += Time.deltaTime;
            if (buttonTimer > gm.ButtonsPerSecond)
            {
                buttonTimer = 0;
                Instantiate(buttonPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.Euler(-90.0f, 0, 0));
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Button")
                    {
                        gm.IncresseScore();
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
#endif

#if UNITY_ANDROID
            foreach (Touch touch in Input.touches)
            {

                Ray ray = cam.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Button")
                    {
                        gm.IncresseScore();
                        Destroy(hit.transform.gameObject);
                    }
                }

            }
#endif

        }
    }


}
