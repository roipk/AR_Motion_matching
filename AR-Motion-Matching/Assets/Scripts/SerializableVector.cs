﻿using UnityEngine;
using System;
using System.Collections;

/// <summary>
public struct SerializableVector3
{

    /// <summary>

    /// <summary>

    /// <summary>

    /// <summary>
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    /// <summary>
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// <summary>
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}