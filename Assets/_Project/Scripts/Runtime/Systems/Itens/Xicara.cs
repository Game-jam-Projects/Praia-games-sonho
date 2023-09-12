using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class Xicara : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController controller))
        {
            controller.GetInput().DisableInput();
            SceneLoader.Instance.NextScene();
        }
    }
}
