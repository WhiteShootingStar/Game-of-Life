using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules
{
    public List<int> ForAlive { get; set;}
    public List<int> ForDead { get; set; }

    public bool ForAliveToLive(int howMuch)
    {
       return  ForAlive.Contains(howMuch);
    }
    public bool ForDeadToBorn(int howMuch)
    {
        return ForDead.Contains(howMuch);
    }

}
