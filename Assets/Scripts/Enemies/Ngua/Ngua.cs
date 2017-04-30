using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ngua
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Ngua(int lv)
    {
        this.lv = lv;
        atk = lv * 20;
        def = lv * 4;
        hp = lv * 24;
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
