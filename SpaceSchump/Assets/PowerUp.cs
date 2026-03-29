using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class PowerUp : MonoBehaviour
{
    [Header("Inscribed")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 10f;
    public float fadeTime = 4f;

    [Header("Dynamic")]
    public eWeaponType type;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;
    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private  Material cubeMat;
    public Vector3 velocity;
    public float lifeTimer;
    public float fadeTimer;

    void Awake()
    {
        cube = transform.GetChild(0).gameObject;
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeMat = cube.GetComponent<Renderer>().material;

        Vector3 vel = Random.onUnitSphere;
    }
}
