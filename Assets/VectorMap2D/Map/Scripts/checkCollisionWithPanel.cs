using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollisionWithPanel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("panel of the statistic dialog")]
    public GameObject goodJobDialog;

    [SerializeField]
    [Tooltip("panel of the statistic dialog")]
    public GameObject statisticDialog;

    [SerializeField]
    [Tooltip("panel of where all the buttons are placed")]
    public GameObject panel;

    [SerializeField]
    [Tooltip("panel of where all the buttons are placed")]
    public GameObject countryInfo;

    [SerializeField]
    [Tooltip("panel of the dialog")]
    public GameObject dialog;
    public bool checkCollision(Vector3 position)
    {
        var rectTransform = panel.GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(position);
        if (rectTransform.rect.Contains(localMousePosition))
        {
            return true;
        }

        if (countryInfo.gameObject.active)
        {
            var rectTransform2 = countryInfo.GetComponent<RectTransform>();
            Vector2 localMousePosition2 = rectTransform2.InverseTransformPoint(position);
            if (rectTransform2.rect.Contains(localMousePosition2))
            {
                return true;
            }
        }

        if (dialog.gameObject.active)
        {
            var rectTransform3 = dialog.GetComponent<RectTransform>();
            Vector2 localMousePosition3 = rectTransform3.InverseTransformPoint(position);
            if (rectTransform3.rect.Contains(localMousePosition3))
            {
                return true;
            }
        }

        if (statisticDialog.gameObject.active)
        {
            var rectTransform4 = statisticDialog.GetComponent<RectTransform>();
            Vector2 localMousePosition4 = rectTransform4.InverseTransformPoint(position);
            if (rectTransform4.rect.Contains(localMousePosition4))
            {
                return true;
            }
        }



        if (goodJobDialog.gameObject.active)
        {
            var rectTransform5 = goodJobDialog.GetComponent<RectTransform>();
            Vector2 localMousePosition5 = rectTransform5.InverseTransformPoint(position);
            if (rectTransform5.rect.Contains(localMousePosition5))
            {
                return true;
            }
        }

        return false;
    }
}
