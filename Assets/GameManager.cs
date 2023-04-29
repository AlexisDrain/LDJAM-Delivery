using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static List<GameObject> babyInventory = new List<GameObject>();

    void Awake() {
        gameManagerObj = gameObject;
    }
}
