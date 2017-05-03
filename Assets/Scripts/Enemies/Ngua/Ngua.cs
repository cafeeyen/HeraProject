using System;

public class Ngua
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Ngua(int lv)
    {
        this.lv = lv;
        atk = 720 - (int)(Math.Log(21 - lv, 2.8) * 720 / 3);
        def = 320 - (int)(Math.Log(21 - lv, 2.8) * 320 / 3);
        hp = 800 - (int)(Math.Log(21 - lv, 2.8) * 800 / 3);
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
Ngua
LV	ATK	DEF	HP
1	22	10	25
2	34	15	38
3	47	21	52
4	60	27	67
5	74	33	82
6	89	40	99
7	105	47	117
8	123	55	136
9	141	63	157
10	162	72	179
11	184	82	204
12	208	93	231
13	236	105	262
14	267	119	297
15	303	135	336
16	345	154	384
17	397	177	441
18	464	207	516
19	559	249	621
20	720	320	800
*/
