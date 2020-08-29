
using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using PathologicalGames;
using HyperCard;

public class UtilityObjectHolder : MonoBehaviour
{
    public void Start()
    {
        ServiceLocator.UtilityObjectHolder = this;
    }

    public GameObject CardHolder;
    public GameState GameState;
    public CardAnimationManager CardAnimationManager;
    public ActionManager ActionManager;
    public SpawnPool SpawnPool;
    public GameObject UiCanvas;
    public CardInstantiator CardInstantiator;
    public ExplainerPanel ExplainerPanel;
    public TemplateHolder TemplateHolder;

}