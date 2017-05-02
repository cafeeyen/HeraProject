using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Golem(int lv)
    {
        this.lv = lv;
        atk = (int)(lv * 25); // Max 500
        def = (int)(lv * 24); // Max 480
        hp = (int)(lv * 60); // Max 1200
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
