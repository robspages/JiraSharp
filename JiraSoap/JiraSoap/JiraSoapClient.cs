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

		private string UserToken;
		private string ProjectCode;
		private RemoteProject Project;


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
		
		
		/** User property
		 * 
		 */
		private RemoteUser JUser;

		public JSClient (string userName, string userPass, string projectCode)
		{
			UserToken = jiraSoapService.login(userName, userPass);
			Project = jiraSoapService.getProjectByKey (UserToken, projectCode);
			JUser = jiraSoapService.getUser(UserToken, userName);
		}

		private JiraSoapService jiraSoapService = new JiraSoapService ();

		/* public static void createTestMessage (string userName, string userPass, string projectCode)
		{
			System.Diagnostics.Debug.WriteLine ("Creating a test issue on http://jira/jira ...");
			
			
			foreach (RemoteIssueType issueType in issueTypes) {
				if (issueType.name.Equals ("Bug")) {
					issue.type = issueType.id;
				}
			}
			
			List<RemoteComponent> components = new List<RemoteComponent> (jiraSoapService.getComponents (token, project.key));
			foreach (RemoteComponent component in components) {
				if (component.name.Equals ("MM - Strategies")) {
					issue.components = new RemoteComponent[] { component };
				}
			}
			
			
			;
			System.Diagnostics.Debug.WriteLine ("Successfully created issue http://jira/jira/browse/" + returnedIssue.key);
		} */

		public RemoteIssue addNewIssue (string summary)
		{
			RemoteIssue issue = new RemoteIssue ();
			issue.project = Project.key;
			issue.reporter = JUser.name;
			issue.summary = summary;
			
			return jiraSoapService.createIssue (UserToken, issue);
		}		
		
	}
	
}
