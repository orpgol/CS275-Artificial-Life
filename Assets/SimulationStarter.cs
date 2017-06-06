using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStarter : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
		StartCoroutine(Simulation ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public int generations = 100;
	public float simulationTime = 15f;

	public IEnumerator Simulation() 
	{
		//bestGenome.RandomizeValues ();
		for (int i = 0; i < generations; i++) 
		{
			Debug.Log ("Starting Generation: " + (i + 1));
			Time.timeScale = timeScale;
			CreateCreatures (i == 0);
			StartSimulation ();

			yield return new WaitForSeconds (simulationTime);

			StopSimulation ();
			EvaluateScore (i == (generations - 1));

			DestroyCreatures ();

			yield return new WaitForSeconds (1);

			// Cool down the annealing
			annealingTemperature -= coolingPerGeneration;
			annealingTemperature = (annealingTemperature < 0f) ? 0f : annealingTemperature;
		}

		// Should only iterate once
		foreach (Genome genome in bestGenomes) {
			Debug.Log ("Best Genome is " + genome);
			Creature creature = Instantiate (prefab, Vector3.zero, Quaternion.identity).GetComponent<Creature> ();
			creature.genome = genome;
		}
	}

	public int initialPopulation = 100;
	public int variations = 10;
	public int numberOfBestOffspring = 5;
	public int numberOfSelectedGenomes = 10;
	//private List<Genome> bestGenomes = new List<Genome>();
	private HashSet<Genome> bestGenomes = new HashSet<Genome>();

	public GameObject prefab;
	private List<Creature> creatures = new List<Creature>();
	public float annealingTemperature = 100;
	public float coolingPerGeneration = 1;
	public bool enableStochasticBeamSearch = true;
	public float timeScale = 1.0f;
	public void CreateCreatures(bool randomStart = false)
	{
		if (randomStart) {
			for (int i = 0; i < initialPopulation; i++) 
			{
				Genome newGenome = new Genome();
				Creature creature = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Creature>();


				newGenome.RandomizeValues();
				creature.genome = newGenome;
				creatures.Add (creature);
			}
			return;
		}
		Debug.Log ("Creating creatures... Annealing Temp: " + annealingTemperature);
		foreach (Genome genome in bestGenomes) 
		{
			// Add multiple copies of our best genomes so we dont go down in score per generation
			for (int i = 0; i < numberOfBestOffspring; i++) {
				Genome newGenome = genome.Clone ();
				Creature creature = Instantiate (prefab, Vector3.zero, Quaternion.identity).GetComponent<Creature> ();
				creature.genome = newGenome;
				creatures.Add (creature);
			}

			// Mutate our genome with the specified annealing temperature
			for (int i = 0; i < variations; i++) 
			{
				Genome newGenome = genome.Clone ();
				Creature creature = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Creature>();

	
				newGenome.Mutate (annealingTemperature);
				//newGenome.RandomizeValues();

				creature.genome = newGenome;
				creatures.Add (creature);
			}
		}
		Debug.Log ("Finished Creating creatures...");
	}

	public void StartSimulation() {
		foreach (Creature creature in creatures)
			creature.enabled = true;
	}

	public void StopSimulation() {
		foreach (Creature creature in creatures)
			creature.enabled = false;
	}

	public void DestroyCreatures() {
		foreach (Creature creature in creatures)
			Destroy (creature.gameObject);

		creatures.Clear ();
	}

	public void EvaluateScore(bool final = false) {
		//TODO: get the best numberOfSelectedGenomes genomes 
		bestGenomes.Clear();
		creatures.Sort(SortByScore);

		if (final) {
			Debug.Log ("Final creature score is " + creatures[0].GetScore());
			bestGenomes.Add (creatures [0].genome);
			return;
		}

		int selectedGenomes = 0;
		foreach (Creature creature in creatures) {
			
			if (selectedGenomes >= numberOfSelectedGenomes)
				break;

			if (bestGenomes.Contains (creature.genome)) {
				Debug.Log ("Duplicate Genome Found. Skipping...");
				continue;
			}
			
			Debug.Log ("Creature Score: " + creature.GetScore ());
			float addProbability = (enableStochasticBeamSearch) ? ((float) numberOfSelectedGenomes - selectedGenomes)/numberOfSelectedGenomes : 1.0f;
			if (Random.Range (0f, 1f) <= addProbability) { // Implements stochastic beam search
				Debug.Log ("Added creature to offspring with score: " + creature.GetScore ());
				bestGenomes.Add (creature.genome);
				selectedGenomes++;
			} else {
				Debug.Log ("Stochastic beam search rejected Creature with score: " + creature.GetScore());
			}
		}
	}

	static int SortByScore(Creature c1, Creature c2) {
		return c2.GetScore().CompareTo(c1.GetScore ());
	}
}
