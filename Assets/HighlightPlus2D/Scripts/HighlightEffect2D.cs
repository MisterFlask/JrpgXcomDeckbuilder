﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HighlightPlus2D {

    public delegate void OnObjectHighlightStartEvent(GameObject obj, ref bool cancelHighlight);
    public delegate void OnObjectHighlightEndEvent(GameObject obj);


    [ExecuteInEditMode]
    [HelpURL("http://kronnect.com/taptapgo")]
    public class HighlightEffect2D : MonoBehaviour {

        public enum SeeThroughMode {
            WhenHighlighted = 0,
            AlwaysWhenOccluded = 1,
            Never = 2
        }

        public enum QualityLevel {
            Simple = 0,
            Medium = 1,
            High = 2
        }

        [Serializable]
        public struct GlowPassData {
            public float offset;
            public float alpha;
            public Color color;
        }

        struct ModelMaterials {
            public Transform transform;
            public bool currentRenderIsVisible;
            public Renderer renderer;
            public Material fxMatMask, fxMatDepthWrite, fxMatGlow, fxMatOutline;
            public Material fxMatSeeThrough, fxMatOverlay, fxMatShadow;
            public Matrix4x4 maskMatrix, effectsMatrix;
            public Vector2 center, pivotPos;
            public float aspectRatio;
            public float rectWidth, rectHeight;
        }

        public bool previewInEditor = true;

        public Transform target;

        public bool occluder;

        [SerializeField]
        bool _highlighted;

        public bool highlighted {
            get {
                return _highlighted;
            }
            set {
                if (_highlighted != value) {
                    SetHighlighted(value);
                }
            }
        }

        [Tooltip("Ignore highlighting on this object.")]
        public bool ignore;
        public bool polygonPacking;

        [Range(0, 1)]
        public float overlay = 0.5f;
        public Color overlayColor = Color.yellow;
        public float overlayAnimationSpeed = 1f;
        [Range(0, 1)]
        public float overlayMinIntensity = 0.5f;
        [Range(0, 1)]
        public float overlayBlending = 1.0f;
        public int overlayRenderQueue = 3000;

        [Range(0, 1)]
        public float outline = 1f;
        public Color outlineColor = Color.black;
        public float outlineWidth = 0.5f;
        [Tooltip("Uses additional passes to create a better outline.")]
        public QualityLevel outlineQuality = QualityLevel.Simple;
        [Tooltip("Forces bilinear sampling to smooth edges when rendering outline effect.")]
        public bool outlineSmooth;
        [Tooltip("Renders effect on top of other sprites in the same sorting layer.")]
        public bool outlineOnTop = true;

        [Range(0, 5)]
        public float glow = 1f;
        public float glowWidth = 1.5f;
        public bool glowDithering = true;
        public float glowMagicNumber1 = 0.75f;
        public float glowMagicNumber2 = 0.5f;
        public float glowAnimationSpeed = 1f;
        [Tooltip("Forces bilinear sampling to smooth edges when rendering glow effect.")]
        public bool glowSmooth;
        [Tooltip("Uses additional passes to create a better glow effect.")]
        public QualityLevel glowQuality = QualityLevel.Simple;
        [Tooltip("Renders effect on top of other sprites in the same sorting layer.")]
        public bool glowOnTop = true;
        public GlowPassData[] glowPasses;

        [Range(0.1f, 3f)]
        [Tooltip("Scales the sprite while highlighted.")]
        public float zoomScale = 1f;

        [Range(0, 1f)]
        public float shadowIntensity = 0f;
        public Color shadowColor = new Color(0, 0, 0, 0.2f);
        public Vector2 shadowOffset = new Vector2(0.1f, -0.1f);
        public bool shadow3D;

        public event OnObjectHighlightStartEvent OnObjectHighlightStart;
        public event OnObjectHighlightEndEvent OnObjectHighlightEnd;

        public SeeThroughMode seeThrough;
        [Range(0, 5f)]
        public float seeThroughIntensity = 0.8f;
        [Range(0, 1)]
        public float seeThroughTintAlpha = 0.5f;
        public Color seeThroughTintColor = Color.red;

        [Tooltip("Snap sprite renderers to a grid in world space at render-time.")]
        public bool pixelSnap;
        [Range(0, 1)]
        public float alphaCutOff = 0.05f;
        [Tooltip("Automatically computes the sprite center based on texture colors.")]
        public bool autoSize = true;
        public Vector2 center;
        public Vector2 scale = Vector2.one;
        public float aspectRatio = 1f;

        // This is informative.
        public Vector2 pivotPos;

        [SerializeField, HideInInspector]
        ModelMaterials[] rms;
        [SerializeField, HideInInspector]
        int rmsCount = 0;

        const string UNIFORM_CUTOFF = "_CutOff";
        const String UNIFORM_ALPHA_TEX = "_AlphaTex";
        const string SKW_PIXELSNAP_ON = "PIXELSNAP_ON";
        const string SKW_POLYGON_PACKING = "POLYGON_PACKING";
        const string SKW_ETC1_EXTERNAL_ALPHA = "ETC1_EXTERNAL_ALPHA";
        const string SKW_SMOOTH_EDGES = "SMOOTH_EDGES";
        Vector2[] offsetsHQ, offsetsMQ;

        public static Material fxMatSpriteMask, fxMatSpriteDepthWrite, fxMatSpriteSeeThrough, fxMatSpriteGlow, fxMatSpriteOutline, fxMatSpriteOverlay, fxMatSpriteShadow, fxMatSpriteShadow3D;
        Mesh quadMesh;
        List<Vector3> vertices;
        List<int> indices;

        MaterialPropertyBlock outlineProps, glowProps;
        int shaderPropPivotId;
        int shaderPropGlowId, shaderPropGlowColorId;

        [SerializeField]
        Vector3 scaleBeforeZoom, scaleAfterZoom;

        Dictionary<Sprite, Mesh> cachedMeshes;
        Dictionary<Texture, Texture> cachedTextures;

        bool hasSpriteMask;

        void OnEnable() {
            if (target == null)
                target = transform;
            if (glowPasses == null || glowPasses.Length == 0) {
                glowPasses = new GlowPassData[4];
                glowPasses[0] = new GlowPassData() { offset = 4, alpha = 0.1f, color = new Color(0.64f, 1f, 0f, 1f) };
                glowPasses[1] = new GlowPassData() { offset = 3, alpha = 0.2f, color = new Color(0.64f, 1f, 0f, 1f) };
                glowPasses[2] = new GlowPassData() { offset = 2, alpha = 0.3f, color = new Color(0.64f, 1f, 0f, 1f) };
                glowPasses[3] = new GlowPassData() { offset = 1, alpha = 0.4f, color = new Color(0.64f, 1f, 0f, 1f) };
            }
            offsetsHQ = new Vector2[] {
                new Vector2 (0, 1),
                new Vector2 (1, 1),
                new Vector2 (1, 0),
                new Vector2 (1, -1),
                new Vector2 (0, -1),
                new Vector2 (-1, -1),
                new Vector2 (-1, 0),
                new Vector2 (-1, 1)
            };
            offsetsMQ = new Vector2[] {
                new Vector2 (1, 1),
                new Vector2 (1, -1),
                new Vector2 (-1, -1),
                new Vector2 (-1, 1)
            };
            outlineProps = new MaterialPropertyBlock();
            glowProps = new MaterialPropertyBlock();
            shaderPropPivotId = Shader.PropertyToID("_Pivot");
            shaderPropGlowId = Shader.PropertyToID("_Glow");
            shaderPropGlowColorId = Shader.PropertyToID("_GlowColor");
            cachedMeshes = new Dictionary<Sprite, Mesh>();
            cachedTextures = new Dictionary<Texture, Texture>();
            CheckSpriteSupportDependencies();
            SetupMaterial();
        }

        void OnDisable() {
            UpdateMaterialProperties();
        }

        void Reset() {
            Refresh();
        }


        public void Refresh() {
            if (!enabled) {
                enabled = true;
            } else {
                SetupMaterial();
            }
        }

        void Update() {
#if UNITY_EDITOR
            if (!previewInEditor && !Application.isPlaying && !occluder)
                return;
#endif
            bool seeThroughReal = seeThroughIntensity > 0 && (this.seeThrough == SeeThroughMode.AlwaysWhenOccluded || (this.seeThrough == SeeThroughMode.WhenHighlighted && _highlighted));
            if (!_highlighted && !seeThroughReal && shadowIntensity <= 0 && !occluder) {
                return;
            }

            // Ensure renderers are valid and visible (in case LODgroup has changed active renderer)
            for (int k = 0; k < rmsCount; k++) {
                if (rms[k].renderer != null && rms[k].renderer.isVisible != rms[k].currentRenderIsVisible) {
                    SetupMaterial();
                    break;
                }
            }

            // Apply effect
            float glowReal = this._highlighted ? this.glow : 0;
            int layer = gameObject.layer;

            // First create masks
            float viewportAspectRatio = (float)Screen.height / Screen.width; //  1f / Camera.current.aspect;
            for (int k = 0; k < rmsCount; k++) {
                Transform t = rms[k].transform;
                if (t == null || rms[k].fxMatMask == null)
                    continue;
                Vector3 lossyScale;
                Vector3 position = t.position;
                Renderer renderer = rms[k].renderer;
                if (renderer == null)
                    continue;

                lossyScale = t.lossyScale;

                Vector2 pivot, flipVector;
                Vector4 uv = Vector4.zero;
                Texture spriteTexture = null;

                if (renderer is SpriteRenderer) {
                    SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
                    Sprite sprite = spriteRenderer.sprite;
                    if (sprite == null)
                        continue;

                    float rectWidth = sprite.rect.width;
                    float rectHeight = sprite.rect.height;
                    if (rectWidth == 0 || rectHeight == 0)
                        continue;
                    rms[k].rectWidth = rectWidth;
                    rms[k].rectHeight = rectHeight;

                    // pass pivot position to shaders
                    pivotPos = new Vector2(sprite.pivot.x / rectWidth, sprite.pivot.y / rectHeight);
                    if (polygonPacking) {
                        pivotPos.x = pivotPos.y = 0.5f;
                        quadMesh = SpriteToMesh(sprite);
                    }
                    pivot = rms[k].pivotPos = new Vector2(pivotPos.x - 0.5f, pivotPos.y - 0.5f);

                    // adjust scale
                    spriteTexture = sprite.texture;
                    if (!polygonPacking) {
                        lossyScale.x *= rectWidth / sprite.pixelsPerUnit;
                        lossyScale.y *= rectHeight / sprite.pixelsPerUnit;
                        uv = new Vector4(sprite.rect.xMin / spriteTexture.width, sprite.rect.yMin / spriteTexture.height, sprite.rect.xMax / spriteTexture.width, sprite.rect.yMax / spriteTexture.height);
                    }

                    // inverted sprite?
                    flipVector = new Vector2(spriteRenderer.flipX ? -1 : 1, spriteRenderer.flipY ? -1 : 1);

                    // external alpha texture?
                    Texture2D alphaTex = sprite.associatedAlphaSplitTexture;
                    if (alphaTex != null) {
                        rms[k].fxMatMask.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatDepthWrite.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatGlow.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatOutline.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatOverlay.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatShadow.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatSeeThrough.SetTexture(UNIFORM_ALPHA_TEX, alphaTex);
                        rms[k].fxMatMask.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatDepthWrite.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatGlow.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatOutline.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatOverlay.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatSeeThrough.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                        rms[k].fxMatShadow.EnableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    }
                } else {
                    pivot = Vector2.zero;
                    uv = new Vector4(0, 0, 1, 1);
                    flipVector = Vector2.one;

                    rms[k].fxMatMask.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatDepthWrite.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatGlow.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatOutline.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatOverlay.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatSeeThrough.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);
                    rms[k].fxMatShadow.DisableKeyword(SKW_ETC1_EXTERNAL_ALPHA);

                    if (renderer.sharedMaterial != null) {
                        spriteTexture = renderer.sharedMaterial.mainTexture;
                    }
                }

                // Assign center to shaders
                rms[k].fxMatMask.SetVector("_Pivot", pivot);
                rms[k].fxMatOverlay.SetVector("_Pivot", pivot);
                rms[k].fxMatSeeThrough.SetVector("_Pivot", pivot);
                rms[k].fxMatShadow.SetVector("_Pivot", shadow3D ? pivot : pivot - shadowOffset);

                // pass center
                Vector3 geom = new Vector3(rms[k].center.x, rms[k].center.y, rms[k].aspectRatio * viewportAspectRatio);
                rms[k].fxMatGlow.SetVector("_Geom", geom);
                rms[k].fxMatOutline.SetVector("_Geom", geom);

                // Assign textures
                rms[k].fxMatMask.mainTexture = spriteTexture;
                rms[k].fxMatGlow.mainTexture = glowSmooth ? TextureWithBilinearSampling(spriteTexture) : spriteTexture;
                rms[k].fxMatOutline.mainTexture = outlineSmooth ? TextureWithBilinearSampling(spriteTexture) : spriteTexture;
                rms[k].fxMatOverlay.mainTexture = spriteTexture;
                rms[k].fxMatSeeThrough.mainTexture = spriteTexture;
                rms[k].fxMatShadow.mainTexture = spriteTexture;

                // UV mapping with atlas support
                rms[k].fxMatMask.SetVector("_UV", uv);
                rms[k].fxMatGlow.SetVector("_UV", uv);
                rms[k].fxMatOutline.SetVector("_UV", uv);
                rms[k].fxMatOverlay.SetVector("_UV", uv);
                rms[k].fxMatSeeThrough.SetVector("_UV", uv);
                rms[k].fxMatShadow.SetVector("_UV", uv);

                // Flip option
                rms[k].fxMatMask.SetVector("_Flip", flipVector);
                rms[k].fxMatGlow.SetVector("_Flip", flipVector);
                rms[k].fxMatOutline.SetVector("_Flip", flipVector);
                rms[k].fxMatOverlay.SetVector("_Flip", flipVector);
                rms[k].fxMatSeeThrough.SetVector("_Flip", flipVector);
                rms[k].fxMatShadow.SetVector("_Flip", flipVector);

                Matrix4x4 matrix = Matrix4x4.TRS(position, t.rotation, lossyScale);
                rms[k].maskMatrix = matrix;
                if (!autoSize) {
                    lossyScale.x *= scale.x;
                    lossyScale.y *= scale.y;
                    rms[k].effectsMatrix = Matrix4x4.TRS(position, t.rotation, lossyScale);
                } else {
                    rms[k].effectsMatrix = matrix;
                }

                if (occluder) {
                    rms[k].fxMatDepthWrite.SetVector("_Pivot", pivot);
                    rms[k].fxMatDepthWrite.mainTexture = spriteTexture;
                    rms[k].fxMatDepthWrite.SetVector("_UV", uv);
                    rms[k].fxMatDepthWrite.SetVector("_Flip", flipVector);
                    Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatDepthWrite, layer);
                } else {
                    if (!hasSpriteMask) {
                        Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatMask, layer);
                    }
                }
            }

            // Add shadow
            if (shadowIntensity > 0) {
                for (int k = 0; k < rmsCount; k++) {
                    Transform t = rms[k].transform;
                    if (t == null)
                        continue;
                    Matrix4x4 matrix = rms[k].maskMatrix;
                    Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatShadow, layer);
                }
            }

            if (occluder)
                return;


            // Add see-through
            if (seeThroughReal) {
                for (int k = 0; k < rmsCount; k++) {
                    Transform t = rms[k].transform;
                    if (t == null)
                        continue;
                    Matrix4x4 matrix = rms[k].maskMatrix;
                    Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatSeeThrough, layer);
                }
            }

            // Highlight effects
            if (_highlighted) {
                // Add Glow
                if (glow > 0) {
                    for (int k = 0; k < rms.Length; k++) {
                        Transform t = rms[k].transform;
                        if (t == null)
                            continue;
                        Matrix4x4 matrix = rms[k].effectsMatrix;
                        Vector2 originalPivotPos = rms[k].pivotPos;
                        for (int j = 0; j < glowPasses.Length; j++) {
                            glowProps.SetColor(shaderPropGlowColorId, glowPasses[j].color);
                            if (glowQuality != QualityLevel.Simple) {
                                Vector2[] offsets = glowQuality == QualityLevel.High ? offsetsHQ : offsetsMQ;
                                glowProps.SetVector(shaderPropGlowId, new Vector4(glowReal * glowPasses[j].alpha, glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
                                float mult = glowPasses[j].offset * glowWidth;
                                Vector2 disp = new Vector2(mult / rms[k].rectWidth, mult / rms[k].rectHeight);
                                for (int z = 0; z < offsets.Length; z++) {
                                    Vector2 newPivot = new Vector2(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y);
                                    glowProps.SetVector(shaderPropPivotId, newPivot);
                                    Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatGlow, layer, null, 0, glowProps);
                                }
                            } else {
                                glowProps.SetVector(shaderPropPivotId, rms[k].pivotPos);
                                glowProps.SetVector(shaderPropGlowId, new Vector4(glowReal * glowPasses[j].alpha, glowPasses[j].offset * glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
                                Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatGlow, layer, null, 0, glowProps);
                            }
                        }
                    }
                }

                // Add Outline
                if (outline > 0) {
                    for (int k = 0; k < rms.Length; k++) {
                        Transform t = rms[k].transform;
                        if (t == null)
                            continue;
                        Matrix4x4 matrix = rms[k].effectsMatrix;
                        if (outlineQuality != QualityLevel.Simple) {
                            Vector2[] offsets = outlineQuality == QualityLevel.High ? offsetsHQ : offsetsMQ;
                            rms[k].fxMatOutline.SetFloat("_OutlineWidth", 0f);
                            Vector2 originalPivotPos = rms[k].pivotPos;
                            Vector2 disp = new Vector2(outlineWidth / rms[k].rectWidth, outlineWidth / rms[k].rectHeight);
                            for (int z = 0; z < offsets.Length; z++) {
                                Vector2 newPivot = new Vector2(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y);
                                outlineProps.SetVector(shaderPropPivotId, newPivot);
                                Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatOutline, layer, null, 0, outlineProps);
                            }
                        } else {
                            outlineProps.SetVector(shaderPropPivotId, rms[k].pivotPos);
                            Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatOutline, layer, null, 0, outlineProps);
                        }
                        if (outlineSmooth) {
                            rms[k].fxMatOutline.EnableKeyword(SKW_SMOOTH_EDGES);
                        } else {
                            rms[k].fxMatOutline.DisableKeyword(SKW_SMOOTH_EDGES);
                        }
                    }
                }

                // Add Overlay
                if (overlay > 0) {
                    for (int k = 0; k < rms.Length; k++) {
                        Transform t = rms[k].transform;
                        if (t == null)
                            continue;
                        Matrix4x4 matrix = rms[k].maskMatrix;
                        Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatOverlay, layer);
                    }
                }
            }
        }

        void InitMaterial(ref Material material, string shaderName) {
            if (material == null) {
                Shader shaderFX = Shader.Find(shaderName);
                if (shaderFX == null) {
                    Debug.LogError("Shader " + shaderName + " not found.");
                    enabled = false;
                    return;
                }
                material = new Material(shaderFX);
            }
        }

        public void SetTarget(Transform transform) {
            if (transform == null || transform == target)
                return;

            if (_highlighted) {
                SetHighlighted(false);
            }

            target = transform;
            SetupMaterial();
        }

        public void SetHighlighted(bool state) {
            if (ignore)
                return;
            bool cancelHighlight = false;
            if (state) {
                if (OnObjectHighlightStart != null) {
                    OnObjectHighlightStart(gameObject, ref cancelHighlight);
                    if (cancelHighlight) {
                        return;
                    }
                }
                SendMessage("HighlightStart", null, SendMessageOptions.DontRequireReceiver);
            } else {
                if (OnObjectHighlightEnd != null) {
                    OnObjectHighlightEnd(gameObject);
                }
                SendMessage("HighlightEnd", null, SendMessageOptions.DontRequireReceiver);
            }
            _highlighted = state;
            Refresh();
        }

        void SetupMaterial() {

            if (target == null)
                return;
            Renderer[] rr = target.GetComponentsInChildren<Renderer>();
            if (rms == null || rms.Length < rr.Length) {
                rms = new ModelMaterials[rr.Length];
            }
            rmsCount = 0;
            hasSpriteMask = false;
            if (aspectRatio < 0.01f) {
                aspectRatio = 0.01f;
            }

            for (int k = 0; k < rr.Length; k++) {
                rms[rmsCount] = new ModelMaterials();
                Renderer renderer = rr[k];
                rms[rmsCount].renderer = renderer;

                if (renderer is SpriteMask) {
                    hasSpriteMask = true;
                    continue;
                }

                if (!renderer.isVisible) {
                    rmsCount++;
                    continue;
                }

                rms[rmsCount].currentRenderIsVisible = true;

                if (renderer.transform != target && renderer.GetComponent<HighlightEffect2D>() != null)
                    continue; // independent subobject

                rms[rmsCount].center = center;
                rms[rmsCount].aspectRatio = aspectRatio;
                if (autoSize && renderer is SpriteRenderer) {
                    SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
                    ComputeSpriteCenter(rmsCount, spriteRenderer.sprite);
                }
                rms[rmsCount].transform = renderer.transform;
                rms[rmsCount].fxMatMask = Instantiate<Material>(fxMatSpriteMask);
                rms[rmsCount].fxMatDepthWrite = Instantiate<Material>(fxMatSpriteDepthWrite);
                rms[rmsCount].fxMatGlow = Instantiate<Material>(fxMatSpriteGlow);
                rms[rmsCount].fxMatOutline = Instantiate<Material>(fxMatSpriteOutline);
                rms[rmsCount].fxMatSeeThrough = Instantiate<Material>(fxMatSpriteSeeThrough);
                rms[rmsCount].fxMatOverlay = Instantiate<Material>(fxMatSpriteOverlay);
                rms[rmsCount].fxMatShadow = Instantiate<Material>(shadow3D ? fxMatSpriteShadow3D : fxMatSpriteShadow);

                if (glowOnTop) {
                    rms[rmsCount].fxMatGlow.renderQueue += 100;
                }
                if (outlineOnTop) {
                    rms[rmsCount].fxMatOutline.renderQueue += 100;
                }

                rmsCount++;
            }

            UpdateMaterialProperties();
        }

        void CheckSpriteSupportDependencies() {
            InitMaterial(ref fxMatSpriteMask, "HighlightPlus2D/Sprite/Mask");
            InitMaterial(ref fxMatSpriteDepthWrite, "HighlightPlus2D/Sprite/JustDepth");
            InitMaterial(ref fxMatSpriteGlow, "HighlightPlus2D/Sprite/Glow");
            InitMaterial(ref fxMatSpriteOutline, "HighlightPlus2D/Sprite/Outline");
            InitMaterial(ref fxMatSpriteOverlay, "HighlightPlus2D/Sprite/Overlay");
            InitMaterial(ref fxMatSpriteSeeThrough, "HighlightPlus2D/Sprite/SeeThrough");
            InitMaterial(ref fxMatSpriteShadow, "HighlightPlus2D/Sprite/Shadow");
            InitMaterial(ref fxMatSpriteShadow3D, "HighlightPlus2D/Sprite/Shadow3D");

            if (quadMesh == null) {
                quadMesh = new Mesh {
                    vertices = new[] {
                        new Vector3 (-0.5f, -0.5f, 0),
                        new Vector3 (-0.5f, +0.5f, 0),
                        new Vector3 (+0.5f, +0.5f, 0),
                        new Vector3 (+0.5f, -0.5f, 0),
                    },

                    normals = new[] {
                        Vector3.forward,
                        Vector3.forward,
                        Vector3.forward,
                        Vector3.forward,
                    },

                    triangles = new[] { 0, 1, 2, 2, 3, 0 },

                    uv = new[] {
                        new Vector2 (0, 0),
                        new Vector2 (0, 1),
                        new Vector2 (1, 1),
                        new Vector2 (1, 0),
                    },
                };

            }



        }


        void UpdateMaterialProperties() {

            if (rms == null)
                return;

            if (ignore)
                _highlighted = false;

            Color outlineColor = this.outlineColor;
            outlineColor.a = outline;
            Color overlayColor = this.overlayColor;
            overlayColor.a = overlay;
            Color seeThroughTintColor = this.seeThroughTintColor;
            seeThroughTintColor.a = this.seeThroughTintAlpha;

            if (outlineWidth < 0.01f) {
                outlineWidth = 0.01f;
            }
            if (glowWidth < 0) {
                glowWidth = 0;
            }
            if (overlayMinIntensity > overlay) {
                overlayMinIntensity = overlay;
            }

            for (int k = 0; k < rmsCount; k++) {
                // Setup materials
                if (rms[k].transform == null)
                    continue;

                ToggleZoom(rms[k].transform);

                // Sprite related
                rms[k].fxMatMask.SetFloat(UNIFORM_CUTOFF, alphaCutOff);
                rms[k].fxMatDepthWrite.SetFloat(UNIFORM_CUTOFF, alphaCutOff);
                rms[k].fxMatGlow.SetFloat(UNIFORM_CUTOFF, alphaCutOff);
                rms[k].fxMatOutline.SetFloat(UNIFORM_CUTOFF, alphaCutOff);
                rms[k].fxMatSeeThrough.SetFloat(UNIFORM_CUTOFF, alphaCutOff);
                if (pixelSnap) {
                    rms[k].fxMatMask.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatDepthWrite.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatGlow.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatOutline.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatOverlay.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatShadow.EnableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatSeeThrough.EnableKeyword(SKW_PIXELSNAP_ON);
                } else {
                    rms[k].fxMatMask.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatDepthWrite.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatGlow.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatOutline.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatOverlay.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatShadow.DisableKeyword(SKW_PIXELSNAP_ON);
                    rms[k].fxMatSeeThrough.DisableKeyword(SKW_PIXELSNAP_ON);
                }
                if (polygonPacking) {
                    rms[k].fxMatMask.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatDepthWrite.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatGlow.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatOutline.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatOverlay.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatShadow.EnableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatSeeThrough.EnableKeyword(SKW_POLYGON_PACKING);
                } else {
                    rms[k].fxMatMask.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatDepthWrite.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatGlow.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatOutline.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatOverlay.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatShadow.DisableKeyword(SKW_POLYGON_PACKING);
                    rms[k].fxMatSeeThrough.DisableKeyword(SKW_POLYGON_PACKING);
                }


                // Glow
                Material fxMat = rms[k].fxMatGlow;
                fxMat.SetVector("_Glow2", new Vector3(0f, glowAnimationSpeed, glowDithering ? 0 : 1));

                // Outline
                fxMat = rms[k].fxMatOutline;
                Color outlineColorAdj = outlineColor;
                if (outlineSmooth) {
                    if (outlineQuality == QualityLevel.Simple) {
                        outlineColorAdj.a *= 4f;
                    } else if (outlineQuality == QualityLevel.Medium) {
                        outlineColorAdj.a *= 2f;
                    }
                }
                fxMat.SetColor("_OutlineColor", outlineColorAdj);
                fxMat.SetFloat("_OutlineWidth", outlineWidth / 100f);

                // See-through
                Renderer renderer = rms[k].renderer;
                Material mat = renderer.sharedMaterial;
                Texture texture = null;
                if (renderer != null && mat != null) {
                    if (renderer is SpriteRenderer) {
                        SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
                        if (spriteRenderer.sprite != null) {
                            texture = spriteRenderer.sprite.texture;
                        }
                    } else {
                        texture = mat.mainTexture;
                    }
                    fxMat = rms[k].fxMatSeeThrough;
                    if (mat.HasProperty("_MainTex")) {
                        fxMat.mainTexture = texture;
                        fxMat.mainTextureOffset = mat.mainTextureOffset;
                        fxMat.mainTextureScale = mat.mainTextureScale;
                    }
                    fxMat.SetFloat("_SeeThrough", seeThroughIntensity);
                    fxMat.SetColor("_SeeThroughTintColor", seeThroughTintColor);
                }

                // Overlay
                fxMat = rms[k].fxMatOverlay;
                if (mat != null) {
                    if (mat.HasProperty("_MainTex")) {
                        fxMat.mainTexture = texture;
                        fxMat.mainTextureOffset = mat.mainTextureOffset;
                        fxMat.mainTextureScale = mat.mainTextureScale;
                    }
                    if (mat.HasProperty("_Color")) {
                        fxMat.SetColor("_OverlayBackColor", mat.GetColor("_Color"));
                    }
                }
                fxMat.color = overlayColor;
                fxMat.SetVector("_OverlayData", new Vector3(overlayAnimationSpeed, overlayMinIntensity, overlayBlending));
                fxMat.renderQueue = overlayRenderQueue;

                // Shadow
                fxMat = rms[k].fxMatShadow;
                if (mat != null) {
                    if (mat.HasProperty("_MainTex")) {
                        fxMat.mainTexture = texture;
                        fxMat.mainTextureOffset = mat.mainTextureOffset;
                        fxMat.mainTextureScale = mat.mainTextureScale;
                    }
                }
                if (shadow3D) {
                    shadowIntensity = 1f;
                }
                Color shadowColor = this.shadowColor;
                shadowColor.a *= shadowIntensity;
                fxMat.color = shadowColor;
            }
        }



        void ComputeSpriteCenter(int index, Sprite sprite) {
            if (sprite == null) return;
            Rect texRect = sprite.textureRect;
            texRect.x -= sprite.rect.xMin;
            texRect.y -= sprite.rect.yMin;
            float xMin = texRect.xMin;
            float xMax = texRect.xMax;
            float yMin = texRect.yMin;
            float yMax = texRect.yMax;
            float xMid = (xMax + xMin) / 2;
            float yMid = (yMax + yMin) / 2;
            // substract pivot
            xMid -= sprite.pivot.x;
            yMid -= sprite.pivot.y;
            // normalize 0-1
            xMid = xMid / sprite.rect.width;
            yMid = yMid / sprite.rect.height;
            rms[index].center = new Vector2(xMid, yMid);
            // also set aspect ratio
            rms[index].aspectRatio = (float)(xMax - xMin) / (yMax - yMin);
        }

        void ToggleZoom(Transform target) {
            if (target == null)
                return;

            if (target.transform.localScale == scaleAfterZoom) {
                target.transform.localScale = scaleBeforeZoom;
            }

            if (_highlighted) {
                Vector3 scale = target.transform.localScale;
                scaleBeforeZoom = scale;
                scaleAfterZoom = new Vector3(scale.x * zoomScale, scale.y * zoomScale, 1f);
                target.transform.localScale = scaleAfterZoom;
            }
        }


        Mesh SpriteToMesh(Sprite sprite) {
            Mesh mesh;
            if (!cachedMeshes.TryGetValue(sprite, out mesh)) {
                mesh = new Mesh();
                Vector2[] spriteVertices = sprite.vertices;
                int vertexCount = spriteVertices.Length;
                if (vertices == null) {
                    vertices = new List<Vector3>(vertexCount);
                } else {
                    vertices.Clear();
                }
                for (int x = 0; x < vertexCount; x++) {
                    vertices.Add(spriteVertices[x]);
                }
                ushort[] triangles = sprite.triangles;
                int indexCount = triangles.Length;
                if (indices == null) {
                    indices = new List<int>(indexCount);
                } else {
                    indices.Clear();
                }
                for (int x = 0; x < indexCount; x++) {
                    indices.Add(triangles[x]);
                }
                mesh.SetVertices(vertices);
                mesh.SetTriangles(indices, 0);
                mesh.uv = sprite.uv;
                cachedMeshes[sprite] = mesh;
            }
            return mesh;
        }


        Texture TextureWithBilinearSampling(Texture tex) {
            if (tex.filterMode != FilterMode.Point) {
                return tex;
            }
            Texture linTex;
            if (!cachedTextures.TryGetValue(tex, out linTex)) {
#if UNITY_EDITOR
                // Ensure texture is readable
                if (!Application.isPlaying) {
                    string path = AssetDatabase.GetAssetPath(tex);
                    if (!string.IsNullOrEmpty(path)) {
                        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
                        if (ti != null) {
                            if (!ti.isReadable) {
                                ti.isReadable = true;
                                ti.SaveAndReimport();
                            }
                        }
                    }
                }
#endif
                linTex = Instantiate<Texture>(tex);
                linTex.filterMode = FilterMode.Bilinear;
                cachedTextures[tex] = linTex;
            }
            return linTex;
        }
    }
}

