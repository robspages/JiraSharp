﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace JiraSoap
{

    public class JSClient
    {

        private JiraSoapService jiraSoapService = new JiraSoapService();

        /** IssueType List Property
         * 
         */
        private List<RemoteIssueType> _issueTypes = new List<RemoteIssueType>();
        public List<RemoteIssueType> issueTypes
        {
            get
            {
                if (_issueTypes.Count == 0)
                {
                    _issueTypes = new List<RemoteIssueType>(jiraSoapService.getIssueTypesForProject(UserToken, Project.id));
                }
                return _issueTypes;
            }
        }

        /*
         * Components listed as part of the JIRA Project
         */
        private List<RemoteComponent> _Components = new List<RemoteComponent>();
        public List<RemoteComponent> Components
        {
            get
            {
                if (_Components.Count == 0)
                {
                    _Components = new List<RemoteComponent>(jiraSoapService.getComponents(UserToken, Project.key));
                }
                return _Components;
            }

        }

        /*
         * Versions listed as part of the JIRA Project
         */
        private List<RemoteVersion> _Versions = new List<RemoteVersion>();
        public List<RemoteVersion> Versions
        {
            get
            {
                if (_Versions.Count == 0)
                {
                    _Versions = new List<RemoteVersion>(jiraSoapService.getVersions(UserToken, Project.key));
                }
                return _Versions;
            }

        }

        // use sparringly 
        private List<RemoteIssue> _Issues = new List<RemoteIssue>();
        public List<RemoteIssue> Issues
        {
            get
            {
                if (_Issues.Count == 0)
                {
                        JiraSearch jql = new JiraSearch(UserToken);
                        _Issues = jql.GetIssuesForProject(Project, 500);                
                }
                return _Issues;
            }
        }

        /**
         * More properties - c# has implicit getters and setters
         */
        private string UserToken = String.Empty;
        private RemoteProject Project = new RemoteProject();
        private RemoteUser JUser = new RemoteUser();

        /**
         * constructor
         */
        public JSClient(string userName, string userPass, string projectCode)
        {
            init(userName, userPass, projectCode);
        }

        /* public JSClient()
         { }
         */
        public void init(string userName, string userPass, string projectCode)
        {
            UserToken = jiraSoapService.login(userName, userPass);
            Project = jiraSoapService.getProjectByKey(UserToken, projectCode);
            JUser = jiraSoapService.getUser(UserToken, userName);
        }

        public RemoteIssue addNewIssue(string summary, int IssueTypeID, RemoteComponent[] ComponentArray)
        {
            RemoteIssue issue = new RemoteIssue();
            issue.summary = summary;
            issue.type = issueTypes[IssueTypeID].name;
            issue.components = ComponentArray;

            return addNewIssue(issue);
        }

        public RemoteIssue addNewIssue(RemoteIssue issue)
        {
            issue.project = Project.key;
            issue.reporter = JUser.name;
            return jiraSoapService.createIssue(UserToken, issue);
        }

        /**
         * Params: UserToken, Remote Issue, List of RemoteFieldsValues - fields you updated
         */ 
        public RemoteIssue updateIssue(String UserToken, RemoteIssue issue, RemoteFieldValue[] updatedFields)
        {
            return jiraSoapService.updateIssue(UserToken, issue.key, updatedFields);
        }

    }

}
