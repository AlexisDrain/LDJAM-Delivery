using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class DistanceMakeBigger : MonoBehaviour
{
    public float scaleFactor = 0.1f;
    void LateUpdate() {

        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        distance = Mathf.Clamp(distance, 1f, 50f);

        Vector3 newScale = new Vector3((distance * scaleFactor), (distance * scaleFactor), (distance * scaleFactor));
        transform.localScale = Vector3.one + newScale;
    }
}
