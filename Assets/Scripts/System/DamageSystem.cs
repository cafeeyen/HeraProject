using UnityEngine;

public class DamageSystem
{
    private static GameData player = GameData.data;
    private HeraControl hera;

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
        if (player.curHp <= 0){
            Time.timeScale = 0;
            //heraDie();
        }
            
    }

    //can't useabel cause nin static method 
    public void heraDie()
    {
        hera.dieHera();
    }

    public static void dropItem()
    {
        // Random item type
        // If not potion -> Item base on level -> Random rare -> Random stat
        // Else Use potion
        //  -> If HP/MP Potion --> Increse Player HP/MP
        //  -> Else(Buff potion) --> Add buff to Player
    }

}
