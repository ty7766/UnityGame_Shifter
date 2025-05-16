using UnityEngine;

public class ShiftController : MonoBehaviour
{
    public Shift shift;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            shift.UseShift();
    }
}
