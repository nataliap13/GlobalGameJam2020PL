using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfPlantEnum { Type1, Type2, Type3 }
public class Plant : MonoBehaviour
{

    public int maxHp = 100;
    private int currentHp;
    public TypeOfPlantEnum TypeOfPlant = TypeOfPlantEnum.Type1;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fishFrontCollider = collision.GetComponent<FishFrontColider>();
        if (fishFrontCollider != null)
        {
            var fish = fishFrontCollider.GetComponentInParent<Fish>();
            if (fish.HatedPlants.Contains(TypeOfPlant))
            {
                fish.Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var fishFrontCollider = collision.GetComponent<FishFrontColider>();
        if (fishFrontCollider != null)
        {
            var fish = fishFrontCollider.GetComponentInParent<Fish>();
            if (fish.LikedPlants.Contains(TypeOfPlant))
            {
                fish.Flip();
            }
        }
    }
}
