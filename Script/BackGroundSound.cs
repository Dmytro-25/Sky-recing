using UnityEngine;

public class BackGroundSound : MonoBehaviour
{
    [SerializeField] private string createdTag;
    private bool enableM;
    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(this.createdTag);
        if (obj != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.createdTag;
            DontDestroyOnLoad(gameObject);
        }
        CheckMusic();
    }
    private void Start()
    {
        CheckMusic();
    }
    private void CheckMusic()
    {
        enableM = PlayerPrefs.GetInt("Music_Enablad") == 1 ? true : false;
        if (enableM == false)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Pause();
        }
    }
}
