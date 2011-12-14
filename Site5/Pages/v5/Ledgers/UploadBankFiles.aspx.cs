﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// ReSharper disable CheckNamespace
namespace Activizr.Site.Pages.Ledgers
// ReSharper restore CheckNamespace
{
    public partial class UploadBankFiles : PageV5Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle = Resources.Pages.Ledgers.UploadBankFiles_PageTitle;
            this.PageIcon = "iconshock-bank";

            this.LabelSidebarInfo.Text = Resources.Pages.Global.Sidebar_Information;
            this.LabelSidebarActions.Text = Resources.Pages.Global.Sidebar_Actions;
            this.LabelSidebarTodo.Text = Resources.Pages.Global.Sidebar_Todo;
            this.LabelDownloadInstructions.Text = Resources.Pages.Ledgers.UploadBankFiles_DownloadInstructions;
            this.LabelClickImage.Text = Resources.Pages.Global.Global_ClickImageToEnlarge;

            this.LabelUploadBankFilesInfo.Text = Resources.Pages.Ledgers.UploadBankFiles_Info;
            this.LabelActionItemsHere.Text = Resources.Pages.Global.Sidebar_Todo_Placeholder;

            this.LabelSelectBankUploadFilter.Text = Resources.Pages.Ledgers.UploadBankFiles_SelectBankFileType;
        }

        protected void ButtonSebAccountFile_Click(object sender, ImageClickEventArgs e)
        {
            this.ButtonSebAccountFile.CssClass = "FileTypeImage FileTypeImageSelected";
            this.ButtonSebPaymentFile.CssClass = "FileTypeImage UnselectedType";

            this.LiteralDownloadInstructions.Text =
            this.LiteralDownloadInstructionsModal.Text = 
                Resources.Pages.Ledgers.UploadBankFiles_DownloadInstructionsSebAccountFile;
            this.ImageDownloadInstructions.ImageUrl = "~/Images/Ledgers/uploadbankfiles-seb-kontoutdrag-small.png";
            this.ImageDownloadInstructionsFull.ImageUrl = "~/Images/Ledgers/uploadbankfiles-seb-kontoutdrag-full.png";

            ScriptManager.RegisterClientScriptBlock(this.Panel1, this.Panel1.GetType(), "FadeDownload2", "$(\"#DivInstructions\").fadeTo('slow',1.0);", true);
            ScriptManager.RegisterClientScriptBlock(this.Panel1, this.Panel1.GetType(), "FadeDownload", "$(\".UnselectedType\").fadeTo('fast',0.2);", true);
            ScriptManager.RegisterClientScriptBlock(this.Panel1, this.Panel1.GetType(), "ShowInstructions", "$(\"#DivInstructions\").css('display','inline');", true);

        }
    }
}