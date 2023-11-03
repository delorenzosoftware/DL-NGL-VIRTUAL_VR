using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.IO;


public class OpenScene : MonoBehaviour
{
    private CreateDBScript createDBScript;


    void Start()
    {
        Debug.Log("Iniciou");
        CreateDBScript createDBScript = GetComponent<CreateDBScript>();
        createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_3);
        Debug.Log("Cena criada com sucesso");

    }

}
   
    
 
