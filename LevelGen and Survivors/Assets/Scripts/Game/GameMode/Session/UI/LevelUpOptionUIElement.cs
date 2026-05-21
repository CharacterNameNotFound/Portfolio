using Game.GameMode.Session.Game.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameMode.Session.UI
{
    public class LevelUpOptionUIElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        public void Show(IItem item)
        {
            _image.sprite = item.GetItemImage();
            _name.text = item.GetItemName();
            _description.text = item.GetLevel() == 0 ? item.GetObtainDescription() : item.GetLevelUpDescription();
        }
            

    }
}