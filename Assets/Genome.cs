using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Genome {

	public GenomeLeg leftLeg;
	public GenomeLeg rightLeg;
	public GenomeLeg leftLeg1;
	public GenomeLeg rightLeg1;

	public GenomeFoot leftFoot;
	public GenomeFoot rightFoot;
	public GenomeFoot leftFoot1;
	public GenomeFoot rightFoot1;

	//public GenomeLeg leftLeg1;
	//public GenomeLeg rightLeg1;

	public Genome Clone()
	{
		Genome genome = new Genome ();
		genome.leftLeg = leftLeg.Clone ();
		genome.rightLeg = rightLeg.Clone ();
		genome.leftLeg1 = leftLeg1.Clone ();
		genome.rightLeg1 = rightLeg1.Clone ();

		genome.leftFoot = leftFoot.Clone ();
		genome.rightFoot = rightFoot.Clone ();
		genome.leftFoot1 = leftFoot1.Clone ();
		genome.rightFoot1 = rightFoot1.Clone ();

		return genome;
	}

	public void Mutate(float annealingTemperature)
	{
		leftLeg.Mutate (annealingTemperature);
		leftFoot.Mutate (annealingTemperature);
		rightLeg.Mutate (annealingTemperature);
		rightFoot.Mutate (annealingTemperature);
		leftLeg1.Mutate (annealingTemperature);
		leftFoot1.Mutate (annealingTemperature);
		rightLeg1.Mutate (annealingTemperature);
		rightFoot1.Mutate (annealingTemperature);
	}

	public void RandomizeValues() {
		leftLeg.RandomizeValues ();
		rightLeg.RandomizeValues ();
		leftLeg1.RandomizeValues ();
		rightLeg1.RandomizeValues ();

		leftFoot.RandomizeValues ();
		rightFoot.RandomizeValues ();
		leftFoot1.RandomizeValues ();
		rightFoot1.RandomizeValues ();
	}

	public static bool operator ==(Genome g1, Genome g2) {
		return g1.Equals (g2);
	}

	public static bool operator !=(Genome g1, Genome g2) {
		return !g1.Equals (g2);
	}

	public override string ToString() {
		StringBuilder resultString = new StringBuilder ();
		resultString.Append("LeftLeg: \n");
		resultString.Append(leftLeg.ToString());
		resultString.Append ("\n");
		resultString.Append("RightLeg: \n");
		resultString.Append(rightLeg.ToString());
		resultString.Append ("\n");
		resultString.Append("LeftFoot: \n");
		resultString.Append(leftFoot.ToString());
		resultString.Append ("\n");
		resultString.Append("RightFoot: \n");
		resultString.Append(rightFoot.ToString());
		return resultString.ToString ();
	}
}
