using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    /*
     0 - Banco
     1 - Bar
     2 - Carruagem
     3 - Charutaria
     4 - Chaveiro
     5 - Docas
     6 - Farmacia
     7 - Hotel
     8 - Livraria
     9 - Museu
     10 - Parque
     11 - Penhores
     12 - Scotland
     13 - Sherlock
     14 - Teatro
     */

    [SerializeField] Image mapa;
    [SerializeField] Text txtTotalCarruagem;
    [SerializeField] Text txtContagemGameOver;
    [SerializeField] Canvas canvasCaso;
    [SerializeField] Canvas canvasObj;
    [SerializeField] Canvas canvasAnotacoes;
    [SerializeField] Canvas canvasRelatorioFinal;
    [SerializeField] Canvas canvasFim;
    [SerializeField] Canvas canvasOptions;
    [SerializeField] Canvas canvasGameOver;
    [SerializeField] Canvas canvasInformacao;
    [SerializeField] Text txtPista;
    [SerializeField] Text txtGameOver;
    [SerializeField] InputField txtAssassino;
    [SerializeField] InputField txtMotivo;
    [SerializeField] InputField txtArma;
    [SerializeField] InputField txtRoubado;
    [SerializeField] Slider sliderMusica;
    [SerializeField] Slider sliderEfeitoSonoro;
    [SerializeField] Sprite[] mapas;
    [SerializeField] Button[] botoes;
    [SerializeField] State[] states;

    bool habilitaCanvasInformacao = true;
    bool habilitaCanvasGameOver = false;
    bool habilitaCanvasCaso = true;
    bool habilitaCanvasObj = true;
    bool habilitaCanvasAnotacoes = false;
    bool habilitaCanvasRelatorioFinal = false;
    bool habilitaCanvasFim = false;
    bool habilitaCanvasOptions = false;

    private AudioSource[] _audios;

    string nomeScene;

    int totalCarruagem = 20;
    int contagemReiniciaFase = 4;
    private void Start()
    {
        nomeScene = SceneManager.GetActiveScene().name;
        _audios = GetComponents<AudioSource>();
        canvasInformacao.enabled = habilitaCanvasInformacao;
        canvasCaso.enabled = habilitaCanvasCaso;
        canvasObj.enabled = habilitaCanvasObj;
        canvasAnotacoes.enabled = habilitaCanvasAnotacoes;
        canvasRelatorioFinal.enabled = habilitaCanvasRelatorioFinal;
        canvasFim.enabled = habilitaCanvasFim;
        canvasGameOver.enabled = habilitaCanvasGameOver;
        canvasOptions.enabled = habilitaCanvasOptions;

        txtPista.text = states[13].GetStateStory();
        txtTotalCarruagem.text = "x" + totalCarruagem;
        HabilitaBotoes(13, 1, 6, 9);
    }

    public void HabilitaBotoes(int a, int b, int c, int d)
    {
        for (int i = 0; i < 15; i++)
        {
            botoes[i].enabled = false;
        }

        botoes[a].enabled = true;
        botoes[b].enabled = true;
        botoes[c].enabled = true;
        botoes[d].enabled = true;
    }
    public void ClickMapas(int posicao)
    {
        totalCarruagem -= 1;
        _audios[1].Play();
        if (totalCarruagem < 1)
        {
            GameOver("Acabaram as carruagens!");
        }
        else
        {
            txtTotalCarruagem.text = "x" + totalCarruagem;
        }
        mapa.sprite = mapas[posicao];
        txtPista.text = states[posicao].GetStateStory();
        HabilitaCanvasInformacoes();
        switch (posicao)
        {
            case 0: // Banco
                HabilitaBotoes(2, 0, 14, 12);
                break;
            case 1: // Bar
                HabilitaBotoes(10, 11, 1, 13);
                break;
            case 2: // Carruagem
                HabilitaBotoes(2, 0, 3, 7);
                break;
            case 3: // Charutaria
                HabilitaBotoes(3, 2, 10, 7);
                break;
            case 4: // Chaveiro
                HabilitaBotoes(4, 9, 8, 5);
                break;
            case 5: // Docas
                HabilitaBotoes(5, 7, 11, 4);
                break;
            case 6: // Farmacia
                HabilitaBotoes(6, 13, 14, 12);
                break;
            case 7: // Hotel
                HabilitaBotoes(7, 5, 2, 3);
                break;
            case 8: // Livraria
                HabilitaBotoes(8, 9, 4, 4);
                break;
            case 9: // Museu
                HabilitaBotoes(9, 13, 8, 4);
                break;
            case 10: // Parque
                HabilitaBotoes(10, 1, 3, 11);
                break;
            case 11: // Penhores
                HabilitaBotoes(11, 10, 5, 1);
                break;
            case 12: // Scotland
                HabilitaBotoes(12, 6, 14, 0);
                break; 
            case 13: // Sherlock
                HabilitaBotoes(13, 1, 6, 9);
                break;
            case 14: // Teatro
                HabilitaBotoes(14, 6, 0, 12);
                break;
        }
    }

    public void PlaySomMenus()
    {
        _audios[2].Play();
    }

    #region Habilita Canvas
    public void HabilitaCanvasInformacoes()
    {
        habilitaCanvasInformacao = !habilitaCanvasInformacao;
        canvasInformacao.enabled = habilitaCanvasInformacao;
    }
    public void HabilitaCanvasGameOver()
    {
        habilitaCanvasGameOver = !habilitaCanvasGameOver;
        canvasGameOver.enabled = habilitaCanvasGameOver;
    }
    public void HabilitaCanvaCaso()
    {
        habilitaCanvasCaso = !habilitaCanvasCaso;
        canvasCaso.enabled = habilitaCanvasCaso;
    }
    public void HabilitaCanvasObjetivo()
    {
        habilitaCanvasObj = !habilitaCanvasObj;
        canvasObj.enabled = habilitaCanvasObj;
    }
    public void HabilitaCanvasAnotacoes()
    {
        habilitaCanvasAnotacoes = !habilitaCanvasAnotacoes;
        canvasAnotacoes.enabled = habilitaCanvasAnotacoes;
    }
    public void HabilitaCanvasRelatorioFinal()
    {
        habilitaCanvasRelatorioFinal = !habilitaCanvasRelatorioFinal;
        canvasRelatorioFinal.enabled = habilitaCanvasRelatorioFinal;
    }
    public void HabilitaCanvasFim()
    {
        habilitaCanvasFim = !habilitaCanvasFim;
        canvasFim.enabled = habilitaCanvasFim;
    }
    public void HabilitaCanvasOptions()
    {
        habilitaCanvasOptions = !habilitaCanvasOptions;
        canvasOptions.enabled = habilitaCanvasOptions;
    }

    #endregion
    private void GameOver(string texto)
    {
        txtGameOver.text = texto;
        canvasGameOver.enabled = true;
        StartCoroutine(FimDeJogo());
    }
    public void RelatorioFinal(int i)
    {
        switch (i)
        {
            case 1:
                string assassino = txtAssassino.text.ToUpper();
                Debug.Log(assassino);
                string motivo = txtMotivo.text.ToUpper();
                Debug.Log(motivo);
                string arma = txtArma.text.ToUpper();
                Debug.Log(arma);
                string objeto = txtRoubado.text.ToUpper();
                Debug.Log(objeto);
                if ((assassino.Equals("EARL AKINTERN")) &&
                    (motivo.Contains("DÍVIDA") || motivo.Contains("DÍVIDA COM BANCO") || motivo.Contains("DÍVIDA COM O BANCO") || motivo.Contains("DIVIDA")) &&
                    (arma.Equals("PEQUENA ESPADA") || arma.Equals("ESPADA PEQUENA")) &&
                    (objeto.Equals("MANUSCRITO HAMLET") || objeto.Equals("MANUSCRITO DE HAMLET") || objeto.Equals("HAMLET")))
                {
                    HabilitaCanvasFim();
                }
                else
                {
                    GameOver("Você não desvendou o caso!");
                }
            break;
        }
        
    }

    public IEnumerator FimDeJogo()
    {
        while (contagemReiniciaFase > 0)
        {
            contagemReiniciaFase -= 1;
            txtContagemGameOver.text = contagemReiniciaFase.ToString();
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(nomeScene);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void VolumeJogo()
    {
        _audios[0].volume = sliderMusica.value;
        _audios[1].volume = sliderEfeitoSonoro.value;
        _audios[2].volume = sliderEfeitoSonoro.value;
        Debug.Log(sliderMusica.value);
    }

}
