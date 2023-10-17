using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AvatarReseau : NetworkBehaviour
{
    // Attributs qui servent à contenir les positions de mon avatar
    [SerializeField] private Transform _rootAvatar = default;  // XR Origin
    [SerializeField] private Transform _headAvatar = default;  // Main Camera
    [SerializeField] private Transform _leftHandAvatar = default;  // LeftHand
    [SerializeField] private Transform _rightHandAvatar = default;  // RightHand

    private PositionsVRRig _positionsVRRig;

    private void Start()
    {
        _positionsVRRig = FindObjectOfType<PositionsVRRig>();
    }

    private void Update()
    {
        if (IsOwner)
        {
            _rootAvatar.position = _positionsVRRig.Root.position;
            _rootAvatar.rotation = _positionsVRRig.Root.rotation;

            _headAvatar.position = _positionsVRRig.Head.position;
            _headAvatar.rotation = _positionsVRRig.Head.rotation;

            _leftHandAvatar.position = _positionsVRRig.LeftHand.position;
            _leftHandAvatar.rotation = _positionsVRRig.LeftHand.rotation;

            _rightHandAvatar.position = _positionsVRRig.RightHand.position;
            _rightHandAvatar.rotation = _positionsVRRig.RightHand.rotation;
        }
    }
}
