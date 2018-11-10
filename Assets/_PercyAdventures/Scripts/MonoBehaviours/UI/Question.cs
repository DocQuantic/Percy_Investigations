using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {

    public Condition[] conditions;
    public ReactionCollection reactionCollection;

    public void CheckActive()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            Debug.Log(conditions[i].description + " is " + conditions[i].satisfied);

            if (!conditions[i].satisfied)
            {
                if (gameObject.activeSelf)
                {
                    gameObject.SetActive(false);
                }
                return;
            }
            gameObject.SetActive(true);
        }
    }

    public void React()
    {
        reactionCollection.React();
    }
}
