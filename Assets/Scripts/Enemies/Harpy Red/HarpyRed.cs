using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyRed
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public HarpyRed(int lv)
    {
        this.lv = lv;
        atk = (int)(lv * 42.5); // Max 850
        def = (int)(lv * 10); // Max 200
        hp = (int)(lv * 27.5); // Max 550
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
