using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{


    [SerializeField] private GameObject platesVisual;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform platesCounterVisualSpawnPoint;
    private List<GameObject> plateGameObjectList;

    void Awake()
    {
        plateGameObjectList = new List<GameObject>();
    }
    void Start()
    {
         platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
         platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(PlatesCounter counter)
    {
        if (platesCounter == counter)
        {
            GameObject plate = Instantiate(platesVisual, platesCounterVisualSpawnPoint);
            float plateHeight = plateGameObjectList.Count * 0.1f;
            plate.transform.localPosition = new Vector3(0, plateHeight, 0);
            plateGameObjectList.Add(plate);
        }
    }
    private void PlatesCounter_OnPlateRemoved(PlatesCounter counter)
    {
        if (platesCounter == counter)
        {
            GameObject plate = plateGameObjectList[plateGameObjectList.Count - 1]; // get lastt plate and destroy 
            plateGameObjectList.Remove(plate);
            Destroy(plate);
        }
    }

}
