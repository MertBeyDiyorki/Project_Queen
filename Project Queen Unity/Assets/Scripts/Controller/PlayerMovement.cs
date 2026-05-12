using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Þerit Ayarlarý (Lane Settings)")]
    [Tooltip("Þeritler arasý X eksenindeki mesafe.")]
    [SerializeField] private float laneDistance = 3f;

    [Header("Hareket Ayarlarý (Movement Settings)")]
    [Tooltip("Þerit deðiþtirme animasyonunun süresi (saniye).")]
    [SerializeField] private float moveDuration = 0.25f;

    private int _currentLane = 0;
    private bool canMove = true;

    private void OnEnable()
    {
        PlayerInputHandler.OnMoveLeft += MoveLeft;
        PlayerInputHandler.OnMoveRight += MoveRight;
        PlayerCollision.OnGameOver += DisableMovement;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnMoveLeft -= MoveLeft;
        PlayerInputHandler.OnMoveRight -= MoveRight;
        PlayerCollision.OnGameOver -= DisableMovement;
    }

    private void DisableMovement()
    {
        canMove = false;
    }

    private void MoveLeft()
    {
        if (!canMove) return;

        ChangeLane(-1);
    }

    private void MoveRight()
    {
        if (!canMove) return;

        ChangeLane(1);
    }

    private void ChangeLane(int direction)
    {
        _currentLane = Mathf.Clamp(_currentLane + direction, -1, 1);
        float targetPositionX = _currentLane * laneDistance;
        transform.DOMoveX(targetPositionX, moveDuration).SetEase(Ease.OutQuad);
    }
}