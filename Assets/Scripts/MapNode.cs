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
    // DONE Make these lists
    [SerializeField] ContactType _X_Pos_Contact;
    [SerializeField] ContactType _X_Neg_Contact;
    [SerializeField] ContactType _Z_Pos_Contact;
    [SerializeField] ContactType _Z_Neg_Contact;
    //[SerializeField] bool _Mirror_X, _Mirror_Y;

    [SerializeField] private GridRotation rot;

    public Quaternion creationRot
    {
        get
        {
            return Quaternion.identity;

            Quaternion quat = Quaternion.identity;
            // this could very well be wrong
            //quat.y = (int)rot;

            switch (rot)
            {   
                case GridRotation._0: quat.y = 0; 
                    break;
                case GridRotation._90: quat.y = 90;
                    break;
                case GridRotation._180: quat.y = 180;
                    break;
                case GridRotation._270: quat.y = 270;
                    break;
            }

            return quat;
        }
        private set { }
    }

    public ContactType X_Pos_Contact 
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

    public ContactType X_Neg_Contact
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

    public ContactType Z_Pos_Contact
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

    public ContactType Z_Neg_Contact
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
    public GameObject Create(Vector3 pos, Transform parent)
    {
        return Instantiate(this, pos, creationRot, parent).gameObject;
    }

    // TEMP
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
    public static bool CheckValidContact(ContactType n1C, ContactType n2C, MapNode n1, MapNode n2)
    {
        // Check edges
        if(n1C == ContactType.EDGE)
            return (n2C == ContactType.EDGE_FLIPPED);
/*        if (n1C == ContactType.EDGE && n2C == ContactType.EDGE_FLIPPED || 
            n2C == ContactType.EDGE && n1C == ContactType.EDGE_FLIPPED) 
        {
            return AreMapsFlipped(n1, n2);
        }*/

        return (n1C == n2C);

        // TODO this is definitely stllnot right
    }

    public static bool AreMapsFlipped(MapNode n1, MapNode n2)
    {
        if (n1.rot == GridRotation._0 && n2.rot == GridRotation._180)
            return true;
        if (n1.rot == GridRotation._90 && n2.rot == GridRotation._270)
            return true;
        if (n1.rot == GridRotation._180 && n2.rot == GridRotation._0)
            return true;
        if (n1.rot == GridRotation._270 && n2.rot == GridRotation._90)
            return true;

        return false;
    }
}