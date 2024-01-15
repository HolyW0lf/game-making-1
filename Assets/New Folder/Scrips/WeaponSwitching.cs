using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectedWeapon = 0;
    public TextMeshProUGUI ammoInfoText;

    void Start()
    {
        switching = new InputAction("SwitchWeapon", binding: "<Keyboard>/f");
        switching.Enable();

        // Check if Gun component is present before calling SelectWeapon
        Gun gun = FindObjectOfType<Gun>();
        if (gun != null)
        {
            SelectWeapon();
        }
        else
        {
            Debug.LogError("Gun component not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gun gun = FindObjectOfType<Gun>();
        if (gun == null)
        {
            Debug.LogError("Gun component not found in the scene.");
            return;
        }

        ammoInfoText.text = $"{gun.currentAmmo} / {gun.magazineAmmo}";
        if (ammoInfoText == null)
        {
            Debug.LogWarning("AmmoInfoText not assigned in the inspector.");
            return;
        }

        int previousSelected = selectedWeapon;

        // Handle switching weapons with the F key
        if (switching.triggered)
        {
            selectedWeapon++;
            if (selectedWeapon >= transform.childCount)
                selectedWeapon = 0;

            SelectWeapon();
        }

        // Only select the weapon if it's different from the previous one
        if (previousSelected != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }

        // Ensure selectedWeapon is within bounds
        if (selectedWeapon < transform.childCount)
        {
            transform.GetChild(selectedWeapon).gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Selected weapon index is out of bounds.");
        }
    }
}
