using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class BabyUIScript : MonoBehaviour
{
    public BabyStats myBabyStats;
    public void GiveChild() {
        GameObject parents = GameManager.gameManagerObj.GetComponent<GameManager>().currentDialogueParents;
        parents.GetComponent<UsableEntity>().currentBaby = Instantiate(myBabyStats);
        parents.GetComponent<UsableEntity>().TakeBaby();
        GameManager.gameManagerObj.GetComponent<GameManager>().CloseBabyExchange();
    }

}
