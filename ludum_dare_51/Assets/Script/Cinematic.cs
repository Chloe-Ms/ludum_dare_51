using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{

    public string level_1;
    public Image spriteRenderer;
    public Sprite newSprite;
    public Sprite newSprite2;
    public Sprite newSprite3;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<Image>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            ChangeSprite();
        }
    }

    
    public void ChangeSprite()
    {
        if (count == 0) {
            spriteRenderer.sprite = newSprite; 
            count += 1;
        }
        if (count == 1) {
            spriteRenderer.sprite = newSprite2; 
            count += 1;
        }
        if (count == 2) {
            spriteRenderer.sprite = newSprite3; 
            count += 1;
        }
        if (count == 3) {
            SceneManager.LoadScene(level_1);
        }
        else 
        {
            Debug.Log("Tu t'es planté connard");
        }
    }
}
