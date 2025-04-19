using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Widget.PopUp
{
    public class PopUpWidget : CanvasWidget
    {
        [SerializeField] private TextMeshProUGUI title;
        
        public TextMeshProUGUI Title => title;
    }
}