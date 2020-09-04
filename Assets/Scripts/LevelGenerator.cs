
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColourToPrefab[] colourMappings;
    
    // Start is called before the first frame update
    void Start()
    {
        /*Vector2 position = new Vector2(0, 0);
        Instantiate(colourMappings[0].prefab, position, Quaternion.identity, transform);*/
        
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColour = map.GetPixel(x, y);

        // Ignore if transparent
        if (pixelColour.a == 0)
        {
            return;
        }


        foreach (ColourToPrefab colourMapping in colourMappings)
        {

            if (colourMapping.color.Equals(pixelColour))
            {
                // Debug.Log(pixelColour);
                Vector2 position = new Vector2(x - 9.5f, y - 6.5f);
                
                if (Random.Range(0, 10) == 7)
                    Instantiate(colourMapping.prefab, position, Quaternion.identity, transform);


            }
        }
    }

}
