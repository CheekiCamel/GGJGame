using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject obstacleMessagPrefab;

    //Spawn Points on the top of screen, bottom = bottom of screen
    public List<Transform> spawnPointsTop = new List<Transform>();
    public List<Transform> spawnPointsBottom = new List<Transform>();

    public float initialSpeed;
    public float speedIncrease;
    private float currentSpeed;

    public float yVariation;

    void Start()
    {
        currentSpeed = initialSpeed;

        SpawnNewMessage(true);
        SpawnNewMessage(false);
    }
    
    public void SpawnNewMessage(bool isTop)
    {
        Transform spawnTransform;

        if (isTop)
        {
            spawnTransform = spawnPointsTop[Random.Range(0, spawnPointsTop.Count)];
        }
        else
        {
            spawnTransform = spawnPointsBottom[Random.Range(0, spawnPointsBottom.Count)];
        }

        Vector3 spawnPosition = spawnTransform.position;
        spawnPosition += Vector3.up * Random.Range(0f, yVariation);


        GameObject GO = Instantiate(obstacleMessagPrefab, spawnPosition, Quaternion.identity) as GameObject;
        GO.GetComponent<ObstacleMessage>().speed = currentSpeed;
        GO.GetComponent<ObstacleMessage>().isTop = isTop;
        GO.GetComponent<ObstacleMessage>().spawnController = this.gameObject;
        GO.GetComponent<ObstacleMessage>().lifespanTracker = Time.time;

        if (isTop)
        {
            GO.transform.eulerAngles = new Vector3(0, 0, 90);

        }
        else
        {
            GO.transform.eulerAngles = new Vector3(0, 0, 270);
        }


    }


}
