using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    private void Start()
    {
        Place[] places = gameObject.GetComponentsInChildren<Place>(true);

        for (int i = 0; i < places.Length; i++)
        {
            Debug.Log(i);
            places[i].CheckActive();
        }
    }
}
