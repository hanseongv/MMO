using System;
using UnityEngine;

public class CharacterIdleState : MonoBehaviour, ICharacterState
{
    private Character _character;

    public void Handle(Character character)
    {
        if (!_character)
            _character = character;
        character.CurrentSpeed = 0;
        _character._animator.SetBool("isWalk", false);
        _character._animator.SetBool("isRun", false);
    }
}
public class MyPlayerMoveState : MonoBehaviour, ICharacterState
{
    private Character _character;

    public void Handle(Character character)
    {
        if (!_character)
            _character = character;
        _character.CurrentSpeed = _character.moveSpeed;

        _character._animator.SetBool("isWalk", true);
    }

    private void Update()
    {
        if (_character)
        {
            if (_character.CurrentSpeed > 0)
            {
                Move();
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Move()
    {
        var controllerValue = GameManager.Instance.controlManager.currentController.GetControllerValue();
        // 카메라가 바라보는 방향
        var cameraDirection = GameManager.Instance.cameraManager.GetCameraDirection();

        var speed = controllerValue.value;

        float y = Quaternion.LookRotation(cameraDirection).eulerAngles.y;

        // 회전
        _character.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
        // 이동
        _character.transform.Translate(
            Vector3.forward * (_character.CurrentSpeed *
                               speed *
                               Time.deltaTime));

        var isRun = 0.5f < speed;
        _character._animator.SetBool("isRun", isRun);
    }
}
public class CharacterMoveState : MonoBehaviour, ICharacterState
{
    private Character _character;

    public void Handle(Character character)
    {
        if (!_character)
            _character = character;
        _character.CurrentSpeed = _character.moveSpeed;

        _character._animator.SetBool("isWalk", true);
    }

    private void Update()
    {
        if (_character)
        {
            if (_character.CurrentSpeed > 0)
            {
                Move();
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Move()
    {
        var controllerValue = GameManager.Instance.controlManager.currentController.GetControllerValue();
        // 카메라가 바라보는 방향
        var cameraDirection = GameManager.Instance.cameraManager.GetCameraDirection();

        var speed = controllerValue.value;

        float y = Quaternion.LookRotation(cameraDirection).eulerAngles.y;

        // 회전
        _character.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
        // 이동
        _character.transform.Translate(
            Vector3.forward * (_character.CurrentSpeed *
                               speed *
                               Time.deltaTime));

        var isRun = 0.5f < speed;
        _character._animator.SetBool("isRun", isRun);
    }
}