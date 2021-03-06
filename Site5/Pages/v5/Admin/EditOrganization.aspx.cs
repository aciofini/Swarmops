﻿using System;
using System.Web.Services;
using System.Web.UI.WebControls;
using Swarmops.Basic.Enums;
using Swarmops.Logic.Financial;
using Swarmops.Logic.Security;
using Swarmops.Logic.Structure;
using Swarmops.Logic.Swarm;

namespace Swarmops.Frontend.Pages.v5.Admin
{
    public partial class EditOrganization : PageV5Base
    {
        protected void Page_Load (object sender, EventArgs e)
        {
            PageIcon = "iconshock-box-cog";
            PageTitle = Resources.Pages.Admin.EditOrganization_PageTitle;
            InfoBoxLiteral = Resources.Pages.Admin.EditOrganization_Info;
            PageAccessRequired = new Access (CurrentOrganization, AccessAspect.Administration, AccessType.Write);

            if (!Page.IsPostBack)
            {
                Localize();
            }


            EasyUIControlsUsed = EasyUIControl.Tabs;
            IncludedControlsUsed = IncludedControl.FileUpload | IncludedControl.SwitchButton |
                                   IncludedControl.JsonParameters;
        }

        private void Localize()
        {
            string participants = Participant.Localized (CurrentOrganization.RegularLabel, TitleVariant.Plural);
            string participantship = Participant.Localized (CurrentOrganization.RegularLabel, TitleVariant.Ship);

            this.LabelParticipationEntry.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_ParticipationBeginsWhen, participants);
            this.LabelParticipationOrg.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_ParticipationBeginsOrg, participants);
            this.LabelParticipationDuration.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_ParticipationDuration, participantship);
            this.LabelParticipationChurn.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_ParticipationChurn, participantship);
            this.LabelRenewalCost.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_RenewalsCost,
                    CurrentOrganization.Currency.DisplayCode);
            this.LabelParticipationCost.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_ParticipationCost,
                    participantship, CurrentOrganization.Currency.DisplayCode);

            this.LabelRenewalsAffect.Text = Resources.Pages.Admin.EditOrganization_RenewalsAffect;
            this.LabelRenewalDateEffect.Text = Resources.Pages.Admin.EditOrganization_RenewalDateEffect;
            this.LabelRenewalReminder.Text = Resources.Pages.Admin.EditOrganization_RenewalReminders;
            this.LabelMemberNumber.Text =
                String.Format (Resources.Pages.Admin.EditOrganization_MemberNumberStyle, participantship);

            this.DropMembersWhen.Items.Clear();
            this.DropMembersWhen.Items.Add (new ListItem ("Application submitted", "Application"));
            this.DropMembersWhen.Items.Add (new ListItem ("Application approved", "ApplicationApproval"));
            this.DropMembersWhen.Items.Add (new ListItem ("Application submitted + paid", "ApplicationPayment"));
            this.DropMembersWhen.Items.Add (new ListItem ("Application paid + approved", "ApplicationPaymentApproval"));
            this.DropMembersWhen.Items.Add (new ListItem ("Invited and accepted", "InvitationAcceptance"));
            this.DropMembersWhen.Items.Add (new ListItem ("Invited and paid", "InvitationPayment"));
            this.DropMembersWhen.Items.Add (new ListItem ("Manual add only", "Manual"));

            this.DropMembersWhere.Items.Clear();
            this.DropMembersWhere.Items.Add (new ListItem ("Root organization only", "Root"));
            this.DropMembersWhere.Items.Add (new ListItem ("Most local org only", "Local"));
            this.DropMembersWhere.Items.Add (new ListItem ("Root and most local org", "RootLocal"));
            this.DropMembersWhere.Items.Add (new ListItem ("All applicable organizations", "All"));

            this.DropMembershipDuration.Items.Clear();
            this.DropMembershipDuration.Items.Add (new ListItem ("One month", "OneMonth"));
            this.DropMembershipDuration.Items.Add (new ListItem ("One year", "OneYear"));
            this.DropMembershipDuration.Items.Add (new ListItem ("Two years", "TwoYears"));
            this.DropMembershipDuration.Items.Add (new ListItem ("Five years", "FiveYears"));
            this.DropMembershipDuration.Items.Add (new ListItem ("Forever", "Forever"));
            this.DropMembershipDuration.SelectedValue = "Year";

            this.DropMembersChurn.Items.Clear();
            this.DropMembersChurn.Items.Add (new ListItem ("Expiry date reached", "Expiry"));
            this.DropMembersChurn.Items.Add (new ListItem ("Not paid final reminder", "NotPaid"));
            this.DropMembersChurn.Items.Add (new ListItem ("Never", "Never"));

            this.DropRenewalDateEffect.Items.Clear();
            this.DropRenewalDateEffect.Items.Add (new ListItem ("Date of renewal", "RenewalDate"));
            this.DropRenewalDateEffect.Items.Add (new ListItem ("Previous expiry", "FromExpiry"));

            this.DropRenewalsAffect.Items.Clear();
            this.DropRenewalsAffect.Items.Add (new ListItem ("All related organizations", "All"));
            this.DropRenewalsAffect.Items.Add (new ListItem ("One organization at a time", "One"));

            this.DropRenewalReminder.Items.Clear();
            this.DropRenewalReminder.Items.Add (new ListItem ("30, 14, 7, 1 days before", "Standard"));
            this.DropRenewalReminder.Items.Add (new ListItem ("Never", "Never"));

            this.DropMemberNumber.Items.Clear();
            this.DropMemberNumber.Items.Add (new ListItem ("Global for installation", "Global"));
            this.DropMemberNumber.Items.Add (new ListItem ("Local for each organzation", "Local"));
        }

        [WebMethod]
        public static InitialOrgData GetInitialData()
        {
            InitialOrgData result = new InitialOrgData();
            AuthenticationData authData = GetAuthenticationDataAndCulture();
            Organization org = authData.CurrentOrganization;

            if (org == null || authData.CurrentUser == null)
            {
                return result; // just... don't
            }

            result.AccountBitcoinCold = (org.FinancialAccounts.AssetsBitcoinCold != null &&
                                         org.FinancialAccounts.AssetsBitcoinCold.Active);
            result.AccountBitcoinHot = (org.FinancialAccounts.AssetsBitcoinHot != null &&
                                        org.FinancialAccounts.AssetsBitcoinHot.Active);
            result.AccountPaypal = (org.FinancialAccounts.AssetsPaypal != null &&
                                    org.FinancialAccounts.AssetsPaypal.Active);
            result.AccountsForex = (org.FinancialAccounts.IncomeCurrencyFluctuations != null &&
                                    org.FinancialAccounts.IncomeCurrencyFluctuations.Active);
            result.AccountsVat = (org.FinancialAccounts.AssetsVatInbound != null &&
                                  org.FinancialAccounts.AssetsVatInbound.Active);

            // TODO: Add all the other fields

            return result;
        }


        [WebMethod]
        public static CallResult SwitchToggled (string switchName, bool switchValue)
        {
            AuthenticationData authData = GetAuthenticationDataAndCulture();

            if (authData.CurrentOrganization == null || authData.CurrentUser == null)
            {
                return null; // just don't... don't anything, actually
            }

            CallResult result = new CallResult();
            result.Success = true;

            bool bitcoinNative = (authData.CurrentOrganization.Currency.Code == "BTC");

            FinancialAccounts workAccounts = new FinancialAccounts();

            switch (switchName)
            {
                case "BitcoinCold":

                    if (switchValue && !bitcoinNative)
                    {
                        result.RequireForex = true;
                    }

                    FinancialAccount coldAccount = authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinCold;
                    if (coldAccount == null)
                    {
                        coldAccount = FinancialAccount.Create (authData.CurrentOrganization, "Bitcoin Assets Cold",
                            FinancialAccountType.Asset, null);
                        FinancialAccount.Create (authData.CurrentOrganization, "Cold Address 1",
                            FinancialAccountType.Asset, coldAccount);
                        FinancialAccount.Create (authData.CurrentOrganization, "Cold Address 2 (rename these)",
                            FinancialAccountType.Asset, coldAccount);
                        FinancialAccount.Create (authData.CurrentOrganization, "Cold Address... etc",
                            FinancialAccountType.Asset, coldAccount);

                        authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinCold = coldAccount;

                        result.DisplayMessage =
                            "Bitcoin cold accounts were created. Edit names and addresses in Account Plan."; // LOC
                    }
                    else
                    {
                        workAccounts.Add (coldAccount);
                    }
                    break;
                case "BitcoinHot":
                    if (switchValue && !bitcoinNative)
                    {
                        result.RequireForex = true;
                    }

                    FinancialAccount hotAccount = authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinHot;
                    if (hotAccount == null)
                    {
                        authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinHot =
                            FinancialAccount.Create (authData.CurrentOrganization, "Bitcoin Wallet Hot",
                                FinancialAccountType.Asset, null);

                        result.DisplayMessage =
                            "Bitcoin hotwallet account was created. Upload its wallet file in Account Plan.";
                    }
                    else
                    {
                        workAccounts.Add (hotAccount);
                    }
                    break;
                case "Forex":
                    FinancialAccount forexGain =
                        authData.CurrentOrganization.FinancialAccounts.IncomeCurrencyFluctuations;
                    FinancialAccount forexLoss =
                        authData.CurrentOrganization.FinancialAccounts.CostsCurrencyFluctuations;

                    if (forexGain == null)
                    {
                        if (forexLoss != null)
                        {
                            throw new InvalidOperationException();
                        }

                        authData.CurrentOrganization.FinancialAccounts.IncomeCurrencyFluctuations =
                            FinancialAccount.Create (authData.CurrentOrganization, "Forex holding gains",
                                FinancialAccountType.Income, null);
                        authData.CurrentOrganization.FinancialAccounts.CostsCurrencyFluctuations =
                            FinancialAccount.Create (authData.CurrentOrganization, "Forex holding losses",
                                FinancialAccountType.Cost, null);

                        result.DisplayMessage =
                            "Forex gain/loss accounts were created and will be used to account for currency fluctuations.";
                    }
                    else
                    {
                        if (forexLoss == null)
                        {
                            throw new InvalidOperationException();
                        }

                        if (!bitcoinNative && switchValue == false &&
                            ((authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinCold != null &&
                              authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinCold.Active) ||
                             (authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinHot != null &&
                              authData.CurrentOrganization.FinancialAccounts.AssetsBitcoinHot.Active)))
                        {
                            // bitcoin is active, and we're not bitcoin native, so we're not turning off forex

                            result.Success = false;
                            result.DisplayMessage =
                                "Cannot disable forex: bitcoin accounts are active in a non-bitcoin-native organization.";
                            result.RequireForex = true;
                        }
                        else
                        {
                            workAccounts.Add (forexGain);
                            workAccounts.Add (forexLoss);
                        }
                    }
                    break;
                case "Vat":
                    FinancialAccount vatInbound = authData.CurrentOrganization.FinancialAccounts.AssetsVatInbound;
                    FinancialAccount vatOutbound = authData.CurrentOrganization.FinancialAccounts.DebtsVatOutbound;

                    if (vatInbound == null)
                    {
                        if (vatOutbound != null)
                        {
                            throw new InvalidOperationException();
                        }

                        authData.CurrentOrganization.FinancialAccounts.AssetsVatInbound =
                            FinancialAccount.Create (authData.CurrentOrganization, "Inbound VAT",
                                FinancialAccountType.Asset, null);
                        authData.CurrentOrganization.FinancialAccounts.DebtsVatOutbound =
                            FinancialAccount.Create (authData.CurrentOrganization, "Outbound VAT",
                                FinancialAccountType.Debt, null);

                        result.DisplayMessage = "Inbound and outbound VAT accounts were created.";
                    }
                    else
                    {
                        if (vatOutbound == null)
                        {
                            throw new InvalidOperationException();
                        }

                        workAccounts.Add (vatInbound);
                        workAccounts.Add (vatOutbound);
                    }
                    break;
                case "Paypal":
                    FinancialAccount assetsPaypal = authData.CurrentOrganization.FinancialAccounts.AssetsPaypal;
                    if (assetsPaypal == null)
                    {
                        authData.CurrentOrganization.FinancialAccounts.AssetsPaypal =
                            FinancialAccount.Create (authData.CurrentOrganization, "Paypal account",
                                FinancialAccountType.Asset, null);

                        result.DisplayMessage = "An account was created for Paypal account tracking.";
                    }
                    else
                    {
                        workAccounts.Add (assetsPaypal);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (workAccounts.Count > 0 && String.IsNullOrEmpty (result.DisplayMessage))
            {
                if (switchValue) // switch has been turned on
                {
                    // accounts can always be re-enabled. This is not a create, it is a re-enable.

                    foreach (FinancialAccount account in workAccounts)
                    {
                        account.Active = true;
                    }

                    if (workAccounts.Count > 1)
                    {
                        result.DisplayMessage = "The accounts were re-enabled.";
                    }
                    else
                    {
                        result.DisplayMessage = "The account was re-enabled.";
                    }
                }
                else // switch is being set to off position
                {
                    // if the accounts are currently enabled, we must first check there aren't
                    // any transactions in them before disabling
                    bool transactionsOnAccount = false;

                    foreach (FinancialAccount account in workAccounts)
                    {
                        if (account.GetLastRows (5).Count > 0)
                        {
                            transactionsOnAccount = true;
                        }
                    }

                    if (transactionsOnAccount)
                    {
                        if (workAccounts.Count > 1)
                        {
                            result.DisplayMessage = "Can't disable these accounts: there are transactions";
                        }
                        else
                        {
                            result.DisplayMessage = "Can't disable this account: there are transactions";
                        }

                        result.Success = false;
                    }
                    else
                    {
                        // Disable accounts

                        foreach (FinancialAccount account in workAccounts)
                        {
                            account.Active = false;
                        }

                        if (workAccounts.Count > 1)
                        {
                            result.DisplayMessage = "The accounts were disabled.";
                        }
                        else
                        {
                            result.DisplayMessage = "The account was disabled.";
                        }
                    }
                }
            }

            return result;
        }


        public class CallResult
        {
            public bool Success { get; set; }
            public string OpResult { get; set; }
            public string DisplayMessage { get; set; }
            public bool RequireForex { get; set; }
        }


        public class InitialOrgData
        {
            public bool AccountBitcoinCold;
            public bool AccountBitcoinHot;
            public bool AccountPaypal;
            public bool AccountsForex;
            public bool AccountsVat;
        }
    }
}