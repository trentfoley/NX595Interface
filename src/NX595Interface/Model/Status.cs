namespace NX595Interface.Model
{
    public class Status
    {
        public string ArmType { get; set; }

        public string SystemStatus { get; set; }

        public bool IsChimeEnabled { get; set; }

        public Zone[] Zones { get; set; }
    }
}
