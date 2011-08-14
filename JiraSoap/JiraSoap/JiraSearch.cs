using System;
using System.Collections.Generic;
using System.Text;

namespace JiraSoap
{
    public class JiraSearch
    {

        private String UserToken = String.Empty;
        private JiraSoapService jiraSoapService = new JiraSoapService();
        private List<RemoteIssue> _issues = new List<RemoteIssue>();
        public List<RemoteIssue> Issues
        {
            get
            {
                return _issues;
            }
            set
            {
                _issues = value;
            }
        }


        public JiraSearch(String userToken) 
        { 
            UserToken = userToken;
        }

        public RemoteIssue GetIssueBySummary(RemoteProject Project, String summary, int limit)
        {
            String searchTerm = String.Format("project = \"{0}\" and summary = \"{1}\"", Project.key, summary);
            return searchIssues(Project, searchTerm);
        }

        public List<RemoteIssue> GetIssuesByUser(RemoteProject Project, String userName, int limit)
        {
            String searchTerm = String.Format("project = \"{0}\" and (reporter = \"{1}\" OR assignee = \"{1}\")", Project.key, userName);
            return searchIssues(Project, searchTerm, limit);
        }

        public List<RemoteIssue> GetIssuesForProject(RemoteProject Project, int limit)
        {
            return searchIssues(Project, String.Format("project = \"{0}\"", Project.key), limit);
        }

        public List<RemoteIssue> GetIssuesForFilter(RemoteProject Project, String filterID)
        {
            RemoteIssue[] issuesArray = jiraSoapService.getIssuesFromFilter(UserToken, filterID);
            return new List<RemoteIssue>(issuesArray);
        }

        public RemoteIssue searchIssues(RemoteProject Project, String searchTerm)
        {
            List<RemoteIssue> issues = searchIssues(Project, searchTerm, 1);
            return issues[0];
        }

        public List<RemoteIssue> searchIssues(RemoteProject Project, String searchTerm, int limit)
        {
            RemoteIssue[] issuesArray = jiraSoapService.getIssuesFromJqlSearch(UserToken, searchTerm, limit);
            return new List<RemoteIssue>(issuesArray);
        }
    }
}
