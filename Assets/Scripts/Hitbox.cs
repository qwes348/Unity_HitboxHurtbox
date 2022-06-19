using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    }

    public LayerMask mask;
    //public bool useSphere = false;
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 offset = Vector3.zero;
    public float radius = 0.5f;

    public Color inactiveColor = Color.grey;
    public Color collisionOpenColor = Color.green;
    public Color collidingColor = Color.magenta;

    private ColliderState _state;
    private IHitboxResponder responder = null;

    private List<Collider> lastHitCollidersList;

    private void Awake()
    {
        lastHitCollidersList = new List<Collider>();

        //StartCheckingCollision();
    }

    private void CheckGizmoColor()
    {
        switch(_state)
        {
            case ColliderState.Closed:
                Gizmos.color = inactiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void StartCheckingCollision()
    {
        lastHitCollidersList.Clear();
        _state = ColliderState.Open;
    }

    public void StopCheckingCollision()
    {
        _state = ColliderState.Closed;
    }

    public void SetResponder(IHitboxResponder newResponder)
    {
        responder = newResponder;
    }

    private void FixedUpdate()
    {
        if (_state == ColliderState.Closed)
            return;

        Vector3 size = new Vector3(
            hitboxSize.x * transform.lossyScale.x,
            hitboxSize.y * transform.lossyScale.y,
            hitboxSize.z * transform.lossyScale.z
            );

        Collider[] colls = Physics.OverlapBox(transform.TransformPoint(offset), size * 0.5f, transform.rotation, mask);

        for (int i = 0; i < colls.Length; i++)
        {
            if (lastHitCollidersList.Contains(colls[i]))
                continue;

            responder?.CollisionedWith(colls[i]);
            lastHitCollidersList.Add(colls[i]);
        }

        _state = colls.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public interface IHitboxResponder
    {
        void CollisionedWith(Collider coll);
    }

    private void OnDrawGizmos()
    {
        CheckGizmoColor();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(offset, hitboxSize);
    }
}
