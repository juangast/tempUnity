using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdministradorConcentracionAaron : MonoBehaviour
{
    public AdministradorBatallaAaron administradorBatalla;
    public Image imagenBarraConcentracion;
    public Sprite concentracion0, concentracion1, concentracion2, concentracion3;
    public GameObject panelMegaAtaque;
    public GameObject nubes1, nubes2;
    public SFXManagerAaron sfxManager;

    private bool megaAtaqueActivoAntes = false;

    private void Start()
    {
        if (sfxManager == null) sfxManager = FindAnyObjectByType<SFXManagerAaron>();
        if (nubes1 != null) nubes1.SetActive(false);
        if (nubes2 != null) nubes2.SetActive(false);
        ActualizarUIConcentracionAaron();
    }

    private void MostrarNubesMegaAaron()
    {
        if (sfxManager != null) sfxManager.SonarAparicionMegaAaron();
        if (nubes1 != null) { nubes1.SetActive(false); nubes1.SetActive(true); }
        if (nubes2 != null) { nubes2.SetActive(false); nubes2.SetActive(true); }
    }

    public void ActualizarUIConcentracionAaron()
    {
        int racha = 0;
        if (administradorBatalla != null)
            racha = administradorBatalla.ObtenerRachaCorrectasAaron();

        int nivel = Mathf.Clamp(racha, 0, 3);

        if (imagenBarraConcentracion != null)
        {
            switch (nivel)
            {
                case 0: imagenBarraConcentracion.sprite = concentracion0; break;
                case 1: imagenBarraConcentracion.sprite = concentracion1; break;
                case 2: imagenBarraConcentracion.sprite = concentracion2; break;
                case 3: imagenBarraConcentracion.sprite = concentracion3; break;
            }
        }

        bool megaDisponible = MegaAtaqueDisponibleAaron();
        if (panelMegaAtaque != null) panelMegaAtaque.SetActive(megaDisponible);
        if (megaDisponible && !megaAtaqueActivoAntes) MostrarNubesMegaAaron();
        megaAtaqueActivoAntes = megaDisponible;
    }

    public bool MegaAtaqueDisponibleAaron()
    {
        if (administradorBatalla == null) return false;
        return administradorBatalla.ObtenerRachaCorrectasAaron() >= 3;
    }

    public bool ConsumirMegaAtaqueAaron()
    {
        if (!MegaAtaqueDisponibleAaron()) return false;
        if (administradorBatalla != null) administradorBatalla.ReiniciarRachaCorrectasAaron();
        ActualizarUIConcentracionAaron();
        return true;
    }
}