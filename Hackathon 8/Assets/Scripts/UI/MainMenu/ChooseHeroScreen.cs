using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHeroScreen : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private HeroCard _heroCard;
    [SerializeField] private Button _nextButton;

    private List<HeroCard> _heroCards = new List<HeroCard>();
    private HeroCard _selectedHeroCard;
    private Action<HeroData> _onChosenHero;

    private void Awake()
    {
        _nextButton.onClick.AddListener(OnClickNext);
    }

    public void Show(HeroData[] heroes, Action<HeroData> onChosenHero)
    {
        _onChosenHero = onChosenHero;
        
        var heroIndex = 0;
        for (; heroIndex < heroes.Length; heroIndex++)
        {
            HeroCard card = null;
            if (_heroCards.Count <= heroIndex)
                card = Instantiate(_heroCard, _container);
            else
                card = _heroCards[heroIndex];
            
            card.gameObject.SetActive(true);
            card.SetHero(heroes[heroIndex]);
        }
        
        for (; heroIndex < _heroCards.Count; heroIndex++)
            _heroCards[heroIndex].gameObject.SetActive(false);

        _nextButton.interactable = false;
        gameObject.SetActive(true);
    }

    public void SelectHeroCard(HeroCard heroCard)
    {
        if (_selectedHeroCard != null)
            _selectedHeroCard.Select(false);

        _selectedHeroCard = heroCard;
        _selectedHeroCard.Select(true);
        _nextButton.interactable = true;
    }

    private void OnClickNext()
    {
        _onChosenHero?.Invoke(_selectedHeroCard != null ? _selectedHeroCard.Hero : null);
    }
}