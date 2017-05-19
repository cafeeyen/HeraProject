using System;

public class MiaNoi
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public MiaNoi(int lv)
    {
        this.lv = lv;
        atk = 500 - (int)(Math.Log(21 - lv, 2.8) * 500 / 3);
        def = 320 - (int)(Math.Log(21 - lv, 2.8) * 320 / 3);
        hp = 1000 - (int)(Math.Log(21 - lv, 2.8) * 1000 / 3);
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

    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }
}