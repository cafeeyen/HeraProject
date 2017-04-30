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
        atk = (8 * lv) + (lv * lv / 2);
        def = (2 * lv) + (lv / 2);
        hp = (6 * lv) + (lv * 3 / 2);
        curHp = hp;
        alive = true;
    }

    public int Lv
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
        set { hp = value; }
    }
}
