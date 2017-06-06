using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GenomeLeg {

	public float m;
	public float M;
	public float o;
	public float p;

	public float EvaluateAt (float time)
	{
		//return 2.5f;
		//Debug.Log( (M - m) / 2 * (1 + Mathf.Sin((time+o) * Mathf.PI * 2 / p)) + m);
		return (M - m) / 2 * (1 + Mathf.Sin((time+o) * Mathf.PI * 2 / p)) + m;
	}

	public void RandomizeValues() {
		m = Random.Range (-1f, 1f);
		M = Random.Range (-1f, 1f);
		p = Random.Range (0.1f, 2f);
		o = Random.Range (-2f, 2f);
	}

	public GenomeLeg Clone()
	{
		GenomeLeg leg = new GenomeLeg ();
		leg.m = m;
		leg.M = M;
		leg.o = o;
		leg.p = o;
		return leg;
	}

	public void Mutate(float annealingTemperature)
	{
		m = SampleGaussean(m, annealingTemperature*0.01f);
		m = Mathf.Clamp (m, -1f, +1f);
		M = SampleGaussean(M, annealingTemperature*0.01f);
		M = Mathf.Clamp (M, -1f, +1f);
		p = SampleGaussean(p, annealingTemperature*0.01f);
		p = Mathf.Clamp (p, 0.1f, 2f);
		o = SampleGaussean(o, annealingTemperature*0.02f);
		o = Mathf.Clamp (o, -2f, 2f);
	}

	public float SampleGaussean(float mean, float stdDev) {
		float u1 = 1.0f - Random.Range (0f, 1.0f);
		float u2 = 1.0f - Random.Range (0f, 1.0f);
		float randStdNormal = Mathf.Sqrt (-2.0f * Mathf.Log (u1)) * Mathf.Sin (2.0f * Mathf.PI * u2);
		return mean + stdDev * randStdNormal;
	}

	public static bool operator ==(GenomeLeg g1, GenomeLeg g2) {
		return g1.Equals (g2);
	}

	public static bool operator !=(GenomeLeg g1, GenomeLeg g2) {
		return !g1.Equals (g2);
	}

	public override string ToString() {
		StringBuilder resultString = new StringBuilder ();
		resultString.Append ("m = " + m + "\n");
		resultString.Append ("M = " + M + "\n");
		resultString.Append ("p = " + p + "\n");
		resultString.Append ("o = " + o + "\n");
		return resultString.ToString ();
	}
}
