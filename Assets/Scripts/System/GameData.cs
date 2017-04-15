using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public static GameData data;
    public int lv, exp, map;
    // Inventory Add here


    public GameData()
    {
        lv = 0;
        exp = 0;
        map = 0;
    }
}