﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject selectedObject;
    public GameObject foodToSpawn;
    public GameObject itemsContainer;


    public void SelectFood()
    {
        selectedObject = Instantiate(foodToSpawn, Vector3.zero,Quaternion.identity);
        selectedObject.transform.parent = itemsContainer.transform;
        selectedObject.transform.localScale = Vector3.one;
    }


    private void Update()
    {
        if(selectedObject!=null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var objectPosition = new Vector3(mousePosition.x, 0, 0);
            selectedObject.transform.position = objectPosition;
        }


        if(Input.GetMouseButtonDown(0) && selectedObject!=null)
        {
            selectedObject = null;

        }
    }
}