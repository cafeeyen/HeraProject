using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{

    public enum Menu
    {
        MainMenu,
        NewGame,
        Continue
    }

    public Menu currentMenu;

    private void Start()
    {
        SaveLoad.Load();
    }

    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (currentMenu == Menu.MainMenu)
        {
            GUILayout.Box("Title here");
            GUILayout.Space(10);

            if (GUILayout.Button("New Game"))
            {
                PlayerInventory.inventory = new PlayerInventory();
                GameData.data = new GameData();
                currentMenu = Menu.NewGame;
            }

            if(SaveLoad.savedGames != null)
            {
                if (GUILayout.Button("Continue"))
                    currentMenu = Menu.Continue;
            }
            else
                GUILayout.Button("--------");


            if (GUILayout.Button("Quit"))
                Application.Quit();
        }

        else if (currentMenu == Menu.NewGame)
            // Test map
            //LoadingScreenManager.LoadScene(8, new Vector3(1122, 3, 259));
            // Real map
            LoadingScreenManager.LoadScene(1, new Vector3(560, 3, 170));
            //map 5 test monster
            //LoadingScreenManager.LoadScene(5, new Vector3(590, 25, 725));

        else if (currentMenu == Menu.Continue)
            LoadingScreenManager.LoadScene(SaveLoad.savedGames.map, new Vector3(SaveLoad.savedGames.posx, SaveLoad.savedGames.posy, SaveLoad.savedGames.posz) );

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
}
