using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI velocidadeTexto;
    public TextMeshProUGUI pontuacaoTexto;
    public TextMeshProUGUI instrucoesTexto;

    public GameObject vitoriaTexto;
    public PlayerController playerController;

    private bool tutorialIniciado = false;
    private bool ganhou = false;

    void Update()
    {
        //Mosta a velocidade
        float velocidade = playerController.GetVelocidadeAtual();
        velocidadeTexto.text = "Velocidade = " + velocidade.ToString("F2");

        //Mostra o Score
        int score = playerController.macas;
        pontuacaoTexto.text = ": " + score;

        if(playerController.moveu == true && tutorialIniciado == false){
            tutorialIniciado = true;
            StartCoroutine(Tutorial());
        }

        if(playerController.macas >= 10 && ganhou == false){
            ganhou = true;
            StartCoroutine(Vitoria(vitoriaTexto.GetComponent<TextMeshProUGUI>()));
        }
    }

    private IEnumerator Tutorial()
    {
        //Pausa a Coroutine por 5 segundos
        yield return new WaitForSeconds(5);

        instrucoesTexto.gameObject.SetActive(false);
    }

    private IEnumerator Vitoria(TextMeshProUGUI texto){
        while(true){
            texto.gameObject.SetActive(true);  // Mostra o texto
            yield return new WaitForSeconds(1);
            texto.gameObject.SetActive(false); // Esconde o texto
            yield return new WaitForSeconds(1);
        }
    }
}
