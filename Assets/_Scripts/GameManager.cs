using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text txtComponente;
    [SerializeField] State inicialState;
    [SerializeField] Text txtLocal;
    [SerializeField] Image imagemLocal;
    [SerializeField] Canvas canvasCaso;
    [SerializeField] Canvas canvasObj;
    [SerializeField] Canvas canvasAnotacoes;
    [SerializeField] Canvas canvasGameOver;
    [SerializeField] Canvas canvasRelatorioFinal;
    [SerializeField] Canvas canvasJogo;
    [SerializeField] Canvas canvasFim;
    [SerializeField] Text txtTotalCarruagem;
    [SerializeField] Text txtContagemGameOver;
    [SerializeField] Text txtInfoGameOver;
    [SerializeField] GameObject txtAssassino;
    [SerializeField] GameObject txtMotivo;
    [SerializeField] GameObject txtArma;
    [SerializeField] GameObject txtObjeto;
    [SerializeField] Sprite[] fotosLocais;

    private AudioSource[] _audios;

    public bool canvasCasoHabilitado = true;
    public bool canvasObjHabilitado = true;
    public bool canvasAnotacoesHabilitado = true;
    public bool canvasGameOverHabilitado = true;
    public bool canvasRelatorioFinalHabilitado = true;
    public bool canvasJogoHabilitado = true;
    public bool canvasFimHabilitado = true;

    bool habilitaInputs = false;

    int totalViagens = 20;
    int contagemReiniciaFase = 4;

    State state;
    void Start()
    {
        state = inicialState;
        txtComponente.text = state.GetStateStory();
        txtLocal.text = state.name;
        imagemLocal.sprite = fotosLocais[7];
        HabilitaCanvasAnotacoes();
        HabilitaCanvasGameOver();
        HabilitaCanvasRelatFinal();
        HabilitaCanvasFim();
        _audios = GetComponents<AudioSource>();
        txtTotalCarruagem.text = "x" + totalViagens;
    }

    // Update is called once per frame
    void Update()
    {
        ManagerState();
    }

    private void ManagerState()
    {
        var nextState = state.GetNextState();
        if (!canvasCasoHabilitado && !canvasObjHabilitado && !canvasAnotacoesHabilitado && !canvasGameOverHabilitado && !canvasRelatorioFinalHabilitado &&
            !canvasFimHabilitado)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _audios[0].Play();
                state = nextState[0];
                AtualizaCarruagens();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _audios[0].Play();
                state = nextState[1];
                AtualizaCarruagens();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _audios[0].Play();
                state = nextState[2];
                AtualizaCarruagens();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (state.name.Equals("Casa do Sherlok Holmes"))
                {
                    //habilitaInputs = false;
                    _audios[1].Play();
                    HabilitaCanvasRelatFinal();
                    HabilitaCanvasJogo();
                }
            }
        }
        txtComponente.text = state.GetStateStory();
        txtLocal.text = state.name;
        SelecionaFoto(state.name);
    }
    public void HabilitaCanvasCaso()
    {
        canvasCasoHabilitado = !canvasCasoHabilitado;
        canvasCaso.enabled = canvasCasoHabilitado;
    }
    public void HabilitaCanvasObj()
    {
        canvasObjHabilitado = !canvasObjHabilitado;
        canvasObj.enabled = canvasObjHabilitado;
    }
    public void HabilitaCanvasAnotacoes()
    {
        canvasAnotacoesHabilitado = !canvasAnotacoesHabilitado;
        canvasAnotacoes.enabled = canvasAnotacoesHabilitado;
    }

    public void HabilitaCanvasGameOver()
    {
        canvasGameOverHabilitado = !canvasGameOverHabilitado;
        canvasGameOver.enabled = canvasGameOverHabilitado;
    }

    public void HabilitaCanvasRelatFinal()
    {
        canvasRelatorioFinalHabilitado = !canvasRelatorioFinalHabilitado;
        canvasRelatorioFinal.enabled = canvasRelatorioFinalHabilitado;
    }

    public void HabilitaCanvasJogo()
    {
        canvasJogoHabilitado = !canvasJogoHabilitado;
        canvasJogo.enabled = canvasJogoHabilitado;
    }
    public void HabilitaCanvasFim()
    {
        canvasFimHabilitado = !canvasFimHabilitado;
        canvasFim.enabled = canvasFimHabilitado;
    }
    public void PlayPaperMenus()
    {
        _audios[1].Play();
    }
    public void ReiniciaJogo()
    {
        
        SceneManager.LoadScene("Menu");
    }

    public void SetHabilitaInputs()
    {
        //habilitaInputs = true;
    }

    public void AtualizaCarruagens()
    {
        totalViagens -= 1;
        if (totalViagens < 0)
        {
            Fim("Acabou as carruagens.");
        }
        else
        {
            txtTotalCarruagem.text = "x" + totalViagens;
        }
        
    }

    public void Fim(string texto)
    {
        HabilitaCanvasGameOver();
        txtInfoGameOver.text = texto;
        StartCoroutine(FimDeJogo());
    }

    public IEnumerator FimDeJogo()
    {
        while (contagemReiniciaFase > 0) {
            contagemReiniciaFase -= 1;
            txtContagemGameOver.text = contagemReiniciaFase.ToString();
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("Fase");
    }

    public void RelatorioFinal()
    {
        string assassino = txtAssassino.GetComponent<Text>().text.ToUpper();
        string motivo = txtMotivo.GetComponent<Text>().text.ToUpper();
        string arma = txtArma.GetComponent<Text>().text.ToUpper();
        string objeto = txtObjeto.GetComponent<Text>().text.ToUpper();
        if ((assassino.Equals("EARL AKINTERN")) && 
            (motivo.Contains("DÍVIDA") || motivo.Contains("DÍVIDA COM BANCO") || motivo.Contains("DÍVIDA COM O BANCO")) &&
            (arma.Equals("PEQUENA ESPADA") || arma.Equals("ESPADA PEQUENA")) && 
            (objeto.Equals("MANUSCRITO HAMLET") || objeto.Equals("MANUSCRITO DE HAMLET") || objeto.Equals("HAMLET")))
        {
            HabilitaCanvasFim();
        }
        else
        {
            Fim("Você não desvendou o caso!");
        }
    }

    public void SelecionaFoto(string nome)
    {
        switch (nome)
        {
            case "Banco":
                imagemLocal.sprite = fotosLocais[0];
                break;
            case "Bar":
                imagemLocal.sprite = fotosLocais[1];
                break;
            case "Carruagem":
                imagemLocal.sprite = fotosLocais[2];
                break;
            case "Casa De Penhores":
                imagemLocal.sprite = fotosLocais[12];
                break;
            case "Casa do Sherlok Holmes":
                imagemLocal.sprite = fotosLocais[7];
                break;
            case "Charutaria":
                imagemLocal.sprite = fotosLocais[3];
                break;
            case "Chaveiro":
                imagemLocal.sprite = fotosLocais[4];
                break;
            case "Docas":
                imagemLocal.sprite = fotosLocais[5];
                break;
            case "Farmacia":
                imagemLocal.sprite = fotosLocais[6];
                break;
            case "Hotel":
                imagemLocal.sprite = fotosLocais[8];
                break;
            case "Livraria":
                imagemLocal.sprite = fotosLocais[9];
                break;
            case "Museu":
                imagemLocal.sprite = fotosLocais[10];
                break;
            case "Parque":
                imagemLocal.sprite = fotosLocais[11];
                break;
            case "Scotland Yard":
                imagemLocal.sprite = fotosLocais[13];
                break;
            case "Teatro":
                imagemLocal.sprite = fotosLocais[14];
                break;

        }
    }
}
