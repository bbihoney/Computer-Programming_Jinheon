using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("설정")]
    public GameObject cardPrefab;    // 아까 만든 카드 프리팹
    public Transform cardParent;    // 카드가 생성될 부모 (GridLayoutGroup 추천)
    public int pairCount = 10;      // 인스펙터에서 조절할 페어 개수

    [Header("카드 이미지 리소스")]
    public List<Sprite> frontImages; // 앞면에 들어갈 이미지 리스트

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateCards();
    }

    void GenerateCards()
    {
        // 1. 실제 필요한 이미지 리스트 생성 (페어만큼 2장씩)
        List<Sprite> selectedImages = new List<Sprite>();

        for (int i = 0; i < pairCount; i++)
        {
            // 준비된 이미지가 페어 수보다 적을 경우를 대비해 % 연산 사용
            Sprite image = frontImages[i % frontImages.Count];
            selectedImages.Add(image);
            selectedImages.Add(image); // 같은 이미지 두 장 추가
        }

        // 2. 리스트 셔플 (카드 섞기)
        for (int i = 0; i < selectedImages.Count; i++)
        {
            int randomIndex = Random.Range(0, selectedImages.Count);
            Sprite temp = selectedImages[i];
            selectedImages[i] = selectedImages[randomIndex];
            selectedImages[randomIndex] = temp;
        }

        // 3. 카드 생성 및 앞면 이미지 설정
        foreach (Sprite sprite in selectedImages)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);

            // 자식 오브젝트인 FrontImage를 찾아 이미지 할당
            Image frontImageComponent = newCard.transform.Find("FrontImage").GetComponent<Image>();
            frontImageComponent.sprite = sprite;

            // 초기 상태: 앞면 이미지를 비활성화 (카드 뒷면만 보이게 함)
            frontImageComponent.gameObject.SetActive(false);
        }

    }
}
