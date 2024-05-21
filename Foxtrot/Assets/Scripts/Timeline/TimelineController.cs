using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector[] timelinesToPlay;

    public void Play() {
        foreach (PlayableDirector director in timelinesToPlay)
            director.Play();
    }
}
