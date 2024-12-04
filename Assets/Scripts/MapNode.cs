using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains list of contacts
/// </summary>
public class MapNode : MonoBehaviour
{
    [SerializeField] public ContactType X_Pos_Contact;
    [SerializeField] public ContactType X_Neg_Contact;
    [SerializeField] public ContactType Z_Pos_Contact;
    [SerializeField] public ContactType Z_Neg_Contact;

    enum Rotation
    {
        _0 = 0,
        _90, 
        _180, 
        _270
    }

    [SerializeField] Rotation rot;

}