﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_NegateSpeed : MonoBehaviour
{
    public Cinemachine.CinemachineDollyCart CMA;
    public Cinemachine.CinemachineDollyCart CMB;
    // Start is called before the first frame update

    public void overrideSpeed(float speed)
    {
        CMA.m_Speed = speed;
        CMB.m_Speed = speed;
    }
}
