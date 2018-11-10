using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsManagement : MonoBehaviour {

    private void OnEnable()
    {
        Question[] questions = gameObject.GetComponentsInChildren<Question>(true);

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].CheckActive();
        }
    }
}
