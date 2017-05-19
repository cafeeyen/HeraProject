using UnityEngine;

public class DamageSystem
{
    private static GameData player = GameData.data;

    public static void DamagetoEnemy(string action, int combo, GameObject enemy)
    {
        // Calculate atk in player side
        int atk = player.baseAtk;
        if (!(player.inventory.hat is BlankItem))
        {
            Equipment hat = (Equipment)player.inventory.hat;
            atk += hat.atk;
        }
        if (!(player.inventory.glove is BlankItem))
        {
            Equipment glove = (Equipment)player.inventory.glove;
            atk += glove.atk;
        }
        if (!(player.inventory.suit is BlankItem))
        {
            Equipment suit = (Equipment)player.inventory.suit;
            atk += suit.atk;
        }

        // Multiply Dmg for each Action
        switch (action)
        {
            case "Kicking": atk = (int)(atk * 1.8); break;
            case "Slaping": atk = (int)(atk * 1.0); break;
            case "Comboing": atk = (int)(atk * 0.6 * combo); break;
        }

        // Find which emeny is target and give it Dmg
        if (enemy.GetComponent<CanipalntAIController>() != null)
        {
            Cani status = enemy.GetComponent<CanipalntAIController>().Status;
        }

        else if (enemy.GetComponent<CyclopAIControl>() != null)
        {
            Cyclop status = enemy.GetComponent<CyclopAIControl>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if(status.CurHP <= 0)
            {
                player.gainExp(status.LV * 10);
                dropItem();
            }
        }

        else if (enemy.GetComponent<GolemAIController>() != null)
        {
            Golem status = enemy.GetComponent<GolemAIController>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if (status.CurHP <= 0)
            {
                player.gainExp(status.LV * 10);
                dropItem();
            }
        }

        else if (enemy.GetComponent<HarpyAIController>() != null)
        {
            Harpy status = enemy.GetComponent<HarpyAIController>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if (status.CurHP <= 0)
            {
                player.gainExp(status.LV * 10);
                dropItem();
            }
        }

        else if (enemy.GetComponent<HarpyRedAIController>() != null)
        {
            HarpyRed status = enemy.GetComponent<HarpyRedAIController>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if (status.CurHP <= 0)
            {
                player.gainExp(status.LV * 10);
                dropItem();
            }
        }

        else if (enemy.GetComponent<NguaAIController>() != null)
        {
            Ngua status = enemy.GetComponent<NguaAIController>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if (status.CurHP <= 0)
            {
                player.gainExp(status.LV * 10);
                dropItem();
            }
        }

        else if (enemy.GetComponent<MiaNoiAIController>() != null)
        {
            MiaNoi status = enemy.GetComponent<MiaNoiAIController>().Status;
            status.CurHP = Mathf.Max(atk - status.DEF);
            if (status.CurHP <= 0)
            {
                player.gainExp(status.LV * 50);
            }
        }
    }

    public static void DamageToPlayer(int atk, string action)
    {
        // Multiply Dmg for each Action
        switch(action)
        {
            // Ngua
            case ("Ngua_Head"): atk = (int)(atk * 1.2);break;
            case ("Ngua_Slap"): atk = (int)(atk * 0.8); break;
            case ("Ngua_Tail"): atk = (int)(atk * 1.0); break;

            // Cyclop
            case ("Cyclop_Head"): atk = (int)(atk * 1.3); break;
            case ("Cyclop_Club"): atk = (int)(atk * 0.9); break;

            // Harpy(Black)
            case ("Harpy_Head"): atk = (int)(atk * 1.2); break;

            // Harpy(Red)
            case ("HarpyRed_Head"): atk = (int)(atk * 1.2); break;

            // CaniPlant
            case ("Cani_Bite"): atk = (int)(atk * 1.4); break;
            case ("Cani_Tentacle"): atk = (int)(atk * 0.9); break;

            // Golem
            case ("Golem_Hand"): atk = (int)(atk * 1.0); break;

            // Boss
            case ("MiaNoi_Hand"): atk = (int)(atk * 1.0); break;
            case ("MiaNoi_Pan"): atk = (int)(atk * 1.2); break;
        }

        // Calculate def in player side
        int def = player.baseDef;
        if (!(player.inventory.hat is BlankItem))
        {
            Equipment hat = (Equipment)player.inventory.hat;
            def += hat.def;
        }
        if (!(player.inventory.glove is BlankItem))
        {
            Equipment glove = (Equipment)player.inventory.glove;
            def += glove.def;
        }
        if (!(player.inventory.suit is BlankItem))
        {
            Equipment suit = (Equipment)player.inventory.suit;
            def += suit.def;
        }

        // Decrease Player HP
        player.curHp -= Mathf.Max(0, atk - def);

        // If Player HP <= 0 ---> Dead
        if (player.curHp <= 0)
        {
            //Time.timeScale = 0;
            //heraDie();
        }
            
    }

    // //can't useabel cause nin static method 
    // public void heraDie()
    // {
    //     hera.dieHera();
    // }

    public static void dropItem()
    {
        // Lucky or Not!?
        int drop = Random.Range(0, 3);
        if(drop == 2)
        {
            // Random item type
        }

        // If not potion -> Item base on level -> Random rare -> Random stat
        // Else Use potion
        //  -> If HP/MP Potion --> Increse Player HP/MP
        //  -> Else(Buff potion) --> Add buff to Player
    }

}
