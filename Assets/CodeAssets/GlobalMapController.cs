using UnityEngine;
using System.Collections;
using HyperCard;
using static TacMapController;

// Singleton, attached to TileMap
public class TacMapController : MonoBehaviour
{
    public void Notify(CardSelectedMessage obj)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    public void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
    public class CardSelectedMessage
    {
        bool Deselected { get; set; } = false;

        PlayerCard CardSelectedOrUnselected { get; set; }
    }



}
