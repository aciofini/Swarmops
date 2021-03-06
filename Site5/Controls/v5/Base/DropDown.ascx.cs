﻿using System;
using System.Web.UI.WebControls;

namespace Swarmops.Frontend.Controls.v5.Base
{
    public partial class DropDown : ControlV5Base
    {
        public ListItemCollection Items
        {
            get { return this.DropControl.Items; }
        }

        public string SelectedValue
        {
            get { return this.DropControl.SelectedValue; }
            set { this.DropControl.SelectedValue = value; }
        }

        public string OnClientChange { get; set; }

        public string ClientControlID
        {
            get { return this.DropControl.ClientID; }
        }

        protected void Page_Load (object sender, EventArgs e)
        {
        }
    }
}