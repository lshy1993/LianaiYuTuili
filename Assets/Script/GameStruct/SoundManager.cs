using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource currentBGM;
    public AudioSource currentSE;
    public AudioSource currentVoice;

    private AudioClip ac;
	// Use this for initialization
	void Start () {
        //SetBGM("HEAT UP");
	
	}

    public void SetBGM(string fileName)
    {
        ac = Resources.Load("Audio/" + fileName) as AudioClip;
        currentBGM.clip = ac;
        currentBGM.Play();
    }
    public void SetSE(string fileName)
    {
        ac = Resources.Load("Audio/" + fileName) as AudioClip;
        currentSE.clip = ac;
        currentSE.Play();
    }
    public void SetVoice(string fileName)
    {
        ac = Resources.Load("Audio/" + fileName) as AudioClip;
        currentVoice.clip = ac;
        currentVoice.Play();
    }
}
