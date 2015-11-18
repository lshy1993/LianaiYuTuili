using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private static AnimationManager instance;

    private AnimationManager()
    {

    }
    public static AnimationManager get()
    {
        if (instance == null) instance = new AnimationManager();

        return instance;
    }

}
