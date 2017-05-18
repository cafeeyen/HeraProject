using System;

public class Cyclop
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Cyclop(int lv)
    {
        this.lv = lv;
        atk = 540 - (int)(Math.Log(21 - lv, 2.8) * 540 / 3);
        def = 420 - (int)(Math.Log(21 - lv, 2.8) * 420 / 3);
        hp = 1050 - (int)(Math.Log(21 - lv, 2.8) * 1050 / 3);
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

/*
Cyclop
LV	ATK	DEF	HP
1	17	13	32
2	26	20	50
3	35	27	68
4	45	35	87
5	56	44	108
6	67	52	130
7	79	62	153
8	92	72	179
9	106	83	206
10	121	94	235
11	138	107	268
12	156	122	304
13	177	138	344
14	200	156	389
15	227	177	441
16	259	202	503
17	298	232	579
18	348	271	677
19	419	326	815
20	540	420	1050
*/
