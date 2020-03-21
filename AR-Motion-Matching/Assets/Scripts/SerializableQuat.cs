﻿using UnityEngine;
using System;
using System.Collections;

/// <summary>
public struct SerializableQuaternion
{

    /// <summary>

    /// <summary>

    /// <summary>

    /// <summary>

    /// <summary>
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }

    /// <summary>
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    /// <summary>
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    /// <summary>
    {
        return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}