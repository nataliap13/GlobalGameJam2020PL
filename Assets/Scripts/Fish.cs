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

    //minNumberOfFishToHapiness of my type/spiecies
    public int minNumberOfFishToHapiness = 1;
    public int maxNumberOfFishToHapiness = 1;

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    public void Flip ()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
        speed *= -1;
    }
}
