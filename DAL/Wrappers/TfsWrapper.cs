using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

namespace DAL.Wrappers
{
    public class TfsWrapper : ITfsWrapper
    {
        public const string _getWorkItemsQry = @"
            Select [System.Id], [System.WorkItemType], [System.Title], [System.State], [System.AssignedTo], [System.IterationPath]
            From WorkItems
            Where [System.AssignedTo] = @Me
                AND [System.IterationPath] = @CurrentIteration
            order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc";

        private readonly string _uri;
        private readonly string _project;
        private WorkItemTrackingHttpClient _client;

        public TfsWrapper(string tfsUri, string projectName)
        {
            _project = projectName;

            _client = new WorkItemTrackingHttpClient(new Uri(tfsUri), new VssCredentials());
        }

        public async Task<IEnumerable<TfsWorkItem>> GetCurrentSprintAsync()
        {
            var workItems = await _client.QueryByWiqlAsync(new Wiql { Query = _getWorkItemsQry }, new TeamContext("STDrive"));
            return (await _client
                .GetWorkItemsAsync(workItems.WorkItems.Select(i => i.Id)))
                .Select(item =>
                {
                    var tfsItem = new TfsWorkItem
                    {
                        Id = item.Id ?? 0,
                        IterationPath = item.Fields["System.IterationPath"].ToString(),
                        Title = item.Fields["System.Title"].ToString(),
                        WorkItemType = item.Fields["System.WorkItemType"].ToString(),
                        AssignedTo = item.Fields["System.AssignedTo"].ToString(),
                        State = item.Fields["System.State"].ToString()
                    };
                    return tfsItem;
                });
        }
    }
}