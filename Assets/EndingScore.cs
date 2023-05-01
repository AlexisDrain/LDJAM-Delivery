using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Author: Alexis Clay Drain
*/
public class EndingScore : MonoBehaviour
{
    public Text endingText;
    public Image endingImage;
    public Color colorBad;

    public string babyName;
    public string parentName;

    void Start() {

    }

    public void UpdateScore() {
        if (parentName == "Hobo") {
            endingImage.color = colorBad;
            endingText.color = Color.red;
            endingText.text = "I hope you're happy this baby turned into a meal :(";
            return;
        }

        if (babyName == "Colette") {
            if (parentName == "Colette Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "Colette is happy with their real parents!";
                return;
            } else if (parentName == "Nicky Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "While not with their real parents, Colette is still happy!";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Colette is not with their real parents.";
                return;
            }
        }

        if (babyName == "Nicky") {
            if (parentName == "Nicky Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "Husband left because the wife cheated. But at least Nicky is with their real mother.";
                return;
            } else if (parentName == "Colette Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "While not with their real parents, Nicky is still happy!";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Nicky is not with their real parents.";
                return;
            }
        }

        if (babyName == "Boy One") {
            if (parentName == "Boy One Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "Boy One is with their real parents!";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Boy One is not with their real parents.";
                return;
            }
        }

        if (babyName == "Lush") {
            if (parentName == "Lush Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "Lush is with their real parents!";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Lush is not with their real parents.";
                return;
            }
        }

        if (babyName == "Jacob") {
            if (parentName == "Jacob Wife") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "You chose to give Jacob to the Ex-wife. I hope that was the right option.";
                return;
            } else if (parentName == "Jacob Husband") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "You chose to give Jacob to the Ex-husband. I hope that was the right option.";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Jacob is not with their real parents.";
                return;
            }
        }

        if (babyName == "Radish") {
            if (parentName == "Radish Parents") {
                endingImage.color = Color.white;
                endingText.color = Color.green;
                endingText.text = "Radish is with the trio parents. Their real parents.";
                return;
            } else {
                endingImage.color = colorBad;
                endingText.color = Color.red;
                endingText.text = "Radish is not with their real parents.";
                return;
            }
        }
    }

}
