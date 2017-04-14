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

    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (currentMenu == Menu.MainMenu)
        {
            SaveLoad.Load();

            GUILayout.Box("Title here");
            GUILayout.Space(10);

            if (GUILayout.Button("New Game"))
            {
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
            LoadingScreenManager.LoadScene(1);

        else if (currentMenu == Menu.Continue)
            LoadingScreenManager.LoadScene(SaveLoad.savedGames.map);

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
}
