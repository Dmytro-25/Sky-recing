using System.Collections;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public static ObjectPlacer instance;

    [SerializeField] private GameObject[] GamObject;
    [SerializeField] private GameObject SaveObject;
    
    private float positionRoadZ = 25f;
    private float positionRoadY;
    private float positionRoadMaxY = 3f;
    private float positionRoadMinY = -3f;
    private float frequency;
    private float frequencySave = 240f;
    private float frequencyMAX = 4f;
    private float frequencyMIN = 2.5f;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        frequency = Random.Range(frequencyMIN, frequencyMAX);
        positionRoadY = Random.Range(positionRoadMinY, positionRoadMaxY);
    }
    public void StartScriptObjectPlacer()
    {
        if (GameManager.instance.StartGame == true)
        {
            StartCoroutine(Spawn());
            StartCoroutine(SaveGame());
        }
        else
        {
            StopCoroutine(Spawn());
            StopCoroutine(SaveGame());
        }

    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds: frequency);
            Instantiate(GamObject[Random.Range(0, GamObject.Length)], new Vector3(0, positionRoadY, positionRoadZ), Quaternion.identity);
        }
    }
    public IEnumerator SaveGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds: frequencySave);
            Instantiate(SaveObject, new Vector3(0, 0, positionRoadZ), Quaternion.identity);
        }
    }
}
