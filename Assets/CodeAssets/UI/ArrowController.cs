using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ArrowRenderer))]
public class ArrowController: MonoBehaviour
{
    ArrowRenderer renderer;
    Vector3 start = Vector3.zero;
    public void Start()
    {
        renderer = this.GetComponent<ArrowRenderer>();
        this.SetVisible(false);
        this.gameObject.SetZ(Constants.ARROW_Z);
    }

    public void SetStart(Vector3 start)
    {
        this.start = start;
    }

    public void SetMaterial(Material material)
    {
        var renderers = renderer.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in renderers)
        {
            item.material = material;
        }
    }

    private bool visible = false;

    public void SetVisible(bool visible)
    {
        var renderers = renderer.GetComponentsInChildren<MeshRenderer>();
        foreach(var item in renderers)
        {
            item.enabled = visible;
        }
        this.visible = visible;
    }


    void Update()
    {
        var distanceFromScreen = 12;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromScreen;
        var arrowRenderer = gameObject.GetComponent<ArrowRenderer>();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.gameObject.GetComponent<ArrowRenderer>().SetPositions(this.start, worldMousePosition);
    }
}
