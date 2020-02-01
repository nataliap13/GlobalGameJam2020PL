using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaEatManager : MonoBehaviour
{
    public SpriteRenderer fishSprite;
    public void SetTargetSprite(typeOfFishEnum typeOfFish)
    {
        switch (typeOfFish)
        {
            case typeOfFishEnum.plantEating:
                {
                    //fishSprite.sprite = ;
                    break;
                }
            case typeOfFishEnum.meatEating:
                {
                    //fishSprite.sprite = ;
                    break;
                }
        }
    }

    public void SetActive(bool active)
    {
        print(active);
        gameObject.SetActive(active);
    }
}
