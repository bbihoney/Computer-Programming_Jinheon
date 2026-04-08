using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Cardgame : MonoBehaviour
{
    public List<card> cards = new List<card>();
    public card firstCard = null;
    public card secondCard = null;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartGame();
    }

    void StartGame()
    {
        List<int> pairNumbers = GeneratePairNumber(cards.Count);


        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].SetCardNumber(pairNumbers[i]);
        }


        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].isFront = false;
        }
    }
    
    void CheckCard()
    {
        if(firstCard.number == secondCard.number )
        {
            firstCard.ChangeColor(Color.red);
            secondCard.ChangeColor(Color.red);

            firstCard.ismatched = true;
            secondCard.ismatched = true;

            firstCard = null;
            secondCard = null;
        }
        else
        {
            Invoke("HideCard", 1.0f);
        }
    }

    public void OnClickCard(card card)
    {
        if(firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
        }

        if(firstCard !=  null && secondCard != null)
        {
            CheckCard();
        }
    }
    void HideCard()
    {
        firstCard.isFront = false;
        secondCard.isFront = false;

        firstCard = null;
        secondCard = null;
    }




    // Update is called once per frame
    void Update()
    {

    }

    List<int> GeneratePairNumber(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNumbers = new List<int>();

        for(int i = 0; i < pairCount;  ++i)
        {
            newCardNumbers.Add(i);
            newCardNumbers.Add(i);
        }


        for(int i = newCardNumbers.Count - 1; i> 0; i--)
        {
       
            int rnd = Random.Range(0, i + 1);
            int temp = newCardNumbers[i];
            newCardNumbers[i] = newCardNumbers[rnd];
            newCardNumbers[rnd] = temp;


        }

        return newCardNumbers;

    }
}
