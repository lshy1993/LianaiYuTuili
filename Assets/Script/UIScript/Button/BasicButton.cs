using UnityEngine;
using System.Collections;

public class BasicButton : MonoBehaviour
{
    protected SoundManager sm;

    private void Awake()
    {
        sm = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    protected virtual void OnHover(bool ishover)
    {
        if (ishover)
        {
            sm.SetSE("SE_hover");
        }
    }

    protected virtual void OnClick()
    {
        if (Input.GetMouseButtonUp(1)) return;
        sm.SetSE("SE_confirm");
    }
}
