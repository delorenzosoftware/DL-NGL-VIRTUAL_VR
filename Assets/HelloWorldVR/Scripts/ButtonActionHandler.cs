using UnityEngine;
using OculusSampleFramework;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ButtonActionHandler : MonoBehaviour
{
    public PokeInteractable ButtonDB;
    private void Awake()
    {
        // Registre a fun��o que voc� deseja acionar quando o bot�o � tocado (poked).
       
    }

    private void OnButtonTouched()
    {
        // Execute a a��o desejada quando o bot�o � tocado (poked).
        Debug.Log("Bot�o tocado!");
        // Insira sua a��o personalizada aqui.
    }
}