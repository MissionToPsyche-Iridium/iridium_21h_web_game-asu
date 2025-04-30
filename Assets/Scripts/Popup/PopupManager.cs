using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popupPrefabs; ///list of different popup prefabs
    [SerializeField] private PlayerController player;
    [SerializeField] private BGScroll bgScroll;
    [SerializeField] private Bar bar;
    [SerializeField] private TimerScript timer;

    private GameObject currentActivePopup;

    //show a random popup
    public void ShowRandomPopup()
    {
        //first deactivate any currently active popup
        if (currentActivePopup != null)
        {
            Popup currentPopupScript = currentActivePopup.GetComponent<Popup>();
            if (currentPopupScript != null)
            {
                currentPopupScript.Close();
            }
        }

        //choose a random popup from the list
        if (popupPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, popupPrefabs.Count);
            GameObject selectedPopup = popupPrefabs[randomIndex];

            //set all popups inactive first
            foreach (GameObject popup in popupPrefabs)
            {
                popup.SetActive(false);
            }

            //get the popup script
            Popup popupScript = selectedPopup.GetComponent<Popup>();
            if (popupScript != null)
            {
                //make sure references are set
                popupScript.Player = player;
                popupScript.BGScroll = bgScroll;
                popupScript.Bar = bar;
                popupScript.timer = timer;

                //setup and activate the popup
                popupScript.Setup();
                currentActivePopup = selectedPopup;
            }
        }
    }
}