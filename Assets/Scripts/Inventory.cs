using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money = 100;
    public StatsManager statsManager;
    public GameObject itemsContainer;
    public GameObject objectToSpawn;

    public void SelectObject(GameObject selectedObject, int cost)
    {
        if(cost > money) { return; }

        money -= cost;
        statsManager.SetMoneyText(money);

        objectToSpawn = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        objectToSpawn.GetComponent<BoxCollider2D>().enabled = false;
        objectToSpawn.transform.parent = itemsContainer.transform;
        objectToSpawn.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        statsManager.SetMoneyText(money);
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
            var objectPosition = objectToSpawn.transform.position;
            objectPosition.z = objectToSpawn.transform.parent.position.z;
            objectToSpawn.transform.position = objectPosition;
            objectToSpawn = null;

        }
    }
}
