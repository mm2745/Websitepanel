// Copyright (c) 2012, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebsitePanel.Portal.ProviderControls
{
    public partial class SmarterStats_Settings : WebsitePanelControlBase, IHostingServiceProviderSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void BindSettings(StringDictionary settings)
        {
            // bind servers
            try
            {
                ddlServers.DataSource = ES.Services.StatisticsServers.GetServers(PanelRequest.ServiceId);
                ddlServers.DataBind();
            }
            catch
            { /* skip */ }

            txtSmarterUrl.Text = settings["SmarterUrl"];
            txtUsername.Text = settings["Username"];
            ViewState["PWD"] = settings["Password"];
            Utils.SelectListItem(ddlServers, settings["ServerID"]);
            Utils.SelectListItem(ddlLogFormat, settings["LogFormat"]);
            txtLogWilcard.Text = settings["LogWildcard"];
            txtLogDeleteDays.Text = settings["LogDeleteDays"];
            txtSmarterLogs.Text = settings["SmarterLogsPath"];
            txtSmarterLogDeleteMonths.Text = settings["SmarterLogDeleteMonths"];
			chkBuildUncLogsPath.Checked = Utils.ParseBool(settings["BuildUncLogsPath"], false);

            if (settings["TimeZoneId"] != null)
                timeZone.TimeZoneId = Utils.ParseInt(settings["TimeZoneId"], 1);

            txtStatsUrl.Text = settings["StatisticsURL"];

        }

        public void SaveSettings(StringDictionary settings)
        {
            settings["SmarterUrl"] = txtSmarterUrl.Text.Trim();
            settings["StatisticsURL"] = txtStatsUrl.Text.Trim();
            settings["Username"] = txtUsername.Text.Trim();
            settings["Password"] = (txtPassword.Text.Length > 0) ? txtPassword.Text : (string)ViewState["PWD"];
            settings["ServerID"] = (ddlServers.SelectedIndex != -1) ? ddlServers.SelectedValue : "1";
            settings["LogFormat"] = ddlLogFormat.SelectedValue;
            settings["LogWildcard"] = txtLogWilcard.Text.Trim();
            settings["LogDeleteDays"] = Utils.ParseInt(txtLogDeleteDays.Text, 0).ToString();
            settings["SmarterLogsPath"] = txtSmarterLogs.Text.Trim();
            settings["SmarterLogDeleteMonths"] = Utils.ParseInt(txtSmarterLogDeleteMonths.Text, 0).ToString();
            settings["TimeZoneId"] = timeZone.TimeZoneId.ToString();
			settings["BuildUncLogsPath"] = chkBuildUncLogsPath.Checked.ToString();
        }
    }
}