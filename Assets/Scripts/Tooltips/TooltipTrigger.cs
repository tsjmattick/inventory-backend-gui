using UnityEngine;
using UnityEngine.EventSystems;

namespace StarterAssets.Tooltips
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string content;
        public string header;
        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipSystem.Show(content, header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipSystem.Hide();
        }
    }
}