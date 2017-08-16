using UnityEngine;
using System.Collections;

public class BacklogVoiceButton : MonoBehaviour {

    public string path;

    void OnClick()
    {
        Debug.Log("Repeat Voice" + path);
        //TODO: 语音重现
        GameObject.Find("GameManager").GetComponent<SoundManager>().SetVoice(path);
    }

}
