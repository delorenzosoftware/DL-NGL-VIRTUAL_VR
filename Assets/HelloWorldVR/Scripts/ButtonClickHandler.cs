using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class ButtonClickHandler : MonoBehaviour
{
    private OVRInput.Button buttonToClick = OVRInput.Button.One; // Escolha o botao apropriado
    private GameObject lastButtonClicked = null;
    private CreateDBScript createDBScript;


    void Start()
    {
        createDBScript = GetComponent<CreateDBScript>();
    }

    private void Update()
    {
        if (OVRInput.GetDown(buttonToClick))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Button")) // Use a tag correta para seus botoes
                {
                    // Execute a funcao desejada quando o botao e clicado.
                    // Por exemplo, chame uma funcao no objeto do botao clicado:

                    createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_2);
                }
            }
        }
    }
}