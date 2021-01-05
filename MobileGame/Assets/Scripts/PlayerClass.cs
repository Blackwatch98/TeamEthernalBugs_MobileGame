using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IF PLAYER PREFS WONT WORKOUT
public class PlayerClass : MonoBehaviour
{
    public float coinsWealth;
    public float dollarsWealth;
    //owned buildins

    PlayerClass()
    {
        //loadStats(coinsWealth, dollarsWealth)
    }

    public float getCoinsWealth()
    {
        return coinsWealth;
    }

    public float getDollarsWealth()
    {
        return dollarsWealth;
    }

    public void setCoinsWealth(int coins)
    {
        coinsWealth = coins;
    }

    public void setDollarsWealth(int dollars)
    {
        dollarsWealth = dollars;
    }
}
