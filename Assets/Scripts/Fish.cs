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
        likedPlants = new List<TypeOfPlantEnum>();
        hatedPlants = new List<TypeOfPlantEnum>();
        switch (typeOfFish)
        {
            case typeOfFishEnum.peaceful:
                {
                    likedPlants.Add(TypeOfPlantEnum.Type1);
                    hatedPlants.Add(TypeOfPlantEnum.Type2);
                    break;
                }
            case typeOfFishEnum.aggresive:
                {
                    likedPlants.Add(TypeOfPlantEnum.Type2);
                    hatedPlants.Add(TypeOfPlantEnum.Type1);
                    break;
                }
        }
        StartCoroutine(TimerToHungry(2));

        if (speed < 0)
        {
            Flip();
            speed *= -1;
        }
    }

    const int maxHappiness = 100;
    int hapiness = 50;
    public typeOfFishEnum typeOfFish = typeOfFishEnum.peaceful;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;
    private Fish TargetFishToEat;
    public List<TypeOfPlantEnum> likedPlants { get; private set; }
    public List<TypeOfPlantEnum> hatedPlants { get; private set; }

    //minNumberOfFishToHapiness of my type/spiecies
    public int minNumberOfFishToHapiness = 1;
    public int maxNumberOfFishToHapiness = 1;

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if (TargetFishToEat != null)
        {
            float step = 1 * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, TargetFishToEat.transform.position.y), step);
        }
    }

    private IEnumerator TimerToHungry(int timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        ChooseFishToEat();
    }

    private void ChooseFishToEat()
    {
        var fish = FindObjectsOfType<Fish>();

        foreach (var f in fish)
        {
            if (f.sizeOfFish < sizeOfFish)
            {
                TargetFishToEat = f;
                break;
            }
        }
    }

    private void MoveHorizontal()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void MoveVertical()
    {

        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void Flip()
    {
        var localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherFish = collision.GetComponent<Fish>();
        if (otherFish != null)
        {
            if (sizeOfFish > otherFish.sizeOfFish)
            {
                Destroy(otherFish.gameObject);
            }
        }
    }
}
