using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    // ÆË¾÷Ã¢ RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup> ();
    }

    /// <summary>
    /// Panel Ç¥½Ã
    /// </summary>

    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1f,0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// Panel ¼û±â±â
    /// </summary>

    public void Hide()
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }
}