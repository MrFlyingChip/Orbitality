using System;
using System.Collections.Generic;

namespace Sources.Scripts.Core
{
    [Serializable]
    public class SaveData 
    {
        public List<PlanetData> planets = new List<PlanetData>();
    }
    
    [Serializable]
    public class PlanetData
    {
        public float smallRadius;
        public float bigRadius;
        public float currentAngle;
        public float rotationSpeed;
        public float changeAngleSpeed;
        public bool isPlayer;
        public float scale;
        public float currentHealth;
        public PlanetPreset preset = new PlanetPreset();
        public int materialIndex;
    }
}
