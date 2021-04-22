using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinger : MonoBehaviour
{
    public static AnimationFinger instance;

    private Animation animTutor;
    void Awake()
    {
        instance = this;
    }
    public void AnimationTutur()
    {
        animTutor = GetComponent<Animation>();
    }
}
