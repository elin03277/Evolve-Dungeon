using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneticAlgorithm : MonoBehaviour
{
    public List<DNA> population;
    public List<DNA> newPopulation;
 
    public List<DNA> bestGenes;

    public GameManager gameManager;
    public SaveManager saveManager;

    public int generation;
    public int generationsPassed;
    public int elitism;
    public int populationSize;
    public int dnaSize;
    private float fitnessSum;
    public bool initialize;

    void Start() {
        populationSize = 100;
        dnaSize = 8;
        generation = 1;
        generationsPassed = 10;
        elitism = 2;
        initialize = true;
        
        gameManager = FindObjectOfType<GameManager>();

        population = new List<DNA>(populationSize);
        newPopulation = new List<DNA>(populationSize);

        bestGenes = new List<DNA>(populationSize);
    }

    public void GenerationsPassed()
    {
        if (initialize)
        {
            gameManager.SetTargetDNA();
            for (int i = 0; i < populationSize; i++)
            {
                DNA dna = new DNA();
                dna.RandomDNA();
                dna.CalculateFitness(gameManager.targetDNA);
                population.Add(dna);
            }

            population.Sort(compareDNA);

            population[0].fitness = Mathf.Round(population[0].fitness);

            // Add best gene of first generation
            bestGenes.Add(population[0]);

            generation++;

            for (int i = 0; i < generationsPassed - 1; i++)
            {
                NewGeneration();

                population.Sort(compareDNA);

                // Add best gene of first generation
                bestGenes.Add(population[0]);
            }


            saveManager.Save(population[0], generation);
            saveManager.LoadSave();
            initialize = false;
        } 
        else
        {
            gameManager.SetTargetDNA();

            for (int i = 0; i < generationsPassed; i++)
            {
                NewGeneration();

                population.Sort(compareDNA);

                // Add best gene of first generation
                bestGenes.Add(population[0]);
            }
            // GenerationCheck();

            saveManager.Save(population[0], generation);
            saveManager.LoadSave();
        }

        //for (int i = 0; i < bestGenes.Count; i++)
        //{
        //    Debug.Log(bestGenes[i].fitness);
        //}
    }

    public void PopulationCheck()
    {
        foreach (DNA dna in population)
        {
            Debug.Log("Genes 0: " + dna.genes[0]);
            Debug.Log("Genes 1: " + dna.genes[1]);
            Debug.Log("Genes 2: " + dna.genes[2]);
            Debug.Log("Genes 3: " + dna.genes[3]);
            Debug.Log("Genes 4: " + dna.genes[4]);
            Debug.Log("Genes 5: " + dna.genes[5]);
            Debug.Log("Genes 6: " + dna.genes[6]);
            Debug.Log("Genes 7: " + dna.genes[7]);
            Debug.Log("Fitness: " + dna.fitness);
        }
    }

    public void GenerationCheck()
    {

        Debug.Log("Generation: " + generation);
        Debug.Log("Gene 1: " + population[0].genes[0]);
        Debug.Log("Gene 2: " + population[1].genes[1]);
        Debug.Log("Gene 3: " + population[2].genes[2]);
        Debug.Log("Gene 4: " + population[3].genes[3]);
        Debug.Log("Gene 5: " + population[4].genes[4]);
        Debug.Log("Gene 6: " + population[5].genes[5]);
        Debug.Log("Gene 7: " + population[6].genes[6]);
        Debug.Log("Gene 8: " + population[7].genes[7]);

        Debug.Log(population[0].fitness);
    }

    public void FitnessCheck()
    {
        foreach (DNA dna in population)
        {
            Debug.Log("Fitness: " + dna.fitness);
        }
    }

    // Generates new population after the individuals in the first population is setup
    public void NewGeneration()
    {
        if (population.Count <= 0)
        {
            return;
        }

        // Calculates the total sum of the fitness in the population to be used when choosing a parent
        CalculatePopulationFitness();

        // Sorts the population from highest fitness to lowest
        population.Sort(compareDNA);

        newPopulation.Clear();

        //Debug.Log(population[0].fitness);

        // Iterates through the population and either adds individuals through elitism or the result of crossover
        for (int i = 0; i < population.Count; i++)
        {
            // Takes x amount of the fittest individuals and directly adds them to the new population
            if (i < elitism)
            {
                newPopulation.Add(population[i]);
            }
            else
            {
                // Chooses two parents from the population by having their fitness divided by the fitness sum to create their probability
                DNA parent1 = ChooseParent();
                DNA parent2 = ChooseParent();

                // A child of the two parents are made through crossover so there is a 50/50 split between the genes of the parents
                DNA child = parent1.Crossover(parent2);

                // Mutation is applied with a 1% to all genes to change by a small amount and is necessary to introduce variance
                child.Mutation();

                // The fitness of the child is calculated based on the target DNA
                child.fitness = child.CalculateFitness(gameManager.targetDNA);

                // Data analyzer all new fitness
                // bestFitness.Add(child.fitness);

                // The child is added to the new generation
                newPopulation.Add(child);

            }
        }

        // New generation replaces old generation
        List<DNA> tempList = population;
        population = newPopulation;
        newPopulation = tempList;

        // Generation counter is incremented
        generation++;
    }

    public int compareDNA(DNA a, DNA b)
    {
        if (a.fitness > b.fitness) {
            return -1;
        } else if (a.fitness < b.fitness)
        {
            return 1;
        } else {
            return 0;
        }
    }

    public void CalculatePopulationFitness()
    {
        fitnessSum = 0;
        //DNA best = population[0];

        for (int i = 0; i < population.Count; i++)
        {
            fitnessSum += population[i].fitness;

            //if (population[i].fitness > best.fitness)
            //{
            //   best = population[i];
            //}
        }
        //Debug.Log("Percent of gene 1: " + population[0].fitness / fitnessSum);
        //Debug.Log("Percent of gene 2: " + population[1].fitness / fitnessSum);
        //Debug.Log("Percent of gene 3: " + population[2].fitness / fitnessSum);
        //bestFitness = best.fitness;
        //best.genes.CopyTo(bestGenes, 0);
    }
    

    private DNA ChooseParent()
    {
        // float randomNum = Random.value * fitnessSum;
        while (true)
        {
            for (int i = 0; i < population.Count; i++)
            {

                if (Random.value < (population[i].fitness) / fitnessSum)
                {
                    return population[i];
                }

                // randomNum -= population[i].fitness;
            }
        }
        //return null;
    }

    public class DNA
    {
        public float[] genes = new float[8];
        public float fitness = 0f;

        public float CalculateFitness(float[] target)
        {
            fitness += Mathf.Abs(target[0] - genes[0]);
            //Debug.Log(fitness);
           // Debug.Log(genes[0]);
            fitness += Mathf.Abs(target[1] - genes[1]);
            // Debug.Log(fitness);
            fitness += Mathf.Abs(target[2] - genes[2]);
            fitness += Mathf.Abs(target[3] - genes[3]);
            fitness += Mathf.Abs(target[4] - genes[4]);
            fitness += Mathf.Abs(target[5] - genes[5]);
            fitness += Mathf.Abs(target[6] - genes[6]);
            fitness += Mathf.Abs(target[7] - genes[7]);
            //Debug.Log("Fitness after |target - genes| and summing up all the values: " + fitness);
            fitness = 300f - fitness;
            //Debug.Log("Fitness after subtracting it from 300: " + fitness);
            fitness *= 10f;
            //Debug.Log("Fitness after multiplying it by 10: " + fitness);
            //Debug.Log(fitness);
            return Mathf.Round(fitness);
        }

        public void RandomDNA()
        {
            genes = new float[8];
            fitness = 0;
            //Random.InitState(System.DateTime.Now.Millisecond);
            genes[0] = Mathf.Round(Random.Range(50f, 150f));
            genes[1] = Mathf.Round(Random.Range(10f, 30f));
            genes[2] = Mathf.Round(Random.Range(10f, 30f));
            genes[3] = Mathf.Round(Random.Range(0.2f, 0.6f) * 100f) / 100f;
            genes[4] = Mathf.Round(Random.Range(2, 7));
            genes[5] = Mathf.Round(Random.Range(50f, 150f));
            genes[6] = Mathf.Round(Random.Range(10f, 30f));
            genes[7] = Mathf.Round(Random.Range(0.2f, 0.6f) * 100f) / 100f;
        }

        public DNA Crossover(DNA otherParent)
        {
            DNA child = new DNA();

            for (int i = 0; i < genes.Length; i++)
            {
                child.genes[i] = Random.value < 0.5 ? genes[i] : otherParent.genes[i];
                //Debug.Log(child.genes[i]);
            }

            return child;
        }

        // Fixed mutation rate of 1%
        public void Mutation()
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (Random.value < 0.01f)
                {
                    if (i == 3 || i == 7)
                    {
                        genes[i] += 0.01f;
                    }
                    else
                    {
                        genes[i]++;
                    }
                }
            }
        }
    }
}
