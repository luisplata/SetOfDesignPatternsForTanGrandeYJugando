using SL;
using TMPro;
using UnityEngine;

public class PointToPlayer : MonoBehaviour, IPointsToPlayer
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private int _points;
    private void Start()
    {
        ServiceLocator.Instance.RegisterService<IPointsToPlayer>(this);
        UpdatePoints(0);
    }

    public void UpdatePoints(int points)
    {
        _points += points;
        textMeshProUGUI.text = $"Points: {_points}";
    }
}

public interface IPointsToPlayer
{
    void UpdatePoints(int points);
}