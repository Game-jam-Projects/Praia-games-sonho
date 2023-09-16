using DreamTeam.Runtime.Systems.UI;
using UnityEngine;

public class QuitGameButton : ButtonBase
{
    protected override void ButtonBehaviour()
    {
        Application.Quit();
    }
}
