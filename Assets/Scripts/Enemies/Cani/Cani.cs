using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cani
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Cani(int lv)
    {
        this.lv = lv;
        atk = (int)(lv * 33); // Max 660
        def = (int)(lv * 15); // Max 300
        hp = (int)(lv * 43.5); // Max 870
        curHp = hp;
        alive = true;
    }

    public int LV
    {
        get { return lv; }
    }

    public int ATK
    {
        get { return atk; }
    }

    public int DEF
    {
        get { return def; }
    }

    public int HP
    {
        get { return hp; }
    }

    public int CurHP
    {
        get { return curHp; }
        set { curHp = value; }
    }
}
