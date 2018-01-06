using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

public class FinUIManager : MonoBehaviour
{
    public SoundManager sm;
    public PanelSwitch ps;
    public GameObject castTable;

    public int maxHeight;

    private void Update()
    {
        //按下左键则跳过
        if (Input.GetMouseButton(0))
        {
            sm.StopBGM();
            StopAllCoroutines();
            ps.SwitchTo_VerifyIterative("Title_Panel");
        }
    }

    private void OnEnable()
    {
        StartCoroutine(OpenAnimate());
    }

    private IEnumerator OpenAnimate()
    {
        sm.SetBGM("Title");
        float t = -360;
        while (t < maxHeight)
        {
            t = Mathf.MoveTowards(t, maxHeight, (maxHeight + 360) / 20f * Time.deltaTime);
            castTable.transform.localPosition = new Vector3(0, t);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        ps.SwitchTo_VerifyIterative("Title_Panel");
    }

}
