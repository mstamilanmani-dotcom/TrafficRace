using UnityEngine;
using TMPro;

public class BeachDecorationBuilder : MonoBehaviour
{
    [Header("Beach Layout")]
    public float beachStartX = 4f;
    public float treeLineX = 26f;
    public float beachLength = 200f;

    [Header("Materials")]
    public Material trunkMaterial;
    public Material leafMaterial;
    public Material sandMaterial;
    public Material umbrellaMaterials;
    public Material chairMaterial;
    public Material poleMaterial;
    public Material boardMaterial;

    [Header("Decoration Counts")]
    public int coconutTreeCount = 8;
    public int umbrellaCount = 6;

    [Header("Tamil Sign")]
    public TMP_FontAsset tamilFont;

    void Start()
    {
        BuildBeachDecorations();
    }

    public void BuildBeachDecorations()
    {
        BuildCoconutTrees();
        BuildBeachUmbrellas();
        BuildBeachSign();
    }

    void BuildCoconutTrees()
    {
        for (int i = 0; i < coconutTreeCount; i++)
        {
            float z = -beachLength / 2f + 12f +
                      i * (beachLength - 24f) /
                      Mathf.Max(1, coconutTreeCount - 1);

            CreatePalmTree(new Vector3(treeLineX, 0f, z));
        }
    }

    void CreatePalmTree(Vector3 position)
    {
        GameObject tree = new GameObject("CoconutTree");
        tree.transform.SetParent(transform);
        tree.transform.localPosition = position;

        GameObject trunk = GameObject.CreatePrimitive(
            PrimitiveType.Cylinder);
        trunk.name = "PalmTrunk";
        trunk.transform.SetParent(tree.transform, false);
        trunk.transform.localPosition = new Vector3(0f, 2.5f, 0f);
        trunk.transform.localScale = new Vector3(0.45f, 2.5f, 0.45f);
        ApplyMaterial(trunk, trunkMaterial);

        Vector3 crown = new Vector3(0f, 5f, 0f);

        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Vector3 direction = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                -0.35f,
                Mathf.Sin(angle * Mathf.Deg2Rad)
            ).normalized;

            GameObject leaf = GameObject.CreatePrimitive(
                PrimitiveType.Capsule);
            leaf.name = "PalmFrond";
            leaf.transform.SetParent(tree.transform, false);
            leaf.transform.localPosition =
                crown + direction * 1.5f;
            leaf.transform.localRotation =
                Quaternion.FromToRotation(Vector3.up, direction);
            leaf.transform.localScale =
                new Vector3(0.22f, 2.2f, 0.22f);
            ApplyMaterial(leaf, leafMaterial);
        }

        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;
            Vector3 coconutOffset = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * 0.35f,
                -0.35f,
                Mathf.Sin(angle * Mathf.Deg2Rad) * 0.35f
            );

            GameObject coconut = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            coconut.name = "Coconut";
            coconut.transform.SetParent(tree.transform, false);
            coconut.transform.localPosition = crown + coconutOffset;
            coconut.transform.localScale = Vector3.one * 0.35f;
            ApplyMaterial(coconut, trunkMaterial);
        }
    }

    void BuildBeachUmbrellas()
    {
        for (int i = 0; i < umbrellaCount; i++)
        {
            float z = -beachLength / 2f + 25f + i * 28f;
            float x = beachStartX + 11f;

            CreateUmbrella(new Vector3(x, 0f, z));
            CreateBeachChair(new Vector3(x + 2.2f, 0f, z + 1.5f));
        }
    }

    void CreateUmbrella(Vector3 position)
    {
        GameObject umbrella = new GameObject("BeachUmbrella");
        umbrella.transform.SetParent(transform);
        umbrella.transform.localPosition = position;

        GameObject pole = GameObject.CreatePrimitive(
            PrimitiveType.Cylinder);
        pole.transform.SetParent(umbrella.transform, false);
        pole.transform.localPosition = new Vector3(0f, 1.1f, 0f);
        pole.transform.localScale = new Vector3(0.08f, 1.1f, 0.08f);
        ApplyMaterial(pole, poleMaterial);

        GameObject canopy = GameObject.CreatePrimitive(
            PrimitiveType.Sphere);
        canopy.transform.SetParent(umbrella.transform, false);
        canopy.transform.localPosition = new Vector3(0f, 2.15f, 0f);
        canopy.transform.localScale = new Vector3(2.2f, 0.35f, 2.2f);
        ApplyMaterial(canopy, umbrellaMaterials);
    }

    void CreateBeachChair(Vector3 position)
    {
        GameObject chair = new GameObject("BeachChair");
        chair.transform.SetParent(transform);
        chair.transform.localPosition = position;

        GameObject seat = GameObject.CreatePrimitive(
            PrimitiveType.Cube);
        seat.transform.SetParent(chair.transform, false);
        seat.transform.localPosition = new Vector3(0f, 0.45f, 0f);
        seat.transform.localScale = new Vector3(0.8f, 0.12f, 1.5f);
        ApplyMaterial(seat, chairMaterial);

        GameObject back = GameObject.CreatePrimitive(
            PrimitiveType.Cube);
        back.transform.SetParent(chair.transform, false);
        back.transform.localPosition = new Vector3(0f, 0.9f, -0.55f);
        back.transform.localRotation = Quaternion.Euler(-18f, 0f, 0f);
        back.transform.localScale = new Vector3(0.8f, 0.9f, 0.12f);
        ApplyMaterial(back, chairMaterial);
    }

    void BuildBeachSign()
    {
        GameObject sign = new GameObject("TamilBeachSign");
        sign.transform.SetParent(transform);
        sign.transform.localPosition = new Vector3(12f, 0f, -85f);

        GameObject board = GameObject.CreatePrimitive(
            PrimitiveType.Cube);
        board.transform.SetParent(sign.transform, false);
        board.transform.localPosition = new Vector3(0f, 2.2f, 0f);
        board.transform.localScale = new Vector3(4.5f, 1.2f, 0.18f);
        ApplyMaterial(board, boardMaterial);

        GameObject textObject = new GameObject("TamilSignText");
        textObject.transform.SetParent(sign.transform, false);
        textObject.transform.localPosition = new Vector3(0f, 2.2f, -0.11f);

        TextMeshPro text = textObject.AddComponent<TextMeshPro>();
        text.text = "கடற்கரை";
        text.fontSize = 3.5f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.rectTransform.sizeDelta = new Vector2(4.3f, 1f);

        if (tamilFont != null)
            text.font = tamilFont;
    }

    void ApplyMaterial(GameObject obj, Material material)
    {
        if (material != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
                renderer.sharedMaterial = material;
        }
    }
}
