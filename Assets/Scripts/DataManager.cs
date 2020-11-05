using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public GeneticAlgorithm bestDNA;
    public TextMeshProUGUI generationNum;
    public TextMeshProUGUI fitnessNum;
    public TextMeshProUGUI playerHealthNum;
    public TextMeshProUGUI playerAttackNum;
    public TextMeshProUGUI playerHealNum;
    public TextMeshProUGUI potionSpawnNum;
    public TextMeshProUGUI roomLengthNum;
    public TextMeshProUGUI enemyHealthNum;
    public TextMeshProUGUI enemyAttackNum;
    public TextMeshProUGUI enemySpawnNum;
    public int index;
    public int generation;

    void Start()
    {
        bestDNA = FindObjectOfType<GeneticAlgorithm>();
    }

    public void DataDisplay()
    {
        index = bestDNA.bestGenes.Count - 1;
        generation = index + 1;

        if (bestDNA.bestGenes.Count != 0)
        {
            generationNum.text     = "" + generation;
            fitnessNum.text        = "" + bestDNA.bestGenes[index].fitness;
            playerHealthNum.text   = "" + bestDNA.bestGenes[index].genes[0];
            playerAttackNum.text   = "" + bestDNA.bestGenes[index].genes[1];
            playerHealNum.text     = "" + bestDNA.bestGenes[index].genes[2];
            potionSpawnNum.text    = 100 * bestDNA.bestGenes[index].genes[3] + "%" ;
            roomLengthNum.text     = "" + bestDNA.bestGenes[index].genes[4];
            enemyHealthNum.text    = "" + bestDNA.bestGenes[index].genes[5];
            enemyAttackNum.text    = "" + bestDNA.bestGenes[index].genes[6];
            enemySpawnNum.text     = 100 * bestDNA.bestGenes[index].genes[7] + "%";
        }
    }

    public void DataNext()
    {
        index++;
        generation++;

        if (index == bestDNA.bestGenes.Count)
        {
            index = 0;
            generation = index + 1;
        }

        if (bestDNA.bestGenes.Count != 0)
        {
            generationNum.text = "" + generation;
            fitnessNum.text = "" + bestDNA.bestGenes[index].fitness;
            playerHealthNum.text = "" + bestDNA.bestGenes[index].genes[0];
            playerAttackNum.text = "" + bestDNA.bestGenes[index].genes[1];
            playerHealNum.text = "" + bestDNA.bestGenes[index].genes[2];
            potionSpawnNum.text = 100 * bestDNA.bestGenes[index].genes[3] + "%";
            roomLengthNum.text = "" + bestDNA.bestGenes[index].genes[4];
            enemyHealthNum.text = "" + bestDNA.bestGenes[index].genes[5];
            enemyAttackNum.text = "" + bestDNA.bestGenes[index].genes[6];
            enemySpawnNum.text = 100 * bestDNA.bestGenes[index].genes[7] + "%";
        }
    }

    public void DataPrevious()
    {
        index--;
        generation--;

        if (index == -1)
        {
            index = bestDNA.bestGenes.Count - 1;
            generation = index + 1;
        }

        if (bestDNA.bestGenes.Count != 0)
        {
            generationNum.text = "" + generation;
            fitnessNum.text = "" + bestDNA.bestGenes[index].fitness;
            playerHealthNum.text = "" + bestDNA.bestGenes[index].genes[0];
            playerAttackNum.text = "" + bestDNA.bestGenes[index].genes[1];
            playerHealNum.text = "" + bestDNA.bestGenes[index].genes[2];
            potionSpawnNum.text = 100 * bestDNA.bestGenes[index].genes[3] + "%";
            roomLengthNum.text = "" + bestDNA.bestGenes[index].genes[4];
            enemyHealthNum.text = "" + bestDNA.bestGenes[index].genes[5];
            enemyAttackNum.text = "" + bestDNA.bestGenes[index].genes[6];
            enemySpawnNum.text = 100 * bestDNA.bestGenes[index].genes[7] + "%";
        }
    }
}
