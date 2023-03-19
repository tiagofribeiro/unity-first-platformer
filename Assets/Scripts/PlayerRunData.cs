using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Run Data")]         //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerRunData : ScriptableObject
{
    [Header("Run")]
    public float runMaxSpeed;                           // Velocidade que o player deve atingir.
    public float runAcceleration;                       // Tempo (aprox.) para o jogador ir de 0 a 100 (rs).
    [HideInInspector] public float runAccelAmount;      // A força (vezes speedDiff) aplicada no jogador (para acelerar).
    public float runDecceleration;                      // Tempo (aprox.) para o jogador ir de 100 a 0.
    [HideInInspector] public float runDeccelAmount;     // A força (vezes speedDiff) aplicada no jogador (para parar).
    [Space(10)]
    [Range(0.01f, 1)] public float accelInAir;          // Multiplicadores aéreos.
    [Range(0.01f, 1)] public float deccelInAir;
    public bool doConserveMomentum;


    private void OnValidate()
    {
        // Calcula as forças de aceleração e "frenagem" utilizando a fórmula: accel = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        #region Variable Ranges
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
        #endregion
    }
}
