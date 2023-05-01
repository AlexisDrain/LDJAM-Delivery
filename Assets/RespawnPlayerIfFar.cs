using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class RespawnPlayerIfFar : MonoBehaviour
{
    public float maxDistance = 2000f;
    public float currentDistance = 0f;
    public Transform respawnPosTrans;
    void Start() {
        
    }

    private void FixedUpdate() {
        currentDistance = Vector3.Distance(transform.position, GameManager.playerController.transform.position);
        if (currentDistance >= maxDistance) {
            GameManager.playerController.transform.position = respawnPosTrans.position;
        }
    }
}
