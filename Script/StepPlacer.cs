using System.Collections.Generic;
using UnityEngine;

public class StepPlacer : MonoBehaviour
{
    public static StepPlacer instance;

    public GameObject[] StepPrefabs;
    public List<GameObject> StepPrefabsList = new List<GameObject>();
    public float maxSpeed = 25;
    public int maxStepCount = 16;
    public float speed = 0f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < maxStepCount; i++)
        {
            CreateNextStep();
        }
        StartLevel();
    }

    private void Update()
    {
        if (speed == 0) return;
        foreach (GameObject stepObject in StepPrefabsList)
        {
            stepObject.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (StepPrefabsList[0].transform.position.z < - 1)
        {
            Destroy(StepPrefabsList[0]);
            StepPrefabsList.RemoveAt(0);
            
            CreateNextStep();
        }
    }
    public void CreateNextStep()
    {
        Vector3 pos = Vector3.zero;
        if (StepPrefabsList.Count > 0)
        {
            pos = StepPrefabsList[StepPrefabsList.Count - 1].transform.position + new Vector3(0, 0, 2f);
        }
        GameObject go = Instantiate(StepPrefabs[Random.Range(0, StepPrefabs.Length)], pos, Quaternion.identity);
        go.transform.SetParent(transform);
        StepPrefabsList.Add(go);
    }
    public void StartLevel()
    {
        speed = maxSpeed;
        Time.timeScale = 1f;
    }
    public void DestroyStep()
    {
        while (StepPrefabsList.Count > 0)
        {
            Destroy(StepPrefabsList[0]);
            StepPrefabsList.RemoveAt(0);
        }
    }
}
