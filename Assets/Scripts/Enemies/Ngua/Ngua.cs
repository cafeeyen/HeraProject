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
        atk = (int)(lv * 36); // Max 720
        def = (int)(lv * 16); // Max 320
        hp = (int)(lv * 40); // Max 800
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
