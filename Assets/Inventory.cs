using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject itemsContainer;
    private GameObject objectToSpawn;

    public void SelectObject(GameObject selectedObject)
    {
        objectToSpawn = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        objectToSpawn.GetComponent<BoxCollider2D>().enabled = false;
        objectToSpawn.transform.parent = itemsContainer.transform;
        objectToSpawn.transform.localScale = Vector3.one;
    }


    private void Update()
    {
        if (objectToSpawn != null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var objectPosition = new Vector3(mousePosition.x, 0, 0);
            objectToSpawn.transform.position = objectPosition;
        }


        if (Input.GetMouseButtonDown(0) && objectToSpawn != null)
        {
            objectToSpawn.GetComponent<BoxCollider2D>().enabled = true;
            objectToSpawn = null;

        }
    }
}
