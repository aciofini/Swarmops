using System;
using Swarmops.Basic.Interfaces;

namespace Swarmops.Basic.Types
{
    public class BasicExpenseClaim : IHasIdentity
    {
        public readonly int GeographyId;
        public readonly int OrganizationId;

        /// <summary>
        ///     Normal constructor
        /// </summary>
        public BasicExpenseClaim (int expenseClaimId, int claimingPersonId, DateTime createdDateTime,
            bool open, bool attested, bool documented, bool claimed, int organizationId,
            int geographyId, int budgetId, DateTime expenseDate,
            string description, double preApprovedAmount, Int64 amountCents, bool repaid, bool keepSeparate)
        {
            ExpenseClaimId = expenseClaimId;
            ClaimingPersonId = claimingPersonId;
            CreatedDateTime = createdDateTime;
            Open = open;
            Attested = attested;
            Validated = documented;
            Claimed = claimed;
            this.OrganizationId = organizationId;
            this.GeographyId = geographyId;
            BudgetId = budgetId;
            ExpenseDate = expenseDate;
            Description = description;
            PreApprovedAmount = preApprovedAmount;
            AmountCents = amountCents;
            Repaid = repaid;
            KeepSeparate = keepSeparate;
        }

        /// <summary>
        ///     Copy constructor
        /// </summary>
        public BasicExpenseClaim (BasicExpenseClaim original)
            : this (original.Identity, original.ClaimingPersonId, original.CreatedDateTime,
                original.Open, original.Attested, original.Validated, original.Claimed,
                original.OrganizationId, original.GeographyId, original.BudgetId,
                original.ExpenseDate, original.Description,
                original.PreApprovedAmount, original.AmountCents, original.Repaid, original.KeepSeparate)
        {
        }


        public int ExpenseClaimId { get; private set; }
        public DateTime ExpenseDate { get; protected set; }
        public string Description { get; protected set; }
        public Int64 AmountCents { get; protected set; }
        public double PreApprovedAmount { get; private set; }
        public int ClaimingPersonId { get; private set; }
        public int BudgetId { get; protected set; }
        public DateTime CreatedDateTime { get; private set; }
        public bool Validated { get; protected set; }
        public bool Open { get; protected set; }
        public bool Attested { get; protected set; }
        public bool Repaid { get; protected set; }
        public bool KeepSeparate { get; protected set; }
        public bool Claimed { get; protected set; }


        // The fields below are not just encapsulated yet

        public int Identity
        {
            get { return ExpenseClaimId; }
        }
    }
}