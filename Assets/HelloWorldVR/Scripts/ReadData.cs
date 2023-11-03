using System.Collections;
using UnityEngine;
using Oculus.Interaction;
using System.IO;


public class ReadData : MonoBehaviour
{
    private CreateDBScript createDBScript;
    void Start()
    {
        Coroutine coroutine = StartCoroutine(CopyDatabase());
        
    }
   
    public void HandlePointerEvent(PointerEvent pointerEvent)
    {
        Debug.Log("HandlePointerEvent Iniciado");

        if (pointerEvent.Type == PointerEventType.Select)
        {
            Debug.Log("botão clicado");
            createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_2);
        }
    }
    IEnumerator CopyDatabase()
    {
        Debug.Log("copiar databases");
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "DataBase.db");
        string targetPath = Path.Combine(Application.persistentDataPath, "DataBase.db");
        Debug.Log("sourcePath - " + sourcePath);

        if (File.Exists(targetPath))
        {
            File.Delete(targetPath);
        }

        WWW www = new WWW(sourcePath);
        yield return www;

        Debug.Log("sourcePath www - " + www.text + www.bytes);
        File.WriteAllBytes(targetPath, www.bytes);
        Debug.Log("copiou");

        CreateDBScript createDBScript = GetComponent<CreateDBScript>();
        yield return createDBScript.CreateTableUserPerformances();
        createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_1);
        createDBScript.completedExeperiment(CreateDBScript.EXEPERIMENT_3);
    }
}
