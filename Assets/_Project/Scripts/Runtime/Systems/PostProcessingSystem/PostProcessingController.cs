using DreamTeam.Runtime.Systems.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DreamTeam.Runtime.Systems.PostProcessing
{
    public class PostProcessingController : MonoBehaviour
    {
        private Volume volume;

        [Header("PaniniProjection")]
        public float maxPaniniDistanceValue;
        public float paniniIncrement;
        private bool isIncreasing;
        [SerializeField] private float paniniDistanceValue;
        private PaniniProjection paniniProjection;


        [Header("Chromatic Aberration")]
        public float maxChromaticAberrationIntensity;
        public float chromaticAberrationIncrement;
        private bool isChromaticIncreasing;
        [SerializeField] private float chromaticAberrationIntensity;
        private ChromaticAberration chromaticAberration;


        [Header("Lens Distortion")]
        public float maxLensDistortionIntensity;
        public float minLensDistortionIntensity; // Novo valor para a intensidade mínima (negativa)
        public float lensDistortionIncrement;
        [SerializeField] private float lensDistortionIntensity;
        private LensDistortion lensDistortion;
        private bool isLensDistortionIncreasing;

        [Header("Film Grain")]
        public float maxFilmGrainIntensity;
        public float filmGrainIncrement;
        private bool isFilmGrainIncreasing;
        [SerializeField] private float filmGrainIntensity;
        private FilmGrain filmGrain;

        [Header("Color Adjustments - Hue Shift")]
        public float maxHueShiftValue;
        public float minHueShiftValue;  // Valor mínimo (pode ser negativo)
        public float hueShiftIncrement;
        private bool isHueShiftIncreasing;
        [SerializeField] private float hueShiftValue;
        private ColorAdjustments colorAdjustments;



        void Start()
        {
            volume = GetComponent<Volume>();
            if (volume.profile.TryGet(out paniniProjection))
            {
                // Obtido com sucesso
            }

            if (volume.profile.TryGet(out chromaticAberration))
            {
                // Obtido com sucesso
            }

            if (volume.profile.TryGet(out lensDistortion))
            {
                // Obtido com sucesso
            }

            if (volume.profile.TryGet(out filmGrain))
            {
                // Obtido com sucesso
            }

            if (volume.profile.TryGet(out colorAdjustments))
            {
                // Obtido com sucesso
            }


        }

        void Update()
        {
            if (CoreSingleton.Instance.gameStateManager.GetStageType() == StageType.Nightmare)
            {
                PaniniManager();
                ChromaticAberrationManager();
                LensDistortionManager();
                FilmGrainManager();
                HueShiftManager();
            }
            else
            {
                if (paniniDistanceValue > 0)
                {
                    paniniDistanceValue -= paniniIncrement * 2 * Time.deltaTime;
                    paniniDistanceValue = Mathf.Clamp(paniniDistanceValue, 0, maxPaniniDistanceValue);
                    SetPaniniProjectionDistance(paniniDistanceValue);
                }

                if (chromaticAberrationIntensity > 0)
                {
                    chromaticAberrationIntensity -= chromaticAberrationIncrement * 2 * Time.deltaTime;
                    chromaticAberrationIntensity = Mathf.Clamp(chromaticAberrationIntensity, 0, maxChromaticAberrationIntensity);
                    chromaticAberration.intensity.value = chromaticAberrationIntensity;
                }

                if (lensDistortionIntensity > 0)
                {
                    lensDistortionIntensity -= lensDistortionIncrement * 2 * Time.deltaTime;
                    lensDistortionIntensity = Mathf.Clamp(lensDistortionIntensity, 0, maxLensDistortionIntensity);
                    lensDistortion.intensity.value = lensDistortionIntensity;
                }

                if (filmGrainIntensity > 0)
                {
                    filmGrainIntensity -= filmGrainIncrement * 2 * Time.deltaTime;
                    filmGrainIntensity = Mathf.Clamp(filmGrainIntensity, 0, maxFilmGrainIntensity);
                    filmGrain.intensity.value = filmGrainIntensity;
                }

                if (hueShiftValue != 0)
                {
                    // Movendo rapidamente de volta para 0
                    hueShiftValue = Mathf.MoveTowards(hueShiftValue, 0, hueShiftIncrement * 10 * Time.deltaTime);
                    colorAdjustments.hueShift.value = hueShiftValue;
                }
            }
        }

        private void PaniniManager()
        {
            if (paniniProjection != null)
            {
                if (isIncreasing)
                {
                    paniniDistanceValue += paniniIncrement * Time.deltaTime;
                }
                else
                {
                    paniniDistanceValue -= paniniIncrement * Time.deltaTime;
                }

                paniniDistanceValue = Mathf.Clamp(paniniDistanceValue, 0, maxPaniniDistanceValue);

                if (paniniDistanceValue >= maxPaniniDistanceValue)
                {
                    isIncreasing = false;
                }
                else if (paniniDistanceValue <= 0)
                {
                    isIncreasing = true;
                }

                SetPaniniProjectionDistance(paniniDistanceValue);
            }
        }

        void SetPaniniProjectionDistance(float newDistance)
        {
            if (paniniProjection != null)
            {
                paniniProjection.distance.value = newDistance;
            }
        }

        private void ChromaticAberrationManager()
        {
            if (chromaticAberration != null)
            {
                if (isChromaticIncreasing)
                {
                    chromaticAberrationIntensity += chromaticAberrationIncrement * Time.deltaTime;
                }
                else
                {
                    chromaticAberrationIntensity -= chromaticAberrationIncrement * Time.deltaTime;
                }

                chromaticAberrationIntensity = Mathf.Clamp(chromaticAberrationIntensity, 0, maxChromaticAberrationIntensity);

                if (chromaticAberrationIntensity >= maxChromaticAberrationIntensity)
                {
                    isChromaticIncreasing = false;
                }
                else if (chromaticAberrationIntensity <= 0)
                {
                    isChromaticIncreasing = true;
                }

                chromaticAberration.intensity.value = chromaticAberrationIntensity;
            }
        }

        private void LensDistortionManager()
        {
            if (lensDistortion != null)
            {
                if (isLensDistortionIncreasing)
                {
                    lensDistortionIntensity += lensDistortionIncrement * Time.deltaTime;
                }
                else
                {
                    lensDistortionIntensity -= lensDistortionIncrement * Time.deltaTime;
                }

                lensDistortionIntensity = Mathf.Clamp(lensDistortionIntensity, minLensDistortionIntensity, maxLensDistortionIntensity);

                if (lensDistortionIntensity >= maxLensDistortionIntensity)
                {
                    isLensDistortionIncreasing = false;
                }
                else if (lensDistortionIntensity <= minLensDistortionIntensity)
                {
                    isLensDistortionIncreasing = true;
                }

                lensDistortion.intensity.value = lensDistortionIntensity;
            }
        }

        private void FilmGrainManager()
        {
            if (filmGrain != null)
            {
                if (isFilmGrainIncreasing)
                {
                    filmGrainIntensity += filmGrainIncrement * Time.deltaTime;
                }
                else
                {
                    filmGrainIntensity -= filmGrainIncrement * Time.deltaTime;
                }

                filmGrainIntensity = Mathf.Clamp(filmGrainIntensity, 0, maxFilmGrainIntensity);

                if (filmGrainIntensity >= maxFilmGrainIntensity)
                {
                    isFilmGrainIncreasing = false;
                }
                else if (filmGrainIntensity <= 0)
                {
                    isFilmGrainIncreasing = true;
                }

                filmGrain.intensity.value = filmGrainIntensity;
            }
        }

        private void HueShiftManager()
        {
            if (colorAdjustments != null)
            {
                if (isHueShiftIncreasing)
                {
                    hueShiftValue += hueShiftIncrement * Time.deltaTime;
                }
                else
                {
                    hueShiftValue -= hueShiftIncrement * Time.deltaTime;
                }

                hueShiftValue = Mathf.Clamp(hueShiftValue, minHueShiftValue, maxHueShiftValue);

                if (hueShiftValue >= maxHueShiftValue)
                {
                    isHueShiftIncreasing = false;
                }
                else if (hueShiftValue <= minHueShiftValue)
                {
                    isHueShiftIncreasing = true;
                }

                colorAdjustments.hueShift.value = hueShiftValue;
            }
        }

    }
}
