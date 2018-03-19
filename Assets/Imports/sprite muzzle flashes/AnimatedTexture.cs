using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private MeshRenderer rendererMy;
    //private float tempsDernierAppel;

    //private int counter = 3;
    //private int counterTemp;
    //private float cool;

    void Start()
    {
        rendererMy = GetComponent<MeshRenderer>();
    }

    void NextFrame()
    {
        // on stop l'animation après un certain nombre de fois
        //if (counter <= 0)
        //{
        //    rendererMy.enabled = false;
        //}
        //counter--;

        //
        //if (Time.time > tempsDernierAppel + cool && counter <= 0)
        //{
        //    LancerAnimation(counterTemp, cool);
        //    return;
        //}

        // Appelle le rendu de la frame suivante
        rendererMy.sharedMaterial.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }

    public void LancerAnimation(/*int count, float cooldown*/)
    {

        CancelInvoke();
        //cool = cooldown;
        //tempsDernierAppel = Time.time;
        //counterTemp = count;
        //counter = count;
        rendererMy.enabled = true;
        NextFrame();
        InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    //Set counter(le nombre d'images qu'on va afficher)
    //public void SetCounter(int count)
    //{
    //    this.counter = count;
    //}

    public void Cancel()
    {

        rendererMy.enabled = false;
        CancelInvoke();
        
    }
}