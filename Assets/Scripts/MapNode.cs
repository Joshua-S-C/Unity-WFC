using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GridRotation
{
    _0 = 0,
    _90 = 90, 
    _180 = 180, 
    _270 = 270
}

/// <summary>
/// Contains list of contacts
/// </summary>
public class MapNode : MonoBehaviour
{
    // TODO Make these lists
    [SerializeField] ContactType[] _X_Pos_Contact;
    [SerializeField] ContactType[] _X_Neg_Contact;
    [SerializeField] ContactType[] _Z_Pos_Contact;
    [SerializeField] ContactType[] _Z_Neg_Contact;

    public GridRotation rot;

    public Quaternion creationRot
    {
        get
        {
            Quaternion quat = Quaternion.identity;
            // this could very well be wrong
            quat.y = (float)rot;
            return quat;
        }
        private set { }
    }

    public ContactType[] X_Pos_Contact 
    { 
        get {
            switch (rot) {
                case GridRotation._0: return _X_Pos_Contact;
                case GridRotation._90: return _Z_Neg_Contact;
                case GridRotation._180: return _X_Neg_Contact;
                case GridRotation._270: return _Z_Pos_Contact;
                default: return _X_Pos_Contact;
            }
        } 
        private set { } 
    }

    public ContactType[] X_Neg_Contact
    {
        get
        {
            switch (rot)
            {
                case GridRotation._180: return _X_Pos_Contact;
                case GridRotation._270: return _Z_Neg_Contact;
                case GridRotation._0: return _X_Neg_Contact;
                case GridRotation._90: return _Z_Pos_Contact;
                default: return _X_Pos_Contact;
            }
        }
        private set { }
    }

    public ContactType[] Z_Pos_Contact
    {
        get
        {
            switch (rot)
            {
                case GridRotation._90: return _X_Pos_Contact;
                case GridRotation._180: return _Z_Neg_Contact;
                case GridRotation._270: return _X_Neg_Contact;
                case GridRotation._0: return _Z_Pos_Contact;
                default: return _X_Pos_Contact;
            }
        }
        private set { }
    }

    public ContactType[] Z_Neg_Contact
    {
        get
        {
            switch (rot)
            {
                case GridRotation._270: return _X_Pos_Contact;
                case GridRotation._0: return _Z_Neg_Contact;
                case GridRotation._90: return _X_Neg_Contact;
                case GridRotation._180: return _Z_Pos_Contact;
                default: return _X_Pos_Contact;
            }
        }
        private set { }
    }

    /// <summary>
    /// Creates the game object with appropriate params. To be called in map
    /// </summary>
    /// <param name="pos">Map grid position</param>
    /// <param name="parent">The map's transform</param>
    /// <returns>The instantiated object</returns>
    public GameObject Create(Vector3 pos, Transform parent, GridRotation rot)
    {
        this.rot = rot;
        return Instantiate(this, pos, creationRot, parent).gameObject;
    }

    /// <summary>
    /// Checks valid pairs with respect to direction
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static bool CheckValiContact(ContactType[] n1C, ContactType[] n2C)
    {
        foreach (ContactType c in n2C)
            if (n1C.ToList().Contains(c))
                return true;
        return false;
    }
}