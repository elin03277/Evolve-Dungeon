using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GeneticTests
    {
        SaveManager saveManager = new SaveManager();
        GeneticAlgorithm.DNA dna = new GeneticAlgorithm.DNA();
        GeneticAlgorithm.DNA dnaParent = new GeneticAlgorithm.DNA();
        float[] targetDNA = new float[8];

        [UnityTest]
        public IEnumerator NewDNATest()
        {
            Debug.Log("Before RandomDNA(): ("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");

            dna.RandomDNA();
            yield return null;
            
            Debug.Log("After RandomDNA(): (" 
                + dna.genes[0] + ",  "
                + dna.genes[1] + ",  "
                + dna.genes[2] + ",  "
                + dna.genes[3] + ",  "
                + dna.genes[4] + ",  "
                + dna.genes[5] + ",  "
                + dna.genes[6] + ",  "
                + dna.genes[7] + ")");
        }

        [UnityTest]
        public IEnumerator CalculateFitnessTest()
        {
            targetDNA[0] = 100f;
            targetDNA[1] = 20f;
            targetDNA[2] = 20f;
            targetDNA[3] = 0.4f;
            targetDNA[4] = 3f;
            targetDNA[5] = 100f;
            targetDNA[6] = 20f;
            targetDNA[7] = 0.4f;

            dna.RandomDNA();
            yield return null;

            Debug.Log("TargetDNA: ("
            + targetDNA[0] + ",  "
            + targetDNA[1] + ",  "
            + targetDNA[2] + ",  "
            + targetDNA[3] + ",  "
            + targetDNA[4] + ",  "
            + targetDNA[5] + ",  "
            + targetDNA[6] + ",  "
            + targetDNA[7] + ")");

            Debug.Log("NewDNA: ("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");

            Debug.Log("Fitness before CalculateFitness(): " + dna.fitness);
            dna.CalculateFitness(targetDNA);
            yield return null;
            Debug.Log("Fitness after CalculateFitness(): " + Mathf.Round(dna.fitness));
            Assert.NotZero(dna.fitness);
        }

        [UnityTest]
        public IEnumerator CrossoverTest()
        {
            dna.RandomDNA();
            dnaParent.RandomDNA();
            yield return null;

            Debug.Log("ParentDNA: ("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");

            Debug.Log("OtherParentDNA: ("
            + dnaParent.genes[0] + ",  "
            + dnaParent.genes[1] + ",  "
            + dnaParent.genes[2] + ",  "
            + dnaParent.genes[3] + ",  "
            + dnaParent.genes[4] + ",  "
            + dnaParent.genes[5] + ",  "
            + dnaParent.genes[6] + ",  "
            + dnaParent.genes[7] + ")");

            GeneticAlgorithm.DNA child = dna.Crossover(dnaParent);
            yield return null;

            Debug.Log("ChildDNA: ("
            + child.genes[0] + ",  "
            + child.genes[1] + ",  "
            + child.genes[2] + ",  "
            + child.genes[3] + ",  "
            + child.genes[4] + ",  "
            + child.genes[5] + ",  "
            + child.genes[6] + ",  "
            + child.genes[7] + ")");
        }

        [UnityTest]
        public IEnumerator MutationTest()
        {
            dna.RandomDNA();
            yield return null;
           
            Debug.Log("Before Mutation(): ("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");

            dna.Mutation();
            yield return null;

            Debug.Log("After Mutation(): ("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");
        }

        [UnityTest]
        public IEnumerator SaveTest()
        {
            dna.RandomDNA();
            yield return null;

            Debug.Log("("
            + dna.genes[0] + ",  "
            + dna.genes[1] + ",  "
            + dna.genes[2] + ",  "
            + dna.genes[3] + ",  "
            + dna.genes[4] + ",  "
            + dna.genes[5] + ",  "
            + dna.genes[6] + ",  "
            + dna.genes[7] + ")");

            saveManager.Save(dna, 1);
        }
    }
}
