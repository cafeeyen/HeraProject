using System;

public class Harpy
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Harpy(int lv)
    {
        this.lv = lv;
        atk = 750 - (int)(Math.Log(21 - lv, 2.8) * 750 / 3);
        def = 300 - (int)(Math.Log(21 - lv, 2.8) * 300 / 3);
        hp = 750 - (int)(Math.Log(21 - lv, 2.8) * 750 / 3);
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

/*
Harpy
LV	ATK	DEF	HP
1	23	10	23
2	36	15	36
3	49	20	49
4	63	25	63
5	77	31	77
6	93	37	93
7	110	44	110
8	128	51	128
9	147	59	147
10	168	68	168
11	191	77	191
12	217	87	217
13	246	99	246
14	278	112	278
15	315	126	315
16	360	144	360
17	414	166	414
18	484	194	484
19	582	233	582
20	750	300	750
*/
