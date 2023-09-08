using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTilePallet : MonoBehaviour
{
    public TileBase[] dream;
    public TileBase[] nightmare;



    [HideInInspector] public Tilemap[] Tilemaps;

    [Header("Versão 2")]
    public bool v2;
    public Color colorDream;
    public Color colorNightmare;

    public float transitionTime;
    public bool isNightmaremode;

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        Tilemaps = GetComponentsInChildren<Tilemap>();

        if (v2 == true)
        {
            foreach (Tilemap map in Tilemaps)
            {
                for (int i = 0; i < dream.Length; i++)
                {
                    map.SwapTile(dream[i], nightmare[i]);
                }
            }
        }
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;
    }

    public void EChangeStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:

                if (v2 == true)
                {
                    isNightmaremode = false;
                }
                else
                {
                    foreach (Tilemap map in Tilemaps)
                    {
                        for (int i = 0; i < nightmare.Length; i++)
                        {
                            map.SwapTile(nightmare[i], dream[i]);
                        }
                    }

                }

                break;

            case StageType.Nightmare:

                if (v2 == true)
                {
                    isNightmaremode = true;
                }
                else
                {
                    foreach (Tilemap map in Tilemaps)
                    {
                        for (int i = 0; i < dream.Length; i++)
                        {
                            map.SwapTile(dream[i], nightmare[i]);
                        }
                    }
                }



                break;
        }
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
}
