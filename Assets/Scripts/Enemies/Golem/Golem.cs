using System;

public class Golem
{
    private int lv, atk, def, hp, curHp;
    private bool alive;

    public Golem(int lv)
    {
        this.lv = lv;
        atk = 500 - (int)(Math.Log(21 - lv, 2.8) * 500 / 3);
        def = 480 - (int)(Math.Log(21 - lv, 2.8) * 480 / 3);
        hp = 1200 - (int)(Math.Log(21 - lv, 2.8) * 1200 / 3);
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
Golem
LV	ATK	DEF	HP
1	16	15	37
2	24	23	57
3	33	31	78
4	42	40	100
5	52	50	123
6	62	60	148
7	73	70	175
8	85	82	204
9	98	94	235
10	112	108	269
11	128	123	306
12	145	139	347
13	164	157	393
14	186	178	445
15	210	202	504
16	240	230	575
17	276	265	662
18	323	310	774
19	388	373	931
20	500	480	1200
*/
