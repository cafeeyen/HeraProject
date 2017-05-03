using System;

public class Cani
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Cani(int lv)
    {
        this.lv = lv;
        atk = 660 - (int)(Math.Log(21 - lv, 2.8) * 660 / 3);
        def = 300 - (int)(Math.Log(21 - lv, 2.8) * 300 / 3);
        hp = 870 - (int)(Math.Log(21 - lv, 2.8) * 870 / 3);
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
Cani
LV	ATK	DEF	HP
20	660	300	870
19	512	233	675
18	426	194	561
17	364	166	480
16	317	144	417
15	278	126	366
14	245	112	322
13	216	99	285
12	191	87	252
11	169	77	222
10	148	68	195
9	130	59	171
8	112	51	148
7	97	44	127
6	82	37	108
5	68	31	90
4	55	25	73
3	43	20	56
2	31	15	41
1	20	10	27
*/
