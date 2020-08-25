using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{

    public enum eTipoPersonagem {Jogador, Inimigo, Conversa, Quest, Chefes}

    [HideInInspector]
    public eTipoPersonagem TipoJogador;

    [Tooltip("Nome desse personagem")]
    public string Nome;

    [Tooltip("Área de alcance do personagem")]
    [Range(1.0f,10f)]
    public float Area = 1.0f;

    public Vector3 Posicao;

    public virtual void OnDrawGizmos()
    {
        switch (TipoJogador)
        {
            //Desenhar tipo de icone do personagem 
            case eTipoPersonagem.Jogador:
                Gizmos.DrawIcon(this.transform.position, "personagem.png");
                break;
            case eTipoPersonagem.Inimigo:
                Gizmos.DrawIcon(this.transform.position, "inimigo.png");
                break;
            case eTipoPersonagem.Conversa:
                Gizmos.DrawIcon(this.transform.position, "balao.png");
                break;
            case eTipoPersonagem.Quest:
                Gizmos.DrawIcon(this.transform.position, "pe.png");
                break;
            case eTipoPersonagem.Chefes:
                Gizmos.DrawIcon(this.transform.position, "boss.png");
                break;           
        }

        //Desenhar a área de alcance deste personagem
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.2f);
        Gizmos.DrawSphere(this.transform.position, Area);
    }    
}
