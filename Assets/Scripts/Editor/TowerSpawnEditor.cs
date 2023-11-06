using Tower;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TowerSpawner))]
    public class TowerSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(TowerSpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 0.3f);
        }
    }
}