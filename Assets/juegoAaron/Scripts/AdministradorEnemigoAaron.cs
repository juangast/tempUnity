using UnityEngine;

public class AdministradorEnemigoAaron : MonoBehaviour
{
    [System.Serializable]
    public class AtaqueEnemigoAaron
    {
        public string nombreAtaque;
        public int danio;
        [Range(0, 100)] public int probabilidadAcierto = 100;
    }

    public AdministradorBatallaAaron administradorBatalla;
    public AtaqueEnemigoAaron[] ataques;

    public void EjecutarAtaqueAleatorioAaron()
    {
        if (administradorBatalla == null) { Debug.LogError("Falta conectar AdministradorBatallaAaron en AdministradorEnemigoAaron."); return; }
        if (ataques == null || ataques.Length == 0) { Debug.LogError("No hay ataques configurados en AdministradorEnemigoAaron."); return; }

        AtaqueEnemigoAaron ataque = ataques[Random.Range(0, ataques.Length)];
        bool acerto = Random.Range(1, 101) <= ataque.probabilidadAcierto;

        if (acerto) administradorBatalla.RecibirAtaqueEnemigoAaron(ataque.danio, ataque.nombreAtaque);
        else administradorBatalla.FallarAtaqueEnemigoAaron(ataque.nombreAtaque);
    }
}