using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money = 100;
    public StatsManager statsManager;
    public GameObject itemsContainer;
    public GameObject objToSpawn;
    public GameObject spriteOfObjectToSpawn;
    private GameManager gameManager;

    private float[] fixedYPositions = { 0.45f, 0.25f, 0.05f, -0.15f };

    private bool freezeY = false;

   

    public void SelectObjectWithFreezeY(GameObject objectToSpawn, GameObject sprite, int cost)
    {
        if (cost > money) { return; }

        freezeY = true;

        money -= cost;
        statsManager.SetMoneyText(money);

        objToSpawn = objectToSpawn;
        spriteOfObjectToSpawn = Instantiate(sprite, Vector3.zero, Quaternion.identity);
        spriteOfObjectToSpawn.transform.parent = itemsContainer.transform;
        spriteOfObjectToSpawn.transform.localScale = Vector3.one;
    }

    public void SelectObject(GameObject objectToSpawn, GameObject sprite, int cost)
    {
        if (cost > money) { return; }

        freezeY = false;

        money -= cost;
        statsManager.SetMoneyText(money);

        objToSpawn = objectToSpawn;
        spriteOfObjectToSpawn = Instantiate(sprite, Vector3.zero, Quaternion.identity);
        spriteOfObjectToSpawn.transform.parent = itemsContainer.transform;
        spriteOfObjectToSpawn.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        statsManager.SetMoneyText(money);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (spriteOfObjectToSpawn != null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (freezeY)
            {
                var objectPosition = new Vector3(mousePosition.x, 0, 0);
                spriteOfObjectToSpawn.transform.position = objectPosition;
            }
            else
            {
                var objectPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                spriteOfObjectToSpawn.transform.position = objectPosition;
            }
        }

        if (Input.GetMouseButtonDown(0) && spriteOfObjectToSpawn != null)
        {
            var objectPosition = spriteOfObjectToSpawn.transform.position;
            objectPosition.z = spriteOfObjectToSpawn.transform.parent.position.z;
            var go = Instantiate(objToSpawn, objectPosition, Quaternion.identity);

            go.transform.parent = spriteOfObjectToSpawn.transform.parent;
            go.transform.localScale = Vector3.one;
            go.transform.position = objectPosition;

            var food = objToSpawn.GetComponent<Food>();

            Destroy(spriteOfObjectToSpawn);
            objToSpawn = null;
            spriteOfObjectToSpawn = null;

            if(food!=null)
            {
                if(food.type == typeOfFood.meat)
                {
                    gameManager.MakeAllMeatFishToSearchForNewTarget();
                }else
                {
                    gameManager.MakeAllPlantFishToSearchForNewTarget();
                }           
            }


        }
    }
}
