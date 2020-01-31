using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfFishEnum { peaceful, aggresive }
public enum sizeOfFishEnum { small, medium, big }
public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    const int maxHappiness = 100;
    int hapiness = 50;
    public typeOfFishEnum typeOfFish = typeOfFishEnum.peaceful;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;

    // Update is called once per frame
    void Update()
    {

    }
}
