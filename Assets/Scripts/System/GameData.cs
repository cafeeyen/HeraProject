using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class GameData
{
    public static GameData data;
    public int lv, exp, next, map, itemID;
    public float posx, posy, posz;
    
    private int  baseHp, baseAtk, baseDef;
    //public List<Items> inventory;

    public GameData()
    {
        itemID = 0;
        lv = 1;
        baseAtk = 10;
        baseDef = 4;
        baseHp = 20;
        exp = 0;
        next = 100;
        map = 0;
        posx = 154;
        posy = 3;
        posz = 142;
        //inventory = PlayerInventory.inventory.pIList;
    }

    public void gainExp(int gain)
    {
        if (lv < 20)
        {
            exp += gain;
            while (exp > next) // if can check only once (1 -> 2 or 2 -> 3) but while can check from 1 -> 3
                LevelUp();
        }
    }

    public void LevelUp()
    {
        lv++;
        baseAtk += (int)(lv * 2);
        baseDef += (int)(lv * 1.4f);
        baseHp += (int)(lv * 5);
        if (lv < 20)
            next = (lv * 100) + (int)(Math.Sqrt(next) * 1.5 * lv);
        else
            next = 0;
    }
}

/*
 *
 *  Exp Table
 *  Lv  | Next | Total
 *  1   |  100 |     0
 *  2   |  230 |   100
 *  3   |  368 |   330
 *  4   |  515 |   698
 *  5   |  670 |  1213
 *  6   |  832 |  1883
 *  7   | 1002 |  2715
 *  8   | 1179 |  3717
 *  9   | 1363 |  4896
 *  10  | 1553 |  6259
 *  11  | 1750 |  7812
 *  12  | 1952 |  9562
 *  13  | 2161 | 11514
 *  14  | 2376 | 16051
 *  15  | 2596 | 16051
 *  16  | 2882 | 18647
 *  17  | 3054 | 21469
 *  18  | 3292 | 24523
 *  19  | 3535 | 27815
 *  20  |  --  | 31350
 * 
 */
