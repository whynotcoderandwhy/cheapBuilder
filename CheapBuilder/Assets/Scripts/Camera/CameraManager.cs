using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Camera manager is to have a list of behaviors
/// </summary>
[System.Serializable]
public class CameraManager : MonoBehaviour, ISerializationCallbackReceiver
{
    /// <summary>
    /// used to determine both camera behaviour and player motion behaviour
    /// </summary>
    public enum CameraState
    {
        Abzu,
    }

    [SerializeField]
    protected GameObject FollowObject;
    [SerializeField]
    protected GameObject CameraObject;
    [SerializeField]
    protected CameraState _currentState;
    public CameraState CurrentState { get { return _currentState; } }

    [SerializeField]
    protected CameraBehavoir currentBehavoir;

    [SerializeField]
    protected CameraBehavoir m_abzu = new AbzuCameraBehaviour(new OrbitPoint(0f, -90f, 10f, 180f, -180f, 180f, -180f), new OrbitPoint(0f, 110f, 10f, 180f, -180f, 360f, -360f));


    Dictionary<CameraState, CameraBehavoir> StateHolder; 

    public void OnBeforeSerialize()
    {
        SwitchState(_currentState);
    }

    public Vector3 LookAtWorldTransform { get { return FollowObject.transform.position +  currentBehavoir.GetCameraLookatPos; } }
    public Vector3 CameraPos { get { return FollowObject.transform.position + currentBehavoir.GetCameraPos; } }


    protected void InitStateHolderIfNeeded()
    {
        if (StateHolder != null)
            return;

        //Setup Dictionary while setting their values
        StateHolder = new Dictionary<CameraState, CameraBehavoir>()
        {
            {CameraState.Abzu, m_abzu.SetCamBehavObjects(FollowObject, CameraObject) },
        };
    }


    public void OnAfterDeserialize() { }

    protected void SwitchState(CameraState newState)
    {
        InitStateHolderIfNeeded();

        _currentState = newState;
        if (!StateHolder.TryGetValue(_currentState, out currentBehavoir))
            throw new System.InvalidOperationException("state not found in StateHolder ");
    }


    public void Awake()
    {
        InitStateHolderIfNeeded();
        SwitchState(CameraState.Abzu);
    }

    public void Update()
    {
        float xValue = -Input.GetAxis("Mouse X");
        float yValue = -Input.GetAxis("Mouse Y");
        float zValue = Input.GetAxis("Mouse ScrollWheel") * -100;
        currentBehavoir.ResolveInput(xValue, yValue, zValue, zValue);

    }

}
