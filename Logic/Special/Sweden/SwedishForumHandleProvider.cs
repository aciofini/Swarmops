using System;
using System.Diagnostics;
using Swarmops.Basic.Enums;
using Swarmops.Basic.Interfaces;
using Swarmops.Logic.Swarm;

namespace Swarmops.Logic.Special.Sweden
{
    [Serializable]
    public class SwedishForumHandleProvider : IHandleProvider
    {
        #region IHandleProvider Members

        public string GetPersonHandle (int personId)
        {
            Person person = Person.FromIdentity (personId);

            if (person.SwedishForumAccountId == 0)
            {
                return null;
            }

            return SwedishForumDatabase.GetDatabase().GetAccountName (person.SwedishForumAccountId);
        }


        public void SetPersonHandle (int personId, string newHandle)
        {
            /* -- unused in Swarmops

            Person person = Person.FromIdentity(personId);
            int newHandleAccountId = 0;

            if (newHandle != null && newHandle.Length > 0)
            {
                try
                {
                    newHandleAccountId = SwedishForumDatabase.GetDatabase().GetAccountId(newHandle);
                }
                catch (Exception)
                {
                    throw new HandleException(newHandle, HandleErrorType.HandleNotFound);
                }

                int[] members = SwarmDb.GetDatabaseForReading().GetObjectsByOptionalData(ObjectType.Person, ObjectOptionalDataType.ForumAccountId, "" + newHandleAccountId);
                if (members.Length > 1 || (members.Length == 1 && members[0] != personId))
                {
                    throw new HandleException(newHandle, HandleErrorType.HandleOccupied);
                }
            }
             
            int currentAccountId = person.SwedishForumAccountId;

            // Remove "party member" status from this account

            if (currentAccountId != 0 && currentAccountId != newHandleAccountId)
            {
                SwedishForumDatabase.GetDatabase().SetPartyNonmember(currentAccountId);
            }

            if (newHandleAccountId != currentAccountId)
            {
                person.SwedishForumAccountId = newHandleAccountId;

                if (newHandleAccountId != 0  && person.MemberOf(Organization.PPSEid) )
                {
                    SwedishForumDatabase.GetDatabase().SetPartyMember(newHandleAccountId);
                }
            } -- */

            // suppress warnings with trace messages

            Trace.WriteLine (personId);
            Trace.WriteLine (newHandle);
        }

        public HandleErrorType CanSetHandle (string newHandle)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        public int GetPersonByHandle (string handle)
        {
            throw new Exception ("The method or operation is not implemented.");
        }

        #endregion
    }
}