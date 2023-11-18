using UnityEngine;
using UnityEngine.UI;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject hitEffect;
    public Text textScore;
    public bool scoreBool = false;

    void Start()
    {
        Destroy(gameObject, 2f);
        GetComponent<Rigidbody>().AddForce(4000 * transform.forward);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject hitTemp = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hitTemp, 1);

        Texture2D bitmapTexture = LoadBitmap("path/to/your/image.png");

        if (bitmapTexture != null)
        {
            Material hitEffectMaterial = hitTemp.GetComponent<Renderer>().material;
            hitEffectMaterial.mainTexture = bitmapTexture;
        }

        if (collider.gameObject.tag == "enemy" || collider.gameObject.tag == "boss")
        {
            EnemyBehaviour enemy = collider.gameObject.GetComponent<EnemyBehaviour>();
            enemy.health--;
            enemy.slider.value = enemy.health;

            if (enemy.health == 0)
            {
                Destroy(collider.gameObject);
                TowerBehaviour.score++;
                textScore.text = "score: " + TowerBehaviour.score;
            }
        }

        Destroy(gameObject);
    }

    Texture2D LoadBitmap(string path)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }
}
