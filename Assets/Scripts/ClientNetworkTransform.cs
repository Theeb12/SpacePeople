using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

// Just an override class on NetworkTransform to give clients full access
// if no override, they won't be able to move objects on the host
public class ClientNetworkTransform : NetworkTransform {
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

}
