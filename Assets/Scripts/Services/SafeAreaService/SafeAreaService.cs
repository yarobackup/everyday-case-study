using UnityEngine;

namespace CompanyName.Services.SafeArea
{
    [DefaultExecutionOrder(-300)]
    public class SafeAreaService : MonoBehaviour
    {
        public Vector2 OffsetMax;
        public Vector2 OffsetMin;

        void Awake()
        {
            var safeArea = Screen.safeArea;
            var diff = Screen.height - safeArea.height;
            var topDiff = diff - safeArea.y;

            var scaleFactor = 3;
            OffsetMax = new Vector2(0, -topDiff / scaleFactor);
            OffsetMin = new Vector2(0, safeArea.y / scaleFactor);
        }
    }
}
