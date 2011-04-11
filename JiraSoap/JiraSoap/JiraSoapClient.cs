using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using JiraSoap;
using System.Collections;

namespace JiraSoap
{
	public class JSClient
	{
		
		/**
		 * create a copy of the service as a property
		 * 
		 * I create a private local property using an underscore to store the value so I only have to calc/create once
		 * then I use the copy without an underscore for doing work
		 */ 
		private JiraSoapService _jiraSoapService;
		private JiraSoapService jiraSoapService
		{
			get 
			{
				if (_jiraSoapService == new JiraSoapService())
				{
					_jiraSoapService = new JiraSoapService();			
				}
				return _jiraSoapService;
			}
		}
		
		/** IssueType List Property
		 * 
		 */
		private List<RemoteIssueType> _issueTypes;
		public List<RemoteIssueType> issueTypes {
			get {
				if (_issueTypes.Count == 0) {
					_issueTypes = new List<RemoteIssueType> (jiraSoapService.getIssueTypesForProject (UserToken, Project.id));
				}
				return _issueTypes;
			}
		}
	
		
		/*
		 * Components listed as part of the JIRA Project
		 */ 
		private List<RemoteComponent> _Components;
		public List<RemoteComponent> Components
		{
			get{
				if (_Components.Count == 0)
				{
					_Components = new List<RemoteComponent> (jiraSoapService.getComponents (UserToken, Project.key));
				}
				return _Components; 
			}
		
		}
				
		/**
		 * More properties - c# has implicit getters and setters
		 */ 
		private string UserToken;
		private RemoteProject Project;
		private RemoteUser JUser;

		
		/**
		 * constructor
		 */ 
		public JSClient (string userName, string userPass, string projectCode)
		{			
			UserToken = jiraSoapService.login(userName, userPass);
			Project = jiraSoapService.getProjectByKey (UserToken, projectCode);
			JUser = jiraSoapService.getUser(UserToken, userName);
		}
		
		public RemoteIssue addNewIssue(string summary, int IssueTypeID, RemoteComponent[] ComponentArray)
		{
			RemoteIssue issue = new RemoteIssue ();
			issue.project = Project.key;
			issue.reporter = JUser.name;
			issue.summary = summary;
			issue.type = issueTypes[IssueTypeID].name;
			issue.components = ComponentArray;
			
			return jiraSoapService.createIssue (UserToken, issue);
		}		
		
	}
	
}
