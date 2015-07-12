using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player
{
    private static Player instance = null;

    private Player()
    {
        
    }
    
    public static Player get()
    {
        if(instance == null)
        {
            instance = new Player();
        }
        return instance;
    }
}
