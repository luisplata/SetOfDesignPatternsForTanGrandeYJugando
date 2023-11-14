using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField] private InputCustom inputCustom;
    private void Update()
    {
        if (inputCustom.IsClicked())
        {
            Debug.Log("Clicked");
            var ray = Camera.main.ScreenPointToRay(inputCustom.GetPositionOfMouse());
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Debug.Log(hit.transform.name, hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<IClickable>().Shot();
            }
        }
    }
}