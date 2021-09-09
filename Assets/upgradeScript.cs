using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeScript
{

    private int nivelAtual = 0;
    private string nomeUpgrade;
    private string descUpgrade;
    private Sprite imgUpgrade;
    private int[] precoNiveis = new int[10];
    private int multiplierPreco = 1;

    // Start is called before the first frame update

    private void attPrecos()
    {
        precoNiveis[0] = 10 * multiplierPreco;
        precoNiveis[1] = 25 * multiplierPreco;
        precoNiveis[2] = 75 * multiplierPreco;
        precoNiveis[3] = 200 * multiplierPreco;
        precoNiveis[4] = 500 * multiplierPreco;
        precoNiveis[5] = 1000 * multiplierPreco;
        precoNiveis[6] = 2000 * multiplierPreco;
        precoNiveis[7] = 5000 * multiplierPreco;
        precoNiveis[8] = 10000 * multiplierPreco;
        precoNiveis[9] = 17500 * multiplierPreco;
    }

    public void setNivel(int nivel)
    {
        nivelAtual = nivel;
    }

    public int getNivelAtual()
    {
        return nivelAtual;
    }

    public int getprecoProxNivel()
    {
        return precoNiveis[nivelAtual];
    }

    public void setPrecoMult(int mult)
    {
        multiplierPreco = mult;
        attPrecos();
    }

    public void setTitulo(string titulo)
    {
        nomeUpgrade = titulo;
    }

    public string getTitulo()
    {
        return nomeUpgrade + " - Lvl " + (nivelAtual + 1).ToString();
    }

    public void setDescUpgrade(string desc)
    {
        descUpgrade = desc;
    }

    public string getDesc()
    {
        return descUpgrade;
    }

    public void setImage(string imagePath)
    {
        imgUpgrade = Resources.Load<Sprite>(imagePath);
    }

    public Sprite getImage()
    {
        return imgUpgrade;
    }

}
