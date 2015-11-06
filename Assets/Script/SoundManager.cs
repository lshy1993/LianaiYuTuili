using UnityEngine;
using System.Collections;

public class SoundManager {

    private static SoundManager instance;
    private SoundManager()
    {

    }


    public static SoundManager get()
    {
        if (instance == null) instance = new SoundManager();

        return instance;
    }
}
