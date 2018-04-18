using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class Rock : MonoBehaviour {

    private float SelfTimer;
    [SerializeField]
    [Tooltip("Rocks Fall Speed")]
    private float FallSpeed = 0.9f;
    [SerializeField]
    [Tooltip("Rocks Despawn Timer")]
    private float DestroyTime = 10;
	
	// Update is called once per frame
	void Update ()
    {
        SelfTimer += Time.deltaTime;
        this.transform.Translate(-Vector3.up * 0.016f * FallSpeed, Space.World);
        if (SelfTimer > DestroyTime)
        {
            Destroy(this.gameObject);
        }
	}
}
