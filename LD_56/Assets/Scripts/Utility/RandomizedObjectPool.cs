using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class RandomizedObjectPool : MonoBehaviour
{
    private List<ObjectPooler> listOfRandomObjects;

    private void Awake() {
        listOfRandomObjects = new();
        GetComponents<ObjectPooler>(listOfRandomObjects);
    }

    public GameObject GetRandomObject() {
        return listOfRandomObjects[Random.Range(0,listOfRandomObjects.Count)].GetPooledObject();
    }
}
