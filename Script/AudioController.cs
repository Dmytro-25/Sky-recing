using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Sprite MusicON;
    [SerializeField] private Sprite MusicOFF;
    [SerializeField] private GameObject BackGroundMusic;
    private bool enableM;
    private bool workButton;
    [SerializeField] private GameObject Music;

    private void Start()
    {
        enableM = PlayerPrefs.GetInt("Music_Enablad") == 1 ? true : false;
    }
    private void FixedUpdate()
    {
        if (Music != null && workButton == false)
        {
            if(BackGroundMusic == null)
            {
                BackGroundMusic = GameObject.FindGameObjectWithTag("BackGroundMusic");
            }

            workButton = true;
            CheckImage();
        }
    }
    public void MusicButton()
    {
        if (enableM == true)
        {
            enableM = false;
            BackGroundMusic.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            enableM = true;
            BackGroundMusic.gameObject.GetComponent<AudioSource>().Pause();
        }
        PlayerPrefs.SetInt("Music_Enablad", enableM ? 1 : 0);
        CheckImage();
    }
    private void CheckMusic()
    {
        if (enableM == false)
        {
            BackGroundMusic.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            BackGroundMusic.gameObject.GetComponent<AudioSource>().Pause();
        }
    }
    private void CheckImage()
    {
        if (enableM == false)
        {
            Music.GetComponent<Button>().image.sprite = MusicON;
        }
        else
        {
            Music.GetComponent<Button>().image.sprite = MusicOFF;
        }
    }
}
