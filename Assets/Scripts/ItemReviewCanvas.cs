using UnityEngine;

public class ItemReviewCanvas : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        PlayerRayCast.DetectionEvent += PlayerRayCastOnDetectionEvent;
    }

    private void PlayerRayCastOnDetectionEvent(ShopItem item)
    {

        if (item is null || !item.IsActivated)
        {
            mainMenu.SetActive(false);
            return;
        }
        mainMenu.SetActive(true);
    }
}