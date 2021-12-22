using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClampCollect
{
    void DoClampCollect(Vector3 position);
    void DoClampCollectExit(Vector3 position);
}
