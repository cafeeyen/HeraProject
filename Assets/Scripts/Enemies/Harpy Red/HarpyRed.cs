using System;

public class HarpyRed
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public HarpyRed(int lv)
    {
        this.lv = lv;
        atk = 850 - (int)(Math.Log(21 - lv, 2.8) * 850 / 3);
        def = 200 - (int)(Math.Log(21 - lv, 2.8) * 200 / 3);
        hp = 550 - (int)(Math.Log(21 - lv, 2.8) * 550 / 3);
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
Harpy Red
LV	ATK	DEF	HP
1	26	7	17
2	40	10	26
3	55	13	36
4	71	17	46
5	88	21	57
6	105	25	68
7	124	30	81
8	145	34	94
9	167	40	108
10	191	45	124
11	217	51	141
12	246	58	159
13	278	66	180
14	315	75	204
15	357	84	231
16	408	96	264
17	469	111	304
18	548	129	355
19	660	156	427
20	850	200	550
*/
