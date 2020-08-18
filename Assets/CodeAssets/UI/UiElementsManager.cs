using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class UiElementsManager
{

    public static List<RichUiElement> RichUiElements { get; set; } = new List<RichUiElement>();

    public static void AddUiElement(RichUiElement richElement)
    {
        if (RichUiElements.Any(item => item.Id == richElement.Id))
        {
            throw new System.Exception($"Attempted to add multipe ui items with id {richElement.Id}");
        }
        RichUiElements.Add(richElement);
    }

    public static RichUiElement GetElementById(string Id)
    {
        var element = RichUiElements.Where(item => item.Id == Id).SingleOrDefault();
        if (element == null)
        {
            throw new System.Exception("Could not find ui element with id " + Id);
        }
        return element;
    }

}
