using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.FMODAudio
{
    public class FmodPlayer : MonoBehaviour
    {
        [SerializeField] private LayerMask materialCheckLayer;
        [SerializeField] private List<GroundType> groundTypes = new();
        [SerializeField] private float distance = 0.4f;
        private float Material;

        public void PlayMeleeEvent(string path)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
        }

        private void FixedUpdate()
        {
            MaterialCheck();
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);
        }

        void MaterialCheck()
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, Vector2.down, distance, materialCheckLayer);           //Layer

            if (hit.collider)
            {
                var existGround = groundTypes.FirstOrDefault(groundType => hit.collider.CompareTag(groundType.tagName));

                if (existGround != null)
                {
                    Material = existGround.materialFloat;
                }


                //Isso de cima faz a mesma coisa, só que vc pode colocar quantas tags quiser pelo inspector.
                //if (hit.collider.CompareTag("ground"))
                //{
                //    Material = 1f;
                //}
                //if (hit.collider.CompareTag("Escada"))
                //{
                //    Material = 2f;
                //}

                //Para colocar esses valores sem estar no inspector vc pode fazer o Start(); e colar o código abaixo
                //groundTypes.Add(new()
                //{
                //    tagName = "ground",
                //    materialFloat = 1f
                //});

                //groundTypes.Add(new()
                //{
                //    tagName = "Escada",
                //    materialFloat = 2f
                //});
            }
        }

        public void PlayFootstepsEvent(string path)
        {
            FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
            Footsteps.setParameterByName("Material", Material);
            Footsteps.start();
            Footsteps.release();
        }
    }
}