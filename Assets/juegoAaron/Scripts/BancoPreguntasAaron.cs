using UnityEngine;

public class BancoPreguntasAaron : MonoBehaviour
{
    public enum RespuestaCorrectaAaron { A, B, C, D }
    public enum DificultadPreguntaAaron { Facil, Media, Dificil }

    [System.Serializable]
    public class PreguntaAaron
    {
        public string tipo;
        [TextArea(2, 4)] public string enunciado;
        public string opcionA, opcionB, opcionC, opcionD;
        public RespuestaCorrectaAaron respuestaCorrecta;
        public DificultadPreguntaAaron dificultad;
    }

    public PreguntaAaron[] preguntas;
    private int indiceActual = -1;

    public PreguntaAaron ObtenerSiguientePreguntaAaron()
    {
        if (preguntas == null || preguntas.Length == 0) { Debug.LogError("No hay preguntas cargadas en BancoPreguntasAaron."); return null; }
        indiceActual++;
        if (indiceActual >= preguntas.Length) indiceActual = 0;
        return preguntas[indiceActual];
    }
}