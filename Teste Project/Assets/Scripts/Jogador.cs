using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : Personagem
{
    public override string ToString()
    {
        string Aux = "";

        Aux += "<b> NOME <\b>:" + Nome + "<p>";
        Aux += "<b> AREA <\b>:" + Area + "<p>";
        Aux += "<b> POS <\b>:" + this.transform.position;

        return Aux;
    }

    public override void OnDrawGizmos()
    {
        //Desenhar icone do personagem
        Gizmos.DrawIcon(this.transform.position, "personagem.png");

        //Desenhar a área de alcance deste personagem
        Gizmos.color = new Color(1.0f, 1.0f, 0.5f, 0.5f);
        Gizmos.DrawSphere(this.transform.position, Area);
    }

    //Contrutor da classe
    public Jogador(string Nome, float Area)
    {
        this.Nome = Nome;
        this.Area = Area;
    }

    public void Start()
    {
        
    }
}
