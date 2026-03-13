using UnityEngine;

public class AdministradorMegaAtaqueAaron : MonoBehaviour
{
    public AdministradorBatallaAaron administradorBatalla;
    public AdministradorConcentracionAaron administradorConcentracion;
    public int danioMegaAtaque;

    public void UsarMegaAtaqueAaron()
    {
        if (administradorBatalla == null || administradorConcentracion == null) return;
        if (administradorBatalla.EstaBatallaTerminadaAaron() || !administradorBatalla.EsTurnoJugadorAaron()) return;
        if (!administradorConcentracion.ConsumirMegaAtaqueAaron()) return;
        administradorBatalla.AtacarConMegaAtaqueAaron(danioMegaAtaque, "Báculo de la Selva");
    }
}