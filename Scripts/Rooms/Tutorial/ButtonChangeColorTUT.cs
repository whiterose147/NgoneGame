using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class ButtonChangeColorTUT : TutorialComponent
{
    [SerializeField] private GameObject button;
    
    
    private Material originalMaterial;
    private Color originalColor;

    private Material newMaterial;

    private void Start()
    {
       
        newMaterial= button.GetComponent<Renderer>().material;

        originalMaterial = button.GetComponent<Renderer>().material;
        originalColor = originalMaterial.color;

    }
    public override void ActivateAction()
    {
        newMaterial.color = Color.green;
    }

    public override void DeActivateAction()
    {
        originalMaterial.color = originalColor;
    }
}
