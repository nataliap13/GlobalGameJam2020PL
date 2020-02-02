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

    private float[] fixedYPositions = { -3f, -1f, 1f, 3f };

    private bool freezeY = false;
    private bool clickableArea = true;
    [SerializeField]
    private bool plantUsed = false;

    public void SelectObjectWithFreezeY(GameObject objectToSpawn, GameObject sprite, int cost)
    {
        if (cost > money || plantUsed) { return; }

        freezeY = true;

        money -= cost;
        statsManager.SetMoneyText(money);
        plantUsed = true;
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

            if(mousePosition.y < -3f)
            {
                clickableArea = false;
            }else
            {
                clickableArea = true;
            }

            if (freezeY)
            {
                var objectPosition = new Vector3(mousePosition.x, 0, 0);
                spriteOfObjectToSpawn.transform.position = objectPosition;
            }
            else
            {
                var yRounded = Mathf.Clamp(mousePosition.y, -3f, 3f);
                print(yRounded);
                float min = float.MaxValue;
                int index = 0;
                for (int i = 0; i < fixedYPositions.Length; i++)
                {
                    var x = Mathf.Abs(mousePosition.y - fixedYPositions[i]);
                    if(x < min)
                    {
                        min = x;
                        index = i;
                    }
                }

                var objectPosition = new Vector3(mousePosition.x, fixedYPositions[index], 0);
                spriteOfObjectToSpawn.transform.position = objectPosition;
            }
        }

        if (Input.GetMouseButtonDown(0) && spriteOfObjectToSpawn != null)
        {
            if(clickableArea)
            {
                var objectPosition = spriteOfObjectToSpawn.transform.position;
                objectPosition.z = spriteOfObjectToSpawn.transform.parent.position.z;
                var go = Instantiate(objToSpawn, objectPosition, Quaternion.identity);
                //print("Utworzono " + go.name);
                go.transform.parent = spriteOfObjectToSpawn.transform.parent;
                go.transform.localScale = Vector3.one;
                go.transform.position = objectPosition;

                var food = objToSpawn.GetComponent<Food>();

                Destroy(spriteOfObjectToSpawn);
                objToSpawn = null;
                spriteOfObjectToSpawn = null;

                if (food != null)
                {
                    if (food.typeOfFood == typeOfFoodEnum.meat)
                    {
                        gameManager.MakeAllMeatFishToSearchForNewTarget();
                    }
                    else
                    {
                        gameManager.MakeAllPlantFishToSearchForNewTarget();
                    }
                }
            }
        }
    }
}
