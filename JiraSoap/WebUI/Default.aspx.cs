//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.UI;
using JiraSoap;


namespace WebUI
{
	public partial class Default : System.Web.UI.Page
	{

		public virtual void button1_Clicked (object sender, EventArgs args)
		{
		 	JSClient.createTestMessage(tbUserName.Text, tbPassword.Text, tbProjectCode.Text);
		}
	}
}

