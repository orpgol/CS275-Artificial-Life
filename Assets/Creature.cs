using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	public Genome genome;

	public LegController leftLeg;
	public LegController rightLeg;
	public LegController leftLeg1;
	public LegController rightLeg1;

	public FootController leftFoot;
	public FootController rightFoot;
	public FootController leftFoot1;
	public FootController rightFoot1;

	private Vector3 initialPosition;

	public GameObject body;
	private float bodyAngle = 0.0f;
	private int iter = 0;

	// Use this for initialization
	void Start () {
		initialPosition = transform.GetChild(0).position;
	}

	void Awake() {
		genome.leftLeg.m = Random.Range (-1f, 1f);
		genome.leftLeg.M = Random.Range (-1f, 1f);
		genome.leftLeg.p = Random.Range (0.1f, 2f);
		genome.leftLeg.o = Random.Range (-2f, 2f);

		genome.rightLeg.m = Random.Range (-1f, 1f);
		genome.rightLeg.M = Random.Range (-1f, 1f);
		genome.rightLeg.p = Random.Range (0.1f, 2f);
		genome.rightLeg.o = Random.Range (-2f, 2f);

		genome.leftFoot.m = Random.Range (-1f, 1f);
		genome.leftFoot.M = Random.Range (-1f, 1f);
		genome.leftFoot.p = Random.Range (0.1f, 2f);
		genome.leftFoot.o = Random.Range (-2f, 2f);

		genome.rightFoot.m = Random.Range (-1f, 1f);
		genome.rightFoot.M = Random.Range (-1f, 1f);
		genome.rightFoot.p = Random.Range (0.1f, 2f);
		genome.rightFoot.o = Random.Range (-2f, 2f);

		genome.leftLeg1.m = Random.Range (-1f, 1f);
		genome.leftLeg1.M = Random.Range (-1f, 1f);
		genome.leftLeg1.p = Random.Range (0.1f, 2f);
		genome.leftLeg1.o = Random.Range (-2f, 2f);

		genome.rightLeg1.m = Random.Range (-1f, 1f);
		genome.rightLeg1.M = Random.Range (-1f, 1f);
		genome.rightLeg1.p = Random.Range (0.1f, 2f);
		genome.rightLeg1.o = Random.Range (-2f, 2f);

		genome.leftFoot1.m = Random.Range (-1f, 1f);
		genome.leftFoot1.M = Random.Range (-1f, 1f);
		genome.leftFoot1.p = Random.Range (0.1f, 2f);
		genome.leftFoot1.o = Random.Range (-2f, 2f);

		genome.rightFoot1.m = Random.Range (-1f, 1f);
		genome.rightFoot1.M = Random.Range (-1f, 1f);
		genome.rightFoot1.p = Random.Range (0.1f, 2f);
		genome.rightFoot1.o = Random.Range (-2f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		leftLeg.position = genome.leftLeg.EvaluateAt(Time.time);
		rightLeg.position = genome.rightLeg.EvaluateAt(Time.time);

		leftFoot.position = genome.leftFoot.EvaluateAt(Time.time);
		rightFoot.position = genome.rightFoot.EvaluateAt(Time.time);

		leftLeg1.position = genome.leftLeg1.EvaluateAt(Time.time);
		rightLeg1.position = genome.rightLeg1.EvaluateAt(Time.time);

		leftFoot1.position = genome.leftFoot1.EvaluateAt(Time.time);
		rightFoot1.position = genome.rightFoot1.EvaluateAt(Time.time);

		bodyAngle += Mathf.Abs(90.0f - body.transform.rotation.z);
		iter += 1;
	}

	public float GetScore() {
		float bodyAngleMean = bodyAngle/iter;
		return (transform.GetChild(0).position.x - initialPosition.x)*4 - bodyAngleMean;
	}
}
