using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInGui : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemToSpawn;
    public GameObject spriteOfItemToSpawn;
    public int cost = 100;
    [SerializeField]
    private Text costText;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        costText.text = cost.ToString();
    }


    public void SelectItem(bool freezeY)
    {
        if(inventory.spriteOfObjectToSpawn!=null)
        {
            return;
        }
        if(freezeY)
        {
            inventory.SelectObjectWithFreezeY(itemToSpawn, spriteOfItemToSpawn, cost);
            costText.text = "X";
            costText.color = Color.red;
        }
        else
        {
            inventory.SelectObject(itemToSpawn, spriteOfItemToSpawn, cost);
        }

    }
}
