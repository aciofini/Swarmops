using System;
using System.Collections.Generic;
using Swarmops.Basic.Enums;

namespace Swarmops.Basic.Types
{
    [Serializable]
    public class BasicObjectOptionalData
    {
        protected Dictionary<ObjectOptionalDataType, string> OptionalData;

        public BasicObjectOptionalData (ObjectType objectType, int objectId,
            Dictionary<ObjectOptionalDataType, string> initialData)
        {
            ObjectType = objectType;
            ObjectId = objectId;
            this.OptionalData = initialData;
        }

        public BasicObjectOptionalData (BasicObjectOptionalData original)
            : this (original.ObjectType, original.ObjectId, original.OptionalData)
        {
            // empty ctor
        }

        public ObjectType ObjectType { get; private set; }
        public int ObjectId { get; private set; }
    }
}