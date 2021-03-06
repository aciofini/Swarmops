using Swarmops.Basic.Types;
using Swarmops.Database;
using Swarmops.Logic.Structure;

namespace Swarmops.Logic.Swarm
{
    public class VolunteerRole : BasicVolunteerRole
    {
        private VolunteerRole (BasicVolunteerRole basic) :
            base (basic)
        {
        }

        public Geography Geography
        {
            get { return Geography.FromIdentity (base.GeographyId); } // Cache later if necessary
        }

        public static VolunteerRole FromBasic (BasicVolunteerRole basic)
        {
            return new VolunteerRole (basic);
        }

        public static VolunteerRole FromIdentity (int identity)
        {
            return FromBasic (SwarmDb.GetDatabaseForReading().GetVolunteerRole (identity));
        }

        public void Close (bool wasAssigned)
        {
            // TODO
        }
    }
}