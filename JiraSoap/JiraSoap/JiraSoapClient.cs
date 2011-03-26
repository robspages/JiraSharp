using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JiraSoap
{
    public class JSClient
    {
        public static void createTestMessage(string userName, string userPass, string projectCode)
        {
            System.Diagnostics.Debug.WriteLine("Creating a test issue on http://jira/jira ...");

            JiraSoapService jiraSoapService = new JiraSoapService();

            string token = jiraSoapService.login(userName, userPass);
            string projectStr = projectCode;

            RemoteProject project = jiraSoapService.getProjectByKey(token, projectStr);

            // Create the issue
            RemoteIssue issue = new RemoteIssue();
            issue.project = project.key;

            List<RemoteIssueType> issueTypes = new List<RemoteIssueType>(jiraSoapService.getIssueTypesForProject(token, project.id));
            foreach (RemoteIssueType issueType in issueTypes)
            {
                if (issueType.name.Equals("Bug"))
                {
                    issue.type = issueType.id;
                }
            }

            List<RemoteComponent> components = new List<RemoteComponent>(jiraSoapService.getComponents(token, project.key));
            foreach (RemoteComponent component in components)
            {
                if (component.name.Equals("MM - Strategies"))
                {
                    issue.components = new RemoteComponent[] { component };
                }
            }

            RemoteUser user = jiraSoapService.getUser(token, userName);

            issue.reporter = user.name;

            issue.summary = "This is a new SOAP issue " + DateTime.Now;

            RemoteIssue returnedIssue = jiraSoapService.createIssue(token, issue);
            System.Diagnostics.Debug.WriteLine("Successfully created issue http://jira/jira/browse/" + returnedIssue.key);
        }

    }
}