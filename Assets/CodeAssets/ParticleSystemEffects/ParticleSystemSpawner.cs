using Assets.CodeAssets.ParticleSystemEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.ParticleSystemEffects
{
    public class ParticleSystemSpawner : MonoBehaviour
    {

        public static ParticleSystemSpawner Instance;
        public void Awake()
        {
            Instance = this;
        }

        public List<ParticleSystemContainer> SpecialEffectsInProgress { get; } = new List<ParticleSystemContainer>();

        public Camera uiCamera;
        public Camera particleCamera;

        private void MoveParticleSystemToUiBoundingBox(ParticleSystem systemToNormalize,
            Transform intendedBoundingBox)
        {
            var screenLocationOfIntendedPosition = uiCamera.WorldToViewportPoint(intendedBoundingBox.position);
            var worldLocationInParticleCameraOfIntendedPosition = particleCamera.ViewportToWorldPoint(screenLocationOfIntendedPosition);
            systemToNormalize.transform.SetPositionAndRotation(worldLocationInParticleCameraOfIntendedPosition, Quaternion.identity);

            Debug.Log($"INTENDED:  {intendedBoundingBox.position.x}, {intendedBoundingBox.position.y}  ACTUAL: {systemToNormalize.transform.position.x}, {systemToNormalize.transform.position.y}");
        }

        const string DefaultParticleSystemPath = "SpecialEffects/Ricochet_normal";

        public static string GetSpecialEffectPath(string path)
        {
            return "SpecialEffects/" + path;
        }

        public ParticleSystemContainer GenerateSpecialEffectAtCharacter(AbstractBattleUnit unit,
            ProtoParticleSystem particleSystem,
            HardpointLocation locationToHit,
            Action afterAnimationIsFinishedAction = null)
        {
            return PlaceParticleSystem(particleSystem,
                unit.CorrespondingPrefab.Hardpoints.FromHardpointLocation(locationToHit),
                afterAnimationIsFinishedAction
                );
        }

        public ParticleSystemContainer PlaceParticleSystem(ProtoParticleSystem particleSystem,
            Transform intendedBoundingBox,
            Action afterAnimationIsFinishedAction = null,
            Transform parent = null)
        {
            if (parent == null)
            {
                parent = particleCamera.transform;
            }

            var loadedPrefab = Resources.Load<ParticleSystem>(particleSystem.PrefabPath);
            if (loadedPrefab == null)
            {
                Debug.LogError("Could not load particles from location: " + particleSystem.PrefabPath + "; using meeple instead");
                loadedPrefab = Resources.Load<ParticleSystem>(DefaultParticleSystemPath);
            }

            var instance = Instantiate(loadedPrefab, parent: parent);

            MoveParticleSystemToUiBoundingBox(instance, intendedBoundingBox);
            instance.transform.localScale *= particleSystem.SizeRatio;
            instance.Play();
            instance.gameObject.layer = 10;//particle layer
            var container =  new ParticleSystemContainer { 
                Particles = instance,
                GoodUntil = DateTime.Now + TimeSpan.FromSeconds(particleSystem.KillAfterNumSeconds)
            };
            if (afterAnimationIsFinishedAction != null)
            {
                container.AfterAnimationIsFinishedAction = afterAnimationIsFinishedAction;
            }
            SpecialEffectsInProgress.Add(container);

            Debug.Log("Started particle effect: " + particleSystem.PrefabPath);

            return container;
        }

        public void Update()
        {
            var expired = SpecialEffectsInProgress.Where(item => item.ShouldKill()).ToList();
            foreach(var item in expired)
            {
                item.KillIfApplicable();
            }
            SpecialEffectsInProgress.RemoveAll(item => expired.Contains(item));
        }
    }
}

public enum HardpointLocation
{
    CENTER,
    LEFT,
    BOTTOM
}
 
public class ProtoParticleSystem
{
    public string PrefabPath { get; set; }
    public float SizeRatio { get; set; } = 0.1f;
    public float KillAfterNumSeconds { get; set; } = 5;
    public HardpointLocation Location { get; set; } = HardpointLocation.CENTER;

    public static ProtoParticleSystem MuzzleFlash = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("ef_10_red"),
        SizeRatio = .1f
    };

    public static ProtoParticleSystem GreenSlash = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("green_circular_slash"),
        SizeRatio = .1f
    };
    public static ProtoParticleSystem Richochet = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("Ricochet_normal"),
        SizeRatio = .1f
    };


}

public class ParticleSystemContainer
{
    public bool OnlyRunTillFinished { get; set; } = true;
   public DateTime GoodUntil { get; set; }
   public ParticleSystem Particles { get; set; }

    public Action AfterAnimationIsFinishedAction { get; set; } = () => { };

    public bool ShouldKill()
    {
        if (GoodUntil < DateTime.Now)
        {
            return true;
        }

        if (OnlyRunTillFinished)
        {
            if (!Particles.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    public void KillIfApplicable()
    {
        if (ShouldKill())
        {
            GameObject.Destroy(Particles.gameObject);
            AfterAnimationIsFinishedAction();
        }
    }
}