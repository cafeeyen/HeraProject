using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Cyclop(int lv)
    {
        this.lv = lv;
        atk = (int)(lv * 27); // Max 540
        def = (int)(lv * 21); // Max 420
        hp = (int)(lv * 52.5); // Max 1050
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
