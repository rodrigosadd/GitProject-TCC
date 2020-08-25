using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codigo : MonoBehaviour
{
    public enum eClasseInimigo { Malee, Ranged }
    public enum eTipoInimigo { Tank, Dps, Mago, Healer }

    [System.Serializable]
    public struct sJogador
    {
        [Tooltip("jogador.")]
        public string nome;

        [Tooltip("Breve descrição do jogador.")]
        [Multiline(8)]
        public string Descriçao;

        [Tooltip("Vida")]
        [Range(1, 100)]
        public int hp;

        [Tooltip("São Paulo")]
        [Range(1, 100)]
        public int sp;

        [Tooltip("Dano")]
        [Range(1, 100)]
        public int forca;

        [Tooltip("Armadura")]
        [Range(1, 100)]
        public int defesa;
    }
    [Header("Dados do jogador")]
    public sJogador Jogador;




    [System.Serializable]
    public struct sInimigo
    {
        [Tooltip("Inimigo.")]
        public string nome;

        [Tooltip("Breve descrição do Inimigo.")]
        [Multiline(8)]
        public string Descriçao;

        [Tooltip("Vida")]
        [Range(1, 100)]
        public int hp;

        [Tooltip("São Paulo")]
        [Range(1, 100)]
        public int sp;

        [Tooltip("Dano")]
        [Range(1, 100)]
        public int forca;

        [Tooltip("Armadura")]
        [Range(1, 100)]
        public int defesa;

        public eClasseInimigo Classe;
        public eTipoInimigo Tipo;
    }
    [Header("Dados do jogador")]
    public sInimigo Inimigo;

    [HideInInspector]
    public string senha;

    [ContextMenu("Oi corno seja bem vindo")]
    public void HelloWord()
    {
        Debug.Log("Hello Word");
    }
}
