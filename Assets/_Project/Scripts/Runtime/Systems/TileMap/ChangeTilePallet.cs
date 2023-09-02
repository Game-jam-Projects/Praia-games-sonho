using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTilePallet : MonoBehaviour
{
    public TileBase[] placeholder;
    public TileBase[] dream;
    public TileBase[] nightmare;

    public Tilemap[] Tilemaps;

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        Tilemaps = GetComponentsInChildren<Tilemap>();
    }

    public void EChangeStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:

                foreach (Tilemap map in Tilemaps)
                {
                    for (int i = 0; i < nightmare.Length; i++)
                    {
                        map.SwapTile(nightmare[i], dream[i]);
                    }
                }

                break;

            case StageType.Nightmare:

                foreach (Tilemap map in Tilemaps)
                {
                    for (int i = 0; i < dream.Length; i++)
                    {
                        map.SwapTile(dream[i], nightmare[i]);
                    }
                }

                break;
        }
    }
}
