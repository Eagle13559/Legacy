using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Projectile
{
  void shootProjectile(Vector2 velocityVector, bool flipAnimation);
}