using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class DBInteractionScript : MonoBehaviour
{
    public PokeInteractable ButtonDB;
    

    private CreateDBScript createDBScript;
    // Start is called before the first frame update
    void Start()
    {
        createDBScript = GetComponent<CreateDBScript>();
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        Debug.Log("botão clicado");
        createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_2);
    }
}