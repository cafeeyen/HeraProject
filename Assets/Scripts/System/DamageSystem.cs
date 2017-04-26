using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController
{
    public void doDamage(GameObject attacker, GameObject defender)
    {
        // Attacker (ATK+-20%)*(Buff or Debuff) - (Defender DEF * (Buff or Debuff))
        // Show Dmg in Defender side
        // Decrease Defender HP
        // If Defender HP 0 -> Dead
        //  -> If Attacker is Player --> gainExp() and dropItem()
        //  -> Else Gameover
    }

    public void dropItem()
    {
        // Random item type
        // If not potion -> Item base on level -> Random rare -> Random stat
        // Else Use potion
        //  -> If HP/MP Potion --> Increse Player HP/MP
        //  -> Else(Buff potion) --> Add buff to Player
    }
}
