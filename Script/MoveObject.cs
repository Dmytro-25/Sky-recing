using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private float speed;
    private float speedMAX = 8f;
    private float speedMIN = 7f;
    private float posDestroy = -17f;

    void FixedUpdate()
    {
        if (GameManager.instance.StartGame == true)
        {
            speed = Random.Range(speedMIN, speedMAX);
            transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (transform.position.z < posDestroy)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            speed = 0;
        }
    }
}
