using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int IndexScore;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Bonus") && other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
