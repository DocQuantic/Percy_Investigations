using UnityEngine;

public class DataResetter : MonoBehaviour {

    public static DataResetter instance;

    public ResettableScriptableObject[] resettableScriptableObjects;

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
}
