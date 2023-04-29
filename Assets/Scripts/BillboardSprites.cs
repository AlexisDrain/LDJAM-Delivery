using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class BillboardSprites : MonoBehaviour
{
    public bool yAxis = true;
    void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        if(yAxis == false) {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f));
        }
    }
}
