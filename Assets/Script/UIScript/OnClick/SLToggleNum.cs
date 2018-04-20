using UnityEngine;
using System.Collections;

public class SLToggleNum : BasicButton
{
    public SaveLoadUIManager slm;
    public int id;

    protected override void Execute()
    {
        slm.SetFileNum();
    }
}
