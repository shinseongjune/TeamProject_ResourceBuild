using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordConverter
{
    // ��ī��Ʈ ��ǥ�踦 ���� ��ǥ��� ��ȯ
    public static Vector3 CartesianToSpherical(Vector3 coord)
    {
        float r = Mathf.Sqrt(coord.x * coord.x + coord.y * coord.y + coord.z * coord.z);
        return new Vector3(
            r,
            Mathf.Atan2(coord.x, coord.z), // ��Ÿ (��)
            Mathf.Acos(coord.y / r)        // ���� (��)
        );
    }

    // ���� ��ǥ�踦 ��ī��Ʈ ��ǥ��� ��ȯ
    public static Vector3 SphericalToCartesian(Vector3 coord)
    {
        return new Vector3(
            coord.x * Mathf.Sin(coord.z) * Mathf.Sin(coord.y), // x ��ǥ
            coord.x * Mathf.Cos(coord.z),                      // y ��ǥ
            coord.x * Mathf.Sin(coord.z) * Mathf.Cos(coord.y)  // z ��ǥ
        );
    }
}
