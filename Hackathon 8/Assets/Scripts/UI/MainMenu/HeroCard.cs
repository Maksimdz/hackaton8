using UnityEngine;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour
{
    [SerializeField] private Image _heroImage;
    [SerializeField] private Button _chooseHeroBtn;
    [SerializeField] private GameObject _selectedFrame;

    public HeroData Hero => _heroData;
    
    private ChooseHeroScreen _chooseHeroScreen;
    private HeroData _heroData;
    
    private void Awake()
    {
        _chooseHeroScreen = GetComponentInParent<ChooseHeroScreen>();
        _chooseHeroBtn.onClick.AddListener(OnClick);
    }
    
    public void SetHero(HeroData hero)
    {
        _heroData = hero;
        _heroImage.sprite = hero.sprite;
        _selectedFrame.SetActive(false);
    }

    public void Select(bool select)
    {
        _selectedFrame.SetActive(select);
    }

    private void OnClick()
    {
        _chooseHeroScreen.SelectHeroCard(this);
    }
}
