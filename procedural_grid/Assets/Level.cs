using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(Material))]
public class Level : MonoBehaviour
{
    // The side length of a single texture in pixels
    [SerializeField] private int textureSideLength = 16;

    // Specifies the dimensions of the texture atlas
    // in number of textures. So suppose your texture
    // atlas had 3 textures in the width and 2 in the
    // height. Then the textureAtlasWidth is 3 and the
    // textureAtlasHeight is 2.
    [SerializeField] private int textureAtlasWidth = 4;
    [SerializeField] private int textureAtlasHeight = 4;

    // TODO: Determine values from grid of texture indecies
    // Specifies the level grid size
    [SerializeField] private int levelWidth = 20;
    [SerializeField] private int levelHeight = 10;

    private int[] levelTiles;

    // Start is called before the first frame update
    void Start()
    {
        ConstructLevel();
    }
    
    private void ConstructLevel()
    {
        levelTiles = new int[levelWidth * levelHeight];

        for (int i = 0; i < levelTiles.Length; i++)
            levelTiles[i] = Random.Range(0, textureAtlasWidth * textureAtlasHeight);

        ConstructLevelMesh();
    }

    private void ConstructLevelMesh()
    {
        Mesh mesh = new Mesh();

        // For each quad, we want four vertecies
        Vector3[] vertecies = new Vector3[levelWidth * levelHeight * 4];
        Vector2[] uv = new Vector2[vertecies.Length];
        int[] triangles = new int[levelWidth * levelHeight * 6];

        int index;
        int textureIndex;
        float tex_u, tex_v;
        float tex_u_offset = (float)textureSideLength / (float)(textureSideLength * textureAtlasWidth);
        float tex_v_offset = (float)textureSideLength / (float)(textureSideLength * textureAtlasHeight);
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                index = y * levelWidth + x;

                vertecies[4 * index] = new Vector3(x - 0.5f, y - 0.5f);
                vertecies[4 * index + 1] = new Vector3(x - 0.5f, y + 0.5f);
                vertecies[4 * index + 2] = new Vector3(x + 0.5f, y + 0.5f);
                vertecies[4 * index + 3] = new Vector3(x + 0.5f, y - 0.5f);

                textureIndex = levelTiles[index];
                int tx, ty;

                tx = textureIndex % textureAtlasWidth;
                ty = (textureIndex - tx) / textureAtlasWidth;

                tex_u = (float)tx / textureAtlasWidth;
                tex_v = (float)ty / textureAtlasHeight;

                /*uv[4 * index] = new Vector2(0.25f, 0.25f);
                uv[4 * index + 1] = new Vector2(0.25f, 0.5f);
                uv[4 * index + 2] = new Vector2(0.5f, 0.5f);
                uv[4 * index + 3] = new Vector2(0.5f, 0.25f);*/

                uv[4 * index] = new Vector2(tex_u, tex_v);
                uv[4 * index + 1] = new Vector2(tex_u, tex_v + tex_v_offset);
                uv[4 * index + 2] = new Vector2(tex_u + tex_u_offset, tex_v + tex_v_offset);
                uv[4 * index + 3] = new Vector2(tex_u + tex_u_offset, tex_v);

                triangles[6 * index] = 4 * index;
                triangles[6 * index + 1] = 4 * index + 1;
                triangles[6 * index + 2] = 4 * index + 3;
                triangles[6 * index + 3] = 4 * index + 1;
                triangles[6 * index + 4] = 4 * index + 2;
                triangles[6 * index + 5] = 4 * index + 3;
            }
        }

        mesh.vertices = vertecies;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
