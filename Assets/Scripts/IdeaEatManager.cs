using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaEatManager : MonoBehaviour
{
    public SpriteRenderer fishSprite;
    public Sprite SpriteMeat;
    public Sprite SpritePlant;
    public void SetTargetSprite(typeOfFoodEnum typeOfFoodThiFishEat)
    {
        switch (typeOfFoodThiFishEat)
        {
            case typeOfFoodEnum.plant:
                {
                    fishSprite.sprite = SpritePlant;
                    break;
                }
            case typeOfFoodEnum.meat:
                {
                    fishSprite.sprite = SpriteMeat;
                    break;
                }
        }
    }

    public void SetActive(bool active)
    {
        //print(active);
        gameObject.SetActive(active);
    }
}
