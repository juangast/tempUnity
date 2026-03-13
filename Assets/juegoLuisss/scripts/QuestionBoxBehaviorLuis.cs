using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionBoxBehaviorLuis : MonoBehaviour
{
    private static readonly QuestionDataLuis[] questions = new QuestionDataLuis[]
    {
        new QuestionDataLuis(
            "AWAQ no cubre lesiones ocasionadas por animales silvestres. Los participantes deben contar con su propio seguro de salud y viaje antes de llegar a la estación biológica.",
            "¿AWAQ cubre lesiones ocasionadas por animales silvestres?",
            new string[] { "Sí, todas", "Solo las graves", "No, el participante debe tener su propio seguro", "Solo con seguro adicional de AWAQ" },
            2
        ),
        new QuestionDataLuis(
            "AWAQ, que significa 'tejedor' en quechua, es una ONGD que desarrolla proyectos de investigación científica y conservación de ecosistemas a través de Estaciones Biológicas.",
            "¿Qué significa 'AWAQ' en quechua?",
            new string[] { "Guardián", "Tejedor", "Naturaleza", "Explorador" },
            1
        ),
        new QuestionDataLuis(
            "En las estaciones biológicas de AWAQ, el hospedaje y la alimentación están incluidos para los practicantes a cambio de su asistencia en las actividades de campo y conservación.",
            "¿Qué incluye AWAQ para sus practicantes en la estación biológica?",
            new string[] { "Solo transporte", "Hospedaje y alimentación", "Seguro médico completo", "Salario mensual" },
            1
        ),
        new QuestionDataLuis(
            "AWAQ opera sus estaciones biológicas en regiones biodiversas de Colombia, trabajando en cooperación internacional entre España y Colombia para la conservación del medio ambiente.",
            "¿En qué país opera AWAQ sus estaciones biológicas?",
            new string[] { "Ecuador", "Perú", "Colombia", "Brasil" },
            2
        ),
        new QuestionDataLuis(
            "Los participantes de AWAQ NO deben tocar, alimentar ni acercarse a los animales silvestres sin supervisión de un experto, ya que esto puede alterar su comportamiento natural y poner en riesgo tanto al animal como al participante.",
            "¿Qué deben hacer los participantes de AWAQ al encontrar un animal silvestre?",
            new string[] { "Alimentarlo", "Tomarse una foto de cerca", "No tocarlo y mantener distancia sin supervisión", "Llevarlo al campamento" },
            2
        ),
        new QuestionDataLuis(
            "AWAQ nació a partir de la organización Hoteles Solidarios, que cuenta con más de 15 años de experiencia en cooperación internacional y desarrollo sostenible.",
            "¿De qué organización surgió AWAQ?",
            new string[] { "Greenpeace", "WWF", "Hoteles Solidarios", "Cruz Roja" },
            2
        ),
        new QuestionDataLuis(
            "En una estación biológica como las de AWAQ, los residuos deben separarse correctamente: orgánicos, reciclables y no reciclables, para minimizar el impacto ambiental en el ecosistema.",
            "¿Cómo se deben manejar los residuos en una estación biológica de AWAQ?",
            new string[] { "Quemarlos en el bosque", "Enterrarlos cerca del río", "Separarlos en orgánicos, reciclables y no reciclables", "Dejarlos para que se degraden solos" },
            2
        ),
        new QuestionDataLuis(
            "AWAQ trabaja con universidades como la Universidad Autónoma de Manizales y el SENA, permitiendo que estudiantes realicen sus prácticas profesionales en las estaciones biológicas.",
            "¿Con qué instituciones educativas colombianas trabaja AWAQ?",
            new string[] { "Universidad de los Andes", "Universidad Autónoma de Manizales y SENA", "Universidad Nacional", "Universidad del Rosario" },
            1
        ),
        new QuestionDataLuis(
            "El transporte hacia la estación biológica, el seguro de viaje y el seguro médico NO están incluidos en el programa de AWAQ. Cada participante es responsable de gestionarlos antes de su llegada.",
            "¿Cuál de estos NO está incluido en el programa de pasantía de AWAQ?",
            new string[] { "Alimentación", "Hospedaje", "Seguro médico y de viaje", "Actividades de campo" },
            2
        ),
        new QuestionDataLuis(
            "Las actividades en las estaciones biológicas de AWAQ van desde el amanecer hasta prácticamente el siguiente amanecer, incluyendo investigación de campo, monitoreo de biodiversidad y educación ambiental.",
            "¿En qué horario se realizan las actividades de campo en AWAQ?",
            new string[] { "Solo en la mañana", "De 9am a 5pm", "Desde el amanecer hasta prácticamente el siguiente amanecer", "Solo en la noche" },
            2
        ),
        new QuestionDataLuis(
            "Si encuentras una especie desconocida durante tu pasantía en AWAQ, debes documentarla con fotografías y reportarla al biólogo encargado. Nunca debes capturarla o manipularla por tu cuenta.",
            "¿Qué debes hacer si encuentras una especie desconocida en la estación de AWAQ?",
            new string[] { "Capturarla para estudiarla", "Ignorarla y seguir caminando", "Documentarla y reportarla al biólogo encargado", "Llevarla al campamento" },
            2
        ),
        new QuestionDataLuis(
            "AWAQ promueve el desarrollo humano y sostenible de comunidades en regiones biodiversas mediante la creación de Estaciones Experimentales para la Investigación de la Biodiversidad.",
            "¿Qué tipo de desarrollo promueve AWAQ en las comunidades?",
            new string[] { "Desarrollo industrial", "Desarrollo urbano", "Desarrollo humano y sostenible", "Desarrollo tecnológico exclusivamente" },
            2
        )
    };

    private bool triggered = false;
    private QuestionDataLuis currentQuestion;
    private UIControllerLuis ui;

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (transform.position.x < player.transform.position.x - 50f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        triggered = true;
        GameControlLuis.Instance.sfxManager.PlayQuestionBox();
        Time.timeScale = 0f;

        ui = GameControlLuis.Instance.uiController;
        currentQuestion = questions[Random.Range(0, questions.Length)];
        ShowInfoPhase();
    }

    private Color originalPanelColor;

    void ShowInfoPhase()
    {
        if (ui.hudPanel != null)
            ui.hudPanel.SetActive(false);

        if (ui.hudPanelImage != null)
        {
            originalPanelColor = ui.hudPanelImage.color;
            ui.hudPanelImage.color = new Color(originalPanelColor.r, originalPanelColor.g, originalPanelColor.b, 0f);
        }

        ui.questionPanel.SetActive(true);
        ui.infoText.text = currentQuestion.infoText;
        ui.infoText.gameObject.SetActive(true);

        ui.questionText.gameObject.SetActive(false);
        foreach (Button btn in ui.answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        ui.continueButton.gameObject.SetActive(true);
        ui.continueButton.onClick.RemoveAllListeners();
        ui.continueButton.onClick.AddListener(() =>
        {
            ShowQuestionPhase();
        });
    }

    void ShowQuestionPhase()
    {
        ui.infoText.gameObject.SetActive(false);
        ui.continueButton.gameObject.SetActive(false);

        if (ui.continueText != null)
            ui.continueText.SetActive(false);

        ui.questionText.text = currentQuestion.questionText;
        ui.questionText.gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            ui.answerButtons[i].gameObject.SetActive(true);
            ui.answerTexts[i].text = currentQuestion.answers[i];

            ui.answerButtons[i].onClick.RemoveAllListeners();
            int index = i;
            ui.answerButtons[i].onClick.AddListener(() =>
            {
                OnAnswerSelected(index);
            });
        }
    }

    void OnAnswerSelected(int selected)
    {
        GameControlLuis.Instance.sfxManager.PlayButtonPress();

        if (selected == currentQuestion.correctIndex)
        {
            GameControlLuis.Instance.sfxManager.PlayCorrectAnswer();
            GameControlLuis.Instance.AddPoints(50);
            GameControlLuis.Instance.uiController.ShowGhostPoints(50);
        }
        else
        {
            GameControlLuis.Instance.sfxManager.PlayWrongAnswer();
        }

        ui.questionPanel.SetActive(false);
        if (ui.hudPanel != null)
            ui.hudPanel.SetActive(true);
        if (ui.hudPanelImage != null)
            ui.hudPanelImage.color = originalPanelColor;
        if (ui.continueText != null)
            ui.continueText.SetActive(true);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
