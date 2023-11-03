using UnityEngine;
using OculusSampleFramework;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ButtonActionHandler : MonoBehaviour
{
    public PokeInteractable ButtonDB;
    private void Awake()
    {
        // Registre a função que você deseja acionar quando o botão é tocado (poked).
       
    }

    private void OnButtonTouched()
    {
        // Execute a ação desejada quando o botão é tocado (poked).
        Debug.Log("Botão tocado!");
        // Insira sua ação personalizada aqui.
    }
}