using UnityEngine;

namespace Spotnose.Stardust
{
    public static class Utilities
    {
        public static Vector3 GenerateRandomPointOutsideCircle(Vector3 centerPos, float minRadius, float maxRadius)
        {
            var randomAngle = Random.Range(0f, 2f * Mathf.PI);
            var randomRadius = Random.Range(minRadius, maxRadius);
            var randomPoint = new Vector3(
                centerPos.x + randomRadius * Mathf.Cos(randomAngle),
                centerPos.y + randomRadius * Mathf.Sin(randomAngle),
                0f
            );
            return randomPoint;
        }
        
        public static Vector3 GenerateRandomVectorOfMagnitude(float magnitude)
        {
            var randomAngle = Random.Range(0f, 2f * Mathf.PI);
            var randomPoint = new Vector3(
                magnitude * Mathf.Cos(randomAngle),
                magnitude * Mathf.Sin(randomAngle),
                0f
            );
            return randomPoint;
        }
    }
}