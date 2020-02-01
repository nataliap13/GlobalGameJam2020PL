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

    private bool freezeY = false;

    public void SelectObjectWithFreezeY(GameObject selectedObject, int cost)
    {
        if(cost > money) { return; }

        freezeY = true;

        money -= cost;
        statsManager.SetMoneyText(money);

        objectToSpawn = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        objectToSpawn.GetComponent<Collider2D>().enabled = false;
        objectToSpawn.transform.parent = itemsContainer.transform;
        objectToSpawn.transform.localScale = Vector3.one;
    }

    public void SelectObject(GameObject selectedObject, int cost)
    {
        if (cost > money) { return; }

        money -= cost;
        statsManager.SetMoneyText(money);

        objectToSpawn = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        objectToSpawn.GetComponent<Collider2D>().enabled = false;
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

            if(freezeY)
            {
                var objectPosition = new Vector3(mousePosition.x, 0, 0);
                objectToSpawn.transform.position = objectPosition;
            }
            else
            {
                var objectPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                objectToSpawn.transform.position = objectPosition;
            }


        }

        if (Input.GetMouseButtonDown(0) && objectToSpawn != null)
        {
            objectToSpawn.GetComponent<Collider2D>().enabled = true;
            var objectPosition = objectToSpawn.transform.position;
            objectPosition.z = objectToSpawn.transform.parent.position.z;
            objectToSpawn.transform.position = objectPosition;
            objectToSpawn = null;

        }
    }
}
