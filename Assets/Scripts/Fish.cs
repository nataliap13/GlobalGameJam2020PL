using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum sizeOfFishEnum { small, medium, big }

public class Fish : MonoBehaviour
{
    public typeOfFoodEnum typeOfFood = typeOfFoodEnum.plant;
    public sizeOfFishEnum sizeOfFish = sizeOfFishEnum.small;
    public IdeaEatManager ideaEatManager;//set In Unity FishPrefab
    public List<TypeOfPlantEnum> LikedPlants { get; private set; }
    public List<TypeOfPlantEnum> HatedPlants { get; private set; }
    public List<TypeOfPlantEnum> EatenPlants { get; private set; }
    public bool eatPlants = false;

    private GameObject TargetToEat;
    private bool hungerTriggerIsOn = false;
    private bool isHunting = false;
    //private bool isHungry = false;
    //private float timeBeforeHungry = 1f;
    private float minHungerInterval = 1f;
    private float maxHungerInterval = 3f;

    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float timeToStartHunting = 4f;
    private GameManager gameManager;

    public float timeToDeath = 10f;
    private Coroutine HungryDieTimerCoroutine;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddFishToLists(this);
        ideaEatManager.SetTargetSprite(typeOfFood);
    }

    private void OnDestroy()
    {
        gameManager.NotifyFishDeath(this);
        print(this.name + " died!");
    }

    // Start is called before the first frame update
    void Start()
    {
        LikedPlants = new List<TypeOfPlantEnum>();
        HatedPlants = new List<TypeOfPlantEnum>();
        EatenPlants = new List<TypeOfPlantEnum>();
        switch (typeOfFood)
        {
            case typeOfFoodEnum.plant:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type1);
                    HatedPlants.Add(TypeOfPlantEnum.Type2);
                    break;
                }
            case typeOfFoodEnum.meat:
                {
                    LikedPlants.Add(TypeOfPlantEnum.Type2);
                    HatedPlants.Add(TypeOfPlantEnum.Type1);
                    break;
                }
        }

        if (transform.localScale.x < 0)
        { speed *= -1; }
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        if (isHunting == true && TargetToEat != null)
        {
            float step = 1 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, TargetToEat.transform.position.y, transform.position.z), step);
            //fix strange bug with strange Z
            var pos = transform.localPosition;
            pos.z = 0;
            transform.localPosition = pos;
        }
        else if (TargetToEat == null)
        {

            if (HungryDieTimerCoroutine != null)
            {
                ChooseTargetToEat(0);//if you are hungry but fish does not have target, search for target now
            }
            else//if (HungryDieTimerCoroutine == null)
            {
                if (!hungerTriggerIsOn)
                {
                    StartCoroutine(HungerTrigger());
                }
            }
        }
    }

    private IEnumerator HungerTrigger()//randomize when fish is hungry again
    {
        hungerTriggerIsOn = true;

        var hungerInterval = Random.Range(minHungerInterval, maxHungerInterval);
        yield return new WaitForSeconds(hungerInterval);//fish will be hungry after this time

        //float timeToShowCloud = Mathf.Clamp(timeToGetHungry - timeBeforeHungry, 0f, timeToGetHungry);
        //yield return new WaitForSeconds(timeToShowCloud);
        //print(gameObject.name +": "+ timeToShowCloud);

        ideaEatManager.SetActive(true);
        ChooseTargetToEat(timeToStartHunting);
        HungryDieTimerCoroutine = StartCoroutine(KillFishAfterDelay(timeToDeath));

        //isHungry = true;
        //isHunting = false;
        hungerTriggerIsOn = false;
    }

    private IEnumerator TimerToHunt(float timeInSeconds)
    {
        //if (HungryDieTimerCoroutine != null)
        //if (isHungry)
        {
            //dieTimerCoroutine=StartCoroutine(KillFishAfterDelay(timeToDeath));
            yield return new WaitForSeconds(timeInSeconds);
            ideaEatManager.SetActive(false);
            //isHungry = false;
            isHunting = true;
        }
    }

    public void ChooseTargetToEat(float timeInSeconds)
    {
        /*if (timeInSeconds > 0)
        {//pop up think cloud            
            ideaEatManager.SetActive(true);
        }*/

        //first try to eat food rather than plant or other fish
        var food = FindObjectsOfType<Food>().Where(x => x.typeOfFood == typeOfFood);
        foreach (var f in food)
        {
            TargetToEat = f.gameObject;
            StartCoroutine(TimerToHunt(timeInSeconds));
            return;
        }

        //if not returned, so if not found their food, do this switch
        switch (typeOfFood)
        {
            case typeOfFoodEnum.meat:
                {
                    var fish = FindObjectsOfType<Fish>();
                    foreach (var f in fish)
                    {
                        if (f.sizeOfFish < sizeOfFish)
                        {
                            TargetToEat = f.gameObject;
                            StartCoroutine(TimerToHunt(timeInSeconds));
                            return;
                        }
                    }
                    break;
                }
            case typeOfFoodEnum.plant:
                {
                    var plants = FindObjectsOfType<Plant>();
                    foreach (var p in plants)
                    {
                        if (EatenPlants.Contains(p.TypeOfPlant))
                        {
                            TargetToEat = p.gameObject;
                            StartCoroutine(TimerToHunt(timeInSeconds));
                            return;
                        }
                    }
                    break;
                }
        }
    }

    private void MoveHorizontal()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        var pos = transform.localPosition;
        pos.z = 0;
        transform.localPosition = pos;
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
        if (TargetToEat != null)
        {
            var food = collision.GetComponent<Food>();
            var targetFood = TargetToEat.GetComponent<Food>();
            if (food != null && targetFood != null && food == targetFood)
            {
                Destroy(food.gameObject);
                //print(gameObject.name + " ate " + collision.gameObject.name);
                isHunting = false;
                //isHungry = false;
                if (HungryDieTimerCoroutine != null)
                {
                    print(gameObject.name + " has stopped the coroutine");
                    StopCoroutine(HungryDieTimerCoroutine);
                    HungryDieTimerCoroutine = null;
                }
                return;
            }

            var otherFish = collision.GetComponent<Fish>();
            var targetFish = TargetToEat.GetComponent<Fish>();
            if (otherFish != null && targetFish != null && otherFish == targetFish)
            {
                Destroy(otherFish.gameObject);
                //print(gameObject.name + " ate " + collision.gameObject.name);
                isHunting = false;
                //isHungry = false;
                if (HungryDieTimerCoroutine != null)
                {
                    print(gameObject.name + " has stopped the coroutine");
                    StopCoroutine(HungryDieTimerCoroutine);
                    HungryDieTimerCoroutine = null;
                }
                return;
            }
        }
    }

    ///*
    private IEnumerator KillFishAfterDelay(float delay)
    {
        print(gameObject.name + " will die after " + delay + " seconds");
        yield return new WaitForSeconds(delay);
        //gameManager.NotifyFishDeath(this);
        Destroy(this.gameObject);
    }//*/
}
