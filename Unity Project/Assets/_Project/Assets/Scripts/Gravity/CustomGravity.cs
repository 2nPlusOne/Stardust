using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public static class CustomGravity
    {
        static List<GravitySource> _gravitySources = new();

        public static Vector3 GetGravity(Vector3 position)
        {
            var g = Vector3.zero;
            foreach (var gravitySource in _gravitySources)
            {
                g += gravitySource.GetGravity(position);
            }
            return g;
        }

        public static Vector3 GetGravity(Vector3 position, out Vector3 upAxis)
        {
            var g = Vector3.zero;
            foreach (var gravitySource in _gravitySources)
            {
                g += gravitySource.GetGravity(position);
            }
            upAxis = -g.normalized;
            return g;
        }
        
        public static void AddGravitySource(GravitySource gravitySource)
        {
            Debug.Assert(!_gravitySources.Contains(gravitySource), "Gravity source already added", gravitySource);
            if (_gravitySources.Contains(gravitySource)) return;
            _gravitySources.Add(gravitySource);
        }
        
        public static void RemoveGravitySource(GravitySource gravitySource)
        {
            Debug.Assert(_gravitySources.Contains(gravitySource), "Gravity source not found", gravitySource);
            _gravitySources.Remove(gravitySource);
        }
    }
}
