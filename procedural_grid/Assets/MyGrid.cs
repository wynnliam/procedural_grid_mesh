using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyGrid : MonoBehaviour
{
    [SerializeField] private int xSize = 10;
    [SerializeField] private int ySize = 10;

    private Mesh mesh;

    private void Awake()
    {
        //Generate();
        Generate2();
    }

    private void Generate2()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // For each quad, we want four vertecies
        Vector3[] vertecies = new Vector3[xSize * ySize * 4];
        Vector2[] uv = new Vector2[vertecies.Length];
        int[] triangles = new int[xSize * ySize * 6];

        int index;
        for(int y = 0; y < ySize; y++)
        {
            for(int x = 0; x < xSize; x++)
            {
                index = y * xSize + x;

                vertecies[4 * index] = new Vector3(x - 0.5f, y - 0.5f);
                vertecies[4 * index + 1] = new Vector3(x - 0.5f, y + 0.5f);
                vertecies[4 * index + 2] = new Vector3(x + 0.5f, y + 0.5f);
                vertecies[4 * index + 3] = new Vector3(x + 0.5f, y - 0.5f);

                uv[4 * index] = new Vector2(0, 0);
                uv[4 * index + 1] = new Vector2(0, 1);
                uv[4 * index + 2] = new Vector2(1, 1);
                uv[4 * index + 3] = new Vector2(1, 0);

                triangles[6 * index] = 4 * index;
                triangles[6 * index + 1] = 4 * index + 1;
                triangles[6 * index + 2] = 4 * index + 3;
                triangles[6 * index + 3] = 4 * index + 1;
                triangles[6 * index + 4] = 4 * index + 2;
                triangles[6 * index + 5] = 4 * index + 3;
            }
        }

        /*for(int x = 0; x < xSize; x++)
        {
            vertecies[4 * x] = new Vector3(x - 0.5f, -0.5f);
            vertecies[4 * x + 1] = new Vector3(x - 0.5f, +0.5f);
            vertecies[4 * x + 2] = new Vector3(x + 0.5f, +0.5f);
            vertecies[4 * x + 3] = new Vector3(x + 0.5f, -0.5f);

            uv[4 * x] = new Vector2(0, 0);
            uv[4 * x + 1] = new Vector2(0, 1);
            uv[4 * x + 2] = new Vector2(1, 1);
            uv[4 * x + 3] = new Vector2(1, 0);

            triangles[6 * x] = 4 * x;
            triangles[6 * x + 1] = 4 * x + 1;
            triangles[6 * x + 2] = 4 * x + 3;
            triangles[6 * x + 3] = 4 * x + 1;
            triangles[6 * x + 4] = 4 * x + 2;
            triangles[6 * x + 5] = 4 * x + 3;
        }*/

        mesh.vertices = vertecies;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    /*private void Generate()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.name = "Procedural Grid";

        // Since adjacent quads share verts,
        // we need one additional vert for each
        // dimensions
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                //uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }

        int[] triangles = new int[ySize * xSize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                ti += 6;
                vi++;
            }
        }

        
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(0, 1);
        uv[4] = new Vector2(1, 1);
        uv[5] = new Vector2(1, 1);

        uv[4] = new Vector2(1, 1);
        uv[5] = new Vector2(1, 1);
        uv[6] = new Vector2(1, 1);
        uv[7] = new Vector2(1, 0);
        for(int i = 0; i < uv.Length; i += 4)
        {
            uv[i] = new Vector2(0, 0);
            if(i + 1 < uv.Length)
                uv[i + 1] = new Vector2(0, 1);
            if(i + 2 < uv.Length)
                uv[i + 2] = new Vector2(1, 1);
            if(i + 3 < uv.Length)
                uv[i + 3] = new Vector2(1, 0);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
    }*/
}
