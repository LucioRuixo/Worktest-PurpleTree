using UnityEngine;

namespace Worktest_PurpleTree.Gameplay
{
    public class GameplayCamera : MonoBehaviour
    {
        [Header("Camera Properties")]
        [SerializeField] Transform anchor;
        [SerializeField] Vector2 offset;
        [Space]
        [SerializeField] bool freezePositionX = false;
        [SerializeField] bool freezePositionY = false;
        [Space]
        [SerializeField] bool useFreeZone = false;
        [SerializeField] MinMax<float> xFreeZone;
        [SerializeField] MinMax<float> yFreeZone;

        Vector3 initialPosition;
        
        void Awake() => initialPosition = transform.position;

        void Update() => MoveWithAnchor();

        void MoveWithAnchor()
        {
            float x = freezePositionX ? initialPosition.x : TranslateInAxis(transform.position.x, anchor.position.x, offset.x, xFreeZone);
            float y = freezePositionY ? initialPosition.y : TranslateInAxis(transform.position.y, anchor.position.y, offset.y, yFreeZone);

            transform.position = new Vector3(x, y, initialPosition.z);
        }

        float TranslateInAxis(float position, float anchorPosition, float offset, MinMax<float> freeZone)
        {
            if (useFreeZone)
            {
                if (anchorPosition - position < freeZone.Min) return anchorPosition - freeZone.Min;
                if (anchorPosition - position > freeZone.Max) return anchorPosition - freeZone.Max;

                return position;
            }

            return anchorPosition + offset;
        }
    }
}