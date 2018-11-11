using UnityEngine;

public class DataResetter : MonoBehaviour {

    public static DataResetter instance;

    public Condition[] exceptionConditions;
    public ResettableScriptableObject[] resettableScriptableObjects;

    private SceneController sceneController;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        for(int i = 0; i < resettableScriptableObjects.Length; i++)
        {
            resettableScriptableObjects[i].Reset();
        }
    }

    private void Start()
    {
        sceneController = SceneController.instance;

        for (int i = 0; i < exceptionConditions.Length; i++)
        {
            exceptionConditions[i].satisfied = true;
        }

        sceneController.FadeAndLoadScene("PercyOffice");
    }
}
