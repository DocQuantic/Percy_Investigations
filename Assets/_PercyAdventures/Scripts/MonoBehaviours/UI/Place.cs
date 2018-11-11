using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {

    public Condition[] conditions;

    public void CheckActive()
    {
        Debug.Log(transform.name);
        if (conditions.Length == 0)
        {
            Debug.Log("empty table");
            return;
        }
        
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

}
