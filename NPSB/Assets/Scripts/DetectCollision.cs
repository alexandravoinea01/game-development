using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameObject beer;
    public GameObject beerPrefab;
    public float coolDown = 3.0f;
    public float respawnTime = 5.0f;
    private float spawnTime;
    private bool isRendered = true;

    public DrunkennessBar drunkennessBar;
    public int currentDrunkenness = 0;

    [SerializeField] private AudioSource beerSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "beer")
        {
            beerSound.Play();
            beer = other.gameObject;
            StartCoroutine(ShowAndHide(coolDown));
            AddDrunkenness();
        }
    }

    private void AddDrunkenness()
    {
        Globals.drunkenness = (Globals.drunkenness + 2) <= drunkennessBar.GetMaxDrunkenness()
                ? Globals.drunkenness + 2
                : drunkennessBar.GetMaxDrunkenness();
        drunkennessBar.SetDrunkenness(Globals.drunkenness);
    }

    void Start()
    {
        randomRespawn();
        drunkennessBar.SetMaxDrunkenness(100);
    }

    void Update()
    {
        if (Globals.freezeInteractions)
            return;

        if(Time.time - spawnTime > respawnTime && isRendered)
        {
            randomRespawn();
            spawnTime = Time.time;
        }
    }

    public void DecreaseHealth()
    {
        if (currentDrunkenness - 1 >= 0)
        {
            currentDrunkenness = currentDrunkenness - 1;
        }
        else
        {
            currentDrunkenness = 0;
        }
        drunkennessBar.SetDrunkenness(currentDrunkenness);
    }

    IEnumerator ShowAndHide(float coolDown)
    {
        Destroy(beer);
        isRendered = false;
        yield return new WaitForSeconds(coolDown);
        spawnTime = Time.time;
        randomRespawn();
        isRendered = true;
    }

    void randomRespawn()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 1, Random.Range(-10, 11));
        beer = Instantiate(beerPrefab, randomSpawnPosition, Quaternion.identity);
        Destroy(beer, 5);
        isRendered = true;
    }
}








