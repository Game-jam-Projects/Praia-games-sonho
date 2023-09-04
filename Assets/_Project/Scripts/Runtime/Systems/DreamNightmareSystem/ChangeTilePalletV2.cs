using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTilePalletV2 : MonoBehaviour
{
    public TileBase[] dream;
    public TileBase[] nightmare;

    public Color colorDream;
    public Color colorNightmare;

    public float transitionTime;
    public bool isNightmaremode;

    [HideInInspector] public Tilemap[] Tilemaps;

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        Tilemaps = GetComponentsInChildren<Tilemap>();

        foreach (Tilemap map in Tilemaps)
        {
            for (int i = 0; i < dream.Length; i++)
            {
                map.SwapTile(dream[i], nightmare[i]);
            }
        }
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;
    }

    private void Update()
    {
        if (isNightmaremode == true)
        {
            foreach (Tilemap map in Tilemaps)
            {
                map.color = Color.Lerp(map.color, colorNightmare, transitionTime * Time.deltaTime);
            }
        }
        else
        {
            foreach (Tilemap map in Tilemaps)
            {
                map.color = Color.Lerp(map.color, colorDream, transitionTime * Time.deltaTime);
            }
        }
    }

    public void EChangeStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:

                isNightmaremode = false;

                //foreach (Tilemap map in Tilemaps)
                //{
                //    for (int i = 0; i < nightmare.Length; i++)
                //    {
                //        map.SwapTile(nightmare[i], dream[i]);
                //    }
                //}

                break;

            case StageType.Nightmare:

                isNightmaremode = true;

                //foreach (Tilemap map in Tilemaps)
                //{
                //    for (int i = 0; i < dream.Length; i++)
                //    {
                //        map.SwapTile(dream[i], nightmare[i]);
                //    }
                //}

                break;
        }
    }
}
