using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundColorManager : MonoBehaviour
{
    public float backgroundChangeSpeed;

    private Material _mat;
    private static readonly int TopColor = Shader.PropertyToID("_TopColor");
    private float h = 0;
    private float s = 59;
    private float v = 76;


    // Start is called before the first frame update
    void Start()
    {
        _mat = transform.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        h += Time.deltaTime * backgroundChangeSpeed;
        h %= 360;
        Color color = Color.HSVToRGB(h, s, v);
        _mat.SetColor(TopColor, color);
    }
}