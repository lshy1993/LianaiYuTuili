using UnityEngine;
using System.Collections;

public class BacklogVoiceButton : BasicButton {

    public string path;

    protected override void OnHover(bool ishover)
    {
        //base.OnHover(ishover);
    }

    protected override void Execute()
    {
        Debug.Log("Repeat Voice" + path);
        //TODO: 语音重现
        GameObject.Find("GameManager").GetComponent<SoundManager>().SetVoice(path);
    }

}
